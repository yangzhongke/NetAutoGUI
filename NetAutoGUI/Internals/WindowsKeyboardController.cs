using InputSimulatorStandard;
using InputSimulatorStandard.Native;
using System;
using System.Threading;
using Vanara.PInvoke;

namespace NetAutoGUI.Internals
{
    internal class WindowsKeyboardController : IKeyboardController
    {
        private IInputSimulator inputSimulator = new InputSimulator();

        public void KeyDown(KeyBoardKey key)
        {
            inputSimulator.Keyboard.KeyDown(ToVirtualKeyCode(key));
        }

        private static VirtualKeyCode ToVirtualKeyCode(KeyBoardKey key)
        {
            int iKey = (int)key;
            return (VirtualKeyCode)iKey;
        }

        public void KeyUp(KeyBoardKey key)
        {
            inputSimulator.Keyboard.KeyUp(ToVirtualKeyCode(key));
        }

        public void Press(params KeyBoardKey[] keys)
        {
            foreach(var key in keys)
            {
                var vKey = ToVirtualKeyCode(key);
                inputSimulator.Keyboard.KeyPress(vKey);
            }
        }


        public void Write(string s)
        {
            inputSimulator.Keyboard.TextEntry(s);
        }

        public void Write(string s, double interval)
        {
            ValidationHelpers.NotNegative(interval,nameof(interval));
            foreach(char c in s)
            {
                inputSimulator.Keyboard.TextEntry(c);
                Thread.Sleep((int)(interval * 1000));
            }
        }

        public IDisposable Hold(KeyBoardKey key)
        {
            return new KeyHoldContext(key, this);
        }

        public void HotKey(params KeyBoardKey[] keys)
        {
            foreach (var key in keys)
            {
                KeyDown(key);
            }
            for(int i=keys.Length-1;i>=0; i--)
            {
                var key = keys[i];
                KeyUp(key);
            }
        }
    }
}
