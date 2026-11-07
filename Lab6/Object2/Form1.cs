namespace Object2;
using Shared;
using System.IO.Pipes;
using System.Globalization;
using System.Runtime.InteropServices;

public partial class Form1 : Form
{
    private const string PipeName = "Lab6.Object2";
    private const int Lab6Height = 100;
    private VectorTable vectorTable = new VectorTable();
    public Form1()
    {
        InitializeComponent();
        Controls.Add(vectorTable);
        Rectangle area = Screen.FromControl(this).WorkingArea;
        StartPosition = FormStartPosition.Manual;
        Bounds = new Rectangle(area.Left, area.Top + Lab6Height, area.Width / 2, area.Height - Lab6Height);
        this.Load += Form_Load;
    }
    private async void Form_Load(object? sender, EventArgs e)
    {
        while (true)
        {
            try
            {
                using NamedPipeServerStream stream = new NamedPipeServerStream(PipeName, PipeDirection.InOut);
                await stream.WaitForConnectionAsync();
                using StreamReader reader = new StreamReader(stream);
                using StreamWriter writer = new StreamWriter(stream);
                writer.AutoFlush = true;
                string? line = await reader.ReadLineAsync();
                string answer = "ERROR";
                if(line != null && ProcessMessage(line))
                {
                    answer = "OK";
                }
                await writer.WriteLineAsync(answer);
            }
            catch (IOException)
            {
                await Task.Delay(100);
            }
            
        }
    }
    private bool ProcessMessage(string message)
    {
        string[] parts = message.Split(';');
        if(parts.Length != 3){return false;}
        if(!int.TryParse(parts[0], out int count) || count <= 0){return false;}
        if(!double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double min)){return false;}
        if(!double.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double max)){return false;}
        double[] generatedNumbers = VectorGenerator.Generate(count, min, max);
        Text = $"n = {count}; min = {min}; max = {max}";
        vectorTable.ShowVector(generatedNumbers);
        return CopyToClipboard(generatedNumbers);
    }
    private bool CopyToClipboard(double[] items)
    {
        string text = VectorFormat.ToText(items);
        try
        {
            Clipboard.SetText(text);
            return true;
        }
        catch (ExternalException)
        {
            MessageBox.Show("Інша пргорама не дає можливості змінити вміст буферу обміну");
            return false;
        }
    }
}
