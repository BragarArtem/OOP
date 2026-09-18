namespace Lab1;
public static class Work2
{
    public static string? Run()
    {
        ListBoxWindow listBoxWindow = new ListBoxWindow();
        DialogResult result = listBoxWindow.ShowDialog();

        if (result == DialogResult.Yes)
        {
            return listBoxWindow.SelectedGroup;
        } else
        {
            return null;
        }
    }
}