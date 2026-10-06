namespace Lab3;

public partial class Form1 : Form
{
    private ToolStripMenuItem pointItem;
    private ToolStripMenuItem rectItem;
    private ToolStripMenuItem lineItem;
    private ToolStripMenuItem ellipseItem;
    private Editor editor;
    private void SelectShapeType(Editor.ShapeType type)
    {
        switch (type)
        {
            case Editor.ShapeType.Point:
            Text = "Lab3-Point";
            break;
            case Editor.ShapeType.Rectangle:
            Text = "Lab3-Rectangle";
            break;
            case Editor.ShapeType.Line:
            Text = "Lab3-Line";
            break;
            case Editor.ShapeType.Ellipse:
            Text = "Lab3-Ellipse";
            break;
        }
        editor.SelectShapeType(type);
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
        editor = new Editor();

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
        pointItem.Click += (sender, e) => {SelectShapeType(Editor.ShapeType.Point);};
        lineItem.Click += (sender, e) => {SelectShapeType(Editor.ShapeType.Line);};
        rectItem.Click += (sender, e) => {SelectShapeType(Editor.ShapeType.Rectangle);};
        ellipseItem.Click += (sender, e) => {SelectShapeType(Editor.ShapeType.Ellipse);};
        SelectShapeType(Editor.ShapeType.Point);
        this.MouseDown += Form_MouseDown;
        this.MouseMove += Form_MouseMove;
        this.MouseUp += Form_MouseUp;
        this.Paint += Form_Paint;
    }
    private void Form_MouseDown(object? sender, MouseEventArgs e)
    {
        editor.OnMouseDown(e.Location);
    }
    private void Form_MouseMove(object? sender, MouseEventArgs e)
    {
        if (editor.OnMouseMove(e.Location))
        {
            Invalidate();
        }
    }
    private void Form_MouseUp(object? sender, MouseEventArgs e)
    {
        switch (editor.OnMouseUp())
        {
            case Editor.OnMouseUpResult.ShapeAdded:
                Invalidate();
            break;
            case Editor.OnMouseUpResult.ArrayIsFull:
                MessageBox.Show("Shapes Array is full");
                Invalidate();
            break;
        }
    }
    private void Form_Paint(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        editor.DrawShapes(g);
    }
}