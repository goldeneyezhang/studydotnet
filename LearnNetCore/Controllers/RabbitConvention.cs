using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNetCore.Controllers
{
	public interface IAmRabbit { }


	public class RabbitControllerAttribute : Attribute, IAmRabbit
	{
		
	}
	public class RabbitConvention : IControllerModelConvention
	{
		
		public void Apply(ControllerModel controller)
		{
			if(IsConventionApplicable(controller))
			{
				var multipliedActions = new List<ActionModel>();
				foreach(var action in controller.Actions)
				{
					var existingAction = action;
					var bunnyAction = new ActionModel(existingAction);
					bunnyAction.ActionName = $"Bunny{bunnyAction.ActionName}";
					multipliedActions.Add(bunnyAction);
				}
				foreach(var action in multipliedActions)
				{
					controller.Actions.Add(action);
				}
			}
		}
		private bool IsConventionApplicable(ControllerModel controller)
		{
			return controller.Attributes.OfType<IAmRabbit>().Any();
		}
	}
}
