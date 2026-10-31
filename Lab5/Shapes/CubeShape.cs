namespace Lab5;
public class CubeShape : Shape
{
    private RectShape frontRect = new RectShape();
    private RectShape backRect = new RectShape();
    private LineShape[] lineShapes = new LineShape[4];
    public CubeShape()
    {
        for(int i = 0; i < lineShapes.Length; i++)
        {
            lineShapes[i] = new LineShape();
        }
    }

    public override void Show(Graphics g)
    {
        frontRect.OutlinePen.DashStyle = OutlinePen.DashStyle;
        backRect.OutlinePen.DashStyle = OutlinePen.DashStyle;
        for(int i = 0; i < lineShapes.Length; i++)
        {
            lineShapes[i].OutlinePen.DashStyle = OutlinePen.DashStyle;
        }
        int left = Math.Min(StartPoint.X, EndPoint.X);
        int right = Math.Max(StartPoint.X, EndPoint.X);
        int top = Math.Min(StartPoint.Y, EndPoint.Y);
        int bottom = Math.Max(StartPoint.Y, EndPoint.Y);
        int width = (right - left) ;
        int height = (bottom - top);
        int depth = (Math.Min(width, height) * 1/3);

        frontRect.StartPoint = new Point(((left + (right - depth)) / 2), (((top + depth) + bottom) / 2));
        frontRect.EndPoint = new Point((right - depth) , bottom);

        backRect.StartPoint = new Point((right + (left + depth)) / 2, (top + (bottom - depth)) / 2 );
        backRect.EndPoint = new Point(right, bottom - depth);


        lineShapes[0].StartPoint = new Point(left, top + depth);
        lineShapes[0].EndPoint = new Point(left + depth, top);

        lineShapes[1].StartPoint = new Point(right - depth, top + depth);
        lineShapes[1].EndPoint = new Point(right, top);

        lineShapes[2].StartPoint = new Point(left, bottom);
        lineShapes[2].EndPoint = new Point(left + depth, bottom - depth);

        lineShapes[3].StartPoint = new Point(right - depth, bottom);
        lineShapes[3].EndPoint = new Point(right, bottom - depth);

        frontRect.Show(g);
        backRect.Show(g);
        for(int i = 0; i < lineShapes.Length; i++)
        {
            lineShapes[i].Show(g);
        }
    }
    public override void SetColor(Color color)
    {
        base.SetColor(color);
        for(int i = 0; i < lineShapes.Length; i++)
        {
            lineShapes[i].SetColor(color);
        }
        frontRect.SetColor(color);
        backRect.SetColor(color);
    }
    public override void Dispose()
    {
        frontRect.Dispose();
        backRect.Dispose();
        for(int i = 0; i < lineShapes.Length; i++)
        {
            lineShapes[i].Dispose();
        }
        base.Dispose();
    }
}