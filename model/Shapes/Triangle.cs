namespace model.Shapes
{
    public class Triangle : Shape
    {
        public int Side1 { get; set; }
        public int Side2 { get; set; }
        public int Side3 { get; set; }

        public Triangle(string name) : base(name, 3) { }
    }
}
