using FingerPrint.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Data.Persistence
{
    interface IUserData
    {
        public Task<ICollection<User>> GetAll();
        public Task<User> Get(int id);
        public Task<bool> Create(User user);
        public Task<bool> Update(User user);
        public Task<bool> Delete(int id);
    }
}
