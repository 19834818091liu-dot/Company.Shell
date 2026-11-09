using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataBase.Base.Models
{
    [Table(nameof(UserEntity))]
    public class UserEntity:EntityBase
    {
        
        public string UserName { get; set; }

        public string Password { get; set; }

        public int Role {  get; set; }
    }
}
