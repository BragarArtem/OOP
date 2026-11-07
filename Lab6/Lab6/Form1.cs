namespace Lab6;
using System.Globalization;

public partial class Form1 : Form
{
    private const string Object2PipeName = "Lab6.Object2";
    private const string Object3PipeName = "Lab6.Object3";
    private const int Lab6Height = 100;
    private ChildProgram object2;
    private ChildProgram object3;
    public Form1()
    {
        InitializeComponent();
        Rectangle area = Screen.FromControl(this).WorkingArea;
        StartPosition = FormStartPosition.Manual;
        Bounds = new Rectangle(area.Left, area.Top, area.Width, Lab6Height);
        CreateMenu();
        object2 = new ChildProgram("Object2");
        object3 = new ChildProgram("Object3");
        this.FormClosed += Form_Closed;
    }
    private void CreateMenu()
    {
        MenuStrip menuStrip = new MenuStrip();
        ToolStripMenuItem workMenuItem = new ToolStripMenuItem("Робота");
        ToolStripMenuItem runMenuItem = new ToolStripMenuItem("Запустити");
        Controls.Add(menuStrip);
        MainMenuStrip = menuStrip;
        menuStrip.Items.Add(workMenuItem);
        workMenuItem.DropDownItems.Add(runMenuItem);
        runMenuItem.Click += RunMenuItem_Click;
    }
    private void Form_Closed(object? sender, EventArgs e)
    {
        object2.Close();
        object3.Close();
    }
    private async void RunMenuItem_Click(object? sender, EventArgs e)
    {
        using (ParametersDialog parametersDialog = new ParametersDialog())
        {
            if(parametersDialog.ShowDialog() == DialogResult.Yes)
            {
                try
                {
                    string message = BuildMessage(parametersDialog);
                    bool object2Ok = await ExchangeAsync(object2, Object2PipeName, message);
                    if (!object2Ok)
                    {
                        MessageBox.Show("Object2 повернув помилку");
                        return;
                    }
                    bool object3Ok = await ExchangeAsync(object3, Object3PipeName, "READ");
                    if (!object3Ok)
                    {
                        MessageBox.Show("Object3 повернув помилку");
                        return;
                    }
                }
                catch (TimeoutException)
                {
                    MessageBox.Show("Одна з програм не відповіла");
                }
                catch (IOException)
                {
                    MessageBox.Show("Зв'язок з програмою перервано");
                }
            }
        }
    }
    private string BuildMessage(ParametersDialog parametersDialog)
    {
        string countValue = parametersDialog.CountValue.ToString();
        string minValue = parametersDialog.MinValue.ToString(CultureInfo.InvariantCulture);
        string maxValue = parametersDialog.MaxValue.ToString(CultureInfo.InvariantCulture);
        return string.Join(";", countValue, minValue, maxValue);
    }
    private async Task<bool> ExchangeAsync(ChildProgram childProgram, string channelName, string message)
    {
        childProgram.Start();
        string? answer = await PipeSender.AsyncSend(channelName, message);
        return answer == "OK";
    }
}
