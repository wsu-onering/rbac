using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoTestSite.Models
{
    public class UserModel
    {
        //////////////////////////////////////////////////////////////////////////////////////
        // Properties
        //////////////////////////////////////////////////////////////////////////////////////
        private int id;
        public int ID {
            get { return id; }
            private set { id = value; }
        }

        private string name;
        public string Name {
            get { return name; }
            private set { name = value; }
        }
        
        private IEnumerable<TodoItemModel> todoItems;
        public IEnumerable<TodoItemModel> TodoItems {
            get { return todoItems; }
            private set { todoItems = value; }
        }

        //////////////////////////////////////////////////////////////////////////////////////
        // Constructor
        //////////////////////////////////////////////////////////////////////////////////////
        public UserModel(string name)
        {
            this.name = name;
            id = name.GetHashCode();
        }


        //////////////////////////////////////////////////////////////////////////////////////
        // Methods
        //////////////////////////////////////////////////////////////////////////////////////

    }
}