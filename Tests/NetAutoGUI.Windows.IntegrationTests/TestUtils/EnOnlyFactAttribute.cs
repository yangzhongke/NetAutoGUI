using System.Globalization;
using Xunit;

namespace NetAutoGUI.Windows.UnitTests.TestUtils;

public class EnOnlyFactAttribute : FactAttribute
{
    public EnOnlyFactAttribute()
    {
        CultureInfo ci = CultureInfo.InstalledUICulture;

        if (ci.TwoLetterISOLanguageName != "en")
        {
            Skip = "Test only runs on English.";
        }
    }
}