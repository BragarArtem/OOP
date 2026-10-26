using System.Drawing.Drawing2D;

namespace Lab4;
public class ALineWithEllipsesInTheFormOfCirclesAtBothEndsOfThisLineShape : Shape
{
    private const int Radius = 16;
    private EllipseShape firstCircle = new EllipseShape();
    private EllipseShape secondCircle = new EllipseShape();
    private LineShape line = new LineShape();

    private void DrawEnclosingCircle(Graphics g, Point center, EllipseShape circle)
    {
        circle.StartPoint = new Point(center.X - Radius, center.Y - Radius);
        circle.EndPoint = new Point(center.X + Radius, center.Y + Radius);
        circle.Show(g);
    }
    public override void Show(Graphics g)
    {
        line.OutlinePen.DashStyle = OutlinePen.DashStyle;
        firstCircle.OutlinePen.DashStyle = OutlinePen.DashStyle;
        secondCircle.OutlinePen.DashStyle = OutlinePen.DashStyle;
        line.StartPoint = StartPoint;
        line.EndPoint = EndPoint;
        line.Show(g);
        DrawEnclosingCircle(g, StartPoint, firstCircle);
        DrawEnclosingCircle(g, EndPoint, secondCircle);
    }
    public override void Dispose()
    {
        firstCircle.Dispose();
        secondCircle.Dispose();
        line.Dispose();
        base.Dispose();
    }
}