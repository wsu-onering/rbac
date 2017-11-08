using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoStaticSource.Models;

namespace ToDoStaticSource.Controllers
{
    public class ToDoItemsController : ApiController
    {
		ToDoItem[] products = new ToDoItem[]
		{
			new ToDoItem { Title = "Stand up", Link = "http://www.cc.com/shows/stand-up", Complete = false, DueDate= "2017-11-24T00:00:00"},
			new ToDoItem { Title = "Order Groceries", Link = "https://www.amazon.com/", Complete = false, DueDate= "2017-11-24T00:00:00"},
			new ToDoItem { Title = "Get Hair Cut", Link = "https://www.greatclips.com/salons/9536", Complete = false, DueDate= "2017-11-24T00:00:00"},
		};

		public IEnumerable<ToDoItem> GetAllProducts() {
			return products;
		}

		public IHttpActionResult GetProduct(string title) {
			var product = products.FirstOrDefault((p) => p.Title == title);
			if (product == null) {
				return NotFound();
			}
			return Ok(product);
		}
	}
}
