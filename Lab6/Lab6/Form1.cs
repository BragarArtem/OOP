namespace Lab6;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        CreateMenu();
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
    private void RunMenuItem_Click(object? sender, EventArgs e)
    {
        using (ParametersDialog parametersDialog = new ParametersDialog())
        {
            if(parametersDialog.ShowDialog() == DialogResult.Yes)
            {
                MessageBox.Show($"{parametersDialog.CountValue}, {parametersDialog.MinValue}, {parametersDialog.MaxValue}");
            }
        }
    }
}
