using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetAutoGUI
{
    /// <summary>
    /// Keyboard controller, used for simulating keyboard events
    /// </summary>
    public interface IKeyboardController
    {
        /// <summary>
        /// Write a character from keyboard
        /// </summary>
        /// <param name="c">the character</param>
        public void Write(char c);

        /// <summary>
        /// Write a string from keyboard
        /// </summary>
        /// <param name="s">the string</param>
        public void Write(string s);

        /// <summary>
        /// Write a string from keyboard, wait a specific interval between each character
        /// </summary>
        /// <param name="s">the string</param>
        /// <param name="intervalInSeconds">interval of wait in seconds</param>
        public void Write(string s, double intervalInSeconds);

        /// <summary>
        /// Write a string from keyboard, wait a specific interval between each character
        /// </summary>
        /// <param name="s">the string</param>
        /// <param name="intervalInSeconds">interval of wait in seconds</param>
        /// <param name="cancellationToken">cancellationToken</param>
        public Task WriteAsync(string s, double intervalInSeconds, CancellationToken cancellationToken = default);

        /// <summary>
        /// Press a keys combination
        /// </summary>
        /// <param name="keys">keys</param>
        public void Press(params VirtualKeyCode[] keys);

        /// <summary>
        /// Press down a key
        /// </summary>
        /// <param name="key">key</param>
        public void KeyDown(VirtualKeyCode key);

        /// <summary>
        /// Press up a key
        /// </summary>
        /// <param name="key">key</param>
        public void KeyUp(VirtualKeyCode key);

        /// <summary>
        /// pressed down keys in order, and then released in reverse order
        /// </summary>
        /// <param name="keys"></param>
        public void HotKey(params VirtualKeyCode[] keys);

        /// <summary>
        /// Press down a key until the return the Dispose() method of the returned IDisposable is invoked.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IDisposable Hold(VirtualKeyCode key);
    }
}
