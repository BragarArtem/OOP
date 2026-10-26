namespace Lab4;
public abstract class Shape : IDisposable
{
    public Point StartPoint = new Point();
    public Point EndPoint = new Point();
    public Pen OutlinePen = new Pen(Color.Black);
    public bool IsPreview {get; set;}
    public abstract void Show(Graphics g);
    public virtual void Dispose()
    {
        OutlinePen.Dispose();
    }
}
