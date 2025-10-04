using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalMed.Domain.Records;

namespace AnimalMed.Domain.Models
{
    public class UserModel
    {
        public UserModel() { }

        public UserModel(UserRecord record)
        {
            Id = record.Id;
            Name = record.Name;
            Email = record.Email;
            Password = record.Password;
            Type = record.Type;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }

        public UserRecord ToRecord()
        {
            return new UserRecord
            {
                Id = this.Id,
                Name = this.Name,
                Email = this.Email,
                Password = this.Password,
                Type = this.Type
            };
        }
    }
}
