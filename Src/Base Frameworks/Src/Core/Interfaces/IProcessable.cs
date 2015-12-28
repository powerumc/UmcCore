using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Interfaces
{
	public interface IProcessable
	{
		event EventHandler<EventArgs> ProcessInitializing;
		event EventHandler<EventArgs> ProcessInitialized;

		event EventHandler<EventArgs> ProcessStarting;
		event EventHandler<EventArgs> Processing;
		event EventHandler<EventArgs> ProcessCompleted;

		int MinValue { get; }
		int MaxValue { get; }

		bool CanProcess { get; }
		bool CanPause { get; }
		bool CanStop { get; }

		void Process();
		void Process(ErrorConfiguration errorConfiguration);
		void Process(ErrorConfiguration errorConfiguration, WarningCollection warningCollection);
		void Process(ErrorConfiguration errorConfiguration, WarningCollection warningCollection, ImpactDetailCollection impactDetailCollection);
	}
}
