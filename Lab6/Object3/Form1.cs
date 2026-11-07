namespace Object3;
using Shared;
using System.IO.Pipes;
using System.Runtime.InteropServices;
public partial class Form1 : Form
{
    private const string PipeName = "Lab6.Object3";
    private const int Lab6Height = 100;
    private VectorTable vectorTable = new VectorTable();
    public Form1()
    {
        InitializeComponent();
        Controls.Add(vectorTable);
        Rectangle area = Screen.FromControl(this).WorkingArea;
        StartPosition = FormStartPosition.Manual;
        Bounds = new Rectangle(area.Left + area.Width / 2, area.Top + Lab6Height, area.Width / 2, area.Height - Lab6Height);
        Text = "Object3";
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
                if(line == "READ" && ReadAndSort())
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
    private bool ReadAndSort()
    {
        string text = "";
        try
        {
            text = Clipboard.GetText();
        } catch (ExternalException)
        {
            return false;
        }
        if(!VectorFormat.TryParse(text, out double[] items)){return false;}
        Array.Sort(items);
        vectorTable.ShowVector(items);
        return true;
    }
}
