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
    public class UserData : IUserData
    {
        private SqlConnection _con;
        private SqlCommand _cmd;
        private SqlDataAdapter _adapt;
        public UserData()
        {
            _con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FingerPrintApp;Integrated Security=True;");
        }

        public async Task<bool> Create(UserModel user)
        {
            return await Task.Run<bool>(() =>
            {
                try
                {

                    _cmd = new SqlCommand("INSERT INTO [dbo].[Users] ([FirstName], [LastName], [BirthDate], [JobName]) " +
                                                                "VALUES (@firstname, @lastname, @birthdate, @job)", _con);
                    _con.Open();
                    _cmd.Parameters.AddWithValue("@firstname", user.FirstName);
                    _cmd.Parameters.AddWithValue("@lastname", user.LastName);
                    _cmd.Parameters.AddWithValue("@birthdate", user.BirthDate);
                    _cmd.Parameters.AddWithValue("@job", user.JobName);
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
                    _cmd = new SqlCommand("DELETE [dbo].[Users] WHERE [Id]=@id", _con);
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

        public async Task<UserModel> Get(int id)
        {
            return await Task.Run<UserModel>(() =>
            {
                try
                {
                    _con.Open();
                    DataTable dt = new DataTable();
                    _adapt = new SqlDataAdapter(string.Format("SELECT * from [dbo].[Users] WHERE [Id]={0}",id), _con);  
                    _adapt.Fill(dt);
                    return Helper.Table.ToModel<UserModel>(dt);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                    return new UserModel();
                }
                finally
                {
                    _con.Close();
                }
            });
        }

        public async Task<ICollection<UserModel>> GetAll()
        {
            return await Task.Run<ICollection<UserModel>>(() =>
            {
                try
                {
                    _con.Open();
                    DataTable dt = new DataTable();
                    _adapt = new SqlDataAdapter("SELECT * from [dbo].[Users]", _con);
                    _adapt.Fill(dt);
                    return Helper.Table.ToListModel<UserModel>(dt);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                    return new List<UserModel>();

                }
                finally
                {
                    _con.Close();
                }
            });
        }

        public async Task<bool> Update(UserModel user)
        {
            return await Task.Run<bool>(() =>
            {
                try
                {

                    _cmd = new SqlCommand("UPDATE [dbo].[Users] set [FirstName]=@firstname, [LastName]=@lastname, [BirthDate]=@birthdate, [JobName]=@job WHERE Id=@id", _con);
                    _con.Open();
                    _cmd.Parameters.AddWithValue("@Id", user.Id);
                    _cmd.Parameters.AddWithValue("@firstname", user.FirstName);
                    _cmd.Parameters.AddWithValue("@lastname", user.LastName);
                    _cmd.Parameters.AddWithValue("@birthdate", user.BirthDate);
                    _cmd.Parameters.AddWithValue("@job", user.JobName);
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
