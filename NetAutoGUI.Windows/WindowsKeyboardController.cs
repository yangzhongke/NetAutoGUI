using NetAutoGUI.Internals;
using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading;
using Vanara.PInvoke;

namespace NetAutoGUI.Windows
{
	[SupportedOSPlatform("windows")]
	internal class WindowsKeyboardController : IKeyboardController
	{
		public void KeyDown(VirtualKeyCode key)
		{
			var inputBuilder = new InputBuilder().AddKeyDown(key);
			SendInputs(inputBuilder);
		}

		public void KeyUp(VirtualKeyCode key)
		{
			var inputBuilder = new InputBuilder().AddKeyUp(key);
			SendInputs(inputBuilder);
		}

		public void Press(params VirtualKeyCode[] keys)
		{
			var inputBuilder = new InputBuilder();
			foreach (var key in keys)
			{
				inputBuilder.AddKeyPress(key);
			}
			SendInputs(inputBuilder);
		}

		public void Write(char c)
		{
			var inputBuilder = new InputBuilder().AddCharacter(c);
			SendInputs(inputBuilder);
		}

		public void Write(string s)
		{
			var inputBuilder = new InputBuilder().AddCharacters(s);
			SendInputs(inputBuilder);
		}

		public void Write(string s, double interval)
		{
			ValidationHelpers.NotNegative(interval, nameof(interval));
			foreach (char c in s)
			{
				Write(c);
				Thread.Sleep((int)(interval * 1000));
			}
		}

		public IDisposable Hold(VirtualKeyCode key)
		{
			return new KeyHoldContext(key, this);
		}

		public void HotKey(params VirtualKeyCode[] keys)
		{
			foreach (var key in keys)
			{
				KeyDown(key);
			}
			for (int i = keys.Length - 1; i >= 0; i--)
			{
				var key = keys[i];
				KeyUp(key);
			}
		}

		private void SendInputs(InputBuilder inputBuilder)
		{
			var inputs = inputBuilder.ToArray();
			var ret = User32.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(User32.INPUT)));
			if (ret != inputs.Length)
			{
				throw new Exception("Some simulated input commands were not sent successfully. The most common reason for this happening are the security features of Windows including User Interface Privacy Isolation (UIPI). Your application can only send commands to applications of the same or lower elevation. Similarly certain commands are restricted to Accessibility/UIAutomation applications. Refer to the project home page and the code samples for more information.");
			}
		}
	}
}
