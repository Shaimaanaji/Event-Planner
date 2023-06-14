using DbManagerStandard;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace ToDoList.Models
{
    public class Event
    {

        static dbAccess con;
        static OracleConnection aOracleConnection;


        public static List<EventPlanner> GetData()
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return GetData( CmdTrans, aOracleConnection);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Close();
            }
        }

        public static List<EventPlanner> GetData( OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            List<EventPlanner> lst = new List<EventPlanner>();

            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"select * from system.event";
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string title = dt.Rows[i]["Title"].ToString();
                        var DATE = (dt.Rows[i]["CreateDate"].ToString());
                        int id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                        string Location =dt.Rows[i]["Location"].ToString();
                        string Desc = dt.Rows[i]["Description"].ToString();

                        lst.Add(new EventPlanner() { Id = id, Title = title, CreateDate = DATE, Description = Desc, Location = Location });

                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }




        public static List<EventPlanner> FilterData(string t)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return FilterData(CmdTrans, aOracleConnection,t);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Close();
            }
        }

        public static List<EventPlanner> FilterData(OracleTransaction CmdTrans, OracleConnection aOracleConnection,string selectTitle)
        {
            List<EventPlanner> lst = new List<EventPlanner>();

            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"select * from system.event where Title=:title";
                cmd.CommandText = cmdText;
                cmd.Parameters.Add("title", selectTitle);
                cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string title = (dt.Rows[i]["Title"].ToString()); 
                        var DATE = (dt.Rows[i]["CreateDate"].ToString());
                        int id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                        string Location = dt.Rows[i]["Location"].ToString();
                        string Desc = dt.Rows[i]["Description"].ToString();
                        lst.Add(new EventPlanner() { Id = id, Title = title, CreateDate = DATE, Description = Desc, Location = Location });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }









        public static void InsertData(EventPlanner ev)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                 InsertData( CmdTrans, aOracleConnection,ev);

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


        public static void InsertData( OracleTransaction CmdTrans, OracleConnection aOracleConnection,EventPlanner e)
        {
           
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"select EVENT_SEQ.nextval as seq from dual";
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                e.Id = Convert.ToInt32(dt.Rows[0]["seq"].ToString());
              
            }
            { 
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"insert into  system.event(Id,Description,LOCATION,TITLE,CreateDate)
                 values (:id,:Description,:location,:Title,TO_DATE(:date_e, 'YYYY-MM-DD'))";
                cmd.CommandText = cmdText;
                cmd.Parameters.Add("id", e.Id);
                cmd.Parameters.Add("Description", e.Description);
                cmd.Parameters.Add("location", e.Location);
                cmd.Parameters.Add("Title", e.Title);
                cmd.Parameters.Add("date_e", e.CreateDate);
                cmd.ExecuteNonQuery();
            }
            CmdTrans.Commit();
        }



        public static void UpdateData(EventPlanner ev)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                UpdateData(CmdTrans, aOracleConnection, ev);

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


        public static void UpdateData(OracleTransaction CmdTrans, OracleConnection aOracleConnection, EventPlanner e)
        {
            List<EventPlanner> lst = new List<EventPlanner>();
            OracleCommand cmd = aOracleConnection.CreateCommand();
            cmd.Transaction = CmdTrans;
            cmd.CommandType = CommandType.Text;

            var cmdText = @"update system.event 
            set Location=:location,Title=:Title,Description=:Description,CreateDate=TO_DATE(:date_e, 'YYYY-MM-DD')
            where Id=:id";
            cmd.CommandText = cmdText;
            cmd.BindByName = true;
            cmd.Parameters.Add("id", e.Id);
            cmd.Parameters.Add("Description", e.Description);
            cmd.Parameters.Add("location", e.Location);
            cmd.Parameters.Add("Title", e.Title);
            cmd.Parameters.Add("date_e", e.CreateDate);
            cmd.ExecuteNonQuery();
            CmdTrans.Commit();

        }

        public static void DeleteData(int id)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                DeleteData(CmdTrans, aOracleConnection, id);

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


        public static void DeleteData(OracleTransaction CmdTrans, OracleConnection aOracleConnection, int Eid)
        {
            OracleCommand cmd = aOracleConnection.CreateCommand();
            cmd.Transaction = CmdTrans;
            cmd.CommandType = CommandType.Text;

            var cmdText = @"delete from system.event 
            where  Id=:id";
            cmd.CommandText = cmdText;
            cmd.Parameters.Add("id", Eid);   
            cmd.ExecuteNonQuery();
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


















