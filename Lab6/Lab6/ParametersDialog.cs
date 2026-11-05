namespace Lab6;
using System.Globalization;
public class ParametersDialog : Form
{
    private TextBox countBox = new TextBox();
    private TextBox minBox = new TextBox();
    private TextBox maxBox = new TextBox();

    public int CountValue {get; private set;}
    public double MinValue {get; private set;}
    public double MaxValue {get; private set;}
    public ParametersDialog()
    {
        Text = "Vector Parameters";
        ClientSize = new Size(260, 170);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MinimizeBox = false;
        MaximizeBox = false;
        AddRow("n", countBox, 20 );
        AddRow("min", minBox, 55 );
        AddRow("max", maxBox, 90 );
        CreateButtons();
        
    }
    private void AddRow (string caption, TextBox insertBox, int top)
    {
        Label label = new Label();
        label.Text = caption;
        label.Location = new Point(20, top + 3);
        label.Size = new Size(120, 25);
        Controls.Add(label);
        insertBox.Location = new Point(140, top);
        insertBox.Size = new Size(100,25);
        Controls.Add(insertBox);
    }
    private void CreateButtons()
    {
        Button okButton = new Button();
        okButton.Text = "Так";
        okButton.Location = new Point(40,125);
        okButton.Size = new Size(80,30);
        okButton.DialogResult = DialogResult.None;
        okButton.Click += OkButton_Click;
        Controls.Add(okButton);

        Button cancelButton = new Button();
        cancelButton.Text = "Відміна";
        cancelButton.Location = new Point(140,125);
        cancelButton.Size = new Size(80,30);
        cancelButton.DialogResult = DialogResult.Cancel;
        Controls.Add(cancelButton);

        AcceptButton = okButton;
        CancelButton = cancelButton;
    }
    private string? TryReadInput(out int count, out double min, out double max)
    {
        min = 0;
        max = 0;
        if(int.TryParse(countBox.Text, out count) == false || count <= 0)
        {
            return "n must be an integer above zero";
        } 
        else if(!TryParseDouble(minBox.Text, out min))
        {
            return "Min must be a number";
        } 
        else if(!TryParseDouble(maxBox.Text, out max))
        {
            return "Max must be a number";
        } 
        else if(min > max)
        {
            return "min cant be bigger then max";
        }
        else
        {
            return null;
        }
    }
    private void OkButton_Click(object? sender, EventArgs e)
    {
        string? error = TryReadInput(out int count, out double min, out double max);
        if(error != null)
        {
            MessageBox.Show(error);
            return;
        }
        CountValue = count;
        MinValue = min;
        MaxValue = max;
        DialogResult = DialogResult.Yes;
    }
    private bool TryParseDouble(string text, out double number)
    {
        return double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out number); 
    }
}