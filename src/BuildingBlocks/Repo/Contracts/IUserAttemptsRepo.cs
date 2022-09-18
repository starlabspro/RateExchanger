using BuildingBlocks.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Repo.Contracts
{
    internal interface IUserAttemptsRepo
    {
        void Insert(UserAttempts item);
    }
}
