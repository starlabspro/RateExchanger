using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Validation.Contracts
{
    public interface IValidatorService
    {
        bool ValidateRequestOfTheUser(int userId);
    }
}
