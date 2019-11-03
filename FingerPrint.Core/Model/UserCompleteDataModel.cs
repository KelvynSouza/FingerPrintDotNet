using FingerPrint.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Model
{
    public class UserCompleteDataModel
    {
        public UserModel User { get; set; }
        public UserRightsModel UserRights { get; set; }
        public ICollection<FingerprintModel> UserFingerprints { get; set; }
        
    }
}
