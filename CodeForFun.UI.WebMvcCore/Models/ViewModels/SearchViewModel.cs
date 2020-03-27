using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeForFun.UI.WebMvcCore.Models.ViewModels
{
	public class SearchViewModel
	{
		public IList<string> Properties { get; set; }
		public IQueryable Entities { get; set; }
		public string EntityName { get; set; }
	}
}
