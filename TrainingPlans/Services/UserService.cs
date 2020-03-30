using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Models;
using TrainingPlans.Repositories;

namespace TrainingPlans.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<int> Create(User user)
        {
            return await _userRepository.Create(user);
        }

        public async Task<User> Find(int id)
        {
            return await _userRepository.Get(id);
        }
    }
}
