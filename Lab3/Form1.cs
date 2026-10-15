namespace Lab3;

public partial class Form1 : Form
{
    private Editor editor;
    private void SelectShapeType(Editor.ShapeType type)
    {
        switch (type)
        {
            case Editor.ShapeType.Point:
            Text = "Lab3-Крапка";
            break;
            case Editor.ShapeType.Rectangle:
            Text = "Lab3-Прямокутник";
            break;
            case Editor.ShapeType.Line:
            Text = "Lab3-Лінія";
            break;
            case Editor.ShapeType.Ellipse:
            Text = "Lab3-Еліпс";
            break;
        }
        editor.SelectShapeType(type);
        
    }
    private void CreateMenu()
    {
        MenuStrip menuStrip = new MenuStrip();
        ToolStripMenuItem fileMenu = new ToolStripMenuItem("Файл");
        ToolStripMenuItem objectsMenu = new ToolStripMenuItem("Об'єкти");
        ToolStripMenuItem helpMenu = new ToolStripMenuItem("Довідка");
        ToolStripMenuItem pointItem = new ToolStripMenuItem("Крапка");
        ToolStripMenuItem rectItem = new ToolStripMenuItem("Прямокутник");
        ToolStripMenuItem lineItem = new ToolStripMenuItem("Лінія");
        ToolStripMenuItem ellipseItem = new ToolStripMenuItem("Еліпс");

        Controls.Add(menuStrip);
        MainMenuStrip = menuStrip;
        menuStrip.Items.Add(fileMenu);
        menuStrip.Items.Add(objectsMenu);
        menuStrip.Items.Add(helpMenu);
        objectsMenu.DropDownItems.Add(pointItem);
        objectsMenu.DropDownItems.Add(rectItem);
        objectsMenu.DropDownItems.Add(lineItem);
        objectsMenu.DropDownItems.Add(ellipseItem);

        pointItem.Tag = Editor.ShapeType.Point;
        lineItem.Tag = Editor.ShapeType.Line;
        rectItem.Tag = Editor.ShapeType.Rectangle;
        ellipseItem.Tag = Editor.ShapeType.Ellipse;

        pointItem.Click += ShapeSelector_Click;
        lineItem.Click += ShapeSelector_Click;
        rectItem.Click += ShapeSelector_Click;
        ellipseItem.Click += ShapeSelector_Click;
        fileMenu.Click += FileMenu_Click;
        helpMenu.Click += HelpMenu_Click;
    }
    private void CreateShapesBar()
    {
        ToolStrip shapesBar = new ToolStrip();
        shapesBar.ImageScalingSize = new Size(24, 24);
        ToolStripButton pointButton = new ToolStripButton();
        ToolStripButton rectButton = new ToolStripButton();
        ToolStripButton lineButton = new ToolStripButton();
        ToolStripButton ellipseButton = new ToolStripButton();

        pointButton.ToolTipText = "Крапка";
        rectButton.ToolTipText = "Прямокутник";
        lineButton.ToolTipText = "Лінія";
        ellipseButton.ToolTipText = "Еліпс";

        pointButton.Tag = Editor.ShapeType.Point;
        rectButton.Tag = Editor.ShapeType.Rectangle;
        lineButton.Tag = Editor.ShapeType.Line;
        ellipseButton.Tag = Editor.ShapeType.Ellipse;

        pointButton.Click += ShapeSelector_Click;
        rectButton.Click += ShapeSelector_Click;
        lineButton.Click += ShapeSelector_Click;
        ellipseButton.Click += ShapeSelector_Click;

        pointButton.Image = IconShapes.CreatePointIcon();
        rectButton.Image = IconShapes.CreateRectIcon();
        lineButton.Image = IconShapes.CreateLineIcon();
        ellipseButton.Image = IconShapes.CreateEllipseIcon();

        Controls.Add(shapesBar);
        shapesBar.Items.Add(pointButton);
        shapesBar.Items.Add(rectButton);
        shapesBar.Items.Add(lineButton);
        shapesBar.Items.Add(ellipseButton);
    }
    public Form1()
    {
        InitializeComponent();
        ClientSize = new Size(1080, 720);
        this.DoubleBuffered = true;
        editor = new Editor();
        CreateShapesBar();
        CreateMenu();
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
    private void ShapeSelector_Click(object? sender, EventArgs e)
    {
        ToolStripItem item = (ToolStripItem)sender!;
        Editor.ShapeType type = (Editor.ShapeType)item.Tag!;
        SelectShapeType(type);
    }
    private void FileMenu_Click(object? sender, EventArgs e)
    {
        MessageBox.Show("there is something, but not here");
    }
    private void HelpMenu_Click(object? sender, EventArgs e)
    {
        MessageBox.Show("Choose Objects option to draw some shapes");
    }
}