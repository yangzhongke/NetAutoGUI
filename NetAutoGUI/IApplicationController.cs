﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NetAutoGUI.Internals;

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
    /// <param name="processName">the process's name.</param>
    /// <param name="arguments">arguments</param>
    /// <returns>true: the application is running; false: the application is not running</returns>
    public bool IsApplicationRunning(string processName, string? arguments = null);


    /// <summary>
    /// Kill all processes with the given name
    /// </summary>
    /// <param name="processName">the process's name.</param>
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
    /// <param name="processName">the process's name.</param>
    /// <param name="timeoutSeconds">timeout in second</param>
    /// <returns>the first found process</returns>
    /// <exception cref="System.TimeoutException">thrown when time is up</exception>
    public Process WaitForApplication(string processName, double timeoutSeconds = Constants.DefaultWaitSeconds);

    /// <summary>
    /// Wait for the first process with the give name running.
    /// </summary>
    /// <param name="processName">the process's name.</param>
    /// <param name="timeoutSeconds">timeout in second</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>the first found process</returns>
    /// <exception cref="System.TimeoutException">thrown when time is up</exception>
    public Task<Process> WaitForApplicationAsync(string processName,
        double timeoutSeconds = Constants.DefaultWaitSeconds,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Wait for the window with the given title 
    /// </summary>
    /// <param name="title">title</param>
    /// <param name="timeoutSeconds">timeout in second</param>
    /// <exception cref="System.TimeoutException">thrown when time is up</exception>
    /// <returns>The first found window</returns>
    public Window WaitForWindowByTitle(string title, double timeoutSeconds = Constants.DefaultWaitSeconds);

    /// <summary>
    /// Wait for the window with the given title 
    /// </summary>
    /// <param name="title">title</param>
    /// <param name="timeoutSeconds">timeout in second</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <exception cref="System.TimeoutException">thrown when time is up</exception>
    /// <returns>The first found window</returns>
    public Task<Window> WaitForWindowByTitleAsync(string title, double timeoutSeconds = Constants.DefaultWaitSeconds,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Wait for a window using the given criteria
    /// </summary>
    /// <param name="predict">the condition</param>
    /// <param name="errorMessageWhenTimeout">errorMessageWhenTimeout</param>
    /// <param name="timeoutSeconds">timeout in second</param>
    /// <exception cref="System.TimeoutException">thrown when time is up</exception>
    /// <returns>the first found window</returns>
    public Window WaitForWindow(Func<Window, bool> predict, string errorMessageWhenTimeout,
        double timeoutSeconds = Constants.DefaultWaitSeconds);

    /// <summary>
    /// Wait for a window using the given criteria
    /// </summary>
    /// <param name="predict">the condition</param>
    /// <param name="errorMessageWhenTimeout">errorMessageWhenTimeout</param>
    /// <param name="timeoutSeconds">timeout in second</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <exception cref="System.TimeoutException">thrown when time is up</exception>
    /// <returns>the first found window</returns>
    public Task<Window> WaitForWindowAsync(Func<Window, bool> predict,
        string errorMessageWhenTimeout,
        double timeoutSeconds = Constants.DefaultWaitSeconds,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Wait for a window using the given wildcard title
    /// </summary>
    /// <param name="wildcard">the wildcard expression. it supports * and ?. For example: *notepad*, n?te</param>
    /// <param name="timeoutSeconds">timeout in second</param>
    /// <exception cref="System.TimeoutException">thrown when time is up</exception>
    /// <returns>the first found window</returns>
    /// <returns></returns>
    public Window WaitForWindowLikeTitle(string wildcard, double timeoutSeconds = Constants.DefaultWaitSeconds);

    /// <summary>
    /// Wait for a window using the given wildcard title
    /// </summary>
    /// <param name="wildcard">the wildcard expression. it supports * and ?. For example: *notepad*, n?te</param>
    /// <param name="timeoutSeconds">timeout in second</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <exception cref="System.TimeoutException">thrown when time is up</exception>
    /// <returns>the first found window</returns>
    /// <returns></returns>
    public Task<Window> WaitForWindowLikeTitleAsync(string wildcard,
        double timeoutSeconds = Constants.DefaultWaitSeconds,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Open the given file with the default application
    /// </summary>
    /// <param name="filePath">the file path to be opened</param>
    public void OpenFileWithDefaultApp(string filePath);
}