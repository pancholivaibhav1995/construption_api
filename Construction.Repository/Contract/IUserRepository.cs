using Construction.Entity.Models;
using Construction.Models.APIModels.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Repository.Contract
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<UserWithRoleResponseModel>> GetUserWithRolesByIdAsync(Guid organisationId);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
