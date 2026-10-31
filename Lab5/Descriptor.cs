namespace Lab5;
public class Descriptor
{
    public string Name {get;}
    public string Key {get;}
    public Bitmap Icon {get;}
    public Func<Shape> Create {get;}
    public Descriptor(string name, string key,Bitmap icon, Func<Shape> create)
    {
        Name = name;
        Key = key;
        Icon = icon;
        Create = create;
    }
}