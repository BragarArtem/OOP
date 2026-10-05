using lab3;

namespace Lab3;

public partial class Form1 : Form
{
    private ToolStripMenuItem pointItem;
    private ToolStripMenuItem rectItem;
    private ToolStripMenuItem lineItem;
    private ToolStripMenuItem ellipseItem;
    private void SelectShapeType(ShapeType type)
    {
        pointItem.Checked = false;
        rectItem.Checked = false;
        lineItem.Checked = false;
        ellipseItem.Checked = false;
        switch (type)
        {
            case ShapeType.Point:
            pointItem.Checked = true;
            break;
            case ShapeType.Rectangle:
            rectItem.Checked = true;
            break;
            case ShapeType.Line:
            lineItem.Checked = true;
            break;
            case ShapeType.Ellipse:
            ellipseItem.Checked = true;
            break;
        }
        currentShapeType = type;
    }
    public Form1()
    {
        InitializeComponent();
        ClientSize = new Size(1080, 720);
        Text = "Lab3";
        this.DoubleBuffered = true;

        MenuStrip menuStrip = new MenuStrip();
        ToolStripMenuItem fileMenu = new ToolStripMenuItem("Файл");
        ToolStripMenuItem objectsMenu = new ToolStripMenuItem("Об'єкти");
        ToolStripMenuItem helpMenu = new ToolStripMenuItem("Довідка");
        pointItem = new ToolStripMenuItem("Крапка");
        rectItem = new ToolStripMenuItem("Прямокутник");
        lineItem = new ToolStripMenuItem("Лінія");
        ellipseItem = new ToolStripMenuItem("Еліпс");

        Controls.Add(menuStrip);
        MainMenuStrip = menuStrip;
        menuStrip.Items.Add(fileMenu);
        menuStrip.Items.Add(objectsMenu);
        menuStrip.Items.Add(helpMenu);
        objectsMenu.DropDownItems.Add(pointItem);
        objectsMenu.DropDownItems.Add(rectItem);
        objectsMenu.DropDownItems.Add(lineItem);
        objectsMenu.DropDownItems.Add(ellipseItem);

        fileMenu.Click += (sender, e) => {MessageBox.Show("there is something, but not here");};
        helpMenu.Click += (sender, e) => {MessageBox.Show("Choose Objects option to draw some shapes");};
        pointItem.Click += (sender, e) => {SelectShapeType(ShapeType.Point);};
        lineItem.Click += (sender, e) => {SelectShapeType(ShapeType.Line);};
        rectItem.Click += (sender, e) => {SelectShapeType(ShapeType.Rectangle);};
        ellipseItem.Click += (sender, e) => {SelectShapeType(ShapeType.Ellipse);};
        SelectShapeType(ShapeType.Point);
        this.MouseDown += (sender, e) => 
        {
            isDrawing = true;
            currentStart = e.Location;
            currentEnd = e.Location;
        };
        this.MouseUp += (sender, e) => 
        {
            isDrawing = false;
            if (ShapeCount >= shapes.Length)
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
                shapes[ShapeCount] = newShape;
                ShapeCount++;
                this.Invalidate();
            }
        };
        this.MouseMove += (sender, e) =>
        {
            if (!isDrawing) return;
            currentEnd = e.Location;
            this.Invalidate();
        };
        this.Paint += (sender, e) =>
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < ShapeCount; i++)
            {
                shapes[i].Show(g);
            }
            if (isDrawing)
            {
                Pen tempPen = new Pen(Color.Blue);
                int width = Math.Abs(currentEnd.X - currentStart.X);
                int height = Math.Abs(currentEnd.Y - currentStart.Y);
                int x = Math.Min(currentStart.X, currentEnd.X);
                int y = Math.Min(currentStart.Y, currentEnd.Y);
                g.DrawRectangle(tempPen, x, y, width, height);
            }
        };
    }
}
