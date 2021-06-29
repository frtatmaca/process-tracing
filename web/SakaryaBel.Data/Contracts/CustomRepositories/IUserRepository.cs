using SakaryaBel.Core.DomainModel.Entities;
using SakaryaBel.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakaryaBel.Data.Contracts.CustomRepositories
{
    public interface IUserRepository : IGenericRepository<Activity>
    {

    }
}
