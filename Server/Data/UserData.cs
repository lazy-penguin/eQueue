using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Data
{
    public class UserData
    {
        public int Id;
        public string Name;
        public readonly string Token;
        public bool IsTemporary;
        public UserData(string name, int id, string token, bool isTemporary)
        {
            Name = name;
            Id = id;
            Token = token;
            IsTemporary = isTemporary;
        }
    }
}