using System.Collections.Generic;
using MazeGenerator.Utils;

namespace MazeGenerator.GenerationAlgorithms
{
    public class CycleDetector
    {
        private List<DisjointSetInfo> nodes;

        public CycleDetector(int totalNodes)
        {
            InitDisJointSets(totalNodes);
        }

        private void InitDisJointSets(int totalNodes)
        {
            nodes = new List<DisjointSetInfo>(totalNodes);
            for (int i = 0; i < totalNodes; i++)
            {
                nodes.Add(new DisjointSetInfo(i));
            }
        }

        private int Find(int node)
        {
            int parent = nodes[node].ParentNode;
            if (parent.Equals(node))
            {
                return node;
            }
            else
            {
                //todo: convert recursion to iteration
                return Find(parent);
            }
        }

        private int PathCompressionFind(int node)
        {
            DisjointSetInfo setInfo = nodes[node];
            int parent = setInfo.ParentNode;
            if (parent.Equals(node))
            {
                return node;
            }
            else
            {
                int parentNode = Find(parent);
                setInfo.ParentNode = parentNode;
                return parentNode;
            }
        }

        private void Union(int rootU, int rootV)
        {
            var setInfoU = nodes[rootU];
            setInfoU.ParentNode = rootV;
        }

        private void UnionByRank(int rootU, int rootV)
        {
            var setInfoU = nodes[rootU];
            var setInfoV = nodes[rootV];
            int rankU = setInfoU.Rank;
            int rankV = setInfoV.Rank;
            if (rankU < rankV)
            {
                setInfoU.ParentNode = rootV;
            }
            else
            {
                setInfoV.ParentNode = rootU;
                if (rankU == rankV)
                {
                    setInfoU.Rank++;
                }
            }
        }

        public bool DetectCycle(int u, int v)
        {
            int rootU = PathCompressionFind(u);
            int rootV = PathCompressionFind(v);
            if (rootU.Equals(rootV))
            {
                return true;
            }

            UnionByRank(rootU, rootV);
            return false;
        }
    }
}