using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Model
{
	[ProtoContract]
    public class User
    {
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public string UserName { get; set; }
		[ProtoMember(3)]
		public int Age { get; set; }

    }
}
