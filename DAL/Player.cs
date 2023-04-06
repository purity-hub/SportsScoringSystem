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
    /// 数据访问类:Player
    /// </summary>
    public partial class Player
    {
        public Player()
        { }
        #region  Method


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Member)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [players]");
            strSql.Append(" where member='" + Member + "' ");
            return DbHelperOleDb.Exists(strSql.ToString());
        }
        /// <summary>
        /// 增加一条数据  
        /// </summary>
        public int Add(SportsScoringSystem.Model.Player model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Member != null)
            {
                strSql1.Append("member,");
                strSql2.Append("'" + model.Member + "',");
            }
            if (model.Pname != null)
            {
                strSql1.Append("pname,");
                strSql2.Append("'" + model.Pname + "',");
            }
            if (model.First != null)
            {
                strSql1.Append("[first],");
                strSql2.Append("'" + model.First + "',");
            }
            strSql.Append("insert into [players](");
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
        public bool Update(SportsScoringSystem.Model.Player model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [players] set ");
            if (model.Member != null)
            {
                strSql.Append("member='" + model.Member + "',");
            }
            else
            {
                strSql.Append("member= null ,");
            }
            if (model.Pname != null)
            {
                strSql.Append("pname='" + model.Pname + "',");
            }
            else
            {
                strSql.Append("pname= null ,");
            }
            if (model.First != null)
            {
                strSql.Append("[first]='" + model.First + "',");
            }
            else
            {
                strSql.Append("[first]= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where [id]=" + model.Id);
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
        /// 根据Tid删除一条数据
        /// </summary>
        public bool DeleteByTid(int Tid)
        {
            DAL.TeamPlayer teamPlayer = new DAL.TeamPlayer();
            //根据Tid查询Pid
            DataSet dataSet = teamPlayer.GetListByTid(Tid);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from [players] ");
                strSql.Append(" where id=" + dataSet.Tables[0].Rows[i]["id"]);
                int rowsAffected = DbHelperOleDb.ExecuteSql(strSql.ToString());
            }
            return true;
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Memberlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [players] ");
            strSql.Append(" where member in (" + Memberlist + ")  ");
            int rows = DbHelperOleDb.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select member,pname,first ");
            strSql.Append(" FROM [players] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }
        #endregion  Method
    }
}
