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
    /// 数据访问类:ProjectTeam
    /// </summary>
    public partial class ProjectTeam
    {
        public void ProjectTeamroject()
        { }
        //select * from project,teaminfo,project_team where project.id=project_team.pid and project_team.tid=team.id
        #region  Method
        /// <summary>
        /// 是否存在该条记录
        /// </summary>
        public bool Exists(int Tid, int Pid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [project_team]");
            strSql.Append(" where tid=" + Tid + " and pid=" + Pid + " ");
            return DbHelperOleDb.Exists(strSql.ToString());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SportsScoringSystem.Model.ProjectTeam model)
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
            strSql.Append("insert into [project_team](");
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
        public bool Update(SportsScoringSystem.Model.ProjectTeam model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [project_team] set ");
            if (model.Pid != 0)
            {
                strSql.Append("pid=" + model.Pid + ",");
            }
            else
            {
                strSql.Append("pid= null ,");
            }
            if (model.Tid != 0)
            {
                strSql.Append("tid=" + model.Tid + ",");
            }
            else
            {
                strSql.Append("tid= null ,");
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
            strSql.Append("delete from [project_team] ");
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
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteByPid(int Pid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [project_team] ");
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
        /// 通过项目ID查询全部Team数据集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public DataSet GetListByPid(int projectId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [teaminfo].[id],rname,rlogo,region,leader,instructor ");
            strSql.Append(" FROM [project_team],[teaminfo] where [project_team].tid=[teaminfo].id and [project_team].pid="+projectId);
            return DbHelperOleDb.Query(strSql.ToString());
        } 
        #endregion Model
    }
}
