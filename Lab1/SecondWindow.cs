namespace Lab1;
public class SecondWindow : Form
{
    public SecondWindow()
    {
        ClientSize = new Size(800, 400);
        Button yes = new Button();
        yes.Text = "Yes";
        yes.Location = new Point(100, 100);
        yes.Size = new Size(80,30);
        Controls.Add(yes);
        yes.DialogResult = DialogResult.Yes;

        Button cancel = new Button();
        cancel.Text = "cancel";
        cancel.Location = new Point(200, 100);
        cancel.Size = new Size(80,30);
        Controls.Add(cancel);
        cancel.DialogResult = DialogResult.Cancel;

        Button back = new Button();
        back.Text = "back";
        back.Location = new Point(300, 100);
        back.Size = new Size(80,30);
        Controls.Add(back);
        back.DialogResult = DialogResult.Retry;
    }
}
