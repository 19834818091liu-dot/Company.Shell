using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataBase.Base.Models
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public class EntityBase:ReactiveObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //自增
        [Reactive]
        public int Id { get; set; }
        [Reactive]
        public DateTime InsertDate { get; set; } = DateTime.Now;
    }
}
