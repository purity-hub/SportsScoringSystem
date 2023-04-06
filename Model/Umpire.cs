using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsScoringSystem.Model
{
    [Serializable]
    public partial class Umpire
    {
        public Umpire()
        { }
        #region Model
        private int _id;
        private string _type;
        private string _uname;
        private string _country;
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
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        ///
        /// </summary>
        public string Uname
        {
            set { _uname = value; }
            get { return _uname; }
        }
        /// <summary>
        ///
        /// </summary>
        public string Country
        {
            set { _country = value; }
            get { return _country; }
        }
        #endregion Model

    }
}
