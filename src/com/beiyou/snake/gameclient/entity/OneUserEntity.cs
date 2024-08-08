using System.Collections.Generic;

namespace com.beiyou.snake.gameclient.entity
{
    //用户信息
    public class OneUserEntity
    {
        //座位号 id 用户名 密码
        private int chairId = -1;
        private int uid = -1;
        private string username = "";
        private string pwd = "";

        public int GetChairId()
        {
            return this.chairId;
        }
        public void SetChairId(int chairId)
        {
            this.chairId = chairId;
        }

        public int GetUid()
        {
            return this.uid;
        }
        public void SetUid(int uid)
        {
            this.uid = uid;
        }

        public string GetUsername()
        {
            return this.username;
        }
        public void SetUsername(string username)
        {
            this.username = username;
        }

        public string GetPwd()
        {
            return this.pwd;
        }
        public void SetPwd(string pwd)
        {
            this.pwd = pwd;
        }

    }

}