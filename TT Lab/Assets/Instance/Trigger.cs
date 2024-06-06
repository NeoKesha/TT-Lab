﻿using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.ViewModels.Editors.Instance;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace TT_Lab.Assets.Instance
{
    public class Trigger : SerializableInstance
    {
        public override UInt32 Section => Constants.LAYOUT_TRIGGERS_SECTION;

        public Trigger(LabURI package, UInt32 id, String name, String chunk, Int32 layId, ITwinTrigger trigger) : base(package, id, name, chunk, layId)
        {
            assetData = new TriggerData(trigger);
        }

        public Trigger()
        {
        }

        public override Type GetEditorType()
        {
            return typeof(TriggerViewModel);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new TriggerData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
