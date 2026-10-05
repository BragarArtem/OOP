namespace Lab3;
public class PointShape : Shape
{
    public Brush FillBrush;
    public PointShape()
    {
        FillBrush = new SolidBrush(OutlinePen.Color);
    }
    public override void Show(Graphics g)
    {
        int width = 4;
        int height = 4;
        int x = StartPoint.X - (width / 2); 
        int y = StartPoint.Y - (height / 2);
        g.FillEllipse(FillBrush, x, y, width, height);
    }
}