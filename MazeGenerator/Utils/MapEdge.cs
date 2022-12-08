namespace MazeGenerator
{
    public readonly struct MapEdge
    {
        private GridCoordinates MapCoordinatesFrom { get; }
        private GridCoordinates MapCoordinatesTo { get; }
        private EdgeDirection EdgeDirection { get; }

        public MapEdge(GridCoordinates vertexCoordinatesFrom, GridCoordinates vertexCoordinatesTo)
        {
            MapCoordinatesFrom =
                new GridCoordinates((vertexCoordinatesFrom.X * 2) + 1, (vertexCoordinatesFrom.Y * 2) + 1);
            MapCoordinatesTo = new GridCoordinates((vertexCoordinatesTo.X * 2) + 1, (vertexCoordinatesTo.Y * 2) + 1);

            EdgeDirection = MapCoordinatesFrom.X == MapCoordinatesTo.X ? EdgeDirection.Top : EdgeDirection.Left;
        }

        public GridCoordinates[] GetAllEdgeCoordinates()
        {
            var allCoordinates = new GridCoordinates[3];
            allCoordinates[0] = MapCoordinatesFrom;
            allCoordinates[1] = EdgeDirection == EdgeDirection.Top
                ? new GridCoordinates(MapCoordinatesFrom.X, MapCoordinatesFrom.Y - 1)
                : new GridCoordinates(MapCoordinatesFrom.X - 1, MapCoordinatesFrom.Y);
            allCoordinates[2] = MapCoordinatesTo;
            return allCoordinates;
        }
    }

    public enum EdgeDirection
    {
        Top,
        Left
    }
}