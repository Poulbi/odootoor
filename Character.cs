namespace Odootoor;

using Raylib_cs;
using static Raylib_cs.Raylib
using System.Numerics;

public partial class Program
{

    public class CharacterElement
    {
        public Vector2 current_position; 
        private KeyboardKey key;

        CharacterElement()
        {

        }

        public bool drop()
        {
            Random num = new();
            if (num.Next(1, 10) < 6)
            {
                return true
            }
            else return false;
            }

        }

        public KeyboardKey _key 
        {
            set {key = value;}
        }

        public static void SelectCharacterDrop(int key, int cursorPos)
        {
            if (pressedChar)
            {
                DrawTextEx()    
            }
        }


        public string toString()
        {
            return ((char)key).ToString();
        }

    }

}
