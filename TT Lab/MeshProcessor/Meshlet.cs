using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics.SubModels;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.MeshProcessor
{
    public class Meshlet
    {
        public List<UInt32> Indices { get; set; } = new();
        public List<Vertex> Vertexes { get; set; } = new();
        public List<SubBlendFaceData>? BlendFaces { get; set; }
        public List<UInt32> Strip { get; set; } = new();
    }
}
