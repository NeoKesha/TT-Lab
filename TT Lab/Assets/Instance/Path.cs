﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.Assets.Instance
{
    public class Path : SerializableInstance
    {
        [JsonProperty(Required = Required.Always)]
        public List<Vector4> Points { get; set; } = new List<Vector4>();

        public Path(UInt32 id, String name, String chunk, Int32 layId, PS2AnyPath path) : base(id, name, chunk, layId)
        {
            Points = path.PointList;
        }

        public override String Type => base.Type + "Path";

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}