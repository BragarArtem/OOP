namespace Lab5;
public class ObjectsTableWindow : Form
{
    private DataGridView grid = new DataGridView();
    public event Action<int>? SelectRow; // Bonus 1
    public event Action<int>? DeleteRow; // Bonus 2
    public bool IsUpdating = false;
    public ObjectsTableWindow()
    {
        grid.Columns.Add("name", "Назва");
        grid.Columns.Add("startX", "StartX");
        grid.Columns.Add("startY", "StartY");
        grid.Columns.Add("endX", "EndX");
        grid.Columns.Add("endY", "EndY");
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.SelectionChanged += Grid_SelectionChanged;
        grid.Dock = DockStyle.Fill;
        grid.KeyDown += Grid_KeyDown;
        Controls.Add(grid);
        ClientSize = new Size(540, 360);
        Text = "Objects Table";
    }
    public void UpdateRows(List<TableRow> rows)
    {
        IsUpdating = true;
        grid.Rows.Clear();
        foreach (TableRow row in rows)
        {
            grid.Rows.Add(row.Name, row.StartX, row.StartY, row.EndX, row.EndY);
        }
        grid.ClearSelection();
        IsUpdating = false;
    }
    private void Grid_SelectionChanged(object? sender, EventArgs e)
    {
        if(IsUpdating) return;
        if(grid.CurrentRow != null)
        {
            SelectRow?.Invoke(grid.CurrentRow.Index);
        }
    }
    private void Grid_KeyDown(object? sender, KeyEventArgs e)
    {
        if(e.KeyCode == Keys.Delete && grid.CurrentRow != null)
        {
            DeleteRow?.Invoke(grid.CurrentRow.Index);
        }
    }
}