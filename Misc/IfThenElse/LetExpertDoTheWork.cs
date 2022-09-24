namespace Misc.IfThenElse;

public class LetExpertDoTheWork
{
    interface IPrinter
    {
        void Print(PdfDoc d);
        void Print(TextDoc d);
        void Print(ImgDoc d);
    }

    class Printer : IPrinter
    {
        private readonly ITestOutputHelper testOutputHelper;

        public Printer(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        public void Print(PdfDoc d) { testOutputHelper.WriteLine($"pdf doc {d.Name} is printed"); }
        public void Print(TextDoc d) { testOutputHelper.WriteLine($"text doc {d.Name} is printed"); }
        public void Print(ImgDoc d) { testOutputHelper.WriteLine($"image doc {d.Name} is printed"); }
    }

    abstract class Document
    {
        public string Name { get; set; }
        public abstract void Print(IPrinter p);
    }

    class PdfDoc : Document { public override void Print(IPrinter p) => p.Print(this); }
    class TextDoc : Document { public override void Print(IPrinter p) => p.Print(this); }
    class ImgDoc : Document { public override void Print(IPrinter p) => p.Print(this); }


    private readonly ITestOutputHelper testOutputHelper;
    private readonly IPrinter printer;

    public LetExpertDoTheWork(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
        printer = new Printer(testOutputHelper);
    }

    [Fact]
    public void PrintPdf()
    {
        var d = new PdfDoc { Name = "pdf-doc" };
        d.Print(printer);
    }

    [Fact]
    public void PrintText()
    {
        var d = new TextDoc { Name = "txt-doc" };
        d.Print(printer);
    }

    [Fact]
    public void PrintImg()
    {
        var d = new ImgDoc { Name = "img-doc" };
        d.Print(printer);
    }
}
