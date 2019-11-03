using FingerPrint.Data.Model;
using FingerPrint.Data.Persistence;
using FingerPrint.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Events
{
    public class DataEvent
    {
        private IFingerPrintData _fingerPrintData;
        private IUserData _userData;
        private IUserRightsData _userRights;
        public DataEvent(IFingerPrintData fingerPrintData, IUserData userData, IUserRightsData userRights)
        {
            _fingerPrintData = fingerPrintData;
            _userData = userData;
            _userRights = userRights;
        }

        public async Task<ICollection<UserModel>> GetAllUser()
        {
            return await Task.Run<ICollection<UserModel>>(async () =>
            {
                try
                {
                    var users = await _userData.GetAll();

                    return users;
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }

        public async Task<UserCompleteDataModel> GetUserInfo(int id)
        {
            return await Task.Run<UserCompleteDataModel>(async () =>
            {
                try
                {
                    return new UserCompleteDataModel()
                    {
                        User = await _userData.Get(id),
                        UserRights = await _userRights.Get(id),
                        UserFingerprints = await _fingerPrintData.Get(id)
                    };
                    
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }

        public async Task<int> AddUser(UserCompleteDataModel user)
        {
            return await Task.Run<int>(async () =>
            {
                try
                {
                    var id = await _userData.Create(user.User);
                    user.UserRights.UserID = id;
                    await _userRights.Create(user.UserRights);
                    foreach (var fingerprint in user.UserFingerprints)
                    {
                        fingerprint.UserId = id;
                        await _fingerPrintData.Create(fingerprint);
                    }
                    return id;

                }
                catch (Exception)
                {
                    return 0;
                }
            });
        }

        public async Task<bool> EditUserInfo(UserCompleteDataModel user)
        {
            return await Task.Run<bool>(async () =>
            {
                try
                {
                    await _userData.Update(user.User);                    
                    await _userRights.Update(user.UserRights);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<bool> AddFinger(FingerprintModel user)
        {
            return await Task.Run<bool>(async () =>
            {
                try
                {
                    
                    await _fingerPrintData.Create(user);                    
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<bool> DeleteFingerprint(int id)
        {
            return await Task.Run<bool>(async () =>
            {
                try
                {
                    await _fingerPrintData.Delete(id);
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

    }
}
