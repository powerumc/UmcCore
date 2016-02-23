using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		<para><see cref="TimeSpan"/> 구조체를 implicit operation을 구현한 <see cref="Time"/> 구조체 입니다.</para>
	///		<para>이 구조체를 이용하여 <see cref="Attribute"/>에 시간을 설정할 수 있습니다.</para>
	/// </summary>
	public struct Time
	{
		private TimeSpan timespan;

		public int Days { get { return this.timespan.Days; } }
		public int Hours { get { return this.timespan.Hours; } }
		public int Minutes { get { return this.timespan.Minutes; } }
		public int Seconds { get { return this.timespan.Seconds; } }

		internal Time(string time)
		{
			convertToTime(time, out this.timespan);
		}

		public static implicit operator Time(string time)
		{
			return new Time(time);
		}

		public static Time operator +(Time left, Time right)
		{
			var timespan = left.ToTimeSpan().Add(right.ToTimeSpan());

			return new Time(timespan.ToString());
		}

		public static bool operator ==(Time left, TimeSpan timespan)
		{
			return left.ToTimeSpan() == timespan;
		}

		public static bool operator !=(Time left, TimeSpan timespan)
		{
			return left.ToTimeSpan() != timespan;
		}

		public static bool operator ==(Time left, Time right)
		{
			return left.ToTimeSpan() == right.ToTimeSpan();
		}

		public static bool operator !=(Time left, Time right)
		{
			return left.ToTimeSpan() != right.ToTimeSpan();
		}

		private static void convertToTime(string time, out TimeSpan datetime)
		{
			var strTimes = time.Split(':');
            if ( strTimes.Length != 3 )
				throw new FrameworkException(ExceptionRS.O_는_1_로_변환할수_없습니다_O_형식이_잘못되었습니다, 
												String.Concat(MessageRS.문자열, " ", time),
												String.Concat(MessageRS.구조체, " ", "Time"));


			int days = 0;

			var dayIndex = strTimes[0].IndexOf('.');
			if (dayIndex > 0)
			{
				if( int.TryParse( strTimes[0].Substring(0, dayIndex), out days) == false )
					throw new FrameworkException(ExceptionRS.O_는_1_로_변환할수_없습니다_O_형식이_잘못되었습니다,
													String.Concat(MessageRS.문자열, " ", time),
													String.Concat(MessageRS.구조체, " ", "Time"));

				strTimes[0] = strTimes[0].Remove(0, dayIndex+1);
			}


			var intTimes = new int[3];

            for (int i = 0; i < 3; i++)
			{
				if (Int32.TryParse(strTimes[i], out intTimes[i]) == false)
				{
					throw new FrameworkException(ExceptionRS.O_는_1_로_변환할수_없습니다_O_형식이_잘못되었습니다,
												String.Concat(MessageRS.문자열, " ", time),
												String.Concat(MessageRS.구조체, " ", "Time"));
				}
			}

			datetime = new TimeSpan(days, intTimes[0], intTimes[1], intTimes[2]);
		}

		public override string ToString()
		{
			return this.timespan.ToString();
		}

		public override int GetHashCode()
		{
			return this.timespan.Days.GetHashCode() ^
				this.timespan.Hours.GetHashCode() ^
				this.timespan.Minutes.GetHashCode() ^
				this.timespan.Seconds.GetHashCode();
		}
		
		public override bool Equals(object obj)
		{
			return this.timespan.Equals(obj);
		}


		/// <summary>	
		/// 	<see cref="Time"/> 객체의 값을 <see cref="TimeSpan"/> 으로 변환합니다.
		/// </summary>
		/// <returns>	
		/// 	<see cref="TimeSpan"/> 객체입니다.
		/// </returns>
		public TimeSpan ToTimeSpan()
		{
			return this.timespan;
		}
	}
}