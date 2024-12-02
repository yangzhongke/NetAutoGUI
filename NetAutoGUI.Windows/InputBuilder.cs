using System;
using System.Collections.Generic;
using static Vanara.PInvoke.User32;

namespace NetAutoGUI.Windows
{
    /// <summary>
    /// Adapted from https://github.com/GregsStack/InputSimulatorStandard
    /// </summary>
    internal class InputBuilder
    {
        private readonly List<INPUT> inputList = new List<INPUT>();

        private static readonly List<VirtualKeyCode> ExtendedKeys = new List<VirtualKeyCode>
        {
            VirtualKeyCode.NUMPAD_RETURN,
            VirtualKeyCode.MENU,
            VirtualKeyCode.RMENU,
            VirtualKeyCode.CONTROL,
            VirtualKeyCode.RCONTROL,
            VirtualKeyCode.INSERT,
            VirtualKeyCode.DELETE,
            VirtualKeyCode.HOME,
            VirtualKeyCode.END,
            VirtualKeyCode.PRIOR,
            VirtualKeyCode.NEXT,
            VirtualKeyCode.RIGHT,
            VirtualKeyCode.UP,
            VirtualKeyCode.LEFT,
            VirtualKeyCode.DOWN,
            VirtualKeyCode.NUMLOCK,
            VirtualKeyCode.CANCEL,
            VirtualKeyCode.SNAPSHOT,
            VirtualKeyCode.DIVIDE
        };


        /// <summary>
        /// Returns the list of <see cref="Input"/> messages as a <see cref="Array"/> of <see cref="Input"/> messages.
        /// </summary>
        /// <returns>The <see cref="System.Array"/> of <see cref="Input"/> messages.</returns>
        public INPUT[] ToArray() => this.inputList.ToArray();

        /// <summary>
        /// Determines if the <see cref="VirtualKeyCode"/> is an ExtendedKey
        /// </summary>
        /// <param name="keyCode">The key code.</param>
        /// <returns>true if the key code is an extended key; otherwise, false.</returns>
        /// <remarks>
        /// The extended keys consist of the ALT and CTRL keys on the right-hand side of the keyboard; the INS, DEL, HOME, END, PAGE UP, PAGE DOWN, and arrow keys in the clusters to the left of the numeric keypad; the NUM LOCK key; the BREAK (CTRL+PAUSE) key; the PRINT SCRN key; and the divide (/) and ENTER keys in the numeric keypad.
        ///
        /// See http://msdn.microsoft.com/en-us/library/ms646267(v=vs.85).aspx Section "Extended-Key Flag"
        /// </remarks>
        public static bool IsExtendedKey(VirtualKeyCode keyCode) => ExtendedKeys.Contains(keyCode);

        /// <summary>
        /// Adds a key down to the list of <see cref="Input"/> messages.
        /// </summary>
        /// <param name="keyCode">The <see cref="VirtualKeyCode"/>.</param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddKeyDown(VirtualKeyCode keyCode)
        {
            var code = (ushort)((int)keyCode & 0xFFFF);
            var down =
            new INPUT
            {
                type = INPUTTYPE.INPUT_KEYBOARD,
                ki = new KEYBDINPUT
                {
                    wVk = code,
                    wScan = (ushort)(MapVirtualKey(code, 0) & 0xFFU),
                    dwFlags = IsExtendedKey(keyCode) ? KEYEVENTF.KEYEVENTF_EXTENDEDKEY : 0,
                    time = 0,
                    dwExtraInfo = IntPtr.Zero
                }
            };

            this.inputList.Add(down);
            return this;
        }

