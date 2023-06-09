﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsScoringSystem.Model
{
    [Serializable]
    public partial class GameUmpire
    {
        public GameUmpire()
        { }
        #region Model
        private int _id;
        private int _gid;
        private int _uid;
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
        public int Gid
        {
            set { _gid = value; }
            get { return _gid; }
        }
        /// <summary>
        ///
        /// </summary>
        public int Uid
        {
            set { _uid = value; }
            get { return _uid; }
        }
        #endregion Model

    }
}
