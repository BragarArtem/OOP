namespace Lab4;
static class IconShapes
{
    public static Bitmap CreatePointIcon()
    {
        Bitmap pointBmp = new Bitmap(24,24);
        using (Graphics g = Graphics.FromImage(pointBmp))
        using (Brush brush = new SolidBrush(Color.Black))
        {
            g.FillEllipse(brush, 8, 8, 8, 8);
        }
        return pointBmp;
    }
    public static Bitmap CreateRectIcon()
    {
        Bitmap rectBmp = new Bitmap(24,24);
        using (Graphics g = Graphics.FromImage(rectBmp))
        using (Pen pen = new Pen(Color.Black))
        {
            g.DrawRectangle(pen, 4, 6, 16, 12);
        }
        return rectBmp;
    }
    public static Bitmap CreateEllipseIcon()
    {
        Bitmap ellipseBmp = new Bitmap(24,24);
        using (Graphics g = Graphics.FromImage(ellipseBmp))
        using (Pen pen = new Pen(Color.Black))
        {
            g.DrawEllipse(pen, 2, 5, 20, 14);
        }
        return ellipseBmp;
    }
    public static Bitmap CreateLineIcon()
    {
        Bitmap lineBmp = new Bitmap(24,24);
        using (Graphics g = Graphics.FromImage(lineBmp))
        using (Pen pen = new Pen(Color.Black))
        {
            g.DrawLine(pen, 3, 3, 20, 20);
        }
        return lineBmp;
    }
    public static Bitmap CreateALineWithEllipsesInTheFormOfCirclesAtBothEndsOfThisLine()
    {
        Bitmap aLineWithEllipsesInTheFormOfCirclesAtBothEndsOfThisLineBmp = new Bitmap(24,24);
        using (Graphics g = Graphics.FromImage(aLineWithEllipsesInTheFormOfCirclesAtBothEndsOfThisLineBmp))
        using (Pen pen = new Pen(Color.Black))
        {
            g.DrawLine(pen, 3, 3, 20, 20);
            g.DrawEllipse(pen, 0, 0, 6, 6);
            g.DrawEllipse(pen, 17, 17, 6, 6); 
        }
        return aLineWithEllipsesInTheFormOfCirclesAtBothEndsOfThisLineBmp;
    }
}