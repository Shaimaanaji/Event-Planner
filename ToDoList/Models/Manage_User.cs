using DbManagerStandard;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ToDoList.Models
{
    public class Manage_User
    {
        static dbAccess con;
        static OracleConnection aOracleConnection;
        public static void RegesterUser(User_Model user)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                RegesterUser(CmdTrans, aOracleConnection, user);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                Close();
            }

        }


        public static void RegesterUser(OracleTransaction CmdTrans, OracleConnection aOracleConnection, User_Model user)
        {

            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"select req_seq.nextval as seq from dual";
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                user.Id = Convert.ToInt32(dt.Rows[0]["seq"].ToString());

            }
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"insert into  system.AppUser(Id,FullName,Address,Email,Password,IAgree,ConfirmPassword)
                 values (:id,:FullName,:Address,:Email,:Password,:IAgree,:ConfirmPassword)";
                cmd.CommandText = cmdText;
                cmd.Parameters.Add("id", user.Id);
                cmd.Parameters.Add("FullName", user.FullName);
                cmd.Parameters.Add("Address", user.Address);
                cmd.Parameters.Add("Email",user.Email );
                cmd.Parameters.Add("Password", user.Password);
                cmd.Parameters.Add("IAgree", user.IAgree?1:0);
                cmd.Parameters.Add("ConfirmPassword", user.ConfirmPassword);

                cmd.ExecuteNonQuery();
            }
            CmdTrans.Commit();
        }
        static void Open()
        {
            con = new dbAccess();
            aOracleConnection = con.get_con();
        }
        static void Close()
        {
            con.Close(aOracleConnection);
        }

    }
}
