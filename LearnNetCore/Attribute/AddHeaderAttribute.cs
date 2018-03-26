using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Attribute
{
    public class AddHeaderAttribute:ResultFilterAttribute
    {
		private readonly string _name;
		private readonly string _value;

		public AddHeaderAttribute(string name,string value)
		{
			this._name = name;
			this._value = value;
		}
		
    }
}
