namespace Lab5;
using System.Drawing.Drawing2D;
public class Editor : IDisposable
{
    public enum OnMouseUpResult
    {
        ShapeAdded,
        ArrayIsFull,
        NothingHappend
    }
    private Descriptor currentShapeType = ShapesDescriptor.Descriptors[0];
    private const int MaxShapes = 103;
    private int shapeCount = 0;
    private int? selectedRow;
    private bool isDrawing = false;
    private Point currentStart;
    private Point currentEnd;
    private static Editor? instance;
    public static Editor Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new Editor();
            }
            return instance;
        }
    }
    private Shape[] shapes;
    private Editor()
    {
        shapes = new Shape[MaxShapes];
    }
    public int ShapeCount
    {
        get
        {
            return shapeCount;
        }
    }
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

        Shape newShape = CreateShape();
        newShape.StartPoint = currentStart;
        newShape.EndPoint = currentEnd;

        if (!AddShape(newShape))
        {
            newShape.Dispose();
            return OnMouseUpResult.ArrayIsFull;
        }
        return OnMouseUpResult.ShapeAdded;
    }
    private Shape CreateShape()
    {
        Shape shape = currentShapeType.Create();
        shape.Key = currentShapeType.Key;
        return shape;
    }
    public void DrawShapes(Graphics g)
    {
        for (int i = 0; i < shapeCount; i++)
        {
            if(i == selectedRow)
            {
                shapes[i].SetColor(Color.Blue);
            }
            shapes[i].Show(g);
            shapes[i].SetColor(Color.Black);
        }
        if (isDrawing)
        {
            DrawTemp(g);
        }
    }
    private void DrawTemp(Graphics g)
    {
        using (Shape previewShape = CreateShape())
        {
            previewShape.IsPreview = true;
            previewShape.StartPoint = currentStart;
            previewShape.EndPoint = currentEnd;
            previewShape.OutlinePen.DashStyle = DashStyle.Dash;
            previewShape.Show(g);
        }
    }
    public void SelectShapeType(Descriptor type)
    {
        currentShapeType = type;
    }
    public void Dispose()
    {
        for(int i = shapeCount - 1; i >= 0; i--)
        {
            shapes[i].Dispose();
        }
    }
    public Shape GetShape(int index)
    {
        if(index < shapeCount && index >= 0)
        {
            return shapes[index];
        }
        throw new ArgumentOutOfRangeException(nameof(index), index , " is out of bounds");
    }
    public void SelectShape(int index)
    {
        selectedRow = index;
    }
    public void DeleteShape(int index)
    {
        if(index >= 0 && index < shapeCount)
        {
            shapes[index].Dispose();
            for(int i = index; i < shapeCount - 1 ; i++)
            {
                shapes[i] = shapes[i+1];
            }
            shapeCount = shapeCount - 1;
            selectedRow = null;
        }
    }
    public bool AddShape(Shape shape)
    {
        if(shapeCount >= shapes.Length)
        {
            return false;
        }
        shapes[shapeCount] = shape;
        shapeCount++;
        return true;
    }    
}
