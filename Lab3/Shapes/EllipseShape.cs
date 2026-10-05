namespace Lab3;
public class EllipseShape : Shape
{
    public Brush FillBrush = new SolidBrush(Color.Cyan);
    public override void Show(Graphics g)
    {
        int radX = Math.Abs(EndPoint.X - StartPoint.X);
        int radY = Math.Abs(EndPoint.Y - StartPoint.Y);
        int x = Math.Min(StartPoint.X, EndPoint.X);
        int y = Math.Min(StartPoint.Y, EndPoint.Y);
        g.FillEllipse(FillBrush, x, y, radX, radY);
        g.DrawEllipse(OutlinePen, x, y, radX, radY);
    }
}