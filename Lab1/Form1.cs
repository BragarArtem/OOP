namespace Lab1;

public partial class Form1 : Form
{
    private Label resultLabel = new Label();
    public Form1()
    {
        InitializeComponent();
        Text = "Lab1";

        Controls.Add(resultLabel);
        resultLabel.Location = new Point(100, 100);
        resultLabel.Size = new Size(500, 100);

        MenuStrip menuStrip = new MenuStrip();
        ToolStripMenuItem actionsMenu = new ToolStripMenuItem("Actions");
        ToolStripMenuItem work1Item = new ToolStripMenuItem("Work1");
        ToolStripMenuItem work2Item = new ToolStripMenuItem("Work2");

        Controls.Add(menuStrip);
        MainMenuStrip = menuStrip;
        menuStrip.Items.Add(actionsMenu);
        actionsMenu.DropDownItems.Add(work1Item);
        actionsMenu.DropDownItems.Add(work2Item);
        
        work1Item.Click += (sender, e) => 
        {  
            Work1.Run();
        };
        work2Item.Click += (sender, e) =>
        {
            string? selected = Work2.Run();
            if(selected != null)
            {
                resultLabel.Text = selected;
            }
        };
    }
}