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
        public int Id
        {
            get { return id; }
            private set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        private string nameConcat;
        public string NameConcat
        {
            get { return nameConcat; }
            private set { nameConcat = value; }
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