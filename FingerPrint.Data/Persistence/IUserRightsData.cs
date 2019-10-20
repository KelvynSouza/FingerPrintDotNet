using FingerPrint.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Data.Persistence
{
    interface IUserRightsData
    {
        public Task<ICollection<UserRights>> GetAll();
        public Task<UserRights> Get(int id);
        public Task<bool> Create(UserRights user);
        public Task<bool> Update(UserRights user);
        public Task<bool> Delete(int id);
    }
}
