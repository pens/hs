using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Omicron
{
    public static class InputManager
    {
        static KeyboardState newKeys;
        static KeyboardState oldKeys;
        static GamePadState newPad;
        static GamePadState oldPad;
        static MouseState newMouse;
        static MouseState oldMouse;
        static Vector2 mousePos;

        public static bool ButtonDown(Buttons button, bool justPressed)
        {
            if (newPad.IsButtonDown(button))
            {
                if (justPressed)
                {
                    if (oldPad.IsButtonDown(button))
                        return false;
                    else return true;
                }
                else return true;
            }
            else return false;
        }
        public static bool ButtonUp(Buttons button, bool justReleased)
        {
            if (newPad.IsButtonUp(button))
            {
                if (justReleased)
                {
                    if (oldPad.IsButtonUp(button))
                        return false;
                    else return true;
                }
                else return true;
            }
            else return false;
        }
        public static float TriggerPosR()
        {
            return newPad.Triggers.Right;
        }
        public static float TriggerPosL()
        {
            return newPad.Triggers.Left;
        }
        public static Vector2 StickPosR()
        {
            return newPad.ThumbSticks.Right;
        }
        public static Vector2 StickPosL()
        {
            return newPad.ThumbSticks.Left;
        }

        public static bool KeyDown(Keys key, bool justPressed)
        {
            if (newKeys.IsKeyDown(key))
            {
                if (justPressed)
                {
                    if (oldKeys.IsKeyDown(key))
                        return false;
                    else return true;
                }
                else return true;
            }
            else return false;
        }
        public static bool KeyUp(Keys key, bool justReleased)
        {
            if (newKeys.IsKeyUp(key))
            {
                if (justReleased)
                {
                    if (oldKeys.IsKeyUp(key))
                        return false;
                    else return true;
                }
                else return true;
            }
            else return false;
        }
        public static bool MouseRClicked(bool justClicked)
        {
            if (newMouse.RightButton == ButtonState.Pressed)
            {
                if (justClicked)
                {
                    if (oldMouse.RightButton == ButtonState.Pressed)
                        return false;
                    else return true;
                }
                else return true;
            }
            else return false;
        }
        public static bool MouseMClicked(bool justClicked)
        {
            if (newMouse.MiddleButton == ButtonState.Pressed)
            {
                if (justClicked)
                {
                    if (oldMouse.MiddleButton == ButtonState.Pressed)
                        return false;
                    else return true;
                }
                else return true;
            }
            else return false;
        }
        public static bool MouseLClicked(bool justClicked)
        {
            if (newMouse.LeftButton == ButtonState.Pressed)
            {
                if (justClicked)
                {
                    if (oldMouse.LeftButton == ButtonState.Pressed)
                        return false;
                    else return true;
                }
                else return true;
            }
            else return false;
        }
        public static Vector2 MousePos()
        {
            mousePos.X = newMouse.X;
            mousePos.Y = newMouse.Y;
            return mousePos;
        }

        public static void Update()
        {
            oldKeys = newKeys;
            newKeys = Keyboard.GetState();
            oldPad = newPad;
            newPad = GamePad.GetState(PlayerIndex.One);
            oldMouse = newMouse;
            newMouse = Mouse.GetState();
        }
    }
}
