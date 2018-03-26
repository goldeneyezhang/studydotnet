using System;
using System.Collections.Generic;
using System.Text;

namespace LearnNetCore
{
   public class MyOptions
    {
		public MyOptions()
		{
			Option1 = "value1_form_ctor";
		}
		public string Option1 { get; set; }
		public int Option2 { get; set; }
    }
}
