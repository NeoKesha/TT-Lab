using QuikGraph.Algorithms.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using TT_Lab.AssetData.Graphics.SubModels;

namespace TT_Lab.MeshProcessor
{
    public class Mesh : QuikGraph.BidirectionalGraph<MeshTriangleNode, MeshTriangleEdge>
    {
        List<Vertex> vertices;
        List<IndexedFace> faces;

        public List<MeshTriangleNode> Triangles { get; set; } = new();
        public List<MeshStrip> Strips { get; set; } = new();

        public Mesh(List<Vertex> vertices, List<IndexedFace> faces) : base()
        {
            this.vertices = vertices;
            this.faces = faces;
        }

        public List<Vertex> GetVertices()
        {
            return vertices;
        }

        public void BuildTopology()
        {
            Triangles.Clear();
            Strips.Clear();
            Clear();

            // Build triangle nodes
            foreach (var face in faces)
            {
                Triangles.Add(new MeshTriangleNode(face.Indexes!));
                AddVertex(Triangles[^1]);
            }

            // Find all the neighbours for the triangles
            foreach (var tri in Triangles)
            {
                foreach (var otherTri in Triangles)
                {
                    if (tri.IsNeighbour(otherTri) && !tri.Neighbours.Contains(otherTri))
                    {
                        tri.Neighbours.Add(otherTri);
                    }
                }
            }

            // Build all the unique edges of the graph
            foreach (var tri in Triangles)
            {
                foreach (var neighbour in tri.Neighbours)
                {
                    var edge = new MeshTriangleEdge(tri, neighbour);
                    if (!Edges.Contains(edge))
                    {
                        AddEdge(edge);
                    }
                }
            }

            foreach (var edge in Edges)
            {
                foreach (var connected in OutEdges(edge.Target))
                {
                    if (connected != edge && !edge.ConnectedEdges.Contains(connected))
                    {
                        edge.ConnectedEdges.Add(connected);
                    }
                }
            }
        }

        public void Strippify(Int32 maxStripLength)
        {
            Strips.Clear();

            static IEnumerable<MeshTriangleEdge> filter(IEnumerable<MeshTriangleEdge> edges)
            {
                return edges.Where(e => !e.IsInStrip());
            }

            var dfs = new DepthFirstSearchAlgorithm<MeshTriangleNode, MeshTriangleEdge>(null, this, new Dictionary<MeshTriangleNode, QuikGraph.GraphColor>(), filter);

            Dictionary<MeshTriangleNode, MeshTriangleEdge> verticesPredecessors = new();
            void TreeEdgeHandler(MeshTriangleEdge edge)
            {
                verticesPredecessors[edge.Target] = edge;
                edge.AddToStrip();
            }

            void VertexFinishedHandler(MeshTriangleNode node)
            {
                node.AddToStrip();
            }

            dfs.TreeEdge += TreeEdgeHandler;
            dfs.FinishVertex += VertexFinishedHandler;

            while (Triangles.Where(t => !t.IsInStrip()).Any())
            {
                verticesPredecessors.Clear();
                dfs.SetRootVertex(Triangles.Where(t => !t.IsInStrip()).First());
                dfs.Compute();
                var strip = new MeshStrip();
                foreach (var (key, value) in verticesPredecessors)
                {
                    strip.Strip.Add(key);
                    if (strip.Length >= maxStripLength)
                    {
                        Strips.Add(strip);
                        strip = new MeshStrip();
                    }
                }
                if (strip.Length > 0)
                {
                    Strips.Add(strip);
                }
            }

            dfs.TreeEdge -= TreeEdgeHandler;
            dfs.FinishVertex -= VertexFinishedHandler;
        }

        public class MeshStrip
        {
            public List<MeshTriangleNode> Strip { get; set; } = new();

            public Int32 Length => Strip.Count == 0 ? 0 : Strip.Count + 2;
        }
    }
}
