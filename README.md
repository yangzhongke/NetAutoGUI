# NetAutoGUI
GUI Automation library. It lets your C# application control the mouse and keyboard to automate interactions with other applications.
It is a cross-platform library, but the current version only supports Windows.
It supports multiple monitors and high DPI displays.

It has several features:

1. Moving the mouse and clicking in the windows of other applications.
2. Sending keystrokes to applications (for example, to fill out forms).
3. Take screenshots, and given an image (for example, of a button or checkbox), and find it on the screen.
4. Display alert, message and more dialogs.

Examples:

```csharp
var window = GUI.Application.FindWindowLikeTitle("*Notepad*");// Find a window with title containing "Notepad"
GUI.Application.WaitForApplication("mspaint.exe"); // Wait for the application "mspaint.exe" to start.
Window window = GUI.Application.WaitForWindowLikeTitle("*Paint*"); // Wait for a window with title containing "Paint" to appear.

GUI.Mouse.MoveTo(100, 100);// Move the mouse to coordinates (100, 100) on the screen.
GUI.Keyboard.Write("I'm Zack.\n"); // Type "I'm Zack." followed by a newline character.
GUI.Keyboard.HotKey(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C); // Press the Ctrl+C hotkey to copy selected text.

GUI.Screenshot.Screenshot().Save("1.png"); // Take a screenshot and save it to "1.png".
var rects = GUI.Screenshot.LocateAll(GUI.Screenshot.Screenshot(), BitmapData.FromFile("Images/startMenu.png")); // Find all occurrences of the image "startMenu.png" in the screenshot.
window.WaitAndClick(BitmapData.FromFile("Images/mspaint_EditColors.png")); // Wait for the image "mspaint_EditColors.png" to appear in the window and click it.

GUI.Dialog.Alert("Notepad is not running.");// Show an alert dialog.
GUI.Dialog.Confirm("Are you sure?"); // Show a confirmation dialog.
string? name = GUI.Dialog.Prompt("What's your name?"); // Show a prompt dialog and get the user's input.
```

# Nuget Package

|| NetAutoGUI.Windows |
|-|--------------------|
|*NuGet*| [v3.0.0](https://www.nuget.org/packages/NetAutoGUI.Windows/)|

# Samples

[See the samples project](./Samples/AllSamplesInOneWinForms)

# API Documentation
[See the API documentation](https://yangzhongke.github.io/NetAutoGUI/Docs/api/toc.html)