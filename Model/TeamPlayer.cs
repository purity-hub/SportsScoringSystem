using System;

namespace SportsScoringSystem.Model
{
    [Serializable]
    public partial class TeamPlayer
    {
        public TeamPlayer()
        { }
        #region Model
        private int _id;
        private int _tid;
        private int _pid;
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
        public int Tid
        {
            set { _tid = value; }
            get { return _tid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Pid
        {
            set { _pid = value; }
            get { return _pid; }
        }
        #endregion Model

    }
}
