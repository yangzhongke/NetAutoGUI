using System.Threading;

namespace NetAutoGUI
{
    public static class KeyboardExtensions
    {
        public static void Ctrl_C(this IKeyboardController kb)
        {
            using (kb.Hold(VirtualKeyCode.CONTROL))
            {
                kb.Press(VirtualKeyCode.VK_C);
            }
            Thread.Sleep(100);
        }

        public static void Ctrl_V(this IKeyboardController kb)
        {
            using (kb.Hold(VirtualKeyCode.CONTROL))
            {
                kb.Press(VirtualKeyCode.VK_V);
            }
            Thread.Sleep(100);
        }

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
