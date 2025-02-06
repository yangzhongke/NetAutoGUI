namespace NetAutoGUI;

/// <summary>
/// Rectangle with confidence
/// </summary>
/// <param name="Rectangle">Rectangle</param>
/// <param name="Confidence">Confidence</param>
public record RectangleWithConfidence(Rectangle Rectangle, double Confidence)
{
    public void Deconstruct(out Rectangle rectangle, out double confidence)
    {
        rectangle = Rectangle;
        confidence = Confidence;
    }

    // Conversion operator from RectangleWithConfidence to Rectangle
    public static implicit operator Rectangle(RectangleWithConfidence rwc)
    {
        return rwc.Rectangle;
    }
}