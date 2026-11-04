namespace Object2;
using System.IO.Pipes;
using System.Globalization;

public partial class Form1 : Form
{
    private const string PipeName = "Lab6.Object2";
    public Form1()
    {
        InitializeComponent();
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
        if(!int.TryParse(parts[0], out int count)){return;}
        if(!double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double min)){return;}
        if(!double.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double max)){return;}
        Text = $"n = {count}; min = {min}; max = {max}";
    }
}
