using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.AssetData;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Graphics;
using TT_Lab.Assets.Instance;
using TT_Lab.Util;
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
    /// PS2 project class, the root has the reserved extension .tson
    /// </summary>
    public class PS2Project : IProject
    {
        public Dictionary<string, Type> StringToAsset { get; }

        public Dictionary<Guid, IAsset> Assets { get; private set; }

        public Dictionary<Guid, UInt32> GuidToTwinId { get; set; }

        public Guid UUID { get; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string DiscContentPath { get; set; }

        public DateTime LastModified { get; set; }

        public string ProjectPath
        {
            get
            {
                return System.IO.Path.Combine(Path, Name);
            }
        }

        public PS2Project()
        {
            LastModified = DateTime.Now;
            UUID = Guid.NewGuid();
            Assets = new Dictionary<Guid, IAsset>();
            StringToAsset = new Dictionary<string, Type>
            {
                { "ObjectInstance", typeof(ObjectInstance) },
                { "AiPath", typeof(AiPath) },
                { "AiPosition", typeof(AiPosition) },
                { "Camera", typeof(Camera) },
                { "CollisionSurface", typeof(CollisionSurface) },
                { "InstanceTemplate", typeof(InstanceTemplate) },
                { "Path", typeof(Path) },
                { "Position", typeof(Position) },
                { "Trigger", typeof(Trigger) },
                { "Animation", typeof(Animation) },
                { "CodeModel", typeof(CodeModel) },
                { "GameObject", typeof(GameObject) },
                { "HeaderScript", typeof(HeaderScript) },
                { "MainScript", typeof(MainScript) },
                { "OGI", typeof(OGI) },
                { "SoundEffect", typeof(SoundEffect) },
                { "BlendSkin", typeof(BlendSkin) },
                { "LodModel", typeof(LodModel) },
                { "Material", typeof(Material) },
                { "Mesh", typeof(Mesh) },
                { "Model", typeof(Model) },
                { "RigidModel", typeof(RigidModel) },
                { "Skin", typeof(Skin) },
                { "Skydome", typeof(Skydome) },
                { "Texture", typeof(Texture) },
                { "CollisionData", typeof(Collision) },
                { "ParticleData", typeof(Particles) },
                { "Scenery", typeof(Scenery) },
                { "DynamicScenery", typeof(DynamicScenery) },
                { "ChunkLinks", typeof(ChunkLinks) },
                { "Folder", typeof(Folder) },
                { "ChunkFolder", typeof(ChunkFolder) }
            };
        }

        public PS2Project(string name, string path, string discContentPath) : this()
        {
            Name = name;
            Path = path;
            DiscContentPath = discContentPath;
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

            GuidToTwinId = GuidManager.GuidToTwinId;

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
            var query = from asset in Assets
                        group asset by asset.Value.Type;
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
                            asset.Value.Serialize();
                        }
                    }
                    catch(Exception ex)
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

        public static PS2Project Deserialize(string projectPath)
        {
            PS2Project pr;
            using (System.IO.FileStream fs = new System.IO.FileStream(projectPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            using (System.IO.BinaryReader reader = new System.IO.BinaryReader(fs))
            {
                var prText = new string(reader.ReadChars((Int32)fs.Length));
                pr = JsonConvert.DeserializeObject<PS2Project>(prText);
            }
            System.IO.Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(projectPath));
            // Deserialize assets
            var assetFiles = System.IO.Directory.GetFiles("assets", "*.json", System.IO.SearchOption.AllDirectories);
            pr.Assets = AssetFactory.GetAssets(pr.StringToAsset, assetFiles);
            return pr;
        }

        public void UnpackAssets()
        {
            // Load only unique resources
            // Graphics
            var graphicsCheck = new Dictionary<uint, List<uint>>
            {
                { Constants.GRAPHICS_BLEND_SKINS_SECTION, new List<uint>() },
                { Constants.GRAPHICS_LODS_SECTION, new List<uint>() },
                { Constants.GRAPHICS_MATERIALS_SECTION, new List<uint>() },
                { Constants.GRAPHICS_MESHES_SECTION, new List<uint>() },
                { Constants.GRAPHICS_MODELS_SECTION, new List<uint>() },
                { Constants.GRAPHICS_RIGID_MODELS_SECTION, new List<uint>() },
                { Constants.GRAPHICS_SKINS_SECTION, new List<uint>() },
                { Constants.GRAPHICS_SKYDOMES_SECTION, new List<uint>() },
                { Constants.GRAPHICS_TEXTURES_SECTION, new List<uint>() }
            };
            // Code
            var codeCheck = new Dictionary<uint, List<uint>>
            {
                { Constants.CODE_ANIMATIONS_SECTION, new List<uint>() },
                { Constants.CODE_CODE_MODELS_SECTION, new List<uint>() },
                { Constants.CODE_GAME_OBJECTS_SECTION, new List<uint>() },
                { Constants.CODE_LANG_ENG_SECTION, new List<uint>() },
                { Constants.CODE_LANG_FRE_SECTION, new List<uint>() },
                { Constants.CODE_LANG_GER_SECTION, new List<uint>() },
                { Constants.CODE_LANG_ITA_SECTION, new List<uint>() },
                { Constants.CODE_LANG_JPN_SECTION, new List<uint>() },
                { Constants.CODE_LANG_SPA_SECTION, new List<uint>() },
                { Constants.CODE_OGIS_SECTION, new List<uint>() },
                { Constants.CODE_SCRIPTS_SECTION, new List<uint>() },
                { Constants.CODE_SOUND_EFFECTS_SECTION, new List<uint>() }
            };
            string[] archivePaths = System.IO.Directory.GetFiles(System.IO.Path.Combine(DiscContentPath, "Crash6"), "*.BD", System.IO.SearchOption.TopDirectoryOnly);
            PS2BD archive = new PS2BD(archivePaths[0].Replace(".BD", ".BH"), "");
            Log.WriteLine("Reading game archives...");
            using (System.IO.FileStream fs = new System.IO.FileStream(archivePaths[0], System.IO.FileMode.Open, System.IO.FileAccess.Read))
            using (System.IO.BinaryReader reader = new System.IO.BinaryReader(fs))
            {
                archive.Read(reader, (int)fs.Length);
            }
            // Create folders for storing assets for display in project tree
            var blendSkinsFolder = new Folder("Blend Skins");
            var skinsFolder = new Folder("Skins");
            var modelsFolder = new Folder("Models");
            var rigidModelsFolder = new Folder("Rigid models");
            var meshesFolder = new Folder("Meshes");
            var materialsFolder = new Folder("Materials");
            var lodsFolder = new Folder("LODs");
            var skydomesFolder = new Folder("Skydomes");
            var texturesFolder = new Folder("Textures");
            var animationsFolder = new Folder("Animations");
            var codeModelsFolder = new Folder("Code models");
            var gameObjectsFolder = new Folder("Game objects");
            var scriptsFolder = new Folder("Scripts");
            var ogisFolder = new Folder("OGIs/Skeletons");
            var sfxFolder = new Folder("Sound Effects");
            var enFolder = new Folder("English Sound Effects");
            var frFolder = new Folder("French Sound Effects");
            var grFolder = new Folder("German Sound Effects");
            var itaFolder = new Folder("Italian Sound Effects");
            var jpnFolder = new Folder("Japanese Sound Effects");
            var spaFolder = new Folder("Spanish Sound Effects");
            var chunksFolder = new Folder("Chunks");
            Assets.Add(blendSkinsFolder.UUID, blendSkinsFolder);
            Assets.Add(skinsFolder.UUID, skinsFolder);
            Assets.Add(modelsFolder.UUID, modelsFolder);
            Assets.Add(rigidModelsFolder.UUID, rigidModelsFolder);
            Assets.Add(meshesFolder.UUID, meshesFolder);
            Assets.Add(materialsFolder.UUID, materialsFolder);
            Assets.Add(lodsFolder.UUID, lodsFolder);
            Assets.Add(skydomesFolder.UUID, skydomesFolder);
            Assets.Add(texturesFolder.UUID, texturesFolder);
            Assets.Add(animationsFolder.UUID, animationsFolder);
            Assets.Add(codeModelsFolder.UUID, codeModelsFolder);
            Assets.Add(gameObjectsFolder.UUID, gameObjectsFolder);
            Assets.Add(scriptsFolder.UUID, scriptsFolder);
            Assets.Add(ogisFolder.UUID, ogisFolder);
            Assets.Add(sfxFolder.UUID, sfxFolder);
            Assets.Add(enFolder.UUID, enFolder);
            Assets.Add(frFolder.UUID, frFolder);
            Assets.Add(grFolder.UUID, grFolder);
            Assets.Add(itaFolder.UUID, itaFolder);
            Assets.Add(jpnFolder.UUID, jpnFolder);
            Assets.Add(spaFolder.UUID, spaFolder);
            Assets.Add(chunksFolder.UUID, chunksFolder);
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
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(item.Data))
                using (System.IO.BinaryReader reader = new System.IO.BinaryReader(ms))
                {
                    // Check for chunk file
                    if (isRm2 || isSm2)
                    {
                        ITwinSection chunk = null;
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
                        chunk.Read(reader, (Int32)ms.Length);
                        // Read graphics stuff
                        var graphics = chunk.GetItem<PS2AnyGraphicsSection>(graphicsSectionID);
                        ReadSectionItems<Texture, PS2AnyTexturesSection, PS2AnyTexture>
                            (graphics, graphicsCheck, Constants.GRAPHICS_TEXTURES_SECTION, texturesFolder);
                        ReadSectionItems<Skydome, PS2AnySkydomesSection, PS2AnySkydome>
                            (graphics, graphicsCheck, Constants.GRAPHICS_SKYDOMES_SECTION, skydomesFolder);
                        ReadSectionItems<Material, PS2AnyMaterialsSection, PS2AnyMaterial>
                            (graphics, graphicsCheck, Constants.GRAPHICS_MATERIALS_SECTION, materialsFolder);
                        ReadSectionItems<Model, PS2AnyModelsSection, PS2AnyModel>
                            (graphics, graphicsCheck, Constants.GRAPHICS_MODELS_SECTION, modelsFolder);
                        ReadSectionItems<RigidModel, PS2AnyRigidModelsSection, PS2AnyRigidModel>
                            (graphics, graphicsCheck, Constants.GRAPHICS_RIGID_MODELS_SECTION, rigidModelsFolder);
                        ReadSectionItems<Skin, PS2AnySkinsSection, PS2AnySkin>
                            (graphics, graphicsCheck, Constants.GRAPHICS_SKINS_SECTION, skinsFolder);
                        ReadSectionItems<BlendSkin, PS2AnyBlendSkinsSection, PS2AnyBlendSkin>
                            (graphics, graphicsCheck, Constants.GRAPHICS_BLEND_SKINS_SECTION, blendSkinsFolder);
                        ReadSectionItems<LodModel, PS2AnyLODsSection, PS2AnyLOD>
                            (graphics, graphicsCheck, Constants.GRAPHICS_LODS_SECTION, lodsFolder);
                        ReadSectionItems<Mesh, PS2AnyMeshesSection, PS2AnyMesh>
                            (graphics, graphicsCheck, Constants.GRAPHICS_MESHES_SECTION, meshesFolder);
                        // Read code stuff
                        var code = chunk.GetItem<PS2AnyCodeSection>(Constants.LEVEL_CODE_SECTION);
                        if (code != null)
                        {
                            ReadSectionItems<GameObject, PS2AnyGameObjectSection, PS2AnyObject>
                                (code, codeCheck, Constants.CODE_GAME_OBJECTS_SECTION, gameObjectsFolder);
                            ReadSectionItems<Animation, PS2AnyAnimationsSection, PS2AnyAnimation>
                                (code, codeCheck, Constants.CODE_ANIMATIONS_SECTION, animationsFolder);
                            ReadSectionItems<OGI, PS2AnyOGIsSection, PS2AnyOGI>
                                (code, codeCheck, Constants.CODE_OGIS_SECTION, ogisFolder);
                            ReadSectionItems<CodeModel, PS2AnyCodeModelsSection, PS2AnyCodeModel>
                                (code, codeCheck, Constants.CODE_CODE_MODELS_SECTION, codeModelsFolder);
                            // Scripts are kinda special because they are ID oddity dependent
                            var items = code.GetItem<PS2AnyScriptsSection>(Constants.CODE_SCRIPTS_SECTION);
                            for (var i = 0; i < items.GetItemsAmount(); ++i)
                            {
                                var asset = items.GetItem<PS2AnyScript>(items.GetItem(i).GetID());
                                if (codeCheck[Constants.CODE_SCRIPTS_SECTION].Contains(asset.GetID())) continue;
                                codeCheck[Constants.CODE_SCRIPTS_SECTION].Add(asset.GetID());
                                var isHeader = asset.GetID() % 2 == 0;
                                var metaAsset = (IAsset)Activator.CreateInstance(isHeader ? typeof(HeaderScript) : typeof(MainScript),
                                    asset.GetID(), asset.GetName(), asset);
                                if (!isHeader)
                                {
                                    scriptsFolder.AddChild(metaAsset);
                                }
                                Assets.Add(metaAsset.UUID, metaAsset);
                            }
                            ReadSectionItems<SoundEffect, PS2AnySoundsSection, PS2AnySound>
                                (code, codeCheck, Constants.CODE_SOUND_EFFECTS_SECTION, sfxFolder);
                            ReadSectionItems<SoundEffectSP, PS2AnySoundsSection, PS2AnySound>
                                (code, codeCheck, Constants.CODE_LANG_SPA_SECTION, spaFolder);
                            ReadSectionItems<SoundEffectJP, PS2AnySoundsSection, PS2AnySound>
                                (code, codeCheck, Constants.CODE_LANG_JPN_SECTION, jpnFolder);
                            ReadSectionItems<SoundEffectIT, PS2AnySoundsSection, PS2AnySound>
                                (code, codeCheck, Constants.CODE_LANG_ITA_SECTION, itaFolder);
                            ReadSectionItems<SoundEffectGR, PS2AnySoundsSection, PS2AnySound>
                                (code, codeCheck, Constants.CODE_LANG_GER_SECTION, grFolder);
                            ReadSectionItems<SoundEffectFR, PS2AnySoundsSection, PS2AnySound>
                                (code, codeCheck, Constants.CODE_LANG_FRE_SECTION, frFolder);
                            ReadSectionItems<SoundEffectEN, PS2AnySoundsSection, PS2AnySound>
                                (code, codeCheck, Constants.CODE_LANG_ENG_SECTION, enFolder);
                        }

                        // Chunk folder
                        pathLow = pathLow.Substring(0, pathLow.Length - 4);
                        var chunkName = System.IO.Path.GetFileNameWithoutExtension(pathLow);
                        var otherFolders = pathLow.Split(System.IO.Path.DirectorySeparatorChar);
                        Folder prevFolder = chunksFolder;
                        // Create chunk folder hierarchy
                        for (var i = 1; i < otherFolders.Length; ++i)
                        {
                            var existFolder = ((FolderData)prevFolder.GetData()).Children.FirstOrDefault(c => GetAsset(c)?.Name == otherFolders[i]);
                            if (existFolder != Guid.Empty)
                            {
                                prevFolder = (Folder)GetAsset(existFolder);
                                continue;
                            }
                            Folder nextFolder;
                            if (i != otherFolders.Length - 1)
                            {
                                nextFolder = new Folder(otherFolders[i], prevFolder);
                            }
                            else
                            {
                                nextFolder = new ChunkFolder(otherFolders[i], prevFolder);
                            }
                            Assets.Add(nextFolder.UUID, nextFolder);
                            prevFolder = nextFolder;
                        }
                        var chunkFolder = prevFolder;
                        // RM2 per chunk instances
                        if (isRm2)
                        {
                            if (chunkName != "default")
                            {
                                // Extract collision data
                                var collisionData = chunk.GetItem<PS2AnyCollisionData>(Constants.LEVEL_COLLISION_ITEM);
                                var colData = new Collision(collisionData.GetID(), collisionData.GetName(), pathLow, collisionData);
                                Assets.Add(colData.UUID, colData);
                                chunkFolder.AddChild(colData);
                            }
                            // Extract particle data
                            var particleData = chunk.GetItem<PS2AnyParticleData>(Constants.LEVEL_PARTICLES_ITEM);
                            var partData = new Particles(particleData.GetID(), particleData.GetName(), pathLow, particleData);
                            Assets.Add(partData.UUID, partData);
                            //chunkFolder.AddChild(partData);
                            // Instance layout
                            var instFolder = new Folder("Instances", chunkFolder);
                            var aiPathFolder = new Folder("AI Paths", chunkFolder);
                            var aiPosFolder = new Folder("AI Positions", chunkFolder);
                            var cameraFolder = new Folder("Cameras", chunkFolder);
                            var colSurfaceFolder = new Folder("Collision Surfaces", chunkFolder);
                            var instTempFolder = new Folder("Instance Templates", chunkFolder);
                            var pathFolder = new Folder("Paths", chunkFolder);
                            var posFolder = new Folder("Positions", chunkFolder);
                            var trgFolder = new Folder("Triggers", chunkFolder);
                            Assets.Add(instFolder.UUID, instFolder);
                            Assets.Add(aiPathFolder.UUID, aiPathFolder);
                            Assets.Add(aiPosFolder.UUID, aiPosFolder);
                            Assets.Add(cameraFolder.UUID, cameraFolder);
                            Assets.Add(colSurfaceFolder.UUID, colSurfaceFolder);
                            Assets.Add(instTempFolder.UUID, instTempFolder);
                            Assets.Add(pathFolder.UUID, pathFolder);
                            Assets.Add(posFolder.UUID, posFolder);
                            Assets.Add(trgFolder.UUID, trgFolder);
                            for (var i = 0; i < 8; ++i)
                            {
                                var layId = Constants.LEVEL_LAYOUT_1_SECTION + i;
                                var layout = chunk.GetItem<PS2AnyLayoutSection>((UInt32)layId);
                                ReadSectionItems<ObjectInstance, PS2AnyInstancesSection, PS2AnyInstance>
                                    (layout, Constants.LAYOUT_INSTANCES_SECTION, pathLow, layId, instFolder);
                                ReadSectionItems<AiPath, PS2AnyAIPathsSection, PS2AnyAIPath>
                                    (layout, Constants.LAYOUT_AI_PATHS_SECTION, pathLow, layId, aiPathFolder);
                                ReadSectionItems<AiPosition, PS2AnyAIPositionsSection, PS2AnyAIPosition>
                                    (layout, Constants.LAYOUT_AI_POSITIONS_SECTION, pathLow, layId, aiPosFolder);
                                ReadSectionItems<Camera, PS2AnyCamerasSection, PS2AnyCamera>
                                    (layout, Constants.LAYOUT_CAMERAS_SECTION, pathLow, layId, cameraFolder);
                                ReadSectionItems<CollisionSurface, PS2AnySurfacesSection, PS2AnyCollisionSurface>
                                    (layout, Constants.LAYOUT_SURFACES_SECTION, pathLow, layId, colSurfaceFolder);
                                ReadSectionItems<InstanceTemplate, PS2AnyTemplatesSection, PS2AnyTemplate>
                                    (layout, Constants.LAYOUT_TEMPLATES_SECTION, pathLow, layId, instTempFolder);
                                ReadSectionItems<Path, PS2AnyPathsSection, PS2AnyPath>
                                    (layout, Constants.LAYOUT_PATHS_SECTION, pathLow, layId, pathFolder);
                                ReadSectionItems<Position, PS2AnyPositionsSection, PS2AnyPosition>
                                    (layout, Constants.LAYOUT_POSITIONS_SECTION, pathLow, layId, posFolder);
                                ReadSectionItems<Trigger, PS2AnyTriggersSection, PS2AnyTrigger>
                                    (layout, Constants.LAYOUT_TRIGGERS_SECTION, pathLow, layId, trgFolder);
                            }
                        }
                        // SM2 per chunk instances
                        if (isSm2)
                        {
                            var scenery = chunk.GetItem<PS2AnyScenery>(Constants.SCENERY_SECENERY_ITEM);
                            var dynamicScenery = chunk.GetItem<PS2AnyDynamicScenery>(Constants.SCENERY_DYNAMIC_SECENERY_ITEM);
                            var chunkLinks = chunk.GetItem<PS2AnyLink>(Constants.SCENERY_LINK_ITEM);
                            var sceneryAsset = new Scenery(scenery.GetID(), scenery.GetName(), pathLow, scenery);
                            var dynamicSceneryAsset = new DynamicScenery(dynamicScenery.GetID(), dynamicScenery.GetName(), pathLow, dynamicScenery);
                            var chunkLinksAsset = new ChunkLinks(chunkLinks.GetID(), chunkLinks.GetName(), pathLow, chunkLinks);
                            Assets.Add(sceneryAsset.UUID, sceneryAsset);
                            Assets.Add(dynamicSceneryAsset.UUID, dynamicSceneryAsset);
                            Assets.Add(chunkLinksAsset.UUID, chunkLinksAsset);
                            chunkFolder.AddChild(sceneryAsset);
                            chunkFolder.AddChild(dynamicSceneryAsset);
                            // Chunk links not added because they are very attached to the chunk and can located in a different UI place from Project Tree
                        }
                    }
                }
            }
        }

        public IAsset GetAsset(Guid id)
        {
            if (Assets.TryGetValue(id, out IAsset getAss))
            {
                return getAss;
            }
            return default;
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
        private void ReadSectionItems<T, S, I>(ITwinSection fromSection, Dictionary<uint, List<uint>> globalCheck, uint secId, Folder folder)
            where T : IAsset where S : ITwinSection where I : ITwinItem
        {
            var items = fromSection.GetItem<S>(secId);
            for (var i = 0; i < items.GetItemsAmount(); ++i)
            {
                var asset = items.GetItem<I>(items.GetItem(i).GetID());
                if (globalCheck[secId].Contains(asset.GetID())) continue;
                globalCheck[secId].Add(asset.GetID());
                var metaAsset = (T)Activator.CreateInstance(typeof(T), asset.GetID(), asset.GetName(), asset);
                folder.AddChild(metaAsset);
                Assets.Add(metaAsset.UUID, metaAsset);
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
        private void ReadSectionItems<T, S, I>(ITwinSection fromSection, uint secId, string chunkName, int layId, Folder folder)
            where T : IAsset where S : ITwinSection where I : ITwinItem
        {
            var items = fromSection.GetItem<S>(secId);
            if (items != null)
            {
                for (var i = 0; i < items.GetItemsAmount(); ++i)
                {
                    var asset = items.GetItem<I>(items.GetItem(i).GetID());
                    var metaAsset = (T)Activator.CreateInstance(typeof(T), asset.GetID(), asset.GetName(), chunkName, layId, asset);
                    folder.AddChild(metaAsset);
                    Assets.Add(metaAsset.UUID, metaAsset);
                }
            }
        }
    }
}
