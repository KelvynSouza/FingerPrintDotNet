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
        public Task<DataTable> GetAll();
        public Task<DataTable> Get(string id);
        public Task Create(User user);
        public Task Update(User user);
        public Task Delete(string id);
    }
}
