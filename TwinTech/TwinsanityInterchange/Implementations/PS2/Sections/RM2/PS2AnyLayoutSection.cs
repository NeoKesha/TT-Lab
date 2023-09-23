﻿using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2.Layout;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2
{
    public class PS2AnyLayoutSection : BaseTwinSection
    {
        public PS2AnyLayoutSection() : base()
        {
            idToClassDictionary.Add(Constants.LAYOUT_TEMPLATES_SECTION, typeof(PS2AnyTemplatesSection));
            idToClassDictionary.Add(Constants.LAYOUT_AI_POSITIONS_SECTION, typeof(PS2AnyAIPositionsSection));
            idToClassDictionary.Add(Constants.LAYOUT_AI_PATHS_SECTION, typeof(PS2AnyAIPathsSection));
            idToClassDictionary.Add(Constants.LAYOUT_POSITIONS_SECTION, typeof(PS2AnyPositionsSection));
            idToClassDictionary.Add(Constants.LAYOUT_PATHS_SECTION, typeof(PS2AnyPathsSection));
            idToClassDictionary.Add(Constants.LAYOUT_SURFACES_SECTION, typeof(PS2AnySurfacesSection));
            idToClassDictionary.Add(Constants.LAYOUT_INSTANCES_SECTION, typeof(PS2AnyInstancesSection));
            idToClassDictionary.Add(Constants.LAYOUT_TRIGGERS_SECTION, typeof(PS2AnyTriggersSection));
            idToClassDictionary.Add(Constants.LAYOUT_CAMERAS_SECTION, typeof(PS2AnyCamerasSection));
        }
    }
}
