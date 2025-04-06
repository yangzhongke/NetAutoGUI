using System.Diagnostics;
using FluentAssertions;
using Xunit;

namespace NetAutoGUI.Windows.UnitTests.ProcessExtensionsTests;

public class ProcessExtensionsShould
{
    private static readonly string pathOfWinFormsAppForTest1;

    static ProcessExtensionsShould()
    {
        string solutionRoot = TestHelpers.GetSolutionRootDirectory();
        pathOfWinFormsAppForTest1 = TestHelpers.FindFile(solutionRoot, "WinFormsAppForTest1.exe");
    }

    #region GetAllWindows

    [Fact]
    public void GetAllWindows_Correctly()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            SpinWait.SpinUntil(() =>
            {
                var windows = process.GetAllWindows();
                return windows.Count() == 3;
            }, TimeSpan.FromSeconds(5)).Should().BeTrue();
        }
        finally
        {
            process.Kill();
        }
    }

    #endregion

    #region WaitForWindow

    [Fact]
    public void WaitForWindow_WhenWindowExists_Correctly()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Func<Window> sut = () => process.WaitForWindow(w => w.Title.Contains("Form"), "");
            sut.Should().NotThrow();
            sut.Should().NotBeNull();
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public void WaitForWindow_Throw_WhenWindowNotExists()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Func<Window> sut = () =>
                process.WaitForWindow(w => w.Title.Contains("Form888"), "Not Found title=Form888");
            sut.Should().Throw<TimeoutException>().And.Message.Should().Be("Not Found title=Form888");
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public void WaitForWindow_Timeout_WhenWindowNotExists()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Stopwatch sw = Stopwatch.StartNew();
            bool exceptionOccurred = false;
            try
            {
                process.WaitForWindow(w => w.Title.Contains("Form888"), "Not Found title=Form888", 6);
            }
            catch (TimeoutException ex)
            {
                exceptionOccurred = true;
            }

            sw.Stop();
            sw.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(6000);
            exceptionOccurred.Should().BeTrue();
        }
        finally
        {
            process.Kill();
        }
    }

    #endregion

    #region WaitForWindowAsync

    [Fact]
    public async Task WaitForWindowAsync_WhenWindowExists_Correctly()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Func<Task<Window>> sut = () => process.WaitForWindowAsync(w => w.Title.Contains("Form"), "", 5);
            await sut.Should().NotThrowAsync();
            sut.Should().NotBeNull();
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public async Task WaitForWindowAsync_Throw_WhenWindowNotExists()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Func<Task<Window>> sut = () =>
                process.WaitForWindowAsync(w => w.Title.Contains("Form888"), "Not Found title=Form888", 5);
            await sut.Should().ThrowAsync<TimeoutException>();
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public async Task WaitForWindowAsync_Timeout_WhenWindowNotExists()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Stopwatch sw = Stopwatch.StartNew();
            bool exceptionOccurred = false;
            try
            {
                await process.WaitForWindowAsync(w => w.Title.Contains("Form888"), "Not Found title=Form888", 6);
            }
            catch (TimeoutException ex)
            {
                exceptionOccurred = true;
            }

            sw.Stop();
            sw.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(6000);
            exceptionOccurred.Should().BeTrue();
        }
        finally
        {
            process.Kill();
        }
    }

    #endregion

    #region WaitForWindowByTitle

    [Fact]
    public void WaitForWindowByTitle_WhenWindowExists_Correctly()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Func<Window> sut = () => process.WaitForWindowByTitle("Form1");
            sut.Should().NotThrow();
            sut.Should().NotBeNull();
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public void WaitForWindowByTitle_Throw_WhenWindowNotExists()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Func<Window> sut = () =>
                process.WaitForWindowByTitle("Form888");
            sut.Should().Throw<TimeoutException>().And.Message.Should().Be("Cannot find a window with title=Form888");
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public void WaitForWindowByTitle_Timeout_WhenWindowNotExists()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Stopwatch sw = Stopwatch.StartNew();
            bool exceptionOccurred = false;
            try
            {
                process.WaitForWindowByTitle("Form888", 6);
            }
            catch (TimeoutException ex)
            {
                exceptionOccurred = true;
            }

            sw.Stop();
            sw.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(6000);
            exceptionOccurred.Should().BeTrue();
        }
        finally
        {
            process.Kill();
        }
    }

    #endregion

    #region WaitForWindowByTitleAsync

    [Fact]
    public async Task WaitForWindowByTitleAsync_WhenWindowExists_Correctly()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Func<Task<Window>> sut = () => process.WaitForWindowByTitleAsync("Form1");
            await sut.Should().NotThrowAsync();
            sut.Should().NotBeNull();
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public async Task WaitForWindowByTitleAsync_Throw_WhenWindowNotExists()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Func<Task<Window>> sut = () =>
                process.WaitForWindowByTitleAsync("Form888");
            await sut.Should().ThrowAsync<TimeoutException>();
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public async Task WaitForWindowByTitleAsync_Timeout_WhenWindowNotExists()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Stopwatch sw = Stopwatch.StartNew();
            bool exceptionOccurred = false;
            try
            {
                await process.WaitForWindowByTitleAsync("Form888", 6);
            }
            catch (TimeoutException ex)
            {
                exceptionOccurred = true;
            }

            sw.Stop();
            sw.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(6000);
            exceptionOccurred.Should().BeTrue();
        }
        finally
        {
            process.Kill();
        }
    }

    #endregion

    #region WaitForWindowLikeTitle

    [Fact]
    public void WaitForWindowLikeTitle_WhenWindowExists_Correctly()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Func<Window> sut = () => process.WaitForWindowLikeTitle("Form*");
            sut.Should().NotThrow();
            sut.Should().NotBeNull();
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public void WaitForWindowLikeTitle_Throw_WhenWindowNotExists()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Func<Window> sut = () =>
                process.WaitForWindowLikeTitle("Form888*");
            sut.Should().Throw<TimeoutException>().And.Message.Should()
                .Be("Cannot find a window with title like Form888*");
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public void WaitForWindowLikeTitle_Timeout_WhenWindowNotExists()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Stopwatch sw = Stopwatch.StartNew();
            bool exceptionOccurred = false;
            try
            {
                process.WaitForWindowLikeTitle("Form888*", 6);
            }
            catch (TimeoutException ex)
            {
                exceptionOccurred = true;
            }

            sw.Stop();
            sw.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(6000);
            exceptionOccurred.Should().BeTrue();
        }
        finally
        {
            process.Kill();
        }
    }

    #endregion

    #region WaitForWindowLikeTitleAsync

    [Fact]
    public async Task WaitForWindowLikeTitleAsync_WhenWindowExists_Correctly()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Func<Task<Window>> sut = () => process.WaitForWindowLikeTitleAsync("Form*");
            await sut.Should().NotThrowAsync();
            sut.Should().NotBeNull();
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public async Task WaitForWindowLikeTitleAsync_Throw_WhenWindowNotExists()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Func<Task<Window>> sut = () => process.WaitForWindowLikeTitleAsync("Form888*");
            await sut.Should().ThrowAsync<TimeoutException>();
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public async Task WaitForWindowLikeTitleAsync_Timeout_WhenWindowNotExists()
    {
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            Stopwatch sw = Stopwatch.StartNew();
            bool exceptionOccurred = false;
            try
            {
                await process.WaitForWindowLikeTitleAsync("Form888*", 6);
            }
            catch (TimeoutException ex)
            {
                exceptionOccurred = true;
            }

            sw.Stop();
            sw.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(6000);
            exceptionOccurred.Should().BeTrue();
        }
        finally
        {
            process.Kill();
        }
    }

    #endregion

}