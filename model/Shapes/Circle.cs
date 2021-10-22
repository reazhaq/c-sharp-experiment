namespace model.Shapes
{
    public class Circle : Shape
    {
        public int Radius { get; set; }

        public Circle(string name) : base(name, 0) { }
    }
}
