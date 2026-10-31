namespace Lab5;
public static class ShapesDescriptor
{
    public static readonly List<Descriptor> Descriptors = new List<Descriptor>
    {
        new Descriptor("Крапка" , "Point", IconShapes.CreatePointIcon(), () => new PointShape()),
        new Descriptor("Лінія" , "Line", IconShapes.CreateLineIcon(), () => new LineShape()),
        new Descriptor("Прямокутник", "Rectangle", IconShapes.CreateRectIcon(), () => new RectShape()),
        new Descriptor("Еліпс", "Ellipse", IconShapes.CreateEllipseIcon(), () => new EllipseShape()),
        new Descriptor("Лінія з еліпсами у формі кола на обох кінцях цієї лінії", "ALineWithEllipsesInTheFormOfCirclesAtBothEndsOfThisLine", IconShapes.CreateALineWithEllipsesInTheFormOfCirclesAtBothEndsOfThisLineIcon(), () => new ALineWithEllipsesInTheFormOfCirclesAtBothEndsOfThisLineShape()),
        new Descriptor("Куб", "Cube", IconShapes.CreateCubeIcon(), () => new CubeShape())
    };
    public static Descriptor? SearchDescriptorByKey(string? key)
    {
        foreach(Descriptor descriptor in Descriptors)
        {
            if (descriptor.Key == key)
            {
                return descriptor;
            }
        }
        return null;
    }
    public static Descriptor? SearchDescriptorByName(string? name)
    {
        foreach(Descriptor descriptor in Descriptors)
        {
            if (descriptor.Name == name)
            {
                return descriptor;
            }
        }
        return null;
    }
}