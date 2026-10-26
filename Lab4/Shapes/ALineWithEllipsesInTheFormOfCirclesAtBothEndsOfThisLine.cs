namespace Lab4;
public class ALineWithEllipsesInTheFormOfCirclesAtBothEndsOfThisLine : Shape
{
    private const int radius = 16;
    private EllipseShape firstCircle = new EllipseShape();
    private EllipseShape secondCircle = new EllipseShape();
    private LineShape line = new LineShape();
}