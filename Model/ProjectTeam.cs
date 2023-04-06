using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsScoringSystem.Model
{
    [Serializable]
    public partial class ProjectTeam
    {
        public ProjectTeam()
        { }
        #region Model
        private int _id;
        private int _pid;
        private int _tid;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Pid
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Tid
        {
            set { _tid = value; }
            get { return _tid; }
        }
        #endregion Model

    }
}
