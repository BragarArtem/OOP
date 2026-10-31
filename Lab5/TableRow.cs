namespace Lab5;
public class TableRow
{
    public int StartX {get;}
    public int StartY {get;}
    public int EndX {get;}
    public int EndY {get;}
    public string Name {get;}
    public TableRow(int startX, int startY, int endX, int endY, string name)
    {
        StartX = startX;
        StartY = startY;
        EndX = endX;
        EndY = endY;
        Name = name;
    }
}