using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TT_Lab.AssetData;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Factory;
using TT_Lab.Assets.Graphics;
using TT_Lab.Assets.Instance;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.PS2;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Archives;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2.Code;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2.Layout;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.Project
{
    /// <summary>
    /// Core project class
    /// </summary>
    public class Project : IProject
    {
        private const string CURRENT_VERSION = "0.2.0";

        private string _version = CURRENT_VERSION;

        public AssetManager AssetManager { get; private set; }

        public Folder Packages { get; private set; }

        public Package BasePackage { get; private set; }

        public Package Ps2Package { get; private set; }

        public Package XboxPackage { get; private set; }

        public Guid UUID { get; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string? DiscContentPathPS2 { get; set; }

        public string? DiscContentPathXbox { get; set; }

        public DateTime LastModified { get; set; }

        public string Version { get => _version; private set => _version = value; }

        public string ProjectPath
        {
            get
            {
                return System.IO.Path.Combine(Path, Name);
            }
        }

        public Project()
        {
            LastModified = DateTime.Now;
            UUID = Guid.NewGuid();
            AssetManager = new();
        }

        public Project(string name, string path, string? discContentPathPS2, string? discContentPathXbox) : this()
        {
            Name = name;
            Path = path;
            DiscContentPathPS2 = discContentPathPS2;
            DiscContentPathXbox = discContentPathXbox;
        }

        public void CreateProjectStructure()
        {
            System.IO.Directory.CreateDirectory(ProjectPath);
            System.IO.Directory.SetCurrentDirectory(ProjectPath);
            System.IO.Directory.CreateDirectory("assets");
        }

        public void Serialize()
        {
            var path = ProjectPath;

            // Update last modified date
            LastModified = DateTime.Now;

            System.IO.Directory.SetCurrentDirectory(path);
            using (System.IO.FileStream fs = new System.IO.FileStream(Name + ".tson", System.IO.FileMode.Create, System.IO.FileAccess.Write))
            using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(fs))
            {
                writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented).ToCharArray());
            }
            // Serialize all the assets
            System.IO.Directory.SetCurrentDirectory("assets");
            var query = from asset in AssetManager.GetAssets()
                        group asset by asset.Type;
            var tasks = new Task[query.Count()];
            var index = 0;
            DateTime startAsset = DateTime.Now;
            foreach (var group in query)
            {
                tasks[index++] = Task.Factory.StartNew(() =>
                {
                    Log.WriteLine($"Serializing {group.Key}...");
                    var now = DateTime.Now;
                    try
                    {
                        foreach (var asset in group)
                        {
                            asset.Serialize();
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.WriteLine($"Error serializing: {ex.Message}");
                    }
                    var span = DateTime.Now - now;
                    Log.WriteLine($"Finished serializing {group.Key} in {span}");
                });
            }
            Task.WaitAll(tasks);
            Log.WriteLine($"Serialized assets in {(DateTime.Now - startAsset)}");
            System.IO.Directory.SetCurrentDirectory(path);
        }

        public static Project Deserialize(string projectPath)
        {
            Project? pr;
            using (System.IO.FileStream fs = new(projectPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            using (System.IO.BinaryReader reader = new(fs))
            {
                var prText = new string(reader.ReadChars((Int32)fs.Length));
                pr = JsonConvert.DeserializeObject<Project>(prText);
            }
            if (pr == null)
            {
                throw new ProjectException("Failed to deserialize the project!");
            }
            if (pr.Version != CURRENT_VERSION)
            {
                throw new ProjectException("The provided version of the project is not supported!");
            }
            System.IO.Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(projectPath)!);

            var taskList = new List<Task<Dictionary<Guid, IAsset>>>();
            // Deserialize assets
            foreach (var dir in System.IO.Directory.GetDirectories("assets"))
            {
                var assetFiles = System.IO.Directory.GetFiles(dir, "*.json", System.IO.SearchOption.AllDirectories);
                taskList.Add(AssetFactory.GetAssets(assetFiles));
            }
            Task.WaitAll(taskList.ToArray());
            Dictionary<Guid, IAsset> Assets = new();
            foreach (var assetsList in taskList)
            {
                foreach (var asset in assetsList.Result)
                {
                    Assets.Add(asset.Key, asset.Value);
                }
            }
            pr.AssetManager = new(Assets);
            return pr;
        }

        public void CreateBasePackages()
        {
            BasePackage = new Package(Name);
            AssetManager.AddAsset(BasePackage);
            Packages = new Folder(BasePackage.URI, "Packages", null, BasePackage);
            AssetManager.AddAsset(Packages);
            Ps2Package = new Package("PS2", Packages);
            XboxPackage = new Package("XBOX", Packages);
            AssetManager.AddAsset(Ps2Package);
            AssetManager.AddAsset(XboxPackage);
            BasePackage.AddDependency(Ps2Package.URI);
            BasePackage.AddDependency(XboxPackage.URI);
        }

        public void UnpackAssetsPS2()
        {
            if (DiscContentPathPS2 == null)
            {
                Log.WriteLine("No PS2 assets provided, skipped...");
                return;
            }

            // Load only unique resources
            Dictionary<Guid, IAsset> assets = new();
            // Folder hierarchy when generating chunk folders
            Dictionary<LabURI, Folder> folders = new();
            // Graphics
            var graphicsCheck = new Dictionary<uint, Dictionary<String, uint>>
            {
                { Constants.GRAPHICS_BLEND_SKINS_SECTION, new() },
                { Constants.GRAPHICS_LODS_SECTION, new() },
                { Constants.GRAPHICS_MATERIALS_SECTION, new() },
                { Constants.GRAPHICS_MESHES_SECTION, new() },
                { Constants.GRAPHICS_MODELS_SECTION, new() },
                { Constants.GRAPHICS_RIGID_MODELS_SECTION, new() },
                { Constants.GRAPHICS_SKINS_SECTION, new() },
                { Constants.GRAPHICS_SKYDOMES_SECTION, new() },
                { Constants.GRAPHICS_TEXTURES_SECTION, new() }
            };
            // Code
            var codeCheck = new Dictionary<uint, Dictionary<String, uint>>
            {
                { Constants.CODE_ANIMATIONS_SECTION, new() },
                { Constants.CODE_BEHAVIOUR_COMMANDS_SEQUENCES_SECTION, new() },
                { Constants.CODE_GAME_OBJECTS_SECTION, new() },
                { Constants.CODE_LANG_ENG_SECTION, new() },
                { Constants.CODE_LANG_FRE_SECTION, new() },
                { Constants.CODE_LANG_GER_SECTION, new() },
                { Constants.CODE_LANG_ITA_SECTION, new() },
                { Constants.CODE_LANG_JPN_SECTION, new() },
                { Constants.CODE_LANG_SPA_SECTION, new() },
                { Constants.CODE_OGIS_SECTION, new() },
                { Constants.CODE_BEHAVIOURS_SECTION, new() },
                { Constants.CODE_SOUND_EFFECTS_SECTION, new() }
            };
            string[] archivePaths = System.IO.Directory.GetFiles(System.IO.Path.Combine(DiscContentPathPS2, "Crash6"), "*.BD", System.IO.SearchOption.TopDirectoryOnly);
            PS2BD archive = new PS2BD(archivePaths[0].Replace(".BD", ".BH"), "");
            Log.WriteLine("Reading game archives...");
            using (System.IO.FileStream fs = new(archivePaths[0], System.IO.FileMode.Open, System.IO.FileAccess.Read))
            using (System.IO.BinaryReader reader = new(fs))
            {
                archive.Read(reader, (int)fs.Length);
            }
            // Create folders for storing assets for display in project tree
            var ps2Package = Ps2Package;
            var blendSkinsFolder = Folder.CreatePS2Folder("Blend Skins", ps2Package);
            var skinsFolder = Folder.CreatePS2Folder("Skins", ps2Package);
            var modelsFolder = Folder.CreatePS2Folder("Models", ps2Package);
            var rigidModelsFolder = Folder.CreatePS2Folder("Rigid models", ps2Package);
            var meshesFolder = Folder.CreatePS2Folder("Meshes", ps2Package);
            var materialsFolder = Folder.CreatePS2Folder("Materials", ps2Package);
            var lodsFolder = Folder.CreatePS2Folder("LODs", ps2Package);
            var skydomesFolder = Folder.CreatePS2Folder("Skydomes", ps2Package);
            var texturesFolder = Folder.CreatePS2Folder("Textures", ps2Package);
            var animationsFolder = Folder.CreatePS2Folder("Animations", ps2Package);
            var codeModelsFolder = Folder.CreatePS2Folder("Code models", ps2Package);
            var gameObjectsFolder = Folder.CreatePS2Folder("Game objects", ps2Package);
            var scriptsFolder = Folder.CreatePS2Folder("Scripts", ps2Package);
            var ogisFolder = Folder.CreatePS2Folder("OGIs/Skeletons", ps2Package);
            var sfxFolder = Folder.CreatePS2Folder("Sound Effects", ps2Package);
            var enFolder = Folder.CreatePS2Folder("English Sound Effects", ps2Package);
            var frFolder = Folder.CreatePS2Folder("French Sound Effects", ps2Package);
            var grFolder = Folder.CreatePS2Folder("German Sound Effects", ps2Package);
            var itaFolder = Folder.CreatePS2Folder("Italian Sound Effects", ps2Package);
            var jpnFolder = Folder.CreatePS2Folder("Japanese Sound Effects", ps2Package);
            var spaFolder = Folder.CreatePS2Folder("Spanish Sound Effects", ps2Package);
            var chunksFolder = Folder.CreatePS2Folder("Chunks", ps2Package);

            assets.Add(blendSkinsFolder.UUID, blendSkinsFolder);
            assets.Add(skinsFolder.UUID, skinsFolder);
            assets.Add(modelsFolder.UUID, modelsFolder);
            assets.Add(rigidModelsFolder.UUID, rigidModelsFolder);
            assets.Add(meshesFolder.UUID, meshesFolder);
            assets.Add(materialsFolder.UUID, materialsFolder);
            assets.Add(lodsFolder.UUID, lodsFolder);
            assets.Add(skydomesFolder.UUID, skydomesFolder);
            assets.Add(texturesFolder.UUID, texturesFolder);
            assets.Add(animationsFolder.UUID, animationsFolder);
            assets.Add(codeModelsFolder.UUID, codeModelsFolder);
            assets.Add(gameObjectsFolder.UUID, gameObjectsFolder);
            assets.Add(scriptsFolder.UUID, scriptsFolder);
            assets.Add(ogisFolder.UUID, ogisFolder);
            assets.Add(sfxFolder.UUID, sfxFolder);
            assets.Add(enFolder.UUID, enFolder);
            assets.Add(frFolder.UUID, frFolder);
            assets.Add(grFolder.UUID, grFolder);
            assets.Add(itaFolder.UUID, itaFolder);
            assets.Add(jpnFolder.UUID, jpnFolder);
            assets.Add(spaFolder.UUID, spaFolder);
            assets.Add(chunksFolder.UUID, chunksFolder);
            // Unpack all assets from chunks
            foreach (var item in archive.Items)
            {
                var pathLow = item.Header.Path.ToLower();
                var isRm2 = pathLow.EndsWith(".rm2");
                var isSm2 = pathLow.EndsWith(".sm2");
                var isDefault = pathLow.EndsWith("default.rm2");
                // TODO: Add these misc formats
                var isTxt = pathLow.EndsWith(".txt");
                var isPsm = pathLow.EndsWith(".psm");
                var isFont = pathLow.EndsWith(".psf");
                var isPtc = pathLow.EndsWith(".ptc");
                var isFrontend = pathLow.EndsWith("frontend.bin");
                Log.WriteLine($"Unpacking {System.IO.Path.GetFileName(pathLow)}...");
                using System.IO.MemoryStream ms = new(item.Data);
                using System.IO.BinaryReader reader = new(ms);
                // Check for chunk file
                if (isRm2 || isSm2)
                {
                    ITwinSection? chunk = null;
                    uint graphicsSectionID = Constants.LEVEL_GRAPHICS_SECTION;
                    if (isDefault)
                    {
                        chunk = new PS2Default();
                    }
                    else if (isRm2)
                    {
                        chunk = new PS2AnyTwinsanityRM2();
                    }
                    else if (isSm2)
                    {
                        chunk = new PS2AnyTwinsanitySM2();
                        graphicsSectionID = Constants.SCENERY_GRAPHICS_SECTION;
                    }
                    // Fill chunk data
                    chunk!.Read(reader, (Int32)ms.Length);

                    // Chunk's path for variation of resources if they happen to be duplicated
                    var chunkPath = pathLow;
                    pathLow = pathLow[..^4];

                    // Read graphics stuff
                    var graphics = chunk.GetItem<PS2AnyGraphicsSection>(graphicsSectionID);
                    ReadSectionItems<Texture, PS2AnyTexturesSection, PS2AnyTexture>
                        (assets, graphics, chunkPath, graphicsCheck, Constants.GRAPHICS_TEXTURES_SECTION, texturesFolder);
                    ReadSectionItems<Skydome, PS2AnySkydomesSection, PS2AnySkydome>
                        (assets, graphics, chunkPath, graphicsCheck, Constants.GRAPHICS_SKYDOMES_SECTION, skydomesFolder);
                    ReadSectionItems<Material, PS2AnyMaterialsSection, PS2AnyMaterial>
                        (assets, graphics, chunkPath, graphicsCheck, Constants.GRAPHICS_MATERIALS_SECTION, materialsFolder);
                    ReadSectionItems<Model, PS2AnyModelsSection, PS2AnyModel>
                        (assets, graphics, chunkPath, graphicsCheck, Constants.GRAPHICS_MODELS_SECTION, modelsFolder);
                    ReadSectionItems<RigidModel, PS2AnyRigidModelsSection, PS2AnyRigidModel>
                        (assets, graphics, chunkPath, graphicsCheck, Constants.GRAPHICS_RIGID_MODELS_SECTION, rigidModelsFolder);
                    ReadSectionItems<Skin, PS2AnySkinsSection, PS2AnySkin>
                        (assets, graphics, chunkPath, graphicsCheck, Constants.GRAPHICS_SKINS_SECTION, skinsFolder);
                    ReadSectionItems<BlendSkin, PS2AnyBlendSkinsSection, PS2AnyBlendSkin>
                        (assets, graphics, chunkPath, graphicsCheck, Constants.GRAPHICS_BLEND_SKINS_SECTION, blendSkinsFolder);
                    ReadSectionItems<LodModel, PS2AnyLODsSection, PS2AnyLOD>
                        (assets, graphics, chunkPath, graphicsCheck, Constants.GRAPHICS_LODS_SECTION, lodsFolder);
                    ReadSectionItems<Mesh, PS2AnyMeshesSection, PS2AnyMesh>
                        (assets, graphics, chunkPath, graphicsCheck, Constants.GRAPHICS_MESHES_SECTION, meshesFolder);

                    // Read code stuff
                    var code = chunk.GetItem<PS2AnyCodeSection>(Constants.LEVEL_CODE_SECTION);
                    if (code != null)
                    {
                        ReadSectionItems<GameObject, PS2AnyGameObjectSection, PS2AnyObject>
                            (assets, code, chunkPath, codeCheck, Constants.CODE_GAME_OBJECTS_SECTION, gameObjectsFolder);
                        ReadSectionItems<Animation, PS2AnyAnimationsSection, PS2AnyAnimation>
                            (assets, code, chunkPath, codeCheck, Constants.CODE_ANIMATIONS_SECTION, animationsFolder);
                        ReadSectionItems<OGI, PS2AnyOGIsSection, PS2AnyOGI>
                            (assets, code, chunkPath, codeCheck, Constants.CODE_OGIS_SECTION, ogisFolder);
                        ReadSectionItems<BehaviourCommandsSequence, PS2AnyBehaviourCommandsSequencesSection, TwinBehaviourCommandsSequence>
                            (assets, code, chunkPath, codeCheck, Constants.CODE_BEHAVIOUR_COMMANDS_SEQUENCES_SECTION, codeModelsFolder);

                        // Behaviours are special because they are ID oddity dependent
                        var items = code.GetItem<PS2AnyBehavioursSection>(Constants.CODE_BEHAVIOURS_SECTION);
                        for (var i = 0; i < items.GetItemsAmount(); ++i)
                        {
                            var asset = items.GetItem<TwinBehaviourWrapper>(items.GetItem(i).GetID());
                            var behaviourChecker = codeCheck[Constants.CODE_BEHAVIOURS_SECTION];
                            if (behaviourChecker.ContainsKey(asset.GetHash())) continue;
                            behaviourChecker.Add(asset.GetHash(), asset.GetID());
                            // If hash was unique but Twinsanity's ID wasn't then we will mark it with a variant which is gonna be chunk's name
                            var needVariant = behaviourChecker.Values.Where(e => e == asset.GetID()).Count() > 1;
                            var isHeader = asset.GetID() % 2 == 0;
                            if (needVariant)
                            {
                                var type = isHeader ? typeof(BehaviourStarter) : typeof(BehaviourGraph);
                                // TODO: Add dupes addition
                            }
                            var metaAsset = (IAsset?)Activator.CreateInstance(isHeader ? typeof(BehaviourStarter) : typeof(BehaviourGraph),
                                Ps2Package.URI, needVariant ? chunkPath : null, asset.GetID(), asset.GetName(), asset) ?? throw new ProjectException($"Could not read asset {asset.GetName()} with ID {asset.GetID()}");
                            if (!isHeader)
                            {
                                scriptsFolder.AddChild(metaAsset);
                            }
                            assets.Add(metaAsset.UUID, metaAsset);
                        }

                        ReadSectionItems<SoundEffect, PS2AnySoundsSection, PS2AnySound>
                            (assets, code, chunkPath, codeCheck, Constants.CODE_SOUND_EFFECTS_SECTION, sfxFolder);
                        ReadSectionItems<SoundEffectSP, PS2AnySoundsSection, PS2AnySound>
                            (assets, code, chunkPath, codeCheck, Constants.CODE_LANG_SPA_SECTION, spaFolder);
                        ReadSectionItems<SoundEffectJP, PS2AnySoundsSection, PS2AnySound>
                            (assets, code, chunkPath, codeCheck, Constants.CODE_LANG_JPN_SECTION, jpnFolder);
                        ReadSectionItems<SoundEffectIT, PS2AnySoundsSection, PS2AnySound>
                            (assets, code, chunkPath, codeCheck, Constants.CODE_LANG_ITA_SECTION, itaFolder);
                        ReadSectionItems<SoundEffectGR, PS2AnySoundsSection, PS2AnySound>
                            (assets, code, chunkPath, codeCheck, Constants.CODE_LANG_GER_SECTION, grFolder);
                        ReadSectionItems<SoundEffectFR, PS2AnySoundsSection, PS2AnySound>
                            (assets, code, chunkPath, codeCheck, Constants.CODE_LANG_FRE_SECTION, frFolder);
                        ReadSectionItems<SoundEffectEN, PS2AnySoundsSection, PS2AnySound>
                            (assets, code, chunkPath, codeCheck, Constants.CODE_LANG_ENG_SECTION, enFolder);
                    }

                    // Chunk folder
                    var otherFolders = pathLow.Split(System.IO.Path.DirectorySeparatorChar);
                    Folder prevFolder = chunksFolder;
                    // Create chunk folder hierarchy
                    for (var i = 1; i < otherFolders.Length; ++i)
                    {
                        var existFolder = prevFolder.GetData().To<FolderData>().Children.FirstOrDefault(c => folders.ContainsKey(c) && folders[c].Name == otherFolders[i], LabURI.Empty);
                        if (existFolder != LabURI.Empty)
                        {
                            prevFolder = folders[existFolder];
                            continue;
                        }
                        Folder nextFolder;
                        if (i != otherFolders.Length - 1)
                        {
                            nextFolder = Folder.CreatePS2Folder(otherFolders[i], prevFolder);
                        }
                        else
                        {
                            nextFolder = new ChunkFolder(Ps2Package.URI, otherFolders[i], prevFolder);
                        }
                        folders.Add(nextFolder.URI, nextFolder);
                        assets.Add(nextFolder.UUID, nextFolder);
                        prevFolder = nextFolder;
                    }
                    var chunkFolder = prevFolder;

                    // RM2 per chunk instances
                    if (isRm2)
                    {
                        if (!isDefault)
                        {
                            // Extract collision data
                            var collisionData = chunk.GetItem<PS2AnyCollisionData>(Constants.LEVEL_COLLISION_ITEM);
                            var colData = new Collision(Ps2Package.URI, collisionData.GetID(), collisionData.GetName(), pathLow, collisionData);
                            assets.Add(colData.UUID, colData);
                            chunkFolder.AddChild(colData);
                        }

                        // Extract particle data
                        var particleData = chunk.GetItem<PS2AnyParticleData>(Constants.LEVEL_PARTICLES_ITEM);
                        var partData = new Particles(Ps2Package.URI, particleData.GetID(), particleData.GetName(), pathLow, particleData);
                        assets.Add(partData.UUID, partData);
                        //chunkFolder.AddChild(partData);

                        // Instance layout
                        var instFolder = Folder.CreatePS2Folder("Instances", chunkFolder, pathLow);
                        var aiPathFolder = Folder.CreatePS2Folder("AI Navigation Paths", chunkFolder, pathLow);
                        var aiPosFolder = Folder.CreatePS2Folder("AI Navigation Positions", chunkFolder, pathLow);
                        var cameraFolder = Folder.CreatePS2Folder("Cameras", chunkFolder, pathLow);
                        var colSurfaceFolder = Folder.CreatePS2Folder("Collision Surfaces", chunkFolder, pathLow);
                        var instTempFolder = Folder.CreatePS2Folder("Instance Templates", chunkFolder, pathLow);
                        var pathFolder = Folder.CreatePS2Folder("Paths", chunkFolder, pathLow);
                        var posFolder = Folder.CreatePS2Folder("Positions", chunkFolder, pathLow);
                        var trgFolder = Folder.CreatePS2Folder("Triggers", chunkFolder, pathLow);
                        assets.Add(instFolder.UUID, instFolder);
                        assets.Add(aiPathFolder.UUID, aiPathFolder);
                        assets.Add(aiPosFolder.UUID, aiPosFolder);
                        assets.Add(cameraFolder.UUID, cameraFolder);
                        assets.Add(colSurfaceFolder.UUID, colSurfaceFolder);
                        assets.Add(instTempFolder.UUID, instTempFolder);
                        assets.Add(pathFolder.UUID, pathFolder);
                        assets.Add(posFolder.UUID, posFolder);
                        assets.Add(trgFolder.UUID, trgFolder);

                        // Instance layouts
                        for (var i = 0; i < 8; ++i)
                        {
                            var layId = Constants.LEVEL_LAYOUT_1_SECTION + i;
                            var layout = chunk.GetItem<PS2AnyLayoutSection>((UInt32)layId);
                            ReadSectionItems<ObjectInstance, PS2AnyInstancesSection, PS2AnyInstance>
                                (assets, layout, Constants.LAYOUT_INSTANCES_SECTION, pathLow, layId, instFolder);
                            ReadSectionItems<AiPath, PS2AnyAIPathsSection, PS2AnyAIPath>
                                (assets, layout, Constants.LAYOUT_AI_PATHS_SECTION, pathLow, layId, aiPathFolder);
                            ReadSectionItems<AiPosition, PS2AnyAIPositionsSection, PS2AnyAIPosition>
                                (assets, layout, Constants.LAYOUT_AI_POSITIONS_SECTION, pathLow, layId, aiPosFolder);
                            ReadSectionItems<Camera, PS2AnyCamerasSection, PS2AnyCamera>
                                (assets, layout, Constants.LAYOUT_CAMERAS_SECTION, pathLow, layId, cameraFolder);
                            ReadSectionItems<CollisionSurface, PS2AnySurfacesSection, PS2AnyCollisionSurface>
                                (assets, layout, Constants.LAYOUT_SURFACES_SECTION, pathLow, layId, colSurfaceFolder);
                            ReadSectionItems<InstanceTemplate, PS2AnyTemplatesSection, PS2AnyTemplate>
                                (assets, layout, Constants.LAYOUT_TEMPLATES_SECTION, pathLow, layId, instTempFolder);
                            ReadSectionItems<Path, PS2AnyPathsSection, PS2AnyPath>
                                (assets, layout, Constants.LAYOUT_PATHS_SECTION, pathLow, layId, pathFolder);
                            ReadSectionItems<Position, PS2AnyPositionsSection, PS2AnyPosition>
                                (assets, layout, Constants.LAYOUT_POSITIONS_SECTION, pathLow, layId, posFolder);
                            ReadSectionItems<Trigger, PS2AnyTriggersSection, PS2AnyTrigger>
                                (assets, layout, Constants.LAYOUT_TRIGGERS_SECTION, pathLow, layId, trgFolder);
                        }
                    }

                    // SM2 per chunk instances
                    if (isSm2)
                    {
                        var scenery = chunk.GetItem<PS2AnyScenery>(Constants.SCENERY_SECENERY_ITEM);
                        var dynamicScenery = chunk.GetItem<PS2AnyDynamicScenery>(Constants.SCENERY_DYNAMIC_SECENERY_ITEM);
                        var chunkLinks = chunk.GetItem<PS2AnyLink>(Constants.SCENERY_LINK_ITEM);
                        var sceneryAsset = new Scenery(Ps2Package.URI, scenery.GetID(), scenery.GetName(), pathLow, scenery);
                        var dynamicSceneryAsset = new DynamicScenery(Ps2Package.URI, dynamicScenery.GetID(), dynamicScenery.GetName(), pathLow, dynamicScenery);
                        var chunkLinksAsset = new ChunkLinks(Ps2Package.URI, chunkLinks.GetID(), chunkLinks.GetName(), pathLow, chunkLinks);
                        assets.Add(sceneryAsset.UUID, sceneryAsset);
                        assets.Add(dynamicSceneryAsset.UUID, dynamicSceneryAsset);
                        assets.Add(chunkLinksAsset.UUID, chunkLinksAsset);
                        chunkFolder.AddChild(sceneryAsset);
                        chunkFolder.AddChild(dynamicSceneryAsset);
                        // Chunk links not added because they are very attached to the chunk and can be located in a different UI place from Project Tree
                    }
                }
            }
            Log.WriteLine("Adding unpacked assets into asset manager...");
            AssetManager.AddAllAssets(assets);
            // Add any additional assets that are stored internally and generate additional URIs that AssetManager needs to know about
            foreach (var uri in codeModelsFolder.GetData().To<FolderData>().Children)
            { 
                var cm = AssetManager.GetAsset<BehaviourCommandsSequence>(uri);
                foreach (var sequence in cm.BehaviourGraphLinks)
                {
                    AssetManager.AddAssetUnsafe(sequence.Value, cm);
                }
            }
        }

        public void UnpackAssetsXbox()
        {
            if (DiscContentPathXbox == null || DiscContentPathXbox.Length == 0)
            {
                Log.WriteLine("No XBox assets provided, skipped...");
                return;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads items from a section and converts them into project assets
        /// </summary>
        /// <typeparam name="T">Project asset type</typeparam>
        /// <typeparam name="S">Section type</typeparam>
        /// <typeparam name="I">Game asset type</typeparam>
        /// <param name="fromSection">Which section to read from</param>
        /// <param name="globalCheck">Dictionary of global resources to check against</param>
        /// <param name="secId">Subsection ID where game asset is stored at</param>
        private void ReadSectionItems<T, S, I>(Dictionary<Guid, IAsset> assets, ITwinSection fromSection, String chunkName, Dictionary<uint, Dictionary<String, uint>> globalCheck, uint secId, Folder folder)
            where T : IAsset where S : ITwinSection where I : ITwinItem
        {
            var items = fromSection.GetItem<S>(secId);
            for (var i = 0; i < items.GetItemsAmount(); ++i)
            {
                var asset = items.GetItem<I>(items.GetItem(i).GetID());
                var checker = globalCheck[secId];
                if (checker.ContainsKey(asset.GetHash())) continue;
                checker.Add(asset.GetHash(), asset.GetID());
                // If hash was unique but Twinsanity's ID wasn't then we will mark it with a variant which is gonna be chunk's name
                var needVariant = checker.Values.Where(e => e == asset.GetID()).Count() > 1;
                if (needVariant)
                {
                    //Log.WriteLine($"Found duplicate Twinsanity ID for {typeof(T).Name} {asset.GetName()} in chunk {chunkName}");
                    // TODO: Add dupes addition
                }
                var metaAsset = (T?)Activator.CreateInstance(typeof(T), Ps2Package.URI, needVariant ? chunkName : null, asset.GetID(), asset.GetName(), asset) ?? throw new ProjectException($"Could not read asset {asset.GetName()} with ID {asset.GetID()}");
                folder.AddChild(metaAsset);
                assets.Add(metaAsset.UUID, metaAsset);
            }
        }

        /// <summary>
        /// Reads items from a section and converts them into project assets.
        /// This is primarily used for instances in chunks which are not unique and belong to chunk specifically.
        /// </summary>
        /// <typeparam name="T">Project asset type</typeparam>
        /// <typeparam name="S">Section type</typeparam>
        /// <typeparam name="I">Game asset type</typeparam>
        /// <param name="fromSection">Which section to read from</param>
        /// <param name="secId">Subsection ID where game asset is stored at</param>
        private void ReadSectionItems<T, S, I>(Dictionary<Guid, IAsset> assets, ITwinSection fromSection, uint secId, string chunkName, int layId, Folder folder)
            where T : IAsset where S : ITwinSection where I : ITwinItem
        {
            var items = fromSection.GetItem<S>(secId);
            if (items != null)
            {
                for (var i = 0; i < items.GetItemsAmount(); ++i)
                {
                    var asset = items.GetItem<I>(items.GetItem(i).GetID());
                    var metaAsset = (T?)Activator.CreateInstance(typeof(T), Ps2Package.URI, asset.GetID(), asset.GetName(), chunkName + layId.ToString(), layId, asset) ?? throw new ProjectException($"Could not read asset {asset.GetName()} with ID {asset.GetID()}");
                    folder.AddChild(metaAsset);
                    assets.Add(metaAsset.UUID, metaAsset);
                }
            }
        }
    }
}
