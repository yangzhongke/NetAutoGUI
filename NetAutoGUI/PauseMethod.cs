namespace NetAutoGUI;

public enum PauseMethod
{
    /// <summary>
    /// Use Thread.Sleep(), which is CPU-friendly;
    /// however, it may cause dead-lock when being used in multiple-thread context, and async methods.
    /// </summary>
    Sleep,

    /// <summary>
    /// Use SpinWait, which causes high CPU usage;
    /// however, it's fool-proof when being used in multiple-thread context, and async methods.
    /// It's the default value.
    /// Warning: Avoid using it for waiting too long.
    /// </summary>
    SpinWait
    //Async wait is not supported, because it will make this library to complicated,
    //and GUI simulation should not be I/O sensitive.
}