namespace Lab6;
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
        Controls.Add(menuStrip);
        MainMenuStrip = menuStrip;
        menuStrip.Items.Add(workMenuItem);
        workMenuItem.DropDownItems.Add(runMenuItem);
        runMenuItem.Click += RunMenuItem_Click;
    }
    private async void RunMenuItem_Click(object? sender, EventArgs e)
    {
        using (ParametersDialog parametersDialog = new ParametersDialog())
        {
            if(parametersDialog.ShowDialog() == DialogResult.Yes)
            {
                try
                {
                    childProgram.Start();
                    string result = BuildMessage(parametersDialog);
                    string? answer = await PipeSender.Send(PipeName, result);
                }
                catch (TimeoutException)
                {
                    MessageBox.Show("Object2 не відповідає");
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
