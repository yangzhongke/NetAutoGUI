using MonoGame.Extended.Tweening;
using System.Diagnostics;
using System.Numerics;
using System.Threading;

namespace NetAutoGUI.Internals
{
    public abstract class AbstractMouseController : IMouseController
    {
        private readonly double MINIMUM_DURATION = 0.1;//If the duration is less than MINIMUM_DURATION the movement will be instant. 

        public abstract void Click(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left, int clicks = 1, double interval = 0);

        public void DoubleClick(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left, double interval = 0)
        {
            Click(x, y, button, 2, interval);
        }

        public abstract void MouseDown(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left);

        public abstract void MouseUp(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left);

        public void Move(int offsetX, int offsetY)
        {
            (int x, int y) = Position();
            MoveTo(x + offsetX, y + offsetY);
        }

        public abstract void MoveTo(int x, int y);

        public void MoveTo(int x, int y, double durationInSeconds, TweeningType tweeningType = TweeningType.Linear)
        {
            durationInSeconds.NotNegative(nameof(durationInSeconds));
            if (durationInSeconds <= MINIMUM_DURATION)
            {
                MoveTo(x, y);
                return;
            }

            Vector2TweeningObject tweenObject = new Vector2TweeningObject();
            tweenObject.CurrentValue = Position();
            tweenObject.ToValue = new Vector2(x, y);

            using Tweener tweener = new Tweener();
            tweener.TweenTo(target: tweenObject, expression: _ => tweenObject.CurrentValue,
                toValue: tweenObject.ToValue,
                duration: (float)durationInSeconds * 1000, delay: 0)
                .Easing(TweeningTypeHelpers.ToEasingFunction(tweeningType));
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            long lastMS = sw.ElapsedMilliseconds;
            while (sw.ElapsedMilliseconds <= durationInSeconds * 1000)
            {
                tweener.Update(sw.ElapsedMilliseconds - lastMS);
                lastMS = sw.ElapsedMilliseconds;
                MoveTo((int)tweenObject.CurrentValue.X, (int)tweenObject.CurrentValue.Y);
                Thread.Sleep(10);
            }
        }

        public bool OnScreen(int x, int y)
        {
            (int width, int height) = Size();
            return x >= 0 && x <= width && y >= 0 && y <= height;
        }

        public abstract Location Position();

        public abstract void Scroll(int value);

        public abstract Size Size();
    }

    class Vector2TweeningObject
    {
        public Vector2 CurrentValue { get; set; }
        public Vector2 ToValue { get; set; }
    }
}
