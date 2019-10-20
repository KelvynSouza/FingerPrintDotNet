using FingerPrint.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Data.Persistence
{
    public interface IFingerPrintData
    {
        public Task<ICollection<FingerprintModel>> GetAll();
        public Task<ICollection<FingerprintModel>> Get(int userId);
        public Task<bool> Create(FingerprintModel finger);
        public Task<bool> Update(FingerprintModel finger);
        public Task<bool> Delete(int id);
    }
}
