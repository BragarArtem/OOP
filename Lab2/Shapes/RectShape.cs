namespace Lab2;
public class RectShape : Shape
{
    public Brush FillBrush = new SolidBrush(Color.Cyan);
    public override void Show(Graphics g)
    {
        int width = Math.Abs(EndPoint.X - StartPoint.X);
        int height = Math.Abs(EndPoint.Y - StartPoint.Y);
        int x = Math.Min(StartPoint.X, EndPoint.X);
        int y = Math.Min(StartPoint.Y, EndPoint.Y);
        g.FillRectangle(FillBrush, x, y, width, height);
        g.DrawRectangle(OutlinePen, x, y, width, height);
    }
}