using System.Collections.Generic;
using UnityEngine;

namespace com.beiyou.snake.gameclient.socketdata
{
    //发送xml信息生成
    public class SendXmlHelper : MonoBehaviour
    {
        //生成用户登入xml
        public static string BuildUserLoginXml(string userName, string pwl)
        {
            string res = "<UserLogin><root>"
                + "<userName><![CDATA[" + userName + "]]></userName>"
                + "<passWord><![CDATA[" + pwl + "]]></passWord>"
                + "</root></UserLogin>";
            return res;
        }

        //生成用户自动入座xml
        public static string BuildAutoSitInfoXml(string userId)
        {
            string res = "<AutoSitInfoXml><root>"
                + "<userId><![CDATA[" + userId + "]]></userId>"
                + "</root></AutoSitInfoXml>";
            
            return res;
        }

        //生成排行榜数据请求xml
        public static string BuildRankListDataRequestXml(string userId,int first)
        {
            string res = "<RankListDataRequest><root>"
                + "<userId><![CDATA[" + userId + "]]></userId>"
                + "<first><![CDATA[" + first + "]]></first>"
                + "</root></RankListDataRequest>";

            return res;
        }

        //生成退出xml
        public static string BuildUserReturnXml()
        {
            string res = "<UserReturn><root>"
                + "</root></UserReturn>";
            return res;
        }

        //生成加入桌子xml
        public static string BuildJoinTableXml(string userId)
        {
            string res = "<JoinTable><root>"
                + "<userId><![CDATA[" + userId + "]]></userId>"
                + "</root></JoinTable>";
            return res;
        }

        //生成食物被吃xml
        public static string BuildFoodEatXml(string name)
        {
            string res = "<FoodEat><root>"
                + "<name><![CDATA[" + name + "]]></name>"
                + "</root></FoodEat>";
            return res;
        }

        //生成蛇信息xml
        //发送 摇杆(速度)信息
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

        //发送 蛇位置信息
        //生成蛇信息xml
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

        //生成蛇死亡xml
         public static string BuildSnakeDeathXml(string userId)
        {
            string res = "<SnakeDeath><root>"
                + "<userId><![CDATA[" + userId + "]]></userId>"
                + "</root></SnakeDeath>"
                ;
            return res;
        }

        //生成蛇结算xml
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