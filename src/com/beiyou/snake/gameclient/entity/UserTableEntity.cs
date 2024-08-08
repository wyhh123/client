using System.Collections.Generic;


namespace com.beiyou.snake.gameclient.entity
{
    //ÓÎÏ·×ÀÉèÖÃ
    public class UserTableEntity
    {

        private List<OneUserEntity> chairInfoList = null;

        public UserTableEntity()
        {
            chairInfoList = new List<OneUserEntity>();

            for (int i = 0; i < 2; i++)
            {
                OneUserEntity oneUserEntity = new OneUserEntity();
                chairInfoList.Add(oneUserEntity);
            }
        }

        public void SetOneUserEntity(int chairId, int userId, string userName)
        {
            OneUserEntity oneUserEntity = chairInfoList[chairId];
            oneUserEntity.SetUid(userId);
            oneUserEntity.SetUsername(userName);
            chairInfoList[chairId] = oneUserEntity;
        }
        public void GetOneUserEntity()
        {

        }

        public int GetUserIdByChairId(int chairId)
        {
            int value = -1;
            OneUserEntity oneUserEntity = chairInfoList[chairId];
            if (oneUserEntity != null)
            {
                value = oneUserEntity.GetUid();
            }
            return value;
        }
        public string GetUserNameByChairId(int chairId)
        {
            string value = "";
            OneUserEntity oneUserEntity = chairInfoList[chairId];
            if (oneUserEntity != null)
            {
                value = oneUserEntity.GetUsername();
            }
            return value;
        }

    }

}
