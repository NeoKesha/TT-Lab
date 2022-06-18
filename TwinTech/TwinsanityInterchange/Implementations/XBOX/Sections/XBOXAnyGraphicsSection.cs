using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.XBOX.Sections.Graphics;

namespace Twinsanity.TwinsanityInterchange.Implementations.XBOX.Sections
{
    public class XBOXAnyGraphicsSection : BaseTwinSection
    {
        public XBOXAnyGraphicsSection() : base()
        {
            idToClassDictionary.Add(Constants.GRAPHICS_TEXTURES_SECTION, typeof(XBOXAnyTexturesSection));
            idToClassDictionary.Add(Constants.GRAPHICS_MATERIALS_SECTION, typeof(PS2AnyMaterialsSection));
            idToClassDictionary.Add(Constants.GRAPHICS_MODELS_SECTION, typeof(XBOXAnyModelsSection));
            idToClassDictionary.Add(Constants.GRAPHICS_RIGID_MODELS_SECTION, typeof(PS2AnyRigidModelsSection));
            idToClassDictionary.Add(Constants.GRAPHICS_SKINS_SECTION, typeof(XBOXAnySkinsSection));
            idToClassDictionary.Add(Constants.GRAPHICS_BLEND_SKINS_SECTION, typeof(XBOXAnyBlendSkinsSection));
            idToClassDictionary.Add(Constants.GRAPHICS_MESHES_SECTION, typeof(PS2AnyMeshesSection));
            idToClassDictionary.Add(Constants.GRAPHICS_LODS_SECTION, typeof(PS2AnyLODsSection));
            idToClassDictionary.Add(Constants.GRAPHICS_SKYDOMES_SECTION, typeof(PS2AnySkydomesSection));
        }
    }
}
