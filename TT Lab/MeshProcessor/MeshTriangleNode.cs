using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TT_Lab.Util;

namespace TT_Lab.MeshProcessor
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MeshTriangleNode
    {
        int[] indexes;
        Boolean isInStrip;

        public List<MeshTriangleNode> Neighbours { get; set; } = new();

        public MeshTriangleNode(params int[] indexes)
        {
            Debug.Assert(indexes.Length == 3, "Must provide 3 indexes");
            this.indexes = indexes;
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

        public int[] GetIndexes()
        {
            return indexes;
        }


        private static int[][] indexPermutations = new int[][]{
                new int[]{ 0, 1, 2 },
                new int[]{ 2, 0, 1 },
                new int[]{ 1, 2, 0 },
                new int[]{ 2, 1, 0 },
                new int[]{ 1, 0, 2 },
                new int[]{ 0, 2, 1 }
            };

        public bool IsNeighbour(MeshTriangleNode node)
        {
            var nodeIndexes = CloneUtils.CloneArray(node.indexes).ToList();
            nodeIndexes.Sort();
            var indexes = this.indexes.ToList();
            indexes.Sort();

            foreach (var i in indexPermutations)
            {
                foreach (var i2 in indexPermutations)
                {
                    if (indexes[i[0]] == nodeIndexes[i2[0]] && indexes[i[1]] == nodeIndexes[i2[1]] && indexes[i[2]] != nodeIndexes[i2[2]])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static Boolean operator ==(MeshTriangleNode? node, MeshTriangleNode? other)
        {
            if (node is null && other is null) return true;
            if (node is null) return false;
            return node.Equals(other);
        }

        public static Boolean operator !=(MeshTriangleNode? node, MeshTriangleNode? other)
        {
            return !(node == other);
        }

        public override Boolean Equals(Object? obj)
        {
            return Equals(obj as MeshTriangleNode);
        }

        public Boolean Equals(MeshTriangleNode? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            return other.indexes[0] == indexes[0] && other.indexes[1] == indexes[1] && other.indexes[2] == indexes[2];
        }

        public override Int32 GetHashCode()
        {
            Int32 hash = 17;
            hash = hash * 31 + indexes[0].GetHashCode();
            hash = hash * 31 + indexes[1].GetHashCode();
            hash = hash * 31 + indexes[2].GetHashCode();
            return hash;
        }

        [DebuggerHidden]
        private String DebuggerDisplay
        {
            get => $"{indexes[0]}, {indexes[1]}, {indexes[2]}";
        }

        public override String ToString()
        {
            return DebuggerDisplay;
        }
    }
}
