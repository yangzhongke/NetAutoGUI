using System;

namespace NetAutoGUI
{
	public interface IKeyboardController
	{
		public void Write(char c);
		public void Write(string s);
		public void Write(string s, double interval);
		public void Press(params VirtualKeyCode[] keys);
		public void KeyDown(VirtualKeyCode key);
		public void KeyUp(VirtualKeyCode key);

		/// <summary>
		/// pressed down in order, and then released in reverse order
		/// </summary>
		/// <param name="keys"></param>
		public void HotKey(params VirtualKeyCode[] keys);
		public IDisposable Hold(VirtualKeyCode key);
	}
}
