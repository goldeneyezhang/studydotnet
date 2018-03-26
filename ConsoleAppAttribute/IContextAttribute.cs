using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppAttribute
{
    public interface IContextAttribute
    {
		void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg);


	}
}
