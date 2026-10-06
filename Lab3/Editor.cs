namespace Lab3;
using System.Drawing.Drawing2D;
public class Editor
{
    public enum ShapeType
    {
        Point, 
        Line,
        Rectangle,
        Ellipse
    }
    public enum OnMouseUpResult
    {
        ShapeAdded,
        ArrayIsFull,
        NothingHappend
    }
    private ShapeType currentShapeType = ShapeType.Point;
    private const int MaxShapes = 103;
    private Shape[] shapes = new Shape[MaxShapes];
    private int shapeCount = 0;
    private bool isDrawing = false;
    private Point currentStart;
    private Point currentEnd;
    public void OnMouseDown(Point point)
    {
        isDrawing = true;
        currentStart = point;
        currentEnd = point;
    }
    public bool OnMouseMove(Point point)
    {
        if (isDrawing)
        {
            currentEnd = point;
        }
        return isDrawing;
    }
    public OnMouseUpResult OnMouseUp()
    {
        if (!isDrawing)
        {
            return OnMouseUpResult.NothingHappend;
        }

        isDrawing = false;

        if (shapeCount >= shapes.Length)
        {
            return OnMouseUpResult.ArrayIsFull;
        }
        Shape newShape = CreateShape();
        newShape.StartPoint = currentStart;
        newShape.EndPoint = currentEnd;
        shapes[shapeCount] = newShape;
        shapeCount++;
        return OnMouseUpResult.ShapeAdded;
    }
    private Shape CreateShape()
    {
        Shape newShape;
        switch (currentShapeType)
        {
            case ShapeType.Point: 
                newShape = new PointShape();
            break;
            case ShapeType.Line: 
                newShape = new LineShape();
            break;
            case ShapeType.Rectangle: 
                newShape = new RectShape();
            break;
            case ShapeType.Ellipse: 
                newShape = new EllipseShape();
            break;
            default:
                throw new InvalidOperationException("unknown shape");
        }
        return newShape;
    }
    public void DrawShapes(Graphics g)
    {
        for (int i = 0; i < shapeCount; i++)
        {
            shapes[i].Show(g);
        }
        if (isDrawing)
        {
            DrawTemp(g);
        }
    }
    private void DrawTemp(Graphics g)
    {
        int width = Math.Abs(currentEnd.X - currentStart.X);
        int height = Math.Abs(currentEnd.Y - currentStart.Y);
        int x = Math.Min(currentStart.X, currentEnd.X);
        int y = Math.Min(currentStart.Y, currentEnd.Y);
        using(Pen tempPen = new Pen(Color.Black))
        {
            tempPen.DashStyle = DashStyle.Dash;
            g.DrawRectangle(tempPen, x, y, width, height);
        } 
    }
    public  void SelectShapeType(ShapeType type)
    {
        currentShapeType = type;
    }
}
