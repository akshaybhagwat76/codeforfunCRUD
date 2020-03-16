﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeForFun.UI.WebMvcCore.Models.ViewModels
{
	public class CategoryViewModel
	{
        public short Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CategoryViewModel> InverseParent { get; set; }
    }
}
