using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using TheWall.Factory;
using TheWall.Models;
namespace TheWall {
    public class MessageFactory : IFactory<Message> {
        private string connectionString;
        internal IDbConnection Connection {
            get {
                return new MySqlConnection (connectionString);
            }
        }
        public MessageFactory () {
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=wall2;SslMode=None";
        }
        public void Add (Message item) {
            using (IDbConnection dbConnection = Connection) {
                string query = $"INSERT INTO messages (MessageBody, UserId, CreatedAt, UpdatedAt) VALUES (@MessageBody, @UserId, NOW(), NOW())";
                dbConnection.Open ();
                dbConnection.Execute (query, item);
            }
        }
        public void Edit (Message message) {
            //TODO: create Edit Method;
        }
        public void Delete (Message message) {
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open ();
                dbConnection.Execute ("DELETE * FROM wall.messages WHERE MId={message.MId}");
            }
        }
        public Message FindByMessageId (int id) {
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open ();
                return dbConnection.Query<Message> ("SELECT * FROM messages WHERE MId = @Id", new { Id = id }).FirstOrDefault ();
            }
        }
        public List<MessageComplete> AllMessagesComplete () {
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open ();
                List<MessageComplete> OutputList = dbConnection.Query<MessageComplete> ("SELECT messages.*, users.FirstName FROM users LEFT JOIN messages on messages.UserId = users.UId").ToList ();
                return OutputList;
            }
        }
    }
}