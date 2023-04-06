using SportsScoringSystem.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SportsScoringSystem.DAL
{
    /// <summary>
    /// 数据访问类:Game
    /// </summary>
    public partial class Game
    {
        public Game()
        { }
        #region  Method


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [game]");
            strSql.Append(" where id=" + Id);
            return DbHelperOleDb.Exists(strSql.ToString());
        }
        /// <summary>
        /// 增加一条数据  
        /// </summary>
        public int Add(SportsScoringSystem.Model.Game model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Time != null)
            {
                strSql1.Append("[time],");
                strSql2.Append("'" + model.Time + "',");
            }
            if (model.Team1 != null)
            {
                strSql1.Append("team1,");
                strSql2.Append("'" + model.Team1 + "',");
            }
            if (model.Team2 != null)
            {
                strSql1.Append("team2,");
                strSql2.Append("'" + model.Team2 + "',");
            }
            if (model.Color1 != null)
            {
                strSql1.Append("color1,");
                strSql2.Append("'" + model.Color1 + "',");
            }
            if (model.Color2 != null)
            {
                strSql1.Append("color2,");
                strSql2.Append("'" + model.Color2 + "',");
            }
            if (model.Clothes1 != null)
            {
                strSql1.Append("clothes1,");
                strSql2.Append("'" + model.Clothes1 + "',");
            }
            if (model.Clothes2 != null)
            {
                strSql1.Append("clothes2,");
                strSql2.Append("'" + model.Clothes2 + "',");
            }
            strSql.Append("insert into [game](");
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
        ///<summary>
        ///通过projectId得到列表
        /// </summary>
        public DataSet GetListByPid(int projectId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [game].[id],[time],team1,team2,color1,color2,clothes1,clothes2 ");
            strSql.Append(" FROM [game],[project_game] where [game].[id] = [project_game].gid ");
            strSql.Append(" and [project_game].pid = " + projectId);
            return DbHelperOleDb.Query(strSql.ToString());
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(Model.Game model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [game] set ");
            if (model.Time != null)
            {
                strSql.Append("[time]='" + model.Time + "',");
            }
            else
            {
                strSql.Append("[time]= null ,");
            }
            if (model.Team1 != null)
            {
                strSql.Append("team1='" + model.Team1 + "',");
            }
            else
            {
                strSql.Append("team1= null ,");
            }
            if (model.Team2 != null)
            {
                strSql.Append("team2='" + model.Team2 + "',");
            }
            else
            {
                strSql.Append("team2= null ,");
            }
            if (model.Color1 != null)
            {
                strSql.Append("color1='" + model.Color1 + "',");
            }
            else
            {
                strSql.Append("color1= null ,");
            }
            if (model.Color2 != null)
            {
                strSql.Append("color2='" + model.Color2 + "',");
            }
            else
            {
                strSql.Append("color2= null ,");
            }
            if (model.Clothes1 != null)
            {
                strSql.Append("clothes1='" + model.Clothes1 + "',");
            }
            else
            {
                strSql.Append("clothes1= null ,");
            }
            if (model.Clothes2 != null)
            {
                strSql.Append("clothes2='" + model.Clothes2 + "',");
            }
            else
            {
                strSql.Append("clothes2= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ID=" + model.Id + " ");
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
        /// 删除通过id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteById(int id)
        {
            DAL.TeamPlayer teamPlayer = new DAL.TeamPlayer();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [game] ");
            strSql.Append(" where id=" + id);
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

        #endregion  Method
    }
}
