using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FingerPrint.Data.Model;

namespace FingerPrint.Data.Persistence
{
    public class FingerPrintData : IFingerPrintData
    {
        private readonly SqlConnection _con;
        private SqlCommand _cmd;
        private SqlDataAdapter _adapt;
        public FingerPrintData()
        {
            _con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FingerPrintApp;Integrated Security=True;");
        }

        public async Task<bool> Create(FingerprintModel userR)
        {
            return await Task.Run<bool>(() =>
            {
                try
                {

                    _cmd = new SqlCommand("INSERT INTO [dbo].[FingerPrints] ([FingerPrintImage], [UserId]) " +
                                                                "VALUES (@fingerprintImage, @userid)", _con);
                    _con.Open();
                    _cmd.Parameters.AddWithValue("@fingerprintImage", userR.FingerPrintImage);
                    _cmd.Parameters.AddWithValue("@userid", userR.UserId);
                    var result = _cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                    throw ex;
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
                    _cmd = new SqlCommand("DELETE [dbo].[FingerPrints] WHERE [Id]=@id", _con);
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

        
        public async Task<ICollection<FingerprintModel>> Get(int Userid)
        {
            return await Task.Run<ICollection<FingerprintModel>>(() =>
            {
                try
                {
                    _con.Open();
                    DataTable dt = new DataTable();
                    _adapt = new SqlDataAdapter(string.Format("SELECT * from [dbo].[FingerPrints] WHERE [UserId]={0}", Userid), _con);  
                    _adapt.Fill(dt);
                    return Helper.Table.ToListModel<FingerprintModel>(dt);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                    return new List<FingerprintModel>();
                }
                finally
                {
                    _con.Close();
                }
            });
        }

        public async Task<ICollection<FingerprintModel>> GetAll()
        {
            return await Task.Run<ICollection<FingerprintModel>>(() =>
            {
                try
                {
                    _con.Open();
                    DataTable dt = new DataTable();
                    _adapt = new SqlDataAdapter("SELECT * from [dbo].[FingerPrints]", _con);
                    _adapt.Fill(dt);
                    return Helper.Table.ToListModel<FingerprintModel>(dt);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                    return new List<FingerprintModel>();

                }
                finally
                {
                    _con.Close();
                }
            });
        }

        public async Task<bool> Update(FingerprintModel userR)
        {
            return await Task.Run<bool>(() =>
            {
                try
                {

                    _cmd = new SqlCommand("UPDATE [dbo].[FingerPrints] set [FingerPrintImage]=@fingerprintImage, [UserId]=@userid WHERE Id=@id", _con);
                    _con.Open();

                    _cmd.Parameters.AddWithValue("@id", userR.Id);                        
                    _cmd.Parameters.AddWithValue("@fingerprintImage", userR.FingerPrintImage);
                    _cmd.Parameters.AddWithValue("@userid", userR.UserId);
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
