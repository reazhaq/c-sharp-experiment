namespace model.Shapes
{
    public class Pentagon : Shape
    {
        public int Side1 { get; set; }
        public int Side2 {  get; set; }
        public int Side3 { get; set; }
        public int Side4 { get; set; }
        public int Side5 { get; set; }

        public Pentagon(string name) : base(name, 5) { }
    }
}
