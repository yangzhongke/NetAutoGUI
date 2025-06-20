using System.Globalization;
using Xunit;

namespace NetAutoGUI.Windows.UnitTests.TestUtils;

public class ZhsOnlyFactAttribute : FactAttribute
{
    public ZhsOnlyFactAttribute()
    {
        CultureInfo ci = CultureInfo.InstalledUICulture;

        if (!ci.Name.StartsWith("zh-Hans", StringComparison.OrdinalIgnoreCase) &&
            !ci.Name.StartsWith("zh-CN", StringComparison.OrdinalIgnoreCase) &&
            !ci.Name.StartsWith("zh-SG", StringComparison.OrdinalIgnoreCase))
        {
            Skip = "Test only runs on simplied Chinese.";
        }
    }
}