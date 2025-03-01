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
        }
    }
}
