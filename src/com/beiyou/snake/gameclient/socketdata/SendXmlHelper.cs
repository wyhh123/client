using System.Collections.Generic;
using UnityEngine;

namespace com.beiyou.snake.gameclient.socketdata
{
    //����xml��Ϣ����
    public class SendXmlHelper : MonoBehaviour
    {
        //�����û�����xml
        public static string BuildUserLoginXml(string userName, string pwl)
        {
            string res = "<UserLogin><root>"
                + "<userName><![CDATA[" + userName + "]]></userName>"
                + "<passWord><![CDATA[" + pwl + "]]></passWord>"
                + "</root></UserLogin>";
            return res;
        }

        //�����û��Զ�����xml
        public static string BuildAutoSitInfoXml(string userId)
        {
            string res = "<AutoSitInfoXml><root>"
                + "<userId><![CDATA[" + userId + "]]></userId>"
                + "</root></AutoSitInfoXml>";
            
            return res;
        }

        //�������а���������xml
        public static string BuildRankListDataRequestXml(string userId,int first)
        {
            string res = "<RankListDataRequest><root>"
                + "<userId><![CDATA[" + userId + "]]></userId>"
                + "<first><![CDATA[" + first + "]]></first>"
                + "</root></RankListDataRequest>";

            return res;
        }

        //�����˳�xml
        public static string BuildUserReturnXml()
        {
            string res = "<UserReturn><root>"
                + "</root></UserReturn>";
            return res;
        }

        //���ɼ�������xml
        public static string BuildJoinTableXml(string userId)
        {
            string res = "<JoinTable><root>"
                + "<userId><![CDATA[" + userId + "]]></userId>"
                + "</root></JoinTable>";
            return res;
        }

        //����ʳ�ﱻ��xml
        public static string BuildFoodEatXml(string name)
        {
            string res = "<FoodEat><root>"
                + "<name><![CDATA[" + name + "]]></name>"
                + "</root></FoodEat>";
            return res;
        }

        //��������Ϣxml
        //���� ҡ��(�ٶ�)��Ϣ
        //public static string BuildSnakeMsgXml(string userId,int length, Vector2 vec)
        //{
        //    string res = "<SnakeMsg><root>"
        //        + "<userId><![CDATA[" + userId + "]]></userId>"
        //        + "<length><![CDATA[" + length + "]]></length>"
        //        + "<vecx><![CDATA[" + vec.x + "]]></vecx>"
        //        + "<vecy><![CDATA[" + vec.y + "]]></vecy>"
        //        + "</root></SnakeMsg>"
        //        ;
        //    return res;
        //}

        //���� ��λ����Ϣ
        //��������Ϣxml
        public static string BuildSnakeMsgXml(string userId,float angle ,float x,float y,int length)
        {
            string res = "<SnakeMsg><root>"
                + "<userId><![CDATA[" + userId + "]]></userId>"
                + "<angle><![CDATA[" + angle + "]]></angle>"
                + "<x><![CDATA[" + x + "]]></x>"
                + "<y><![CDATA[" + y + "]]></y>"
                + "<length><![CDATA[" + length + "]]></length>"
                + "</root></SnakeMsg>"
                ;
            return res;
        }

        //����������xml
         public static string BuildSnakeDeathXml(string userId)
        {
            string res = "<SnakeDeath><root>"
                + "<userId><![CDATA[" + userId + "]]></userId>"
                + "</root></SnakeDeath>"
                ;
            return res;
        }

        //�����߽���xml
        public static string BuildJiesuanMsgXml(string userId,int score)
        {
            string res = "<JiesuanMsg><root>"
                + "<userId><![CDATA[" + userId + "]]></userId>"
                + "<score><![CDATA[" + score + "]]></score>"
                + "</root></JiesuanMsg>"
                ;
            return res;
        }

    }


}