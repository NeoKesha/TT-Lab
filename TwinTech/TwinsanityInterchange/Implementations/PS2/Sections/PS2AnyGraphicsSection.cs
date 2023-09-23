﻿using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.Graphics;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections
{
    public class PS2AnyGraphicsSection : BaseTwinSection
    {
        public PS2AnyGraphicsSection() : base()
        {
            idToClassDictionary.Add(Constants.GRAPHICS_TEXTURES_SECTION, typeof(PS2AnyTexturesSection));
            idToClassDictionary.Add(Constants.GRAPHICS_MATERIALS_SECTION, typeof(PS2AnyMaterialsSection));
            idToClassDictionary.Add(Constants.GRAPHICS_MODELS_SECTION, typeof(PS2AnyModelsSection));
            idToClassDictionary.Add(Constants.GRAPHICS_RIGID_MODELS_SECTION, typeof(PS2AnyRigidModelsSection));
            idToClassDictionary.Add(Constants.GRAPHICS_SKINS_SECTION, typeof(PS2AnySkinsSection));
            idToClassDictionary.Add(Constants.GRAPHICS_BLEND_SKINS_SECTION, typeof(PS2AnyBlendSkinsSection));
            idToClassDictionary.Add(Constants.GRAPHICS_MESHES_SECTION, typeof(PS2AnyMeshesSection));
            idToClassDictionary.Add(Constants.GRAPHICS_LODS_SECTION, typeof(PS2AnyLODsSection));
            idToClassDictionary.Add(Constants.GRAPHICS_SKYDOMES_SECTION, typeof(PS2AnySkydomesSection));
        }
    }
}
