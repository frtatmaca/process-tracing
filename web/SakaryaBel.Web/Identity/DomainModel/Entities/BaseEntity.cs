using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakaryaBel.Web.DomainModel.Entities
{
    /// <summary>
    /// Entity sınıflarını temsil eden genel entity sınıfı
    /// tüm entity sınıfları bu sınıftan kalıtım alır
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }
}
