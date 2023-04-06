using System;
using System.Collections.Generic;

namespace SportsScoringSystem.Model
{
    [Serializable]
    public partial class TeamInfoShow
    {
        public TeamInfoShow()
        { }
        #region Model
        private int _id;
        private string _rname;
        private string _rlogo;
        private string _region;
        private string _leader;
        private string _instructor;
        private List<Model.Player> _players;
        /// <summary>
        /// 
        /// </summary>
        public int Id { get { return _id; } set { _id = value; } }
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
        /// <summary>
        ///
        /// </summary>
        public List<Model.Player> Players
        {
            set { _players = value; }
            get { return _players; }
        }
        #endregion Model

    }
}
