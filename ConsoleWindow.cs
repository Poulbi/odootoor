namespace RaylibConsole;
using System.Diagnostics;
using Raylib_cs;
using System.Numerics;

class ConsoleWindow
{
    public bool IsVisible { get; set; }
    public Rectangle Bounds { get; set; }
    public float ScrollOffset { get; set; }
    public Output output;
    
    public ConsoleWindow()
    {
        output = new Output();
        output.Init();
        
        IsVisible = false;
        Bounds = new Rectangle(200, 100, 800, 500);
    }
    
    public void HandleScroll(Vector2 mousePos)
    {
        if (IsVisible && Raylib.CheckCollisionPointRec(mousePos, Bounds))
        {
            float mouseWheel = Raylib.GetMouseWheelMove();
            ScrollOffset -= mouseWheel * 20;
            ScrollOffset = Math.Clamp(ScrollOffset, 0, Math.Max(0, CountLines() * 20 - Bounds.Height + 40));
        }
    }
    
    private int CountLines()
    {
        // lock (output.buffer)
        // {
        //     return output.buffer.Count;
        // }
        return 0;
    }
    
    public void Draw()
    {
        if (!IsVisible) return;
        
        // Window background with border
        Raylib.DrawRectangleRec(Bounds, new Color(20, 20, 30, 255));
        Raylib.DrawRectangleLines((int)Bounds.X, (int)Bounds.Y, (int)Bounds.Width, (int)Bounds.Height, new Color(80, 80, 120, 255));
        Raylib.DrawRectangleLines((int)Bounds.X - 1, (int)Bounds.Y - 1, (int)Bounds.Width + 2, (int)Bounds.Height + 2, new Color(120, 120, 160, 255));
        
        // Title bar
        Raylib.DrawRectangle((int)Bounds.X, (int)Bounds.Y, (int)Bounds.Width, 30, new Color(40, 40, 60, 255));
        Raylib.DrawText("PROGRAM OUTPUT", (int)Bounds.X + 10, (int)Bounds.Y + 5, 20, Color.Gold);
        
        // Close button
        Rectangle closeButton = new Rectangle(Bounds.X + Bounds.Width - 35, Bounds.Y + 5, 20, 20);
        Color closeColor = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), closeButton) ? Color.Red : new Color(200, 100, 100, 255);
        Raylib.DrawRectangleRec(closeButton, closeColor);
        Raylib.DrawText("X", (int)closeButton.X + 6, (int)closeButton.Y + 2, 16, Color.White);
        
        // Output content
        output.Draw(Bounds);
    }
    
    public bool CloseButtonClicked()
    {
        output.Stop();
        if (!IsVisible) return false;
        
        Rectangle closeButton = new Rectangle(Bounds.X + Bounds.Width - 35, Bounds.Y + 5, 20, 20);
        return Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), closeButton) && Raylib.IsMouseButtonPressed(MouseButton.Left);
    }
}

public class Output 
{
    private Process proc;
    private List<string> buffer {get; set;} 
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
    public void Draw(Rectangle bounds)
    {
        int y = (int)bounds.Y + 40;
        
        lock (buffer)
        {
            foreach (var line in buffer)
            {
                Raylib.DrawText(line, (int)bounds.X + 10, y, 14, Color.White);
                y += 20;
            }
        }
    }
    public void Stop()
    {
        if (proc != null && !proc.HasExited)
            proc.Kill();
    }
}