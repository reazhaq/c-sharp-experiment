namespace model.Shapes
{
    public class Shape
    {
        public string Name { get; private set; }
        public int NumberOfCorners { get; private set; }

        public Shape(string name, int numberOfCorners)
        {
            Name = name;
            NumberOfCorners = numberOfCorners;
        }
    }
}
