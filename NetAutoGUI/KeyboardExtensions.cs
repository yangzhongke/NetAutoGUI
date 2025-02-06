using System.Threading;

namespace NetAutoGUI
{
    public static class KeyboardExtensions
    {
        /// <summary>
        /// Press Ctrl+C.
        /// </summary>
        /// <param name="kb"></param>
        public static void Ctrl_C(this IKeyboardController kb)
        {
            using (kb.Hold(VirtualKeyCode.CONTROL))
            {
                kb.Press(VirtualKeyCode.VK_C);
            }
            Thread.Sleep(100);
        }

        /// <summary>
        /// Press Ctrl+V
        /// </summary>
        /// <param name="kb"></param>
        public static void Ctrl_V(this IKeyboardController kb)
        {
            using (kb.Hold(VirtualKeyCode.CONTROL))
            {
                kb.Press(VirtualKeyCode.VK_V);
            }
            Thread.Sleep(100);
        }

        /// <summary>
        /// Press Ctrl+A
        /// </summary>
        /// <param name="kb"></param>
        public static void Ctrl_A(this IKeyboardController kb)
        {
            using (kb.Hold(VirtualKeyCode.CONTROL))
            {
                kb.Press(VirtualKeyCode.VK_A);
            }
            Thread.Sleep(100);
        }
    }
}
