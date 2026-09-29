namespace Lab2;
public class EllipseShape : Shape
{
    public override void Show(Graphics g)
    {
        int radX = Math.Abs(EndPoint.X - StartPoint.X);
        int radY = Math.Abs(EndPoint.Y - StartPoint.Y);
        int width = radX * 2;
        int height = radY * 2;
        int x = StartPoint.X - radX; 
        int y = StartPoint.Y - radY;
        g.DrawEllipse(OutlinePen, x, y, width, height);
    }
}