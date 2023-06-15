using DbManagerStandard;
using Microsoft.AspNetCore.Authorization;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ToDoList.Models
{
    [AllowAnonymous]

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
            OracleCommand checkCmd = aOracleConnection.CreateCommand();
            checkCmd.Transaction = CmdTrans;
            checkCmd.CommandType = CommandType.Text;
            var checkCmdText = @"SELECT COUNT(*) FROM system.AppUser WHERE Email = :Email";
            checkCmd.CommandText = checkCmdText;
            checkCmd.Parameters.Add("Email", user.Email);
            int emailCount = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (emailCount > 0)
            {

                throw new Exception("Email already exists.");
                return;


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





        public static void LoginUser(Login_Model user)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                LoginUser(CmdTrans, aOracleConnection, user);

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
        public static void LoginUser(OracleTransaction CmdTrans, OracleConnection aOracleConnection, Login_Model user)
        {

            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"select login_seq.nextval as log_seq from dual";
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                user.Id = Convert.ToInt32(dt.Rows[0]["log_seq"].ToString());
            }
            OracleCommand checkCmd = aOracleConnection.CreateCommand();
            checkCmd.Transaction = CmdTrans;
            checkCmd.CommandType = CommandType.Text;
            var checkCmdText = @"SELECT COUNT(*) FROM system.AppUser WHERE Email = :Email";
            checkCmd.CommandText = checkCmdText;
            checkCmd.Parameters.Add("Email", user.Email);
            int emailCount = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (emailCount > 0)
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"insert into  system.Login(Id,Email,Password,Remmemberme)
                 values (:id,:Email,:Password,:Remmemberme)";
                cmd.CommandText = cmdText;
                cmd.Parameters.Add("id", user.Id);
                cmd.Parameters.Add("Email", user.Email);
                cmd.Parameters.Add("Password", user.Password);
                cmd.Parameters.Add("Remmemberme", user.RemmemberMe ? 1 : 0);
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
