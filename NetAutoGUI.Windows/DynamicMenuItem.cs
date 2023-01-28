using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Vanara.PInvoke;
using static Vanara.PInvoke.User32;

namespace NetAutoGUI.Windows;
public class DynamicMenuItem : DynamicObject
{
	private HWND hwnd;
	private HMENU menu;
	private uint menuId;
	public DynamicMenuItem(HWND hwnd, HMENU menu, uint menuId)
	{
		this.hwnd = hwnd;
		this.menu = menu;
		this.menuId = menuId;
	}

	public override bool TryGetMember(GetMemberBinder binder, out object? result)
	{
		DynamicMenuItem? menuItem = MenuHelpers.FindMenuItem(hwnd, menu, binder.Name);
		if (menuItem != null)
		{
			result = menuItem;
			return true;
		}
		return base.TryGetMember(binder, out result);
	}

	/// <summary>
	/// asynchronous
	/// </summary>
	public void Click()
	{
		User32.PostMessage(hwnd, (uint)WindowMessage.WM_COMMAND, new IntPtr(this.menuId), IntPtr.Zero);
	}

	/// <summary>
	/// synchronous
	/// </summary>
	/// <param name="binder"></param>
	/// <param name="args"></param>
	/// <param name="result"></param>
	/// <returns></returns>
	public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
	{
		DynamicMenuItem? menuItem = MenuHelpers.FindMenuItem(hwnd, menu, binder.Name);
		if (menuItem != null)
		{
			result = menuItem;
			User32.SendMessage(hwnd, WindowMessage.WM_COMMAND, menuItem.menuId, IntPtr.Zero);
			return true;
		}
		return base.TryInvokeMember(binder, args, out result);
	}

	public override IEnumerable<string> GetDynamicMemberNames()
	{
		return MenuHelpers.GetSubMenuItems(menu).Select(e => e.Text);
	}
}