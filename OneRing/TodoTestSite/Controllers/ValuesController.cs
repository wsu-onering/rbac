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
        // Constructor
        //////////////////////////////////////////////////////////////////////////////////////
        public ValuesController()
        {
        }


        //////////////////////////////////////////////////////////////////////////////////////
        // Methods
        //////////////////////////////////////////////////////////////////////////////////////
        // GET api/values/
        //public IEnumerable<TodoItemModel> Get()
        //{
        //    return new List<TodoItemModel>();
        //}


        // GET api/values/frodo_baggins
        public IEnumerable<TodoItemModel> Get(string id)
        {
            UserDatabase userDB = new UserDatabase();

            // Open database
            userDB.Open();
            // Get todo items with name
            IEnumerable<TodoItemModel> todoItems = userDB.GetTodoItems(id);
            // Close database
            userDB.Close();

            return todoItems;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //    UserDatabase userDB = new UserDatabase();

        //    // Open database
        //    userDB.Open();
        //    // Update complete todo item
        //    userDB.CompleteTodoItem(id);
        //    // Close database
        //    userDB.Close();
        //}

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            UserDatabase userDB = new UserDatabase();

            // Open database
            userDB.Open();
            // Update complete todo item
            userDB.CompleteTodoItem(id);
            // Close database
            userDB.Close();
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

