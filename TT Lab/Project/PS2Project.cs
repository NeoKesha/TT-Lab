using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Graphics;
using TT_Lab.Assets.Instance;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.PS2;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Archives;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;
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

        public PS2Project() { }

        public PS2Project(string name, string path, string discContentPath)
        {
            Name = name;
            Path = path;
            DiscContentPath = discContentPath;
            LastModified = DateTime.Now;
            UUID = Guid.NewGuid();
            Assets = new Dictionary<Guid, IAsset>();
        }

        public void Serialize(string path)
        {
            if (path == "") path = ProjectPath;
            System.IO.Directory.CreateDirectory(path);
            System.IO.Directory.SetCurrentDirectory(path);
            using (System.IO.FileStream fs = new System.IO.FileStream(Name + ".tson", System.IO.FileMode.Create, System.IO.FileAccess.Write))
            using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(fs))
            {
                writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented).ToCharArray());
            }
        }

        public static PS2Project Deserialize(string projectPath)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(projectPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            using (System.IO.BinaryReader reader = new System.IO.BinaryReader(fs))
            {
                var prText = new string(reader.ReadChars((Int32)fs.Length));
                var p = JsonConvert.DeserializeObject<PS2Project>(prText);
                return p;
            }
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
            using (System.IO.FileStream fs = new System.IO.FileStream(archivePaths[0], System.IO.FileMode.Open, System.IO.FileAccess.Read))
            using (System.IO.BinaryReader reader = new System.IO.BinaryReader(fs))
            {
                archive.Read(reader, (int)fs.Length);
            }
            foreach (var item in archive.Items)
            {
                var pathLow = item.Header.Path.ToLower();
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(item.Data))
                using (System.IO.BinaryReader reader = new System.IO.BinaryReader(ms))
                {
                    // Check for chunk file
                    if (pathLow.EndsWith(".rm2") || pathLow.EndsWith(".sm2"))
                    {
                        ITwinSection chunk = null;
                        uint graphicsSectionID = Constants.LEVEL_GRAPHICS_SECTION;
                        if (pathLow.EndsWith("default.rm2"))
                        {
                            chunk = new PS2Default(true);
                        }
                        else if (pathLow.EndsWith(".rm2"))
                        {
                            chunk = new PS2AnyTwinsanityRM2(true);
                        }
                        else if (pathLow.EndsWith(".sm2"))
                        {
                            chunk = new PS2AnyTwinsanitySM2(true);
                            graphicsSectionID = Constants.SCENERY_GRAPHICS_SECTION;
                        }
                        chunk.Read(reader, (Int32)ms.Length);
                        // Read graphics stuff
                        var graphics = chunk.GetItem<PS2AnyGraphicsSection>(graphicsSectionID);
                        ReadSectionItems<Texture, PS2AnyTexturesSection, PS2AnyTexture>
                            (graphics, graphicsCheck, Constants.GRAPHICS_TEXTURES_SECTION);
                        ReadSectionItems<Skydome, PS2AnySkydomesSection, PS2AnySkydome>
                            (graphics, graphicsCheck, Constants.GRAPHICS_SKYDOMES_SECTION);
                        ReadSectionItems<Material, PS2AnyMaterialsSection, PS2AnyMaterial>
                            (graphics, graphicsCheck, Constants.GRAPHICS_MATERIALS_SECTION);
                        ReadSectionItems<Model, PS2AnyModelsSection, PS2AnyModel>
                            (graphics, graphicsCheck, Constants.GRAPHICS_MODELS_SECTION);
                        ReadSectionItems<RigidModel, PS2AnyRigidModelsSection, PS2AnyRigidModel>
                            (graphics, graphicsCheck, Constants.GRAPHICS_RIGID_MODELS_SECTION);
                        ReadSectionItems<Skin, PS2AnySkinsSection, PS2AnySkin>
                            (graphics, graphicsCheck, Constants.GRAPHICS_SKINS_SECTION);
                        ReadSectionItems<BlendSkin, PS2AnyBlendSkinsSection, PS2AnyBlendSkin>
                            (graphics, graphicsCheck, Constants.GRAPHICS_BLEND_SKINS_SECTION);
                        ReadSectionItems<LodModel, PS2AnyLODsSection, PS2AnyLOD>
                            (graphics, graphicsCheck, Constants.GRAPHICS_LODS_SECTION);
                        ReadSectionItems<Mesh, PS2AnyMeshesSection, PS2AnyMesh>
                            (graphics, graphicsCheck, Constants.GRAPHICS_MESHES_SECTION);
                        // Read code stuff
                        var code = chunk.GetItem<PS2AnyCodeSection>(Constants.LEVEL_CODE_SECTION);
                        if (code != null)
                        {
                            ReadSectionItems<GameObject, PS2AnyGameObjectSection, PS2AnyObject>
                                (code, codeCheck, Constants.CODE_GAME_OBJECTS_SECTION);
                            ReadSectionItems<Animation, PS2AnyAnimationsSection, PS2AnyAnimation>
                                (code, codeCheck, Constants.CODE_ANIMATIONS_SECTION);
                            ReadSectionItems<OGI, PS2AnyOGIsSection, PS2AnyOGI>
                                (code, codeCheck, Constants.CODE_OGIS_SECTION);
                            ReadSectionItems<CodeModel, PS2AnyCodeModelsSection, PS2AnyCodeModel>
                                (code, codeCheck, Constants.CODE_CODE_MODELS_SECTION);
                            // Scripts are kinda special because they are ID oddity dependent
                            var items = code.GetItem<PS2AnyScriptsSection>(Constants.CODE_SCRIPTS_SECTION);
                            for (var i = 0; i < items.GetItemsAmount(); ++i)
                            {
                                var asset = items.GetItem<PS2AnyScript>(items.GetItem(i).GetID());
                                if (codeCheck[Constants.CODE_SCRIPTS_SECTION].Contains(asset.GetID())) continue;
                                codeCheck[Constants.CODE_SCRIPTS_SECTION].Add(asset.GetID());
                                var metaAsset = (IAsset)Activator.CreateInstance(asset.GetID() % 2 == 0 ? typeof(HeaderScript) : typeof(MainScript),
                                    asset.GetID(), asset.GetName(), asset);
                                metaAsset.Serialize();
                                Assets.Add(metaAsset.UUID, metaAsset);
                            }
                            ReadSectionItems<Script, PS2AnyScriptsSection, PS2AnyScript>
                                (code, codeCheck, Constants.CODE_SCRIPTS_SECTION);
                            ReadSectionItems<SoundEffect, PS2AnySoundsSection, PS2AnySound>
                                (code, codeCheck, Constants.CODE_SOUND_EFFECTS_SECTION);
                            ReadSectionItems<SoundEffect, PS2AnySoundsSection, PS2AnySound>
                                (code, codeCheck, Constants.CODE_LANG_SPA_SECTION);
                            ReadSectionItems<SoundEffect, PS2AnySoundsSection, PS2AnySound>
                                (code, codeCheck, Constants.CODE_LANG_JPN_SECTION);
                            ReadSectionItems<SoundEffect, PS2AnySoundsSection, PS2AnySound>
                                (code, codeCheck, Constants.CODE_LANG_ITA_SECTION);
                            ReadSectionItems<SoundEffect, PS2AnySoundsSection, PS2AnySound>
                                (code, codeCheck, Constants.CODE_LANG_GER_SECTION);
                            ReadSectionItems<SoundEffect, PS2AnySoundsSection, PS2AnySound>
                                (code, codeCheck, Constants.CODE_LANG_FRE_SECTION);
                            ReadSectionItems<SoundEffect, PS2AnySoundsSection, PS2AnySound>
                                (code, codeCheck, Constants.CODE_LANG_ENG_SECTION);
                        }
                        // RM2 per chunk instances
                        if (pathLow.EndsWith(".rm2"))
                        {
                            for (var i = 0; i < 8; ++i)
                            {
                                var layId = Constants.LEVEL_LAYOUT_1_SECTION + i;
                                var layout = chunk.GetItem<PS2AnyLayoutSection>((UInt32)layId);
                                ReadSectionItems<ObjectInstance, PS2AnyInstancesSection, PS2AnyInstance>
                                    (layout, Constants.LAYOUT_INSTANCES_SECTION, pathLow, layId);
                                ReadSectionItems<AiPath, PS2AnyAIPathsSection, PS2AnyAIPath>
                                    (layout, Constants.LAYOUT_AI_PATHS_SECTION, pathLow, layId);
                                ReadSectionItems<AiPosition, PS2AnyAIPositionsSection, PS2AnyAIPosition>
                                    (layout, Constants.LAYOUT_AI_POSITIONS_SECTION, pathLow, layId);
                                ReadSectionItems<Camera, PS2AnyCamerasSection, PS2AnyCamera>
                                    (layout, Constants.LAYOUT_CAMERAS_SECTION, pathLow, layId);
                                ReadSectionItems<CollisionSurface, PS2AnySurfacesSection, PS2AnyCollisionSurface>
                                    (layout, Constants.LAYOUT_SURFACES_SECTION, pathLow, layId);
                                ReadSectionItems<InstanceTemplate, PS2AnyTemplatesSection, PS2AnyTemplate>
                                    (layout, Constants.LAYOUT_TEMPLATES_SECTION, pathLow, layId);
                                ReadSectionItems<Path, PS2AnyPathsSection, PS2AnyPath>
                                    (layout, Constants.LAYOUT_PATHS_SECTION, pathLow, layId);
                                ReadSectionItems<Position, PS2AnyPositionsSection, PS2AnyPosition>
                                    (layout, Constants.LAYOUT_POSITIONS_SECTION, pathLow, layId);
                                ReadSectionItems<Trigger, PS2AnyTriggersSection, PS2AnyTrigger>
                                    (layout, Constants.LAYOUT_TRIGGERS_SECTION, pathLow, layId);
                            }
                        }
                    }
                }
            }
        }

        public T GetAsset<T>(Guid id) where T : IAsset
        {
            return (T)Assets[id];
        }

        /// <summary>
        /// Reads items from a section, converts them into project assets and serializes them on disk
        /// </summary>
        /// <typeparam name="T">Project asset type</typeparam>
        /// <typeparam name="S">Section type</typeparam>
        /// <typeparam name="I">Game asset type</typeparam>
        /// <param name="fromSection">Which section to read from</param>
        /// <param name="globalCheck">Dictionary of global resources to check against</param>
        /// <param name="secId">Subsection ID where game asset is stored at</param>
        private void ReadSectionItems<T, S, I>(ITwinSection fromSection, Dictionary<uint, List<uint>> globalCheck, uint secId)
            where T : SerializableAsset where S : ITwinSection where I : ITwinItem
        {
            var items = fromSection.GetItem<S>(secId);
            for (var i = 0; i < items.GetItemsAmount(); ++i)
            {
                var index = items.GetIdByIndex(i);
                if (globalCheck[secId].Contains(index)) continue;

                var asset = items.GetItem<I>(items.GetItem(i).GetID());
                globalCheck[secId].Add(asset.GetID());
                var metaAsset = (T)Activator.CreateInstance(typeof(T), asset.GetID(), asset.GetName(), asset);
                metaAsset.Serialize();
                Assets.Add(metaAsset.UUID, metaAsset);
            }
        }

        /// <summary>
        /// Reads items from a section, converts them into project assets and serializes them on disk
        /// </summary>
        /// <typeparam name="T">Project asset type</typeparam>
        /// <typeparam name="S">Section type</typeparam>
        /// <typeparam name="I">Game asset type</typeparam>
        /// <param name="fromSection">Which section to read from</param>
        /// <param name="secId">Subsection ID where game asset is stored at</param>
        private void ReadSectionItems<T, S, I>(ITwinSection fromSection, uint secId, string chunkName, int layId)
            where T : SerializableAsset where S : ITwinSection where I : ITwinItem
        {
            var items = fromSection.GetItem<S>(secId);
            if (items != null)
            {
                for (var i = 0; i < items.GetItemsAmount(); ++i)
                {
                    var asset = items.GetItem<I>(items.GetItem(i).GetID());
                    var metaAsset = (T)Activator.CreateInstance(typeof(T), asset.GetID(), asset.GetName(), chunkName, layId, asset);
                    metaAsset.Serialize();
                    Assets.Add(metaAsset.UUID, metaAsset);
                }
            }
        }
    }
}
