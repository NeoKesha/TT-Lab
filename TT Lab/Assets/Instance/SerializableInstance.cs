﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData;

namespace TT_Lab.Assets.Instance
{
    public abstract class SerializableInstance<T> : SerializableAsset<T> where T : AbstractAssetData, new()
    {
        protected override String SavePath => $"Instance\\{Chunk}\\{Type}";

        public override String Type => $"InstanceAsset";

        public SerializableInstance(UInt32 id, String name, String chunk, Int32? layId) : base(id, name)
        {
            Chunk = chunk;
            LayoutID = layId;
        }

        protected SerializableInstance()
        {
        }

    }
}
