using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsScoringSystem.Model
{
    [Serializable]
    public partial class ProjectGame
    {
        public ProjectGame()
        { }
        #region Model
        private int _id;
        private int _pid;
        private int _gid;
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
        public int Gid
        {
            set { _gid = value; }
            get { return _gid; }
        }
        
        #endregion Model

    }
}
