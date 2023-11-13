namespace MazeGenerator.Utils
{
    public class CycleDetector
    {
        private readonly DisjointSetInfo[] _nodes;

        public CycleDetector(int totalNodes)
        {
            _nodes = new DisjointSetInfo[totalNodes];
            for (int i = 0; i < totalNodes; i++)
            {
                _nodes[i] = new DisjointSetInfo(i);
            }
        }

        private int Find(int node)
        {
            while (true) // Avoid recursive call
            {
                int parent = _nodes[node].ParentNode;
                if (parent.Equals(node)) return node;
                node = parent;
            }
        }

        private int PathCompressionFind(int node)
        {
            var disjointSetInfo = _nodes[node];
            int parent = disjointSetInfo.ParentNode;
            if (parent.Equals(node))
            {
                return node;
            }

            int parentNode = Find(parent);
            disjointSetInfo.ParentNode = parentNode;
            return parentNode;
        }

        private void Union(int rootU, int rootV)
        {
            var setInfoU = _nodes[rootU];
            setInfoU.ParentNode = rootV;
        }

        private void UnionByRank(int rootU, int rootV)
        {
            var setInfoU = _nodes[rootU];
            var setInfoV = _nodes[rootV];
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