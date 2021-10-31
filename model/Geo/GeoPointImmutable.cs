using System;

namespace model.Geo
{
    public class GeoPointImmutable : IEquatable<GeoPointImmutable>
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }

        public GeoPointImmutable(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(GeoPointImmutable other)
        {
            if (other is null) return false;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj) =>
            Equals(obj as GeoPointImmutable);

        public override int GetHashCode() => X + Y;

        public static bool operator ==(GeoPointImmutable left, GeoPointImmutable right) =>
            (left is null && right is null) || (left?.Equals(right) ?? false);

        public static bool operator !=(GeoPointImmutable left, GeoPointImmutable right) =>
            !(left == right);
    }
}
