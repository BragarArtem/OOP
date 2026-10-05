namespace Lab3;
public class RectShape : Shape
{
    public override void Show(Graphics g)
    {
        int dx = Math.Abs(EndPoint.X - StartPoint.X);
        int dy = Math.Abs(EndPoint.Y - StartPoint.Y);
        int width = dx * 2;
        int height = dy * 2; 
        int x = StartPoint.X - dx;
        int y = StartPoint.Y - dy;
        g.DrawRectangle(OutlinePen, x, y, width, height);
    }
}