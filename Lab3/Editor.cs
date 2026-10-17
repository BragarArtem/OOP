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
    private Shape[] shapes;
    public Editor()
    {
        shapes = new Shape[MaxShapes];
    }
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
        Shape previewShape = CreateShape();
        previewShape.IsPreview = true;
        previewShape.StartPoint = currentStart;
        previewShape.EndPoint = currentEnd;
        previewShape.OutlinePen.DashStyle = DashStyle.Dash;
        previewShape.Show(g);
    }
    public  void SelectShapeType(ShapeType type)
    {
        currentShapeType = type;
    }
}
