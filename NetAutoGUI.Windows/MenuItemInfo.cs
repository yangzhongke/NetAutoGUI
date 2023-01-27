using Vanara.PInvoke;

namespace NetAutoGUI.Windows
{
	internal record MenuItemInfo(string Text, uint MenuItemId, HMENU HSubMenu);
}
