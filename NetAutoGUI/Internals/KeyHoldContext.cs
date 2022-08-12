using System;

namespace NetAutoGUI.Internals
{
    public class KeyHoldContext : IDisposable
    {
        private KeyBoardKey holdedKey;
        private IKeyboardController keyboardController;

        public KeyHoldContext(KeyBoardKey holdedKey, IKeyboardController keyboardController)
        {
            this.holdedKey = holdedKey;
            this.keyboardController = keyboardController;
            keyboardController.KeyDown(holdedKey);
        }

        public void Dispose()
        {
            keyboardController.KeyUp(this.holdedKey);
        }
    }
}
