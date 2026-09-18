namespace Lab1;
public class ListBoxWindow : Form
{
    public string? SelectedGroup {get; private set;}
    public ListBoxWindow()
    {
        ClientSize = new Size(800, 400);
        
        Button yes = new Button();
        yes.Text = "yes";
        yes.Location = new Point(100, 200);
        yes.Size = new Size(80,30);
        Controls.Add(yes);
        yes.DialogResult = DialogResult.None;

        Button cancel = new Button();
        cancel.Text = "cancel";
        cancel.Location = new Point(200, 200);
        cancel.Size = new Size(80,30);
        Controls.Add(cancel);
        cancel.DialogResult = DialogResult.Cancel;

        ListBox groupList = new ListBox();
        groupList.Size = new Size(100, 100);
        groupList.Location = new Point(100, 50);
        Controls.Add(groupList);
        groupList.Items.AddRange(new string[] {"IM-51", "IM-52", "IM-53", "IM-54", "IM-55", "IM-o51", });

        yes.Click += (sender, e) =>
        {
            if(groupList.SelectedItem == null)
            {
                MessageBox.Show("Choose the GROUP from the list");
            }
            else
            {
                SelectedGroup = (string)groupList.SelectedItem;
                this.DialogResult = DialogResult.Yes;
            }
        };
    }
}