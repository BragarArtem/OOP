namespace Lab1;
public static class Work1
{
    enum DialogFlowState
    {
        MainMenuWindow,
        FirstDialogWindow,
        SecondDialogWindow
    }
    public static void Run()
    {
        DialogFlowState state = DialogFlowState.FirstDialogWindow;
        while(state != DialogFlowState.MainMenuWindow)
            {
                switch (state)
                {
                    case DialogFlowState.FirstDialogWindow:
                    FirstWindow dialog1 = new FirstWindow();
                    DialogResult result1 = dialog1.ShowDialog();
                        switch (result1)
                        {
                            case DialogResult.OK:
                            state = DialogFlowState.SecondDialogWindow;
                            break;

                            case DialogResult.Cancel:
                            state = DialogFlowState.MainMenuWindow;
                            break;
                        }
                    break;
                    
                    case DialogFlowState.SecondDialogWindow:
                    SecondWindow dialog2 = new SecondWindow();
                    DialogResult result2 = dialog2.ShowDialog();
                    switch (result2)
                        {
                            case DialogResult.Yes:
                            state = DialogFlowState.MainMenuWindow;
                            break;

                            case DialogResult.Retry:
                            state = DialogFlowState.FirstDialogWindow;
                            break;

                            case DialogResult.Cancel:
                            state = DialogFlowState.MainMenuWindow;
                            break;
                        }
                    break;

                }
            }
    }
}