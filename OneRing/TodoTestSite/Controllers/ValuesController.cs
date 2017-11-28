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
        // GET api/values/####
        public IEnumerable<TodoItemModel> Get(string guid)
        {
            UserDatabase userDB = new UserDatabase();

            // Open database
            userDB.Open();
            // Get todo items with name
            IEnumerable<TodoItemModel> todoItems = userDB.GetTodoItems(guid);
            // Close database
            userDB.Close();

            return todoItems;
        }

        // PUT api/values?guid=####&todId=#
        public void Put([FromUri]string guid, [FromUri]int todoId)
        {
            UserDatabase userDB = new UserDatabase();

            // Open database
            userDB.Open();
            // Update complete todo item
            userDB.CompleteTodoItem(guid, todoId);
            // Close database
            userDB.Close();
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

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}

        //// POST api/values
        //public void Post([FromBody]string value)
        //{

        //}

        // GET api/values/
        //public IEnumerable<TodoItemModel> Get()
        //{
        //    return new List<TodoItemModel>();
        //}
    }
}

