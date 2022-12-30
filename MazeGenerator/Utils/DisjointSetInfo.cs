namespace MazeGenerator.Utils
{
    public class DisjointSetInfo
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