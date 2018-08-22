using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using AuditionProj.Models;

namespace AuditionProj
{
    public class MessagePersistence
    {
        private SqlConnection conn;

        public MessagePersistence()
        {
            string connectionString = "Server=qlikmessagedb.cjf458mpgsxw.us-east-1.rds.amazonaws.com;Database=messageDB;User ID=msgdeveloper; Password=password123";
            conn = new SqlConnection
            {
                ConnectionString = connectionString
            };
        }

        //Checks if submitted message is a palindrome before saving to db
        public void SaveMessage(MessageModel message)
        {
            //Check if the message is a palindrome before saving to db
            message.IsPalindrome = CheckIfIsPalindrome(message.MessageContent);

            string command = "INSERT into [dbo].[messageTable] (messageContent, isPalindrome) values(@messageContent, @isPalindrome)";

            conn.Open();

            SqlCommand sqlCommand = new SqlCommand(command, conn);
            sqlCommand.Parameters.AddWithValue("@messageContent", message.MessageContent);
            sqlCommand.Parameters.AddWithValue("@isPalindrome", Convert.ToInt16(message.IsPalindrome));
            SqlDataAdapter adapter = new SqlDataAdapter
            {
                InsertCommand = sqlCommand
            };
            adapter.InsertCommand.ExecuteNonQuery();

            sqlCommand.Dispose();
            conn.Close();
        }

        //Retrieves all saved messages from the db
        public List<MessageModel> GetMessages()
        {
            string command = "SELECT * from [dbo].[messageTable]";
            conn.Open();

            List<MessageModel> messages = new List<MessageModel>();

            SqlCommand sqlCommand = new SqlCommand(command, conn);
            SqlDataAdapter adapter = new SqlDataAdapter
            {
                InsertCommand = sqlCommand
            };
            using (SqlDataReader rd = adapter.InsertCommand.ExecuteReader())
            {
                //Add messages to the list while there are still messages to be read
                while (rd.Read())
                {
                    MessageModel msg = new MessageModel();
                    msg.ID = (int)rd["ID"];
                    msg.MessageContent = (string)rd["messageContent"];
                    msg.IsPalindrome = (bool)rd["isPalindrome"];

                    messages.Add(msg);
                }
            }
            sqlCommand.Dispose();
            conn.Close();

            return messages;
        }

        //Retrieves specific message with given id
        public MessageModel GetMessage(int id)
        {
            string command = "SELECT * from [dbo].[messageTable] where ID = @ID";
            conn.Open();

            MessageModel message = new MessageModel();

            SqlCommand sqlCommand = new SqlCommand(command, conn);
            sqlCommand.Parameters.AddWithValue("@ID", id);
            SqlDataAdapter adapter = new SqlDataAdapter
            {
                InsertCommand = sqlCommand
            };
            using (SqlDataReader sqlDataReader = adapter.InsertCommand.ExecuteReader())
            {
                while (sqlDataReader.Read())
                {
                    message.ID = (int)sqlDataReader["ID"];
                    message.MessageContent = (string)sqlDataReader["messageContent"];
                    message.IsPalindrome = (bool)sqlDataReader["isPalindrome"];
                }
            }
            sqlCommand.Dispose();
            conn.Close();

            return message;
        }

        //Deletes specific message with given id
        public void DeleteMessage(int id)
        {
            string command = "DELETE from [dbo].[messageTable] where ID = @ID";
            conn.Open();

            SqlCommand sqlCommand = new SqlCommand(command, conn);
            sqlCommand.Parameters.AddWithValue("@ID", id);
            SqlDataAdapter adapter = new SqlDataAdapter
            {
                InsertCommand = sqlCommand
            };
            adapter.InsertCommand.ExecuteNonQuery();

            sqlCommand.Dispose();
            conn.Close();
        }

        //Updates specific message with given id
        public void UpdateMessage(int id, MessageModel message)
        {
            message.IsPalindrome = CheckIfIsPalindrome(message.MessageContent);

            string command = "UPDATE [dbo].[messageTable] SET messageContent = @messageContent, isPalindrome = @isPalindrome where ID = @ID";
            conn.Open();

            SqlCommand sqlCommand = new SqlCommand(command, conn);
            sqlCommand.Parameters.AddWithValue("@messageContent", message.MessageContent);
            sqlCommand.Parameters.AddWithValue("@isPalindrome", Convert.ToInt16(message.IsPalindrome));
            sqlCommand.Parameters.AddWithValue("@ID", id);
            SqlDataAdapter adapter = new SqlDataAdapter
            {
                InsertCommand = sqlCommand
            };
            adapter.InsertCommand.ExecuteNonQuery();

            sqlCommand.Dispose();
            conn.Close();
        }

        //Determines if a given string is a palindrome
        private bool CheckIfIsPalindrome(string text)
        {
            char[] textArray = text.ToLower().ToArray();
            char[] reverseTextArray = text.ToLower().ToArray();
            Array.Reverse(reverseTextArray);

            int size = textArray.Length;

            for (int i = 0; i < size; i++)
            {
                if (textArray[i] != reverseTextArray[i])
                    return false;
            }
            return true;
        }
    }
}