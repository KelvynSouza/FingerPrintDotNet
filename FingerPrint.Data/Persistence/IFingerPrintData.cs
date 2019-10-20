using FingerPrint.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Data.Persistence
{
    interface IFingerPrintData
    {
        public Task<ICollection<Fingerprint>> GetAll();
        public Task<Fingerprint> Get(int id);
        public Task<bool> Create(Fingerprint finger);
        public Task<bool> Update(Fingerprint finger);
        public Task<bool> Delete(int id);
    }
}
