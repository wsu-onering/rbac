using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoTestSite.Models;

namespace TodoTestSite.Controllers
{
    public class ValuesController : ApiController
    {        
        //////////////////////////////////////////////////////////////////////////////////////
        // Methods
        //////////////////////////////////////////////////////////////////////////////////////
        private Dictionary<string, IEnumerable<TodoItemModel>> userTodoItems;
        public Dictionary<string, IEnumerable<TodoItemModel>> UserTodoItems {
            get { return userTodoItems; }
            private set { userTodoItems = value; }
        }


        //////////////////////////////////////////////////////////////////////////////////////
        // Constructor
        //////////////////////////////////////////////////////////////////////////////////////
        public ValuesController()
        {
            userTodoItems = new Dictionary<string, IEnumerable<TodoItemModel>>() {
                { "dave_chapelle",
                    new List<TodoItemModel>() {
                        new TodoItemModel("Stand Up", "http://www.cc.com/shows/stand-up", new DateTime(2017, 11, 24)),
                        new TodoItemModel("Order Groceries", "https://www.amazon.com/", new DateTime(2017, 11, 10)),
                        new TodoItemModel("Get Hair Cut", "https://www.greatclips.com/salons/9536", new DateTime(2017, 11, 12)) }
                },
                { "keanu_reeves",
                    new List<TodoItemModel>() {
                        new TodoItemModel("Enter the Matrix", "https://www.dmv.org/", new DateTime(2017, 12, 13)),
                        new TodoItemModel("Edit Wikipedia Page", "https://en.wikipedia.org/wiki/Keanu_Reeves", new DateTime(2017, 11, 2)),
                        new TodoItemModel("Meet Dave Chapelle", "http://www.cc.com/shows/stand-up", new DateTime(2017, 11, 24)) }
                },
                { "frodo_baggins",
                    new List<TodoItemModel>() {
                        new TodoItemModel("Learn About Mordor", "http://lotr.wikia.com/wiki/Frodo_Baggins", new DateTime(2017, 8, 22)),
                        new TodoItemModel("Get Pedicure", "https://www.yelp.com/biz/4-ever-nails-richland", new DateTime(2017, 11, 2)),
                        new TodoItemModel("Meet Sam", "http://www.firstshowing.net/2012/the-green-dragon-inn-from-the-lord-of-the-rings-is-now-a-real-pub/", new DateTime(2017, 11, 9)) }
                }
            };
        }


        //////////////////////////////////////////////////////////////////////////////////////
        // Methods
        //////////////////////////////////////////////////////////////////////////////////////
        // GET api/values
        public IEnumerable<TodoItemModel> Get()
        {
            return userTodoItems.FirstOrDefault().Value;
        }

        // GET api/values/frodo_baggins
        public IEnumerable<TodoItemModel> Get(string id)
        {
            IEnumerable<TodoItemModel> todoItems = userTodoItems[id];
            if (todoItems == null)
                return null;

            return todoItems;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
