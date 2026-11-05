namespace Object2;
using System.IO.Pipes;
using System.Globalization;
using System.Runtime.InteropServices;

public partial class Form1 : Form
{
    private const string PipeName = "Lab6.Object2";
    private const int ColumnsAmount = 5;
    private DataGridView grid = new DataGridView();
    public Form1()
    {
        InitializeComponent();
        CreateGrid();
        ClientSize = new Size(520, 400);
        this.Load += Form_Load;
    }
    private async void Form_Load(object? sender, EventArgs e)
    {
        while (true)
        {
            using NamedPipeServerStream stream = new NamedPipeServerStream(PipeName, PipeDirection.In);
            await stream.WaitForConnectionAsync();
            using StreamReader reader = new StreamReader(stream);
            string? line = await reader.ReadLineAsync();
            if(line != null)
            {
                ProcessMessage(line);
            }
        }
    }
    private void ProcessMessage(string message)
    {
        string[] parts = message.Split(';');
        if(parts.Length != 3){return;}
        if(!int.TryParse(parts[0], out int count) || count <= 0){return;}
        if(!double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double min)){return;}
        if(!double.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double max)){return;}
        double[] generatedNumbers = VectorGenerator.Generate(count, min, max);
        Text = $"n = {count}; min = {min}; max = {max}";
        ShowVector(generatedNumbers);
        CopyToClipboard(generatedNumbers);
    }
    private void CreateGrid()
    {
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.Dock = DockStyle.Fill;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        Controls.Add(grid);
    }
    private void ShowVector(double[] items)
    {
        grid.Rows.Clear();
        grid.Columns.Clear();
        int rowsAmount = (items.Length + ColumnsAmount -1) / ColumnsAmount;
        for(int i = 0; i < ColumnsAmount; i++)
        {
            grid.Columns.Add($"{i+1}-Item", (i + 1).ToString());
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
            grid.Rows.Add(cells); 
        }
    }
    private void CopyToClipboard(double[] items)
    {
        string[] texts = items.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray();
        string result = string.Join(';', texts);
        try
        {
            Clipboard.SetText(result);
        }
        catch (ExternalException)
        {
            MessageBox.Show("other program is not allowing to paste a text to the Clipboard");
        }
    }
}
