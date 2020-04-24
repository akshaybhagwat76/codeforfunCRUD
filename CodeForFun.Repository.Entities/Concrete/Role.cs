using CodeForFun.UI.WebMvcCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeForFun.Repository.Entities.Concrete
{
public	class Role
	{
		public int RoleID { get; set; }
		public string Name { get; set; }
		public IList<User>Users { get; set; }
	}
}
