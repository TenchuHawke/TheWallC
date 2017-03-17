using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using TheWall.Factory;
using TheWall.Models;
namespace TheWall {
    public class CommentFactory : IFactory<Comment> {
        private string connectionString;
        internal IDbConnection Connection {
            get {
                return new MySqlConnection (connectionString);
            }
        }
        public CommentFactory () {
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=wall2;SslMode=None";
        }
        public void Add (Comment item) {
            using (IDbConnection dbConnection = Connection) {
                string query = "INSERT INTO comments (CommentBody, UserId, MessageId, CreatedAt, UpdatedAt) VALUES (@CommentBody, @UserId, @MessageId, NOW(), NOW())";
                dbConnection.Open ();
                dbConnection.Execute (query, item);
            }
        }
        public void Edit (Comment comment) {
            //TODO: create update method;
        }
        public void Delete (Comment comment) {
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open ();
                dbConnection.Execute ("DELETE * FROM comments WHERE CId={comment.CId}");
            }
        }
        public List<Comment> FindAllByMessageID (Message message) {
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open ();
                List<Comment> OutputList = dbConnection.Query<Comment> ("SELECT * FROM comments WHERE MessageId={message.MId}").ToList<Comment> ();
                return OutputList;
            }
        }
        public List<CommentComplete> AllCommentsComplete () {
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open ();
                List<CommentComplete> OutputList = dbConnection.Query<CommentComplete> ("SELECT comments.*, users.FirstName FROM users LEFT JOIN comments on comments.UserId = users.UId").ToList<CommentComplete>();
                return OutputList;
            }
        }

    }
}