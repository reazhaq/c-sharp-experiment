namespace model.Shapes
{
    public class Square : Shape
    {
        public int Side { get; set; }

        public Square(string name) : base(name, 4) { }
    }
}
