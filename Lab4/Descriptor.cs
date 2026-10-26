namespace Lab4;
public class Descriptor
{
    public string Name {get;}
    public Bitmap Icon {get;}
    public Func<Shape> Create {get;}
    public Descriptor(string name, Bitmap icon, Func<Shape> create)
    {
        Name = name;
        Icon = icon;
        Create = create;
    }
}