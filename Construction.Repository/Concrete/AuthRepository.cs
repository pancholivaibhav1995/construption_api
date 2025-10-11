using Construction.Entity.Models;
using Construction.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Repository.Concrete
{
    public class AuthRepository : GenericRepository<User>, IAuthRepository
    {
        protected readonly constructiondbContext _dbContext; 
         public AuthRepository(constructiondbContext context) : base(context)
        { 
        }
    }
}
