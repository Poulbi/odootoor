namespace RaylibConsole;
using System.Diagnostics;
using Raylib_cs;

public class ConsoleWindow
{
    private Process proc;
    private List<string> buffer = new List<string>();
    private const int MaxLines = 200;

    public void Init()
    {
        proc = new Process();
        proc.StartInfo.FileName = "/bin/bash";
        proc.StartInfo.Arguments = "-c \"ping -c 20 google.com\"";
        proc.StartInfo.RedirectStandardOutput = true;
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.CreateNoWindow = true;

        proc.Start();

        Task.Run(() =>
        {
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                lock (buffer)
                {
                    buffer.Add(line);
                    if (buffer.Count > MaxLines)
                        buffer.RemoveAt(0);
                }
            }
        });
    }
    public void Draw()
    {
        int y = 10;
        lock (buffer)
        {
                
            foreach (var line in buffer)
            {
                Raylib.DrawText(line, 10, y, 14, Color.White);
                y += 18;
            }
        }
    }
    public void Stop()
    {
        if (proc != null && !proc.HasExited)
            proc.Kill();
    }
}
