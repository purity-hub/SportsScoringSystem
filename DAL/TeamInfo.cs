using SportsScoringSystem.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SportsScoringSystem.DAL
{
    /// <summary>
    /// 数据访问类:Player
    /// </summary>
    public partial class TeamInfo
    {
        public TeamInfo()
        { }
        #region  Method


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Rname)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [teaminfo]");
            strSql.Append(" where rname='" + Rname + "' ");
            return DbHelperOleDb.Exists(strSql.ToString());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SportsScoringSystem.Model.TeamInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Rname != null)
            {
                strSql1.Append("rname,");
                strSql2.Append("'" + model.Rname + "',");
            }
            if (model.Rlogo != null)
            {
                strSql1.Append("rlogo,");
                strSql2.Append("'" + model.Rlogo + "',");
            }
            if (model.Region != null)
            {
                strSql1.Append("region,");
                strSql2.Append("'" + model.Region + "',");
            }
            if (model.Leader != null)
            {
                strSql1.Append("leader,");
                strSql2.Append("'" + model.Leader + "',");
            }
            if (model.Instructor != null)
            {
                strSql1.Append("instructor,");
                strSql2.Append("'" + model.Instructor + "',");
            }
            strSql.Append("insert into [teaminfo](");
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
        /// 更新
        /// </summary>
        /// <returns></returns>
        public bool Update(Model.TeamInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [teaminfo] set ");
            if (model.Rname != null)
            {
                strSql.Append("rname='" + model.Rname + "',");
            }
            else
            {
                strSql.Append("rname= null ,");
            }
            if (model.Rlogo != null)
            {
                strSql.Append("rlogo='" + model.Rlogo + "',");
            }
            else
            {
                strSql.Append("rlogo= null ,");
            }
            if (model.Region != null)
            {
                strSql.Append("region='" + model.Region + "',");
            }
            else
            {
                strSql.Append("region= null ,");
            }
            if (model.Leader != null)
            {
                strSql.Append("leader='" + model.Leader + "',");
            }
            else
            {
                strSql.Append("leader= null ,");
            }
            if (model.Instructor != null)
            {
                strSql.Append("instructor='" + model.Instructor + "',");
            }
            else
            {
                strSql.Append("instructor= null ,");
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
        /// 根据项目删除项目下的全部数据
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public bool DeleteByPid(int projectId)
        {
            
            DAL.ProjectTeam projectTeam = new DAL.ProjectTeam();
            DataSet dataSet = projectTeam.GetListByPid(projectId);
            if (dataSet != null)
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("delete from [teaminfo] ");
                    strSql.Append(" where [id]=" + dataSet.Tables[0].Rows[i]["id"]);
                    int rowsAffected = DbHelperOleDb.ExecuteSql(strSql.ToString());
                }
                return true;
            }
            else
            {
                return false;
            }
            
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM [teaminfo] ");
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
        ///     
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [teaminfo] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }
        ///<summary>
        ///根据project_id查询相应的teaminfo信息
        /// </summary>
        public DataSet GetTeamInfoById(int projectId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [teaminfo].[id],[teaminfo].rname,[teaminfo].rlogo,[teaminfo].region,[teaminfo].leader,[teaminfo].instructor ");
            strSql.Append("From [teaminfo],[project],[project_team] ");
            strSql.Append("where [teaminfo].[id]=[project_team].tid and [project].[id]=[project_team].pid ");         
            strSql.Append(" and [project].[id]=" + projectId);
            return DbHelperOleDb.Query(strSql.ToString());
        }
        ///<summary>
        ///根据team_id查询队员信息
        /// </summary>
        public DataSet GetPlayersById(int teamId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [players].id,[players].member,[players].pname,[players].first ");
            strSql.Append("From [teaminfo],[players],[team_player] ");
            strSql.Append("where [teaminfo].[id]=[team_player].tid and [players].id=[team_player].pid ");
            strSql.Append("and [teaminfo].id = " + teamId);
            return DbHelperOleDb.Query(strSql.ToString());
        }
        #endregion  Method
    }
}
