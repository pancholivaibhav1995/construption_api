using Construction.Entity.Models;
using Construction.Models.APIModels.response;
using Construction.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Repository.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly constructiondbContext _constructiondbContext;
        public UserRepository(constructiondbContext context) : base(context)
        {
            _constructiondbContext = context;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _constructiondbContext.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<List<UserWithRoleResponseModel>> GetUserWithRolesByIdAsync(Guid organisationId)
        {
            var result = await (
        from user in _dbContext.Users
        join organisation in _dbContext.Organisations on user.OrganisationId equals organisation.OrganisationId
        join userrole in _dbContext.Userroles on user.Userid equals userrole.UserId
        join role in _dbContext.Roles on userrole.Roleid equals role.Roleid
        where organisation.OrganisationId == organisationId
        select new UserWithRoleResponseModel
        {
            Userid = user.Userid,
            Firstname = user.Firstname,
            LastName = user.Lastname,
            City   = user.City,
            Address  = user.Address,
            IsActive = user.Isactive,
            Email = user.Email,
            Contact = user.Contactnumber,
            Wageperday = user.Wageperday,
            Roleid = role.Roleid,
            Rolename = role.Rolename
        }).ToListAsync();

            return result;
        }
        public async Task<List<UserManagerResponseModel>> GetAllUsersByOrganisationAsync(Guid organisationId)
        {
            var result = await (
                from user in _dbContext.Users
                join userrole in _dbContext.Userroles on user.Userid equals userrole.UserId
                where user.OrganisationId == organisationId && user.Isactive == true
                select new UserManagerResponseModel
                {
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Userid = user.Userid,
                    Roleid = userrole.Roleid,
                    Wageperday = user.Wageperday
                }).ToListAsync();
            return result;
        }
    }
}
