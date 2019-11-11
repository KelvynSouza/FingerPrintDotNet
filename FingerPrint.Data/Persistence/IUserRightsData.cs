using FingerPrint.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Data.Persistence
{
    public interface IUserRightsData
    {
        public Task<ICollection<UserRightsModel>> GetAll();
        public Task<UserRightsModel> Get(int userid);
        public Task<bool> Create(UserRightsModel user);
        public Task<bool> Update(UserRightsModel user);
        public Task<bool> Delete(int id);
    }
}
