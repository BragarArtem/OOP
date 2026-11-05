namespace Shared;
public class VectorTable : DataGridView
{
    private const int ColumnsAmount = 5;
    public VectorTable()
    {
        ReadOnly = true;
        AllowUserToAddRows = false;
        Dock = DockStyle.Fill;
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }
    public void ShowVector(double[] items)
    {
        Rows.Clear();
        Columns.Clear();
        int rowsAmount = (items.Length + ColumnsAmount -1) / ColumnsAmount;
        for(int i = 0; i < ColumnsAmount; i++)
        {
            Columns.Add($"{i+1}-Item", (i + 1).ToString());
        }
        for (int row = 0; row < rowsAmount; row++)
        {
            object[] cells = new object[ColumnsAmount];
            for(int column = 0; column < ColumnsAmount; column++)
            {   
                int index = row * ColumnsAmount + column;
                if (index < items.Length)
                {
                    cells[column] = items[index].ToString("F2");
                }
                else
                {
                    cells[column] = "";
                }
            }
            Rows.Add(cells); 
        }
    }
}