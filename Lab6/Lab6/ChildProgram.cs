namespace Lab6;
using System.Diagnostics;
public class ChildProgram
{
    private readonly string programName;
    private Process? process = null;
    public ChildProgram(string name)
    {
        programName = name;
    }
    public void Start()
    {
        if(process != null && !process.HasExited){return;}
        Process[] runningProcesses = Process.GetProcessesByName(programName);
        for(int i = 1; i < runningProcesses.Length; i++)
        {
            runningProcesses[i].Dispose();
        }
        if(runningProcesses.Length > 0)
        {
            process = runningProcesses[0];
            return;
        }
        string exePath = Path.Combine(AppContext.BaseDirectory, programName + ".exe");
        process = Process.Start(exePath);
        if(process != null){process.WaitForInputIdle();};
    }
    public void Close()
    {
        if(process != null && !process.HasExited)
        {
            process.Kill();
            process.Dispose();
            process = null;
        }
    }
}