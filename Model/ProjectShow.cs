using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsScoringSystem.Model
{
    [Serializable]
    public partial class ProjectShow
    {
        public ProjectShow()
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
        private List<TeamInfoShow> _teaminfoshow;

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
        /// <summary>
        ///
        /// </summary>
        public List<TeamInfoShow> TeamInfoShow
        {
            set { _teaminfoshow = value; }
            get { return _teaminfoshow; }
        }
        #endregion Model

    }
}
