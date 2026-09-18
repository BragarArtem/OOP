namespace Lab1;
public class FirstWindow : Form
{
    public FirstWindow()
    {
        ClientSize = new Size(800, 400);
        
        Button next = new Button();
        next.Text = "next";
        next.Location = new Point(100, 100);
        next.Size = new Size(80,30);
        Controls.Add(next);
        next.DialogResult = DialogResult.OK;

        Button cancel = new Button();
        cancel.Text = "cancel";
        cancel.Location = new Point(200, 100);
        cancel.Size = new Size(80,30);
        Controls.Add(cancel);
        cancel.DialogResult = DialogResult.Cancel;
    }
}
