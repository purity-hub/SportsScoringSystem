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
    /// 数据访问类:ProjectGame
    /// </summary>
    public partial class ProjectGame
    {
        public ProjectGame()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Pid, int Gid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [project_game]");
            strSql.Append(" where pid=" + Pid + " and gid=" + Gid + " ");
            return DbHelperOleDb.Exists(strSql.ToString());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SportsScoringSystem.Model.ProjectGame model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Pid != 0)
            {
                strSql1.Append("pid,");
                strSql2.Append("" + model.Pid + ",");
            }
            if (model.Gid != 0)
            {
                strSql1.Append("gid,");
                strSql2.Append("" + model.Gid + ",");
            }
            strSql.Append("insert into [project_game](");
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
        public bool Update(SportsScoringSystem.Model.ProjectGame model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [project_game] set ");
            if (model.Pid != 0)
            {
                strSql.Append("pid=" + model.Pid + ",");
            }
            if (model.Gid != 0)
            {
                strSql.Append("gid=" + model.Gid + ",");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where pid=" + model.Pid + " and gid=" + model.Gid + " ");
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
        public bool Delete(int Pid, int Gid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [project_game] ");
            strSql.Append(" where pid=" + Pid + " and gid=" + Gid + " ");
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
        public bool Delete(int Pid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [project_game] ");
            strSql.Append(" where pid=" + Pid);
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
        /// 得到一个对象实体
        /// </summary>
        public SportsScoringSystem.Model.ProjectGame GetModel(int Pid, int Gid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" pid,gid ");
            strSql.Append(" from [project_game] ");
            strSql.Append(" where pid=" + Pid + " and gid=" + Gid + " ");
            SportsScoringSystem.Model.ProjectGame model = new SportsScoringSystem.Model.ProjectGame();
            DataSet ds = DbHelperOleDb.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["pid"].ToString() != "")
                {
                    model.Pid = int.Parse(ds.Tables[0].Rows[0]["pid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["gid"].ToString() != "")
                {
                    model.Gid = int.Parse(ds.Tables[0].Rows[0]["gid"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select pid,gid ");
            strSql.Append(" FROM [project_game] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }
        #endregion Model
    }
}
