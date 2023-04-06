using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsScoringSystem.Model
{
    [Serializable]
    public partial class Game
    {
        public Game()
        { }
        #region Model
        private int _id;
        private string _time;
        private string _team1;
        private string _team2;
        private string _color1;
        private string _color2;
        private string _clothes1;
        private string _clothes2;
        private string _score1;
        private string _score2;
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
        public string Time
        {
            set { _time = value; }
            get { return _time; }
        }
        /// <summary>
        ///
        /// </summary>
        public string Team1
        {
            set { _team1 = value; }
            get { return _team1; }
        }
        /// <summary>
        ///
        /// </summary>
        public string Team2
        {
            set { _team2 = value; }
            get { return _team2; }
        }
        /// <summary>
        ///
        /// </summary>
        public string Color1
        {
            set { _color1 = value; }
            get { return _color1; }
        }
        /// <summary>
        ///
        /// </summary>
        public string Color2
        {
            set { _color2 = value; }
            get { return _color2; }
        }
        /// <summary>
        ///
        /// </summary>
        public string Clothes1
        {
            set { _clothes1 = value; }
            get { return _clothes1; }
        }
        /// <summary>
        ///
        /// </summary>
        public string Clothes2
        {
            set { _clothes2 = value; }
            get { return _clothes2; }
        }

        /// <summary>
        ///
        /// </summary>
        public string Score1
        {
            set { _score1 = value; }
            get { return _score1; }
        }

        /// <summary>
        ///
        /// </summary>
        public string Score2
        {
            set { _score2 = value; }
            get { return _score2; }
        }
        #endregion Model

    }
}
