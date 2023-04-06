using System;

namespace SportsScoringSystem.Model
{

    /// <summary>
    /// t_Users:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class User
    {
        public User()
        { }
        #region Model
        private string _username;
        private string _password;
        private string _type;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        #endregion Model

    }
}
