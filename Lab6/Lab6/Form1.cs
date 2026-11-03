namespace Lab6;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }
    private void CreateMenu()
    {
        MenuStrip menuStrip = new MenuStrip();
        ToolStripMenuItem workItem = new ToolStripMenuItem("Робота");
        ToolStripMenuItem launchItem = new ToolStripMenuItem("Запустити");
        Controls.Add(menuStrip);
        MainMenuStrip = menuStrip;
        menuStrip.Items.Add(workItem);
    }
}
