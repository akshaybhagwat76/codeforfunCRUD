using CodeForFun.Core.Entities;
using System;

namespace CodeForFun.Repository.Entities.Concrete
{
    public class User:IEntity
    {
        public Guid UserID { get; set; }
        public string Email{ get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }


    }
}
