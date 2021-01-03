using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using TT_Lab.AssetData.Graphics.SubModels;
using TT_Lab.Util.FBX;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.AssetData.Graphics
{
    public class ModelData : AbstractAssetData
    {
        public ModelData()
        {
            Vertexes = new List<List<Vertex>>();
            Faces = new List<List<IndexedFace>>();
        }

        public ModelData(PS2AnyModel model) : this()
        {
            twinRef = model;
        }
        [JsonProperty(Required = Required.Always)]
        public List<List<Vertex>> Vertexes { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<List<IndexedFace>> Faces { get; set; }
        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Save(string dataPath)
        {
            using (FileStream fs = new FileStream(dataPath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                FbxModel model = new FbxModel(Vertexes, Faces);
                model.SaveBinary(writer);
            }
        }

        public override void Load(String dataPath)
        {
            Vertexes.Clear();
            Faces.Clear();
            using (FileStream fs = new FileStream(dataPath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                FbxModel model = new FbxModel();
                model.ReadBinary(reader);
            }
        }

        public override void Import()
        {
            PS2AnyModel model = (PS2AnyModel)twinRef;
            Vertexes = new List<List<Vertex>>();
            Faces = new List<List<IndexedFace>>();
            var refIndex = 0;
            foreach (var e in model.SubModels)
            {
                var vertList = new List<Vertex>();
                var faceList = new List<IndexedFace>();
                refIndex = 0;
                e.CalculateData();
                for (var j = 0; j < e.Vertexes.Count; ++j)
                {
                    if (j < e.Vertexes.Count - 2)
                    {
                        if (e.Connection[j + 2])
                        {
                            if ((/*offset +*/ j) % 2 == 0)
                            {
                                faceList.Add(new IndexedFace(new int[] { refIndex, refIndex + 1, refIndex + 2 }));
                            }
                            else
                            {
                                faceList.Add(new IndexedFace(new int[] { refIndex + 1, refIndex, refIndex + 2 }));
                            }
                        }
                        ++refIndex;
                    }
                    vertList.Add(new Vertex(e.Vertexes[j], e.Colors[j], e.UVW[j], e.EmitColor[j]));
                }
                Vertexes.Add(vertList);
                Faces.Add(faceList);
                //offset += e.Vertexes.Count;
                refIndex += 2;
            }
        }
    }
}
