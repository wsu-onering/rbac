using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneRing.Models
{
	public class ToDoItem
	{
		public string Title { get; set; }
		public string Link { get; set; }
		public bool Complete { get; set; }
		public string DueDate { get; set; }
	}
}