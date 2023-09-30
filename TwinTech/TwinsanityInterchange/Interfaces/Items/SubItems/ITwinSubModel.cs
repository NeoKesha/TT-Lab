﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems
{
    public interface ITwinSubModel : ITwinSerializable
    {
        Byte[] UnusedBlob { get; set; }
        List<Vector4> Vertexes { get; set; }
        List<Vector4> UVW { get; set; }
        List<Color> Colors { get; set; }
        List<Vector4> EmitColor { get; set; }
        List<Vector4> Normals { get; set; }
        List<bool> Connection { get; set; }

        void CalculateData();
    }
}
