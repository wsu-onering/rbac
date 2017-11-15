using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;

namespace TodoTestSite.Models
{
    public class UserDatabase
    {
        //////////////////////////////////////////////////////////////////////////////////////
        // Properties
        //////////////////////////////////////////////////////////////////////////////////////
        private SqlConnection sqlConnection;

        //////////////////////////////////////////////////////////////////////////////////////
        // Constructor
        //////////////////////////////////////////////////////////////////////////////////////
        public UserDatabase()
        {
            //ConnectionStringSettings collection = ConfigurationManager.ConnectionStrings["TodoDatabase"];
            //string connectionString = collection.ConnectionString;
            string connectionString = "Server=tcp:todositeserver.database.windows.net,1433;Initial Catalog=TodoUserDatabase;Persist Security Info=False;User ID=TodoAdmin;Password=TodoPass9;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            sqlConnection = new SqlConnection(connectionString);
        }


        //////////////////////////////////////////////////////////////////////////////////////
        // Public Methods
        //////////////////////////////////////////////////////////////////////////////////////
        public void Open()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
                return;
            sqlConnection.Open();
        }

        public void Close()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
                return;
            sqlConnection.Close();
        }

        public IEnumerable<TodoItemModel> GetTodoItems(string nameConcat)
        {
            int id = getUserId(nameConcat);
            if (id == -1)
                return null;
            return GetTodoItems(id);
        }

        public IEnumerable<TodoItemModel> GetTodoItems(int userId)
        {
            List<TodoItemModel> todoItems = new List<TodoItemModel>();

            // Create sql command
            SqlCommand command = new SqlCommand("SELECT * FROM TodoTable WHERE UserId=@parameter AND IsComplete='False'", sqlConnection);
            command.Parameters.AddWithValue("parameter", userId);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                // Read read todo items
                while (reader.Read())
                {
                    TodoItemModel todoItem = new TodoItemModel();
                    todoItem.Id = reader.GetInt32(0);
                    todoItem.Title = reader.GetString(2);
                    todoItem.Link = reader.GetString(3);
                    todoItem.DueDate = reader.GetDateTime(4);
                    todoItems.Add(todoItem);
                }
            }

            return todoItems;
        }

        public void CompleteTodoItem(int todoId)
        {

            // Create sql command
            SqlCommand command = new SqlCommand("UPDATE TodoTable SET CompleteDate=@p0, IsComplete='True' WHERE Id=@p1", sqlConnection);
            command.Parameters.AddWithValue("p0", DateTime.Now);
            command.Parameters.AddWithValue("p1", todoId);
            command.ExecuteNonQuery();
        }


        //////////////////////////////////////////////////////////////////////////////////////
        // Private Methods
        //////////////////////////////////////////////////////////////////////////////////////
        private int getUserId(string nameConcat)
        {
            int id = -1;
            // Create sql command
            SqlCommand command = new SqlCommand("SELECT * FROM UserTable WHERE NameConcat=@parameter", sqlConnection);
            command.Parameters.AddWithValue("parameter", nameConcat);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                // Read user id
                if (reader.Read())
                    id = reader.GetInt32(0);
            }
            return id;
        }
    }
}