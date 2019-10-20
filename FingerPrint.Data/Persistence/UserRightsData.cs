using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FingerPrint.Data.Model;

namespace FingerPrint.Data.Persistence
{
    public class UserRightsData : IUserRightsData
    {
        private SqlConnection _con;
        private SqlCommand _cmd;
        private SqlDataAdapter _adapt;
        public UserRightsData()
        {
            _con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FingerPrintApp;Integrated Security=True;");
        }

        public async Task<bool> Create(UserRights userR)
        {
           return await Task.Run<bool>(() =>
            {
                try
                {

                    _cmd = new SqlCommand("INSERT INTO [dbo].[UsersRight] ([Read], [Write], [Delete], [UserID]) " +
                                                                "VALUES (@read, @write, @delete, @userid)", _con);
                    _con.Open();
                    _cmd.Parameters.AddWithValue("@read", userR.Read);
                    _cmd.Parameters.AddWithValue("@write", userR.Write);
                    _cmd.Parameters.AddWithValue("@delete", userR.Delete);
                    _cmd.Parameters.AddWithValue("@userid", userR.UserID);
                    var result = _cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                    return false;
                }
                finally
                {
                    _con.Close();
                }
            });
        }

        public async Task<bool> Delete(int id)
        {
            return await Task.Run<bool>(() =>
            {
                try
                {
                    _cmd = new SqlCommand("DELETE [dbo].[UsersRight] WHERE [Id]=@id", _con);
                    _con.Open();
                    _cmd.Parameters.AddWithValue("@id", id);
                    _cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {                    
                    MessageBox.Show("Erro : " + ex.Message);
                    return false;
                }
                finally
                {
                    _con.Close();
                }


            });
        }

        public async Task<UserRights> Get(int Userid)
        {
            return await Task.Run<UserRights>(() =>
            {
                try
                {
                    _con.Open();
                    DataTable dt = new DataTable();
                    _adapt = new SqlDataAdapter(string.Format("SELECT * from [dbo].[UsersRight] WHERE [UserId]={0}", Userid), _con);  
                    _adapt.Fill(dt);
                    return Helper.Table.ToModel<UserRights>(dt);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                    return new UserRights();
                }
                finally
                {
                    _con.Close();
                }
            });
        }

        public async Task<ICollection<UserRights>> GetAll()
        {
            return await Task.Run<ICollection<UserRights>>(() =>
            {
                try
                {
                    _con.Open();
                    DataTable dt = new DataTable();
                    _adapt = new SqlDataAdapter("SELECT * from [dbo].[UsersRight]", _con);
                    _adapt.Fill(dt);
                    return Helper.Table.ToListModel<UserRights>(dt);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                    return new List<UserRights>();

                }
                finally
                {
                    _con.Close();
                }
            });
        }

        public async Task<bool> Update(UserRights userR)
        {
            return await Task.Run<bool>(() =>
            {
                try
                {

                    _cmd = new SqlCommand("UPDATE [dbo].[UsersRight] set [Read]=@read, [Write]=@write, [Delete]=@delete, [UserID]=@userid WHERE Id=@id", _con);
                    _con.Open();                   

                    _cmd.Parameters.AddWithValue("@read", userR.Read);
                    _cmd.Parameters.AddWithValue("@write", userR.Write);
                    _cmd.Parameters.AddWithValue("@delete", userR.Delete);
                    _cmd.Parameters.AddWithValue("@userid", userR.UserID);
                    _cmd.Parameters.AddWithValue("@id", userR.Id);
                    var result = _cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                    return false;
                }
                finally
                {
                    _con.Close();
                }

            });
        }

        

    }
 
}
