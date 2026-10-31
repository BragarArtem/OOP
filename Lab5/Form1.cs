namespace Lab5;

public partial class Form1 : Form
{
    private Editor editor;
    private ObjectsTableWindow? objectsTableWindow;
    private Descriptor currentShapeType = ShapesDescriptor.Descriptors[0];
    private void SelectShapeType(Descriptor type)
    {
        Text = "Lab5 - " + type.Name;
        editor.SelectShapeType(type);
    }
    private void CreateMenu()
    {
        MenuStrip menuStrip = new MenuStrip();
        ToolStripMenuItem fileMenu = new ToolStripMenuItem("Файл");
        ToolStripMenuItem objectsMenu = new ToolStripMenuItem("Об'єкти");
        ToolStripMenuItem helpMenu = new ToolStripMenuItem("Довідка");
        ToolStripMenuItem tableMenu = new ToolStripMenuItem("Таблиця");

        ToolStripMenuItem saveTable = new ToolStripMenuItem("Зберегти Таблицю");
        ToolStripMenuItem loadTable = new ToolStripMenuItem("Завантажити Таблицю");

        Controls.Add(menuStrip);
        MainMenuStrip = menuStrip;
        menuStrip.Items.Add(fileMenu);
        menuStrip.Items.Add(objectsMenu);
        menuStrip.Items.Add(helpMenu);
        menuStrip.Items.Add(tableMenu);
        fileMenu.DropDownItems.Add(saveTable);
        fileMenu.DropDownItems.Add(loadTable);

        helpMenu.Click += HelpMenu_Click;
        tableMenu.Click += TableMenu_Click;
        saveTable.Click += SaveMenu_Click;
        loadTable.Click += LoadTable_Click;

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
        editor = Editor.Instance;
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
                UpdateTable();
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
    private void HelpMenu_Click(object? sender, EventArgs e)
    {
        MessageBox.Show("Choose Objects option to draw some shapes");
    }
    private void TableMenu_Click(object? sender, EventArgs e)
    {
        if( objectsTableWindow == null || objectsTableWindow.IsDisposed)
        {
            objectsTableWindow = new ObjectsTableWindow();
            objectsTableWindow.UpdateRows(GetRows());
            objectsTableWindow.SelectRow += OnTableRowSelected;
            objectsTableWindow.DeleteRow += OnTableRowDeleted;
            objectsTableWindow.Show(this);
        }
        else
        {
            objectsTableWindow.Close();
        }
    }
    private void Form_Closed(object? sender, FormClosedEventArgs e)
    {
        editor.Dispose();
    }
    private List<TableRow> GetRows()
    {
        List<TableRow> result = new List<TableRow>();
        for(int i = 0; i < Editor.Instance.ShapeCount; i++)
        {
            Shape shape = Editor.Instance.GetShape(i);
            int startX = shape.StartPoint.X;
            int startY = shape.StartPoint.Y;
            int endX = shape.EndPoint.X;
            int endY = shape.EndPoint.Y;
            string name;
            Descriptor? descriptor = ShapesDescriptor.SearchDescriptorByKey(shape.Key);
            if(descriptor != null)
            {
                name = descriptor.Name;
            }
            else
            {
                name = "unknown Shape";
            }
            result.Add(new TableRow(startX, startY, endX, endY, name));
        }
        return result;
    }
    private void UpdateTable()
    {
        if(objectsTableWindow != null && !objectsTableWindow.IsDisposed)
        {
            objectsTableWindow.UpdateRows(GetRows());
        }
    }
    private List<string> RowsToCsv(List<TableRow> rows)
    {
        List<string> lines = new List<string>();
        foreach (TableRow row in rows)
        {
            lines.Add($"{row.Name},{row.StartX},{row.StartY},{row.EndX},{row.EndY}");
        }
        return lines;
    }
    private void SaveMenu_Click(object? sender, EventArgs e)
    {
        using(SaveFileDialog dialog = new SaveFileDialog())
        {
            dialog.Filter = "CSV files (*.csv)|*.csv";
            dialog.FileName = "shapesInfoReport.csv";

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(dialog.FileName, RowsToCsv(GetRows()));
            }
        }
    }
    private void OnTableRowSelected(int index)
    {
        editor.SelectShape(index);
        Invalidate();
    }
    private void OnTableRowDeleted(int index)
    {
        editor.DeleteShape(index);
        UpdateTable();
        Invalidate();
    }
    private void LoadTable_Click(object? sender, EventArgs e)
    {
        using (OpenFileDialog dialog = new OpenFileDialog())
        {
            dialog.Filter = "CSV files (*.csv)|*.csv";
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string[] lines = File.ReadAllLines(dialog.FileName);
            editor.Clear();
            foreach(string line in lines)
            {
                string[] parts = line.Split(',');
                if(parts.Length != 5)
                {
                    continue;
                }
                Descriptor? descriptor = ShapesDescriptor.SearchDescriptorByName(parts[0]);
                if(descriptor == null)
                {
                    continue;
                }
                if(!int.TryParse(parts[1], out int x1) || !int.TryParse(parts[2], out int y1) || !int.TryParse(parts[3], out int x2) || !int.TryParse(parts[4], out int y2))
                {
                    continue;
                }
                Shape shape = descriptor.Create();
                shape.Key = descriptor.Key;
                shape.StartPoint = new Point(x1, y1);
                shape.EndPoint = new Point(x2, y2);
                if (!editor.AddShape(shape))
                {
                    shape.Dispose();
                    MessageBox.Show("SHAPE ARRAY IS FULL");
                    break;
                }
            }
            UpdateTable();
            Invalidate();
        }
    }
}   