using FingerPrint.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Data.Persistence
{
    public interface IUserData
    {
        public Task<ICollection<UserModel>> GetAll();
        public Task<UserModel> Get(int id);
        public Task<bool> Create(UserModel user);
        public Task<bool> Update(UserModel user);
        public Task<bool> Delete(int id);
    }
}
