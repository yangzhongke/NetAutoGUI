using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetAutoGUI.Internals;
using Vanara.PInvoke;
using WildcardMatch;

namespace NetAutoGUI;

public static class ProcessExtensions
{
    private static HWND[] EnumerateProcessWindowHandles(this Process process)
    {
        var hwndList = new List<HWND>();
        User32.EnumWindows((hWnd, lParam) =>
        {
            User32.GetWindowThreadProcessId(hWnd, out uint windowProcessId);
            if (windowProcessId == process.Id)
            {
                hwndList.Add(hWnd);
            }

            return true; // Continue enumeration
        }, IntPtr.Zero);
        return hwndList.ToArray();
    }

    public static Window[] AllWindows(this Process process)
    {
        var windowList = new List<Window>();
        foreach (var hwnd in EnumerateProcessWindowHandles(process))
        {
            if (User32.IsWindowVisible(hwnd) == false) continue;
            windowList.Add(WindowLoader.CreateWindow(hwnd));
        }

        return windowList.ToArray();
    }

    /// <summary>
    /// Wait for a window using the given criteria
    /// </summary>
    /// <param name="predict">the condition</param>
    /// <param name="errorMessageWhenTimeout">errorMessageWhenTimeout</param>
    /// <param name="timeoutSeconds">timeout in second</param>
    /// <exception cref="System.TimeoutException">thrown when time is up</exception>
    /// <returns>the first found window</returns>
    public static Window WaitForWindow(this Process process, Func<Window, bool> predict, string errorMessageWhenTimeout,
        double timeoutSeconds = Constants.DefaultWaitSeconds)
    {
        return TimeBoundWaiter.WaitForNotNull(() => process.AllWindows().FirstOrDefault(predict),
            timeoutSeconds, errorMessageWhenTimeout);
    }

    /// <summary>
    /// Wait for a window using the given criteria
    /// </summary>
    /// <param name="predict">the condition</param>
    /// <param name="errorMessageWhenTimeout">errorMessageWhenTimeout</param>
    /// <param name="timeoutSeconds">timeout in second</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <exception cref="System.TimeoutException">thrown when time is up</exception>
    /// <returns>the first found window</returns>
    public static async Task<Window> WaitForWindowAsync(this Process process, Func<Window, bool> predict,
        string errorMessageWhenTimeout,
        double timeoutSeconds = Constants.DefaultWaitSeconds,
        CancellationToken cancellationToken = default)
    {
        return await TimeBoundWaiter.WaitForNotNullAsync(() => process.AllWindows().FirstOrDefault(predict),
            timeoutSeconds, errorMessageWhenTimeout, cancellationToken);
    }

    /// <summary>
    /// Wait for the window with the given title 
    /// </summary>
    /// <param name="title">title</param>
    /// <param name="timeoutSeconds">timeout in second</param>
    /// <exception cref="System.TimeoutException">thrown when time is up</exception>
    /// <returns>The first found window</returns>
    public static Window WaitForWindowByTitle(this Process process, string title,
        double timeoutSeconds = Constants.DefaultWaitSeconds)
    {
        return WaitForWindow(process, w => w.Title == title, $"Cannot find a window with title={title}",
            timeoutSeconds);
    }

    /// <summary>
    /// Wait for the window with the given title 
    /// </summary>
    /// <param name="title">title</param>
    /// <param name="timeoutSeconds">timeout in second</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <exception cref="System.TimeoutException">thrown when time is up</exception>
    /// <returns>The first found window</returns>
    public static async Task<Window> WaitForWindowByTitleAsync(this Process process, string title,
        double timeoutSeconds = Constants.DefaultWaitSeconds,
        CancellationToken cancellationToken = default)
    {
        return await WaitForWindowAsync(process, w => w.Title == title, $"Cannot find a window with title={title}",
            timeoutSeconds, cancellationToken);
    }


    /// <summary>
    /// Wait for a window using the given wildcard title
    /// </summary>
    /// <param name="wildcard">the wildcard expression. it supports * and ?. For example: *notepad*, n?te</param>
    /// <param name="timeoutSeconds">timeout in second</param>
    /// <exception cref="System.TimeoutException">thrown when time is up</exception>
    /// <returns>the first found window</returns>
    /// <returns></returns>
    public static Window WaitForWindowLikeTitle(this Process process, string wildcard,
        double timeoutSeconds = Constants.DefaultWaitSeconds)
    {
        return WaitForWindow(process, w => wildcard.WildcardMatch(w.Title),
            $"Cannot find a window with title like {wildcard}",
            timeoutSeconds);
    }

    /// <summary>
    /// Wait for a window using the given wildcard title
    /// </summary>
    /// <param name="wildcard">the wildcard expression. it supports * and ?. For example: *notepad*, n?te</param>
    /// <param name="timeoutSeconds">timeout in second</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <exception cref="System.TimeoutException">thrown when time is up</exception>
    /// <returns>the first found window</returns>
    /// <returns></returns>
    public static async Task<Window> WaitForWindowLikeTitleAsync(this Process process, string wildcard,
        double timeoutSeconds = Constants.DefaultWaitSeconds,
        CancellationToken cancellationToken = default)
    {
        return await WaitForWindowAsync(process, w => wildcard.WildcardMatch(w.Title),
            $"Cannot find a window with title like {wildcard}",
            timeoutSeconds, cancellationToken);
    }
}