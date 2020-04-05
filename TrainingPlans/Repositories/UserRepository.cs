using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Repositories
{
    public class UserRepository : AbstractEntityRepositoryBase<User>, IUserRepository
    {
        public UserRepository(TrainingPlanDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<User> Get(int id)
        {
            return await _dbContext.User.Include(x => x.UserDefaults).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
