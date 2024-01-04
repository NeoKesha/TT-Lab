using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TT_Lab.MeshProcessor
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MeshTriangleEdge : QuikGraph.IUndirectedEdge<MeshTriangleNode>
    {
        readonly MeshTriangleNode edgeStart;
        readonly MeshTriangleNode edgeEnd;
        MeshTriangleEdge? nextEdgeInStrip;
        Boolean isInStrip;

        public List<MeshTriangleEdge> ConnectedEdges { get; private set; } = new();

        public MeshTriangleEdge(MeshTriangleNode edgeStart, MeshTriangleNode edgeEnd)
        {
            this.edgeStart = edgeStart;
            this.edgeEnd = edgeEnd;
        }

        public void Strippify(QuikGraph.IBidirectionalGraph<MeshTriangleNode, MeshTriangleEdge> graph, Int32 maxStripLength, ref Int32 currentStripLength)
        {
            if (currentStripLength >= maxStripLength) return;

            foreach (var connected in graph.OutEdges(Target))
            {
                if (connected == this || connected.IsInStrip()) continue;

                currentStripLength++;
                nextEdgeInStrip = connected;
                nextEdgeInStrip.Strippify(graph, maxStripLength, ref currentStripLength);
                break;
            }
        }

        public MeshTriangleEdge? GetNextEdgeInStrip()
        {
            return nextEdgeInStrip;
        }

        public Boolean IsInStrip()
        {
            return isInStrip;
        }

        public void AddToStrip()
        {
            isInStrip = true;
        }

        public void RemoveFromStrip()
        {
            isInStrip = false;
        }

        public Boolean IsEdgeConnected(MeshTriangleEdge edge)
        {
            return edge != this && (edge.edgeStart == edgeEnd || edge.edgeEnd == edgeStart);
        }

        public static Boolean operator ==(MeshTriangleEdge? edge, MeshTriangleEdge? other)
        {
            if (edge is null && other is null) return true;
            if (edge is null) return false;
            return edge.Equals(other);
        }

        public static Boolean operator !=(MeshTriangleEdge? edge, MeshTriangleEdge? other)
        {
            return !(edge == other);
        }

        public override Boolean Equals(Object? obj)
        {
            return Equals(obj as MeshTriangleEdge);
        }

        public Boolean Equals(MeshTriangleEdge? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            return (other.edgeStart == edgeStart && other.edgeEnd == edgeEnd) || (other.edgeEnd == edgeStart && other.edgeStart == edgeEnd);
        }

        public override Int32 GetHashCode()
        {
            return edgeStart.GetHashCode() + edgeEnd.GetHashCode();
        }

        [DebuggerHidden]
        private String DebuggerDisplay
        {
            get => $"Source: {Source} Target: {Target}";
        }

        public override String ToString()
        {
            return DebuggerDisplay;
        }

        public MeshTriangleNode Source => edgeStart;

        public MeshTriangleNode Target => edgeEnd;
    }
}
