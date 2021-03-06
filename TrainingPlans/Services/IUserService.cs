﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Services
{
    public interface IUserService
    {
        Task<User> Find(int id);

        Task<bool> Create(User user);
    }
}
