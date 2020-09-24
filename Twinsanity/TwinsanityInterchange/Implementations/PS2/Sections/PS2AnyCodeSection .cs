using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections
{
    public class PS2AnyCodeSection : BaseTwinSection
    {
        public PS2AnyCodeSection() : base()
        {
            idToClassDictionary.Add(Constants.CODE_GAME_OBJECTS_SECTION, typeof(PS2AnyGameObjectSection));
            idToClassDictionary.Add(Constants.CODE_SCRIPTS_SECTION, typeof(PS2AnyScriptsSection));
            idToClassDictionary.Add(Constants.CODE_ANIMATIONS_SECTION, typeof(PS2AnyAnimationsSection));
            idToClassDictionary.Add(Constants.CODE_OGIS_SECTION, typeof(PS2AnyOGIsSection));
            idToClassDictionary.Add(Constants.CODE_CODE_MODELS_SECTION, typeof(PS2AnyCodeModelsSection));
            idToClassDictionary.Add(Constants.CODE_UNK_ITEM, typeof(BaseTwinItem));
            idToClassDictionary.Add(Constants.CODE_SOUND_EFFECTS_SECTION, typeof(PS2AnySoundsSection));
            idToClassDictionary.Add(Constants.CODE_LANG_ENG_SECTION, typeof(PS2AnySoundsSection));
            idToClassDictionary.Add(Constants.CODE_LANG_FRE_SECTION, typeof(PS2AnySoundsSection));
            idToClassDictionary.Add(Constants.CODE_LANG_GER_SECTION, typeof(PS2AnySoundsSection));
            idToClassDictionary.Add(Constants.CODE_LANG_SPA_SECTION, typeof(PS2AnySoundsSection));
            idToClassDictionary.Add(Constants.CODE_LANG_ITA_SECTION, typeof(PS2AnySoundsSection));
            idToClassDictionary.Add(Constants.CODE_LANG_JPN_SECTION, typeof(PS2AnySoundsSection));
        }
    }
}
