namespace Lab4;

public partial class Form1 : Form
{
    private Editor editor;
    private Descriptor currentShapeType = ShapesDescriptor.Descriptors[0];
    private void SelectShapeType(Descriptor type)
    {
        Text = "Lab4 - " + type.Name;
        editor.SelectShapeType(type);
    }
    private void CreateMenu()
    {
        MenuStrip menuStrip = new MenuStrip();
        ToolStripMenuItem fileMenu = new ToolStripMenuItem("Файл");
        ToolStripMenuItem objectsMenu = new ToolStripMenuItem("Об'єкти");
        ToolStripMenuItem helpMenu = new ToolStripMenuItem("Довідка");

        Controls.Add(menuStrip);
        MainMenuStrip = menuStrip;
        menuStrip.Items.Add(fileMenu);
        menuStrip.Items.Add(objectsMenu);
        menuStrip.Items.Add(helpMenu);

        fileMenu.Click += FileMenu_Click;
        helpMenu.Click += HelpMenu_Click;

        foreach (Descriptor descriptor in ShapesDescriptor.Descriptors)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(descriptor.Name);
            item.Tag = descriptor;
            item.Click += ShapeSelector_Click;
            objectsMenu.DropDownItems.Add(item);
        }
    }
    private void CreateShapesBar()
    {
        ToolStrip shapesBar = new ToolStrip();
        shapesBar.ImageScalingSize = new Size(24, 24);

        Controls.Add(shapesBar);

        foreach (Descriptor descriptor in ShapesDescriptor.Descriptors)
        {
            ToolStripButton item = new ToolStripButton(descriptor.Name);
            item.DisplayStyle = ToolStripItemDisplayStyle.Image;
            item.ToolTipText = descriptor.Name;
            item.Tag = descriptor;
            item.Image = descriptor.Icon;
            item.Click += ShapeSelector_Click;
            shapesBar.Items.Add(item);
        }
    }
    public Form1()
    {
        InitializeComponent();
        ClientSize = new Size(1080, 720);
        this.DoubleBuffered = true;
        editor = new Editor();
        CreateShapesBar();
        CreateMenu();
        SelectShapeType(currentShapeType);
        
        this.MouseDown += Form_MouseDown;
        this.MouseMove += Form_MouseMove;
        this.MouseUp += Form_MouseUp;
        this.Paint += Form_Paint;
        this.FormClosed += Form_Closed;
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
        Descriptor type = (Descriptor)item.Tag!;
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
    private void Form_Closed(object? sender, FormClosedEventArgs e)
    {
        editor.Dispose();
    }
}   