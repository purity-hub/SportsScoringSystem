using SportsScoringSystem.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsScoringSystem.DAL
{
    /// <summary>
    /// 数据访问类:TeamPlayer
    /// </summary>
    public partial class TeamPlayer
    {
        public TeamPlayer()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Tid, int Pid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [team_player]");
            strSql.Append(" where tid=" + Tid + " and pid=" + Pid + " ");
            return DbHelperOleDb.Exists(strSql.ToString());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SportsScoringSystem.Model.TeamPlayer model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Tid != 0)
            {
                strSql1.Append("tid,");
                strSql2.Append("" + model.Tid + ",");
            }
            if (model.Pid != 0)
            {
                strSql1.Append("pid,");
                strSql2.Append("" + model.Pid + ",");
            }
            strSql.Append("insert into [team_player](");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            object obj = DbHelperOleDb.InsertSql(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SportsScoringSystem.Model.TeamPlayer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [team_player] set ");
            if (model.Tid != 0)
            {
                strSql.Append("tid=" + model.Tid + ",");
            }
            else
            {
                strSql.Append("tid= null ,");
            }
            if (model.Pid != 0)
            {
                strSql.Append("pid=" + model.Pid + ",");
            }
            else
            {
                strSql.Append("pid= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where tid=" + model.Tid + " and pid=" + model.Pid + " ");
            int rowsAffected = DbHelperOleDb.ExecuteSql(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Tid, int Pid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [team_player] ");
            strSql.Append(" where tid=" + Tid + " and pid=" + Pid + " ");
            int rowsAffected = DbHelperOleDb.ExecuteSql(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteByTid(int Tid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [team_player] ");
            strSql.Append(" where tid=" + Tid);
            int rowsAffected = DbHelperOleDb.ExecuteSql(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataSet GetListByTid(int Tid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [players].[id],[players].member,[players].pname,[players].first from [team_player],[players] where [team_player].Pid=[players].id and [team_player].Tid="+Tid);
            return DbHelperOleDb.Query(strSql.ToString());
        }
        #endregion Model
    }
}
