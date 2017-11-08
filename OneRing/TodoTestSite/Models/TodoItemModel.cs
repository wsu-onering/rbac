using System;
using System.Runtime.Serialization;

namespace TodoTestSite.Models
{
    [DataContract]
    public class TodoItemModel
    {
        //////////////////////////////////////////////////////////////////////////////////////
        // Properties
        //////////////////////////////////////////////////////////////////////////////////////
        [DataMember]
        private string title;
        public string Title {
            get { return title; }
            private set { title = value; }
        }

        [DataMember]
        private string link;
        public string Link {
            get { return link; }
            private set { link = value; }
        }

        [DataMember]
        private DateTime dueDate;
        public DateTime DueDate {
            get { return dueDate; }
            private set { dueDate = value; }
        }
        

        //////////////////////////////////////////////////////////////////////////////////////
        // Constructor
        //////////////////////////////////////////////////////////////////////////////////////
        public TodoItemModel(string title, string link, DateTime dueDate)
        {
            this.title = title;
            this.link = link;
            this.dueDate = dueDate;
        }


        //////////////////////////////////////////////////////////////////////////////////////
        // Methods
        //////////////////////////////////////////////////////////////////////////////////////

    }
}