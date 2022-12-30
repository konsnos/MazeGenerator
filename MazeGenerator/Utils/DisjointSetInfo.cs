namespace MazeGenerator.Utils
{
    public struct DisjointSetInfo
    {
        public int ParentNode { set; get; }

        public int Rank { set; get; }

        public DisjointSetInfo(int parent)
        {
            ParentNode = parent;
            Rank = 0;
        }
    }
}