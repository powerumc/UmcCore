using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umc.Core.IoC;
using Umc.Core.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using System.Web.Handlers;
using System.Reflection;

namespace System.Web.Mvc
{
	public class MvcController : Controller
	{
		protected IFrameworkContainer ModelContainer { get; set; }

		public MvcController()
		{
			this.ModelContainer = new FrameworkContainerForUnity();
		}


		protected ActionResult JsonJqGrid(JqGridSummery data)
		{
			return Json(data);
		}

		protected ActionResult JsonJqGrid(JqGridSummery data, string contentType)
		{
			return Json(data, contentType);
		}

		protected ActionResult JsonJqGrid(JqGridSummery data, JsonRequestBehavior behavior)
		{
			return Json(data, behavior);
		}

		protected ActionResult JsonJqGrid(JqGridSummery data, string contentType, Text.Encoding encoding)
		{
			return Json(data, contentType, encoding);
		}

		protected ActionResult JsonJqGrid(JqGridSummery data, string contentType, Text.Encoding encoding, JsonRequestBehavior behavior)
		{
			return Json(data, contentType, encoding, behavior);
		}


		public virtual ActionResult GetList(bool _search, int rows, int page, int idx, string sord)
		{
			throw new NotImplementedException();
		}
	}
}