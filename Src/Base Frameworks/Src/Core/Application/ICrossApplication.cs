using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Umc.Core.Application
{
	public interface ICrossApplication : IInitializableWithNotification
	{
		string AssemblyName { get; set; }
		string TypeName { get; set; }

		event EventHandler<CrossApplicationCommand> Received;

		void OnReceived();

		void Send(string message);

		void Execute(ApartmentState apartmentState);
		void Execute();

		void InitializeEvents();

		void Attach(ICrossApplication crossApplication);
	}
}
