using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;

namespace TodoTestSite2.Models
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

        /// <summary>
        /// This method uses the guid to return all todo items specific to the 
        /// user and that are NOT complete.
        /// </summary>
        /// <param name="guid">16-byte global unique identifier</param>
        /// <returns></returns>
        public IEnumerable<TodoItemModel> GetTodoItems(string guid)
        {
            List<TodoItemModel> todoItems = new List<TodoItemModel>();

            // Get user id
            int userId = getUserId(guid);

            // If user not found
            if (userId == -1)
                return null;

            // Create sql command
            SqlCommand command = new SqlCommand("SELECT * FROM TodoTable2 WHERE UserId=@parameter AND IsComplete='False'", sqlConnection);
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

        /// <summary>
        /// This method completes the given todo item given the user guid and
        /// todo id. If the guid does not match any users in the database it
        /// does nothing.
        /// </summary>
        /// <param name="guid">Global unique identifier of the todo item</param>
        /// <param name="todoId">Id of the todo item to mark as complete</param>
        public void CompleteTodoItem(string guid, int todoId)
        {
            // Get user id
            int userId = getUserId(guid);

            // If user not found
            if (userId == -1)
                return;

            // Create sql command
            SqlCommand command = new SqlCommand("UPDATE TodoTable2 SET CompleteDate=@p0, IsComplete='True' WHERE Id=@p1", sqlConnection);
            command.Parameters.AddWithValue("p0", DateTime.Now);
            command.Parameters.AddWithValue("p1", todoId);
            command.ExecuteNonQuery();
        }


        //////////////////////////////////////////////////////////////////////////////////////
        // Private Methods
        //////////////////////////////////////////////////////////////////////////////////////
        private int getUserId(string guid)
        {
            int id = -1;
            // Create sql command
            SqlCommand command = new SqlCommand("SELECT * FROM UserTable2 WHERE Guid=@parameter", sqlConnection);
            command.Parameters.AddWithValue("parameter", guid);
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