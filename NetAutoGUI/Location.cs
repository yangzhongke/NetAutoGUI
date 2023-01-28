using System.Numerics;

namespace NetAutoGUI
{
	public record Location(int X, int Y)
	{
		public void Deconstruct(out int x, out int y)
		{
			x = X;
			y = Y;
		}

		public static implicit operator Vector2(Location loc)
		{
			return new Vector2(loc.X, loc.Y);
		}

		public static implicit operator Location(Vector2 vec2)
		{
			return new Location((int)vec2.X, (int)vec2.Y);
		}
	}
}
