using SportsScoringSystem.DBUtility;
using SportsScoringSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsScoringSystem.DAL
{
    /// <summary>
    /// 数据访问类:Umpire
    /// </summary>
    public partial class Umpire
    {
        public Umpire()
        { }
        #region  Method


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [umpire]");
            strSql.Append(" where id=" + Id);
            return DbHelperOleDb.Exists(strSql.ToString());
        }
        /// <summary>
        /// 增加一条数据  
        /// </summary>
        public int Add(SportsScoringSystem.Model.Umpire model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Type != null)
            {
                strSql1.Append("type,");
                strSql2.Append("'" + model.Type + "',");
            }
            if (model.Uname != null)
            {
                strSql1.Append("uname,");
                strSql2.Append("'" + model.Uname + "',");
            }
            if (model.Country != null)
            {
                strSql1.Append("country,");
                strSql2.Append("'" + model.Country + "',");
            }
            strSql.Append("insert into [umpire](");
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
        /// 通过比赛id查询列表
        /// </summary>
        /// <param name="Gid"></param>
        /// <returns></returns>
        public DataSet GetListByGid(int Gid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  [umpire].[id],[type],uname,country ");
            strSql.Append(" FROM [umpire],[game_umpire] where [umpire].id = [game_umpire].uid ");
            strSql.Append(" and [game_umpire].gid = " + Gid);
            return DbHelperOleDb.Query(strSql.ToString());
        }
        /// <summary>
        /// 通过game_id删除裁判
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteByGid(int Gid)
        {
            //1、通过Gid查询全部的裁判
            DataSet dataSet = GetListByGid(Gid);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from [umpire] ");
                strSql.Append(" where id=" + dataSet.Tables[0].Rows[i]["id"]);
                int rowsAffected = DbHelperOleDb.ExecuteSql(strSql.ToString());
            }
            return true;
        }

        #endregion  Method
    }
}
