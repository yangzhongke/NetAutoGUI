using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace NetAutoGUI.Internals;

public static class TimeBoundWaiter
{
    public static T WaitForNotNull<T>(Func<T?> func, double timeoutSeconds, string errorMessageWhenTimeout)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        T? result;
        while ((result = func()) == null)
        {
            if (stopwatch.ElapsedMilliseconds > timeoutSeconds * 1000)
            {
                throw new TimeoutException(errorMessageWhenTimeout);
            }

            GUI.Pause(0.05);
        }

        return result;
    }

    public static async Task<T> WaitForNotNullAsync<T>(Func<T?> func, double timeoutSeconds,
        string errorMessageWhenTimeout,
        CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        T? result;
        while ((result = func()) == null)
        {
            if (stopwatch.ElapsedMilliseconds > timeoutSeconds * 1000)
            {
                throw new TimeoutException(errorMessageWhenTimeout);
            }

            await Task.Delay(50, cancellationToken);
        }

        return result;
    }
}