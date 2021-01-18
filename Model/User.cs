using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvernoteClone.Model {
    public class User {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public string  Username { get; set; }
        public string Password { get; set; }
    }
}
