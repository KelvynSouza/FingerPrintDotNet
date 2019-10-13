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

        public async Task Create(User user)
        {
            await Task.Run(() =>
            {
                try
                {

                    _cmd = new SqlCommand("INSERT INTO [dbo].[Users] ([First Name], [Last Name], [BirthDate], [JobName]) " +
                                                                "VALUES (@firstname, @lastname, @birthdate, @job)", _con);
                    _con.Open();
                    _cmd.Parameters.AddWithValue("@firstname", user.FirstName);
                    _cmd.Parameters.AddWithValue("@lastname", user.LastName);
                    _cmd.Parameters.AddWithValue("@birthdate", user.BirthDate);
                    _cmd.Parameters.AddWithValue("@job", user.JobName);
                    var result = _cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro incluído com sucesso...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                }
                finally
                {
                    _con.Close();
                }
            });
        }

        public async Task Delete(string id)
        {
            await Task.Run(() =>
            {
                try
                {
                    _cmd = new SqlCommand("DELETE [dbo].[Users] WHERE [Id]=@id", _con);
                    _con.Open();
                    _cmd.Parameters.AddWithValue("@id", id);
                    _cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro deletado com sucesso...!");
                }
                catch (Exception ex)
                {                    
                    MessageBox.Show("Erro : " + ex.Message);
                }
                finally
                {
                    _con.Close();
                }


            });
        }

        public async Task<User> Get(string id)
        {
            return await Task.Run<User>(() =>
            {
                try
                {
                    _con.Open();
                    DataTable dt = new DataTable();
                    _adapt = new SqlDataAdapter(string.Format("SELECT * from [dbo].[Users] WHERE [Id]={0}",id), _con);  
                    _adapt.Fill(dt);
                    return Helper.Table.ToModel<User>(dt);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                    return new User();
                }
                finally
                {
                    _con.Close();
                }
            });
        }

        public async Task<ICollection<User>> GetAll()
        {
            return await Task.Run<ICollection<User>>(() =>
            {
                try
                {
                    _con.Open();
                    DataTable dt = new DataTable();
                    _adapt = new SqlDataAdapter("SELECT * from [dbo].[Users]", _con);
                    _adapt.Fill(dt);
                    return Helper.Table.ToListModel<User>(dt);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                    return new List<User>();

                }
                finally
                {
                    _con.Close();
                }
            });
        }

        public async Task Update(User user)
        {
            await Task.Run(() =>
            {
                try
                {

                    _cmd = new SqlCommand("UPDATE [dbo].[Users] set [First Name]=@firstname, [Last Name]=@lastname, [BirthDate]=@birthdate, [JobName]=@job WHERER Id=@id", _con);
                    _con.Open();
                    _cmd.Parameters.AddWithValue("@Id", user.Id);
                    _cmd.Parameters.AddWithValue("@firstname", user.FirstName);
                    _cmd.Parameters.AddWithValue("@lastname", user.LastName);
                    _cmd.Parameters.AddWithValue("@birthdate", user.BirthDate);
                    _cmd.Parameters.AddWithValue("@job", user.JobName);
                    var result = _cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro atualizado com sucesso...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                }
                finally
                {
                    _con.Close();
                }

            });
        }

        

    }
 
}
