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
    /// 数据访问类:GameUmpire
    /// </summary>
    public partial class GameUmpire
    {
        public GameUmpire()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Gid, int Uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [game_umpire]");
            strSql.Append(" where gid=" + Gid + " and uid=" + Uid + " ");
            return DbHelperOleDb.Exists(strSql.ToString());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SportsScoringSystem.Model.GameUmpire model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Gid != 0)
            {
                strSql1.Append("gid,");
                strSql2.Append("" + model.Gid + ",");
            }
            if (model.Uid != 0)
            {
                strSql1.Append("uid,");
                strSql2.Append("" + model.Uid + ",");
            }
            strSql.Append("insert into [game_umpire](");
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
        public bool Update(SportsScoringSystem.Model.GameUmpire model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [game_umpire] set ");
            if (model.Gid != 0)
            {
                strSql.Append("gid=" + model.Gid + ",");
            }
            if (model.Uid != 0)
            {
                strSql.Append("uid=" + model.Uid + ",");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where gid=" + model.Gid + " and uid=" + model.Uid + " ");
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
        public bool Delete(int Gid, int Uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [game_umpire] ");
            strSql.Append(" where gid=" + Gid + " and uid=" + Uid + " ");
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
        public bool DeleteByGid(int Gid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [game_umpire] ");
            strSql.Append(" where gid=" + Gid);
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
        public SportsScoringSystem.Model.GameUmpire GetModel(int Gid, int Uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" gid,uid ");
            strSql.Append(" from [game_umpire] ");
            strSql.Append(" where gid=" + Gid + " and uid=" + Uid + " ");
            SportsScoringSystem.Model.GameUmpire model = new SportsScoringSystem.Model.GameUmpire();
            DataSet ds = DbHelperOleDb.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["gid"].ToString() != "")
                {
                    model.Gid = int.Parse(ds.Tables[0].Rows[0]["gid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["uid"].ToString() != "")
                {
                    model.Uid = int.Parse(ds.Tables[0].Rows[0]["uid"].ToString());
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
            strSql.Append("select gid,uid ");
            strSql.Append(" FROM [game_umpire] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" gid,uid ");
            strSql.Append(" FROM [game_umpire] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperOleDb.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SportsScoringSystem.Model.GameUmpire> GetModelList(string strWhere)
        {
            DataSet ds = GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SportsScoringSystem.Model.GameUmpire> DataTableToList(DataTable dt)
        {
            List<SportsScoringSystem.Model.GameUmpire> modelList = new List<SportsScoringSystem.Model.GameUmpire>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SportsScoringSystem.Model.GameUmpire model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SportsScoringSystem.Model.GameUmpire();
                    if (dt.Rows[n]["gid"].ToString() != "")
                    {
                        model.Gid = int.Parse(dt.Rows[n]["gid"].ToString());
                    }
                    if (dt.Rows[n]["uid"].ToString() != "")
                    {
                        model.Uid = int.Parse(dt.Rows[n]["uid"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion Model
    }
}
