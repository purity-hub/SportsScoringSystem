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
    /// 数据访问类:User
    /// </summary>
    public partial class User
    {
        public User()
        { }
        #region  Method


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string UserName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [user]");
            strSql.Append(" where username='" + UserName + "' ");
            return DbHelperOleDb.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SportsScoringSystem.Model.User model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.UserName != null)
            {
                strSql1.Append("username,");
                strSql2.Append("'" + model.UserName + "',");
            }
            if (model.password != null)
            {
                strSql1.Append("password,");
                strSql2.Append("'" + model.password + "',");
            }
            if (model.Type != null)
            {
                strSql1.Append("type,");
                strSql2.Append("'" + model.Type + "',");
            }
            strSql.Append("insert into [user](");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
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
        /// 更新一条数据
        /// </summary>
        public bool Update(SportsScoringSystem.Model.User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [user] set ");
            if (model.password != null)
            {
                strSql.Append("password='" + model.password + "',");
            }
            else
            {
                strSql.Append("password= null ,");
            }
            if (model.Type != null)
            {
                strSql.Append("type='" + model.Type + "',");
            }
            else
            {
                strSql.Append("type= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where username='" + model.UserName + "' ");
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
        public bool Delete(string UserName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [user] ");
            strSql.Append(" where username='" + UserName + "' ");
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string UserNamelist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [user] ");
            strSql.Append(" where username in (" + UserNamelist + ")  ");
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
        /// 得到一个对象实体
        /// </summary>
        public SportsScoringSystem.Model.User GetModel(string UserName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select");
            strSql.Append(" username,password,type ");
            strSql.Append(" from [user]");
            strSql.Append(" where username='" + UserName + "' ");
            SportsScoringSystem.Model.User model = new SportsScoringSystem.Model.User();
            DataSet ds = DbHelperOleDb.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["username"] != null && ds.Tables[0].Rows[0]["username"].ToString() != "")
                {
                    model.UserName = ds.Tables[0].Rows[0]["username"].ToString();
                }
                if (ds.Tables[0].Rows[0]["password"] != null && ds.Tables[0].Rows[0]["password"].ToString() != "")
                {
                    model.password = ds.Tables[0].Rows[0]["password"].ToString();
                }
                if (ds.Tables[0].Rows[0]["type"] != null && ds.Tables[0].Rows[0]["type"].ToString() != "")
                {
                    model.Type = ds.Tables[0].Rows[0]["type"].ToString();
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
            strSql.Append("select username,password,type ");
            strSql.Append(" FROM [user] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM [user] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperOleDb.GetSingle(strSql.ToString());
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
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.username desc");
            }
            strSql.Append(")AS Row, T.*  from [user] T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperOleDb.Query(strSql.ToString());
        }
        #endregion  Method
    }
}
