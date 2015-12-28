var NCsoft = {};
NCsoft.Script = {};
NCsoft.Managed = {};
Umc.Core = {};
Umc.Core.Web = {};
Umc.Core.Web.Mvc = 
{
		GoActionSumit = function (actionName, method) {
			var form = document.createElement("form");
			form.action = actionName;
			form.method = method;

			document.appendChild(form);
			form.submit();
		}
	}
};