namespace model.Geo
{
    public class GeoPoint
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }

        public GeoPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);
    }
}
