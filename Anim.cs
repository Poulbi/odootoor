using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Diagnostics;

namespace Odootoor;

public class Frames
{
    public Texture2D atlas;
    public int width;
    public int height;
    public int count;

    public int index;
    // NOTE(luca): timers are used to know when to advance the frame index.
    public int timer;
    public int prevTimer;

    public float speed;
    public int done;

    public Frames(Texture2D _atlas, int _width, int _height, int _count, float _speed)
    {
        atlas = _atlas;
        width = _width;
        height = _height;
        count = _count;
        speed = _speed;
    }

    // Returns true if the index changed
    public static bool UpdateIndex(Frames frames)
    {
        frames.timer = (int)(frames.count * GetTime() * frames.speed);

        bool changed = (frames.timer != frames.prevTimer);
        if (changed)
        {
            frames.index += changed ? 1 : 0;
            Debug.Assert(frames.index <= frames.count);
        }

        if (frames.index == frames.count)
        {
            frames.index -= frames.count;
            frames.done += 1;
        }

        frames.prevTimer = frames.timer;

        return changed;
    }
}

public partial class Program
{
    const bool DEBUGShowBoxes = false;

    static void DrawCharacterWithPunchAnimation(Vector2 charPos, string character, Frames frames)
    {
        if (character.Length == 0) return;

        Font defaultFont = GetFontDefault();

        var fontDim = MeasureTextEx(defaultFont, character, codeFontSize, 0);

        float width = 64 * 1;
        float height = 64 * 1;
        var manPos = new Vector2(charPos.X + (int)(fontDim.X + width / 2), charPos.Y + (int)(fontDim.Y / 2));
        // manual hand offset
        manPos.X -= 16;
        manPos.Y += 3;

        // Flip horizontal
        Rectangle source = new Rectangle(frames.index * frames.width, 0, -1f * frames.width, frames.height);
        Rectangle dest = new Rectangle(manPos.X, manPos.Y, width, height);

        // if (DEBUGShowBoxes)
        // {
        //     DrawRectangle((int)dest.X - frames.width / 2, (int)dest.Y - frames.height / 2, frames.width, frames.height, Color.Green);
        // }
        // if (DEBUGShowBoxes)
        // {
        //     DrawRectangleV(charPos, fontDim, Color.Pink);
        // }

        // DrawText(character, (int)charPos.X, (int)charPos.Y, codeFontSize, Color.Red);
        DrawTexturePro(frames.atlas, source, dest, new Vector2(frames.width / 2, frames.height / 2), 0, Color.White);
    }

}
