using Company.DataBase.Base.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Share.Models
{
    public class LoginEvent:PubSubEvent<UserEntity>
    {

    }
}
