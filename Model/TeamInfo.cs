using System;

namespace SportsScoringSystem.Model
{
    [Serializable]
    public partial class TeamInfo
    {
        public TeamInfo()
        { }
        #region Model
        private int _id;
        private string _rname;
        private string _rlogo;
        private string _region;
        private string _leader;
        private string _instructor;
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
        public string Rname
        {
            set { _rname = value; }
            get { return _rname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Rlogo
        {
            set { _rlogo = value; }
            get { return _rlogo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Region
        {
            set { _region = value; }
            get { return _region; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Leader
        {
            set { _leader = value; }
            get { return _leader; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Instructor
        {
            set { _instructor = value; }
            get { return _instructor; }
        }
        #endregion Model

    }
}
