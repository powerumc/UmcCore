using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Threading;

namespace Umc.Core.Application
{
	public abstract class CrossApplication : MarshalByRefObject, ICrossApplication
	{
		public string AssemblyName { get; set; }
		public string TypeName { get; set; }

		private List<ICrossApplication> _attachedCrossApplication = new List<ICrossApplication>();
		protected List<ICrossApplication> AttachedCrossApplication { get { return _attachedCrossApplication; }}

		public event EventHandler<CrossApplicationCommand> Received;

		public virtual void OnReceived()
		{
			if (this.Received != null)
				this.Received(this, new CrossApplicationCommand());
		}

		public void Send(string message)
		{
			Console.WriteLine("Waiting...." + AppDomain.CurrentDomain.FriendlyName);

			OnReceived();
		}

		public void Attach(ICrossApplication crossApplication)
		{
			AttachedCrossApplication.Add(crossApplication);
		}

		

		public void Execute(ApartmentState apartmentState)
		{
			Action<ICrossApplication> action = (app) =>
			{
				try
				{
					Thread thread = new Thread(new ThreadStart(app.Initialize));
					thread.SetApartmentState(apartmentState);
					thread.Start();
				}
				catch
				{
					throw;
				}
			};

			action.BeginInvoke(this, null, null);
		}

		public void Execute()
		{
			this.Execute(ApartmentState.STA);
		}

		public virtual void InitializeEvents()
		{
		}

		public abstract void Initialize();

		public event EventHandler Initialized;

		public bool IsInitialized
		{
			get { return false; }
		}

		public virtual void BeginInit()
		{
		}

		public virtual void EndInit()
		{
		}

		protected virtual void OnEndInit()
		{
			if (this.Initialized != null)
				this.Initialized(this, EventArgs.Empty);
		}
	}
}