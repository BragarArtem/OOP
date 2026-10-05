namespace Lab3;
public class LineShape : Shape
{
    public override void Show(Graphics g)
    {
        g.DrawLine(OutlinePen, StartPoint, EndPoint);
    }
}