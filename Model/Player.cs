using System;

namespace SportsScoringSystem.Model
{
    [Serializable]
    public partial class Player
    {
        public Player()
        { }
        #region Model
        private int _id;
        private string _member;
        private string _pname;
        private string _first;
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
        public string Member
        {
            set { _member = value; }
            get { return _member; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Pname
        {
            set { _pname = value; }
            get { return _pname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string First
        {
            set { _first = value; }
            get { return _first; }
        }
        #endregion Model

    }
}
