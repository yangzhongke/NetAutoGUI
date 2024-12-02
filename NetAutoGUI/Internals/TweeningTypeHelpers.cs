using MonoGame.Extended.Tweening;
using System;
using System.Reflection;

namespace NetAutoGUI.Internals
{
    internal static class TweeningTypeHelpers
    {
        public static Func<float, float> ToEasingFunction(TweeningType type)
        {
            string name = type.ToString();
            Type typeEasingFunctions = typeof(EasingFunctions);
            MethodInfo methodInfo = typeEasingFunctions.GetMethod(name, BindingFlags.Static | BindingFlags.Public);
            Func<float, float> easingfunction = (Func<float, float>)methodInfo.CreateDelegate(typeof(Func<float, float>));
            return easingfunction;
        }
    }
}
