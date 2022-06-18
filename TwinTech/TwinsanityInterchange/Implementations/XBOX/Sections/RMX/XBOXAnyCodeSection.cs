using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2.Code;
using Twinsanity.TwinsanityInterchange.Implementations.XBOX.Sections.RMX.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.XBOX.Sections.RMX
{
    public class XBOXAnyCodeSection : BaseTwinSection
    {
        public XBOXAnyCodeSection() : base()
        {
            idToClassDictionary.Add(Constants.CODE_GAME_OBJECTS_SECTION, typeof(XBOXAnyGameObjectSection));
            idToClassDictionary.Add(Constants.CODE_SCRIPTS_SECTION, typeof(XBOXAnyScriptsSection));
            idToClassDictionary.Add(Constants.CODE_ANIMATIONS_SECTION, typeof(PS2AnyAnimationsSection));
            idToClassDictionary.Add(Constants.CODE_OGIS_SECTION, typeof(PS2AnyOGIsSection));
            idToClassDictionary.Add(Constants.CODE_CODE_MODELS_SECTION, typeof(XBOXAnyCodeModelsSection));
            idToClassDictionary.Add(Constants.CODE_UNK_ITEM, typeof(BaseTwinItem));
            idToClassDictionary.Add(Constants.CODE_SOUND_EFFECTS_SECTION, typeof(XBOXAnySoundsSection));
            idToClassDictionary.Add(Constants.CODE_LANG_ENG_SECTION, typeof(XBOXAnySoundsSection));
            idToClassDictionary.Add(Constants.CODE_LANG_FRE_SECTION, typeof(XBOXAnySoundsSection));
            idToClassDictionary.Add(Constants.CODE_LANG_GER_SECTION, typeof(XBOXAnySoundsSection));
            idToClassDictionary.Add(Constants.CODE_LANG_SPA_SECTION, typeof(XBOXAnySoundsSection));
            idToClassDictionary.Add(Constants.CODE_LANG_ITA_SECTION, typeof(XBOXAnySoundsSection));
            idToClassDictionary.Add(Constants.CODE_LANG_JPN_SECTION, typeof(XBOXAnySoundsSection));
        }
    }
}
