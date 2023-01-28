using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace NetAutoGUI
{
	public static class EnumerableExtensions
	{
		public static TSource WaitSingle<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, double timeoutSeconds = 2)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			TSource? result;
			while ((result = source.SingleOrDefault(predicate)) == null)
			{
				if (stopwatch.ElapsedMilliseconds > timeoutSeconds * 1000)
				{
					throw new TimeoutException("WaitSingle timeout");
				}
				Thread.Sleep(50);
			}
			return result;
		}

		public static TSource WaitFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, double timeoutSeconds = 2)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			TSource? result;
			while ((result = source.FirstOrDefault(predicate)) == null)
			{
				if (stopwatch.ElapsedMilliseconds > timeoutSeconds * 1000)
				{
					throw new TimeoutException("WaitSingle timeout");
				}
				Thread.Sleep(50);
			}
			return result;
		}
	}
}
