namespace Lab3;
public class Editor
{
    public enum ShapeType
    {
        Point, 
        Line,
        Rectangle,
        Ellipse
    }
    private ShapeType currentShapeType = ShapeType.Point;
    private Shape[] shapes = new Shape[103];
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
    public void OnMouseUp()
    {
        isDrawing = false;
        if (shapeCount >= shapes.Length)
        {
            MessageBox.Show("reached max amount of shapes on the sheet (" + shapes.Length + ")");
        }
        else
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
            newShape.StartPoint = currentStart;
            newShape.EndPoint = currentEnd;
            shapes[shapeCount] = newShape;
            shapeCount++;
        }
    }
}