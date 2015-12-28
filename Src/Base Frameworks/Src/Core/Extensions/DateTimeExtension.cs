using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace Umc.Core
{
	public static class DateTimeExtension
	{
		#region FindPrevious
		
		/// <summary>
		/// 지정한 날짜로부터 바로 이전의 요일을 찾습니다.
		/// </summary>
		/// <param name="now">기준 날짜 입니다.</param>
		/// <param name="dayOfWeek">찾을 요일입니다.</param>
		/// <param name="includeToday"><c>true</c>를 설정하면 오늘을 포함하고, <c>false</c>면 오늘을 제외한 이전 날짜를 찾습니다.</param>
		/// <returns>찾을 요일의 날짜입니다.</returns>
		public static DateTime FindPrevious(this DateTime now, DayOfWeek dayOfWeek, bool includeToday = false)
		{
			if (includeToday)
			{
				now = now.AddDays(1);
			}

			do
			{
				now = now.AddDays(-1);
				if (now.DayOfWeek == dayOfWeek)
					return now;

			} while (true);
		}

		/// <summary>
		/// 지정한 날짜로부터 바로 이전의 요일을 찾습니다.
		/// </summary>
		/// <param name="now">기준 날짜 입니다.</param>
		/// <param name="dayOfWeek">찾을 요일입니다.</param>
		/// <returns>찾은 요일의 날짜 입니다.</returns>
		/// <exception cref="System.ArgumentNullException">now</exception>
		public static DateTime FindPrevious(this DateTime? now, DayOfWeek dayOfWeek)
		{
			if (now == null)
				throw new ArgumentNullException("now");

			return FindPrevious((DateTime)now, dayOfWeek);
		} 
		#endregion

		#region FindNext
		public static DateTime FindNext(this DateTime now, DayOfWeek dayOfWeek, bool includeToday = false)
		{
			if (includeToday)
			{
				now = now.AddDays(-1);
			}

			do
			{
				now = now.AddDays(1);
				if (now.DayOfWeek == dayOfWeek)
					return now;
			} while (true);
		}

		public static DateTime FindNext(this DateTime? now, DayOfWeek dayOfWeek)
		{
			if (now == null)
				throw new ArgumentNullException("now");

			return FindNext(now.Value, dayOfWeek);
		} 
		#endregion

		#region Between
		/// <summary>
		/// 지정한 시간이 지정한 날짜 사이에 있는지 확인합니다.
		/// </summary>
		/// <param name="start">확인할 시작 날짜입니다.</param>
		/// <param name="now">기준 날짜 입니다.</param>
		/// <param name="end">확인할 종료 날짜입니다.</param>
		/// <returns></returns>
		public static bool Between(this DateTime start, DateTime now, DateTime end)
		{
			return start.Ticks <= now.Ticks && now.Ticks <= end.Ticks;
		}

		/// <summary>
		/// 현재 날짜시간을 기준으로 지정한 날짜 사이에 있는지 확인합니다.
		/// </summary>
		/// <param name="start">확인할 시작 날짜입니다.</param>
		/// <param name="end">확인할 종료 날짜입니다.</param>
		/// <returns></returns>
		public static bool Between(this DateTime start, DateTime end)
		{
			return Between(start, DateTime.Now, end);
		} 
		#endregion

		public static DateTime FirstDateTimeOfWeek(this DateTime datetime)
		{
			return FindPrevious(datetime, DayOfWeek.Sunday, true);
		}

		public static DateTime LastDateTimeIfWeek(this DateTime datetime)
		{
			return FindNext(datetime, DayOfWeek.Saturday, true);
		}

		public static DateTime DateTimeOfWeek(this DateTime datetime, DayOfWeek dayOfWeek)
		{
			var startDate = FirstDateTimeOfWeek(datetime);
			var endDate = LastDateTimeIfWeek(datetime);

			for (var i = startDate; i <= endDate; i = i.AddDays(1))
			{
				if (i.DayOfWeek == dayOfWeek)
					return i;
			}

			throw new InvalidOperationException("DateTimeOfWeek");
		}
	}
}
