using SportsScoringSystem.DBUtility;
using SportsScoringSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsScoringSystem.DAL
{
    /// <summary>
    /// 数据访问类:Project
    /// </summary>
    public partial class Project
    {
        public Project()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string PName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [project]");
            strSql.Append(" where pname='" + PName + "' ");
            return DbHelperOleDb.Exists(strSql.ToString());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SportsScoringSystem.Model.Project model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            StringBuilder strSql3 = new StringBuilder();
            if (model.PName != null)
            {
                strSql1.Append("pname,");
                strSql2.Append("'" + model.PName + "',");
            }
            if (model.DateTime != null)
            {
                strSql1.Append("[datetime],");
                strSql2.Append("'" + model.DateTime + "',");
            }
            if (model.PType != null)
            {
                strSql1.Append("ptype,");
                strSql2.Append("'" + model.PType + "',");
            }
            if (model.Type != null)
            {
                strSql1.Append("[type],");
                strSql2.Append("'" + model.Type + "',");
            }
            if (model.TypeValue != null)
            {
                strSql1.Append("typevalue,");
                strSql2.Append("'" + model.TypeValue + "',");
            }
            if (model.Boxing != null)
            {
                strSql1.Append("boxing,");
                strSql2.Append("'" + model.Boxing + "',");
            }
            if (model.Apparatus != null)
            {
                strSql1.Append("apparatus,");
                strSql2.Append("'" + model.Apparatus + "',");
            }
            strSql.Append("insert into [project] (");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            //获取自增ID
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
        public bool Update(SportsScoringSystem.Model.Project model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [project] set ");
            if (model.PName != null)
            {
                strSql.Append("pname='" + model.PName + "',");
            }
            else
            {
                strSql.Append("pname= null ,");
            }
            if (model.DateTime != null)
            {
                strSql.Append("[datetime]='" + model.DateTime + "',");
            }
            else
            {
                strSql.Append("[datetime]= null ,");
            }
            if (model.PType != null)
            {
                strSql.Append("ptype='" + model.PType + "',");
            }
            else
            {
                strSql.Append("ptype= null ,");
            }
            if (model.Type != null)
            {
                strSql.Append("[type]='" + model.Type + "',");
            }
            else
            {
                strSql.Append("[type]= null ,");
            }
            if (model.TypeValue != null)
            {
                strSql.Append("typevalue='" + model.TypeValue + "',");
            }
            else
            {
                strSql.Append("typevalue= null ,");
            }
            if (model.Boxing != null)
            {
                strSql.Append("boxing='" + model.Boxing + "',");
            }
            else
            {
                strSql.Append("boxing= null ,");
            }
            if (model.Apparatus != null)
            {
                strSql.Append("apparatus='" + model.Apparatus + "',");
            }
            else
            {
                strSql.Append("apparatus= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ID=" + model.ID + " ");
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
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [project] ");
            strSql.Append(" where ID=" + ID + " ");
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
        public SportsScoringSystem.Model.Project GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" ID,pname,[datetime],ptype,[type],typevalue,boxing,apparatus ");
            strSql.Append(" from [project] ");
            strSql.Append(" where ID=" + ID + " ");
            SportsScoringSystem.Model.Project model = new SportsScoringSystem.Model.Project();
            DataSet ds = DbHelperOleDb.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.PName = ds.Tables[0].Rows[0]["pname"].ToString();
                //将时间格式为yyyy/MM/dd转化为DateTime时间格式
                DateTimeFormatInfo timeFormatInfo = new DateTimeFormatInfo();
                timeFormatInfo.ShortDatePattern = "yyyy/MM/dd";
                model.DateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["datetime"].ToString(),timeFormatInfo);
                model.PType = ds.Tables[0].Rows[0]["ptype"].ToString();
                model.Type = ds.Tables[0].Rows[0]["type"].ToString();
                model.TypeValue = ds.Tables[0].Rows[0]["typevalue"].ToString();
                model.Boxing = ds.Tables[0].Rows[0]["boxing"].ToString();
                model.Apparatus = ds.Tables[0].Rows[0]["apparatus"].ToString();
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
            strSql.Append("select [id],pname,[datetime],ptype,[type],typevalue,boxing,apparatus ");
            strSql.Append(" FROM [project] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }
        #endregion  Method
    }
}
