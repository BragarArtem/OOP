namespace Lab4;
public static class ShapesDescriptor
{
    public static readonly List<Descriptor> Descriptors = new List<Descriptor>
    {
        new Descriptor("Крапка" ,IconShapes.CreatePointIcon(), () => new PointShape()),
        new Descriptor("Лінія" ,IconShapes.CreateLineIcon(), () => new LineShape()),
        new Descriptor("Прямокутник",IconShapes.CreateRectIcon(), () => new RectShape()),
        new Descriptor("Еліпс", IconShapes.CreateEllipseIcon(), () => new EllipseShape())
    };
}