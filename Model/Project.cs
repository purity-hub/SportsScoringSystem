using System;

namespace SportsScoringSystem.Model
{
    [Serializable]
    public partial class Project
    {
        public Project()
        { }
        #region Model
        private int _id;
        private string pname;
        private DateTime datetime;
        private string _ptype;
        private string _type;
        private string _typevalue;
        private string _boxing;
        private string _apparatus;
        
        /// <summary>
        ///
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        ///
        /// </summary>
        public string PName
        {
            set { pname = value; }
            get { return pname; }
        }
        /// <summary>
        ///
        /// </summary>
        public DateTime DateTime
        {
            set { datetime = value; }
            get { return datetime; }
        }
        /// <summary>
        ///
        /// </summary>
        public string PType
        {
            set { _ptype = value; }
            get { return _ptype; }
        }
        /// <summary>
        ///
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        ///
        /// </summary>
        public string TypeValue
        {
            set { _typevalue = value; }
            get { return _typevalue; }
        }
        /// <summary>
        ///
        /// </summary>
        public string Boxing
        {
            set { _boxing = value; }
            get { return _boxing; }
        }
        /// <summary>
        ///
        /// </summary>
        public string Apparatus
        {
            set { _apparatus = value; }
            get { return _apparatus; }
        }
        #endregion Model

    }
}
