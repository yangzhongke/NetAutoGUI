using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Vanara.PInvoke;
using static Vanara.PInvoke.User32;

namespace NetAutoGUI.Windows;
public class DynamicMainMenu : DynamicObject
{
	private HWND hwnd;
	public DynamicMainMenu(HWND hwnd)
	{
		this.hwnd = hwnd;
	}
	public override bool TryGetMember(GetMemberBinder binder, out object? result)
	{
		HMENU menu = GetMenu(hwnd);
		DynamicMenuItem? menuItem = MenuHelpers.FindMenuItem(hwnd, menu, binder.Name);
		if (menuItem != null)
		{
			result = menuItem;
			return true;
		}
		return base.TryGetMember(binder, out result);
	}

	public override IEnumerable<string> GetDynamicMemberNames()
	{
		var menu = GetMenu(hwnd);
		return MenuHelpers.GetSubMenuItems(menu).Select(e => e.Text);
	}
}