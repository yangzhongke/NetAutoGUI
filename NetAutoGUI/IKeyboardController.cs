using System;

namespace NetAutoGUI
{
    public interface IKeyboardController
    {
        public void Write(string s);
        public void Write(string s, double interval);
        public void Press(params KeyBoardKey[] keys);
        public void KeyDown(KeyBoardKey key);
        public void KeyUp(KeyBoardKey key);

        /// <summary>
        /// pressed down in order, and then released in reverse order
        /// </summary>
        /// <param name="keys"></param>
        public void HotKey(params KeyBoardKey[] keys);
        public IDisposable Hold(KeyBoardKey key);
    }
}
