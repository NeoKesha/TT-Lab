using System.Collections.Generic;
using TT_Lab.AssetData.Graphics.SubModels;
using Twinsanity.PS2Hardware;

namespace TT_Lab.MeshProcessor
{
    public static class MeshProcessor
    {
        public static Mesh CreateMesh(List<Vertex> vertices, List<IndexedFace> faces)
        {
            return new Mesh(vertices, faces);
        }

        public static void ProcessMesh(Mesh mesh)
        {
            mesh.BuildTopology();
            mesh.Strippify(TwinVIFCompiler.VertexStripCache);
        }

        public static void TunnelMesh(Mesh mesh)
        {
            //foreach (var strip in mesh.Strips)
            //{
            //    if (strip.Length >= TwinVIFCompiler.VertexStripCache) continue;

            //    Queue<MeshTriangleEdge> edgeQueue = new();
            //    List<MeshTriangleEdge> tunnel = new();
            //    edgeQueue.Enqueue(strip.End);

            //    Func<MeshTriangleEdge, Boolean>[] addToTunnelCheck =
            //    {
            //        (e) => e.IsInStrip(),
            //        (e) => !e.IsInStrip()
            //    };
            //    var checkIndex = 0;
            //    while (edgeQueue.Count != 0)
            //    {
            //        var edge = edgeQueue.Dequeue();
            //        foreach (var connected in edge.ConnectedEdges)
            //        {
            //            if (addToTunnelCheck[checkIndex](connected)) continue;

            //            edgeQueue.Enqueue(connected);
            //            tunnel.Add(connected);
            //            checkIndex ^= 1;
            //            break;
            //        }
            //    }
            //}
        }
    }
}
