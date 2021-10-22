namespace model.Shapes
{
    public class Rectangle : Shape
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public Rectangle(string name) : base(name, 4) { }
    }
}
