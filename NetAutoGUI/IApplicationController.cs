using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace NetAutoGUI;

/// <summary>
/// A controller for control processes and windows
/// </summary>
public interface IApplicationController
{
    /// <summary>
    /// Launch an application
    /// </summary>
    /// <param name="appPath">the path to the application</param>
    /// <param name="arguments">arguments passed to the application</param>
    /// <returns>the Process object associated with the started application</returns>
    public Process LaunchApplication(string appPath, string? arguments = null);

    /// <summary>
    /// Check if there is any processes running with the given process name
    /// </summary>
    /// <param name="processName">the process's name, it can be with or without an extension. For example, both 'notepad' and 'notepad.exe' are accepted.</param>
    /// <returns>true: the application is running; false: the application is not running</returns>
    public bool IsApplicationRunning(string processName);

    /// <summary>
    /// Kill all processes with the given name
    /// </summary>
    /// <param name="processName">the process's name, it can be with or without an extension. For example, both 'notepad' and 'notepad.exe' are accepted.</param>
    public void KillProcesses(string processName);

    /// <summary>
    /// Get all opened windows.
    /// </summary>
    /// <returns>all opend windows</returns>
    public Window[] GetAllWindows();

    /// <summary>
    /// Find the first window with the given title.
    /// </summary>
    /// <param name="title">the title of the window</param>
    /// <returns>The first found window</returns>
    public Window? FindWindowByTitle(string title);

    /// <summary>
    /// Find a window by its id/handler
    /// </summary>
    /// <param name="id"></param>
    /// <returns>the found window</returns>
    public Window? FindWindowById(long id);

    /// <summary>
    /// Find a window using the given criteria
    /// </summary>
    /// <param name="predict">the criteria</param>
    /// <returns>the first found window</returns>
    public Window? FindWindow(Func<Window, bool> predict);

    /// <summary>
    /// Find a window with a given title using wildcard
    /// </summary>
    /// <param name="wildcard">the wildcard expression. it supports * and ?. For example: *notepad*, n?te</param>
    /// <returns>the first found window</returns>
    public Window? FindWindowLikeTitle(string wildcard);

    /// <summary>
    /// Wait for the first process with the give name running.
    /// </summary>
    /// <param name="processName">the process's name, it can be with or without an extension. For example, both 'notepad' and 'notepad.exe' are accepted.</param>
    /// <param name="timeoutSeconds">timeout in second</param>
    /// <exception cref="System.TimeoutException">thrown when time is up</exception>
    public void WaitForApplication(string processName, double timeoutSeconds = 2);

    /// <summary>
    /// Wait for the first process with the give name running.
    /// </summary>
    /// <param name="processName">the process's name, it can be with or without an extension. For example, both 'notepad' and 'notepad.exe' are accepted.</param>
    /// <param name="timeoutSeconds">timeout in second</param>
    /// <exception cref="System.TimeoutException">thrown when time is up</exception>
    public Task WaitForApplicationAsync(string processName, double timeoutSeconds = 2,
        CancellationToken cancellationToken = default);

    public Window WaitForWindowByTitle(string title, double timeoutSeconds = 2);

    public Task<Window> WaitForWindowByTitleAsync(string title, double timeoutSeconds = 2,
        CancellationToken cancellationToken = default);
    
    public Window WaitForWindow(Func<Window, bool> predict, double timeoutSeconds = 2);

    public Task<Window> WaitForWindowAsync(Func<Window, bool> predict, double timeoutSeconds = 2,
        CancellationToken cancellationToken = default);
    
    public Window WaitForWindowLikeTitle(string wildcard, double timeoutSeconds = 2);

    public Task<Window> WaitForWindowLikeTitleAsync(string wildcard, double timeoutSeconds = 2,
        CancellationToken cancellationToken = default);

    public void OpenFileWithDefaultApp(string filePath);
}