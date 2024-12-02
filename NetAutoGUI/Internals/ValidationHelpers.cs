using System;

namespace NetAutoGUI.Internals
{
    public static class ValidationHelpers
    {
        public static void NotNegative(this int value, string argName)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(argName, "Cannot be negative");
            }
        }

        public static void NotNegative(this double value, string argName)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(argName, "Cannot be negative");
            }
        }

        public static void CheckReturn(this bool retValue, string funcName)
        {
            if (!retValue)
            {
                throw new InvalidOperationException($"{funcName} failed.");
            }
        }
    }
}
