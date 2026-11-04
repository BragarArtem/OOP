namespace Lab6;
using System.IO.Pipes;
using System.Globalization;

public partial class Form1 : Form
{
    private const string PipeName = "Lab6.Object2";
    private ChildProgram childProgram;
    public Form1()
    {
        InitializeComponent();
        CreateMenu();
        childProgram = new ChildProgram("Object2");
    }
    private void CreateMenu()
    {
        MenuStrip menuStrip = new MenuStrip();
        ToolStripMenuItem workMenuItem = new ToolStripMenuItem("Робота");
        ToolStripMenuItem runMenuItem = new ToolStripMenuItem("Запустити");
        ToolStripMenuItem testMenuItem = new ToolStripMenuItem("Тест");
        Controls.Add(menuStrip);
        MainMenuStrip = menuStrip;
        menuStrip.Items.Add(workMenuItem);
        workMenuItem.DropDownItems.Add(runMenuItem);
        workMenuItem.DropDownItems.Add(testMenuItem);
        runMenuItem.Click += RunMenuItem_Click;
    }
    private void RunMenuItem_Click(object? sender, EventArgs e)
    {
        using (ParametersDialog parametersDialog = new ParametersDialog())
        {
            if(parametersDialog.ShowDialog() == DialogResult.Yes)
            {
                try
                {
                    childProgram.Start();
                    string result = BuildMessage(parametersDialog);
                    PipeSender.Send(PipeName, result);
                }
                catch (TimeoutException)
                {
                    MessageBox.Show("Client is not responding");
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
}