        /// <summary>
        /// Adds a key up to the list of <see cref="Input"/> messages.
        /// </summary>
        /// <param name="keyCode">The <see cref="VirtualKeyCode"/>.</param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddKeyUp(VirtualKeyCode keyCode)
        {
            var code = (ushort)((int)keyCode & 0xFFFF);
            var up =
                new INPUT
                {
                    type = INPUTTYPE.INPUT_KEYBOARD,
                    ki = new KEYBDINPUT
                    {
                        wVk = code,
                        wScan = (ushort)(MapVirtualKey(code, 0) & 0xFFU),
                        dwFlags = IsExtendedKey(keyCode) ? KEYEVENTF.KEYEVENTF_KEYUP | KEYEVENTF.KEYEVENTF_EXTENDEDKEY
                                                                  : KEYEVENTF.KEYEVENTF_KEYUP,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                };

            this.inputList.Add(up);
            return this;
        }

        /// <summary>
        /// Adds a key press to the list of <see cref="Input"/> messages which is equivalent to a key down followed by a key up.
        /// </summary>
        /// <param name="keyCode">The <see cref="VirtualKeyCode"/>.</param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddKeyPress(VirtualKeyCode keyCode)
        {
            this.AddKeyDown(keyCode);
            this.AddKeyUp(keyCode);
            return this;
        }

        /// <summary>
        /// Adds the character to the list of <see cref="Input"/> messages.
        /// </summary>
        /// <param name="character">The <see cref="System.Char"/> to be added to the list of <see cref="Input"/> messages.</param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddCharacter(char character)
        {
            ushort scanCode = character;

            KEYEVENTF flagDown = KEYEVENTF.KEYEVENTF_UNICODE;
            KEYEVENTF flagUp = KEYEVENTF.KEYEVENTF_KEYUP | KEYEVENTF.KEYEVENTF_UNICODE;
            // Handle extended keys:
            // If the scan code is preceded by a prefix byte that has the value 0xE0 (224),
            // we need to include the KEYEVENTF_EXTENDEDKEY flag in the Flags property.
            if ((scanCode & 0xFF00) == 0xE000)
            {
                flagDown |= KEYEVENTF.KEYEVENTF_EXTENDEDKEY;
                flagUp |= KEYEVENTF.KEYEVENTF_EXTENDEDKEY;
            }
            var down = new INPUT
            {
                type = INPUTTYPE.INPUT_KEYBOARD,
                ki = new KEYBDINPUT
                {
                    wVk = 0,
                    wScan = scanCode,
                    dwFlags = flagDown,
                    time = 0,
                    dwExtraInfo = IntPtr.Zero
                }
            };

            var up = new INPUT
            {
                type = INPUTTYPE.INPUT_KEYBOARD,
                ki = new KEYBDINPUT
                {
                    wVk = 0,
                    wScan = scanCode,
                    dwFlags = flagUp,
                    time = 0,
                    dwExtraInfo = IntPtr.Zero
                }
            };

            this.inputList.Add(down);
            this.inputList.Add(up);
            return this;
        }

        /// <summary>
        /// Adds all of the characters in the specified <see cref="IEnumerable{T}"/> of <see cref="char"/>.
        /// </summary>
        /// <param name="characters">The characters to add.</param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddCharacters(IEnumerable<char> characters)
        {
            foreach (var character in characters)
            {
                this.AddCharacter(character);
            }
            return this;
        }

        /// <summary>
        /// Adds the characters in the specified <see cref="string"/>.
        /// </summary>
        /// <param name="characters">The string of <see cref="char"/> to add.</param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddCharacters(string characters) => this.AddCharacters(characters.ToCharArray());

        /// <summary>
        /// Moves the mouse relative to its current position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddRelativeMouseMovement(int x, int y)
        {
            MOUSEINPUT mi = new MOUSEINPUT
            {
                dwFlags = MOUSEEVENTF.MOUSEEVENTF_MOVE,
                dx = x,
                dy = y
            };
            var movement = new INPUT { type = INPUTTYPE.INPUT_MOUSE, mi = mi };
            this.inputList.Add(movement);
            return this;
        }

        /// <summary>
        /// Move the mouse to an absolute position.
        /// </summary>
        /// <param name="absoluteX"></param>
        /// <param name="absoluteY"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddAbsoluteMouseMovement(int absoluteX, int absoluteY)
        {
            MOUSEINPUT mi = new MOUSEINPUT
            {
                dwFlags = MOUSEEVENTF.MOUSEEVENTF_MOVE | MOUSEEVENTF.MOUSEEVENTF_ABSOLUTE,
                dx = (absoluteX * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN),
                dy = (absoluteY * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN),
            };
            var movement = new INPUT { type = INPUTTYPE.INPUT_MOUSE, mi = mi };
            this.inputList.Add(movement);
            return this;
        }

        /// <summary>
        /// Move the mouse to the absolute position on the virtual desktop.
        /// </summary>
        /// <param name="absoluteX"></param>
        /// <param name="absoluteY"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddAbsoluteMouseMovementOnVirtualDesktop(int absoluteX, int absoluteY)
        {
            MOUSEINPUT mi = new MOUSEINPUT
            {
                dwFlags = MOUSEEVENTF.MOUSEEVENTF_MOVE | MOUSEEVENTF.MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF.MOUSEEVENTF_VIRTUALDESK,
                dx = (absoluteX * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN),
                dy = (absoluteY * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN),
            };
            var movement = new INPUT { type = INPUTTYPE.INPUT_MOUSE, mi = mi };
            this.inputList.Add(movement);
            return this;
        }

        /// <summary>
        /// Adds a mouse button down for the specified button.
        /// </summary>
        /// <param name="button"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseButtonDown(MouseButtonType button)
        {
            var flags = ToMouseButtonDownFlag(button);
            var buttonDown = new INPUT { type = INPUTTYPE.INPUT_MOUSE, mi = new MOUSEINPUT { dwFlags = flags } };
            this.inputList.Add(buttonDown);

            return this;
        }

        /// <summary>
        /// Adds a mouse button down for the specified button.
        /// </summary>
        /// <param name="xButtonId"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseXButtonDown(int xButtonId)
        {
            var buttonDown = new INPUT
            {
                type = INPUTTYPE.INPUT_MOUSE,
                mi = new MOUSEINPUT
                {
                    dwFlags = MOUSEEVENTF.MOUSEEVENTF_XDOWN,
                    mouseData = xButtonId
                }
            };
            this.inputList.Add(buttonDown);

            return this;
        }

        /// <summary>
        /// Adds a mouse button up for the specified button.
        /// </summary>
        /// <param name="button"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseButtonUp(MouseButtonType button)
        {
            var flags = ToMouseButtonUpFlag(button);
            var buttonUp = new INPUT
            {
                type = INPUTTYPE.INPUT_MOUSE,
                mi = new MOUSEINPUT
                {
                    dwFlags = flags
                }
            };
            this.inputList.Add(buttonUp);
            return this;
        }

        /// <summary>
        /// Adds a mouse button up for the specified button.
        /// </summary>
        /// <param name="xButtonId"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseXButtonUp(int xButtonId)
        {

            MOUSEINPUT mi = new MOUSEINPUT
            {
                dwFlags = MOUSEEVENTF.MOUSEEVENTF_XUP,
                mouseData = xButtonId,
            };
            var buttonUp = new INPUT { type = INPUTTYPE.INPUT_MOUSE, mi = mi };
            this.inputList.Add(buttonUp);
            return this;
        }

        /// <summary>
        /// Adds a single click of the specified button.
        /// </summary>
        /// <param name="button"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseButtonClick(MouseButtonType button) => this.AddMouseButtonDown(button).AddMouseButtonUp(button);

        /// <summary>
        /// Adds a single click of the specified button.
        /// </summary>
        /// <param name="xButtonId"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseXButtonClick(int xButtonId) => this.AddMouseXButtonDown(xButtonId).AddMouseXButtonUp(xButtonId);

        /// <summary>
        /// Adds a double click of the specified button.
        /// </summary>
        /// <param name="button"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseButtonDoubleClick(MouseButtonType button) => this.AddMouseButtonClick(button).AddMouseButtonClick(button);

        /// <summary>
        /// Adds a double click of the specified button.
        /// </summary>
        /// <param name="xButtonId"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseXButtonDoubleClick(int xButtonId) => this.AddMouseXButtonClick(xButtonId).AddMouseXButtonClick(xButtonId);

        /// <summary>
        /// Scroll the vertical mouse wheel by the specified amount.
        /// </summary>
        /// <param name="scrollAmount"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseVerticalWheelScroll(int scrollAmount)
        {
            MOUSEINPUT mi = new MOUSEINPUT
            {
                dwFlags = MOUSEEVENTF.MOUSEEVENTF_WHEEL,
                mouseData = scrollAmount,
            };
            var scroll = new INPUT { type = INPUTTYPE.INPUT_MOUSE, mi = mi };
            this.inputList.Add(scroll);
            return this;
        }

        /// <summary>
        /// Scroll the horizontal mouse wheel by the specified amount.
        /// </summary>
        /// <param name="scrollAmount"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseHorizontalWheelScroll(int scrollAmount)
        {
            MOUSEINPUT mi = new MOUSEINPUT
            {
                dwFlags = MOUSEEVENTF.MOUSEEVENTF_HWHEEL,
                mouseData = scrollAmount,
            };
            var scroll = new INPUT { type = INPUTTYPE.INPUT_MOUSE, mi = mi };
            this.inputList.Add(scroll);
            return this;
        }

        private static MOUSEEVENTF ToMouseButtonDownFlag(MouseButtonType button)
        {
            switch (button)
            {
                case MouseButtonType.Left:
                    return MOUSEEVENTF.MOUSEEVENTF_LEFTDOWN;

                case MouseButtonType.Middle:
                    return MOUSEEVENTF.MOUSEEVENTF_MIDDLEDOWN;

                case MouseButtonType.Right:
                    return MOUSEEVENTF.MOUSEEVENTF_RIGHTDOWN;

                default:
                    throw new ArgumentException(nameof(button));
            }
        }

        private static MOUSEEVENTF ToMouseButtonUpFlag(MouseButtonType button)
        {
            switch (button)
            {
                case MouseButtonType.Left:
                    return MOUSEEVENTF.MOUSEEVENTF_LEFTUP;

                case MouseButtonType.Middle:
                    return MOUSEEVENTF.MOUSEEVENTF_MIDDLEUP;

                case MouseButtonType.Right:
                    return MOUSEEVENTF.MOUSEEVENTF_RIGHTUP;

                default:
                    throw new ArgumentException(nameof(button));
            }
        }
    }
}
