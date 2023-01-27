using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Vanara.InteropServices;
using Vanara.PInvoke;
using static Vanara.PInvoke.User32;

namespace NetAutoGUI.Windows;
internal static class MenuHelpers
{
	public static MenuItemInfo GetMenuItem(HMENU menu, uint position)
	{
		MENUITEMINFO menuItemInfo = new MENUITEMINFO();
		menuItemInfo.cbSize = (uint)Marshal.SizeOf(typeof(MENUITEMINFO));
		menuItemInfo.fMask = MenuItemInfoMask.MIIM_STRING | MenuItemInfoMask.MIIM_SUBMENU | MenuItemInfoMask.MIIM_ID;
		menuItemInfo.fType = MenuItemType.MFT_STRING;
		menuItemInfo.cch = 256;
		StrPtrAuto strPtrText = new StrPtrAuto(menuItemInfo.cch);
		menuItemInfo.dwTypeData = strPtrText;
		bool e = GetMenuItemInfo(menu, position, true, ref menuItemInfo);
		string menuItemText = strPtrText.ToString();
		strPtrText.Free();
		menuItemText = menuItemText.Replace("&", "");//remove &
		menuItemText = menuItemText.Split('\t')[0];//remove accelerate key, like 'ctrl+s'
		menuItemText = menuItemText.Replace(" ", "");//remove space
		menuItemText = menuItemText.Replace(".", "");//remove "..."
		return new MenuItemInfo(menuItemText, menuItemInfo.wID, menuItemInfo.hSubMenu);
	}

	public static IEnumerable<MenuItemInfo> GetSubMenuItems(HMENU menu)
	{
		int menuItemCount = GetMenuItemCount(menu);
		for (int i = 0; i < menuItemCount; i++)
		{
			yield return GetMenuItem(menu, (uint)i);
		}
	}

	public static DynamicMenuItem? FindMenuItem(HWND hwnd, HMENU menu, string name)
	{
		var items = GetSubMenuItems(menu);
		foreach (var item in items)
		{
			if (name.Equals(item.Text, StringComparison.CurrentCultureIgnoreCase))
			{
				return new DynamicMenuItem(hwnd, item.HSubMenu, item.MenuItemId);
			}
		}
		return null;
	}
}