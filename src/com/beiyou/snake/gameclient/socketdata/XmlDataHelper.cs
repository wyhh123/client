using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace com.beiyou.snake.gameclient.socketdata
{
    //接收xml处理
    public class XmlDataHelper : MonoBehaviour
    {
        //获取节点值
        private static string GetNodeValue(XmlNode tempNode)
        {
            string res = "";
            if (tempNode.FirstChild != null)
            {
                res = tempNode.FirstChild.Value;
            }
            return res;
        }

        //判断获取登录信息
        public static List<string> IsLoginInfoXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//是否保留空白符
            reXML_xml.LoadXml(xmlStr);//加载xml
            string tempStr = "";
            tempList.Add("LoginSuccess");

            for (XmlNode tempNode = reXML_xml.FirstChild.FirstChild; tempNode != null; tempNode = tempNode.NextSibling)
            {
                if (tempNode.HasChildNodes)
                {
                    if (tempNode.ChildNodes.Count > 0)
                    {
                        for (XmlNode temp = tempNode.FirstChild; temp != null; temp = temp.NextSibling)
                        {
                            if (temp.NodeType == XmlNodeType.Element)
                            {
                                if (temp.Name == "success")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                                else if (temp.Name == "userId")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                            }
                        }
                    }
                }
            }
            return tempList;
        }

        //判断获取异地登录信息
        public static List<string> IsYiDiLoginMsgXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//是否保留空白符
            reXML_xml.LoadXml(xmlStr);//加载xml
            string tempStr = "";
            tempList.Add("YiDiLoginMsg");

            for (XmlNode tempNode = reXML_xml.FirstChild.FirstChild; tempNode != null; tempNode = tempNode.NextSibling)
            {
                if (tempNode.HasChildNodes)
                {
                    if (tempNode.ChildNodes.Count > 0)
                    {
                        for (XmlNode temp = tempNode.FirstChild; temp != null; temp = temp.NextSibling)
                        {
                            if (temp.NodeType == XmlNodeType.Element)
                            {
                                if (temp.Name == "userId")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                            }
                        }
                    }
                }
            }
            return tempList;
        }

        //判断获取自动入座信息
        public static List<string> IsAutoSitInfoXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//是否保留空白符
            reXML_xml.LoadXml(xmlStr);//加载xml
            string tempStr = "";
            tempList.Add("AutoSitInfo");

            for (XmlNode tempNode = reXML_xml.FirstChild.FirstChild; tempNode != null; tempNode = tempNode.NextSibling)
            {
                if (tempNode.HasChildNodes)
                {
                    if (tempNode.ChildNodes.Count > 0)
                    {
                        for (XmlNode temp = tempNode.FirstChild; temp != null; temp = temp.NextSibling)
                        {
                            if (temp.NodeType == XmlNodeType.Element)
                            {
                                if (temp.Name == "allSitUserInfo")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                                else if (temp.Name == "sitType")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                            }
                        }
                    }
                }
            }
            return tempList;
        }

        //判断获取排行榜信息
        public static List<string> IsRankListXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//是否保留空白符
            reXML_xml.LoadXml(xmlStr);//加载xml
            string tempStr = "";
            tempList.Add("RankList");

            for (XmlNode tempNode = reXML_xml.FirstChild.FirstChild; tempNode != null; tempNode = tempNode.NextSibling)
            {
                if (tempNode.HasChildNodes)
                {
                    if (tempNode.ChildNodes.Count > 0)
                    {
                        for (XmlNode temp = tempNode.FirstChild; temp != null; temp = temp.NextSibling)
                        {
                            if (temp.NodeType == XmlNodeType.Element)
                            {
                                if (temp.Name == "rank")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }

                            }
                        }
                    }
                }
            }
            return tempList;
        }

        //public static List<string> IsSendUserReadyInfoToClientXml(string xmlStr)
        //{
        //    List<string> tempList = new List<string>();
        //    XmlDocument reXML_xml = new XmlDocument();
        //    reXML_xml.PreserveWhitespace = false;//是否保留空白符
        //    reXML_xml.LoadXml(xmlStr);//加载xml
        //    string tempStr = "";
        //    tempList.Add("SendUserReadyInfoToClient");

        //    for (XmlNode tempNode = reXML_xml.FirstChild.FirstChild; tempNode != null; tempNode = tempNode.NextSibling)
        //    {
        //        if (tempNode.HasChildNodes)
        //        {
        //            if (tempNode.ChildNodes.Count > 0)
        //            {
        //                for (XmlNode temp = tempNode.FirstChild; temp != null; temp = temp.NextSibling)
        //                {
        //                    if (temp.NodeType == XmlNodeType.Element)
        //                    {
        //                        if (temp.Name == "chairId")
        //                        {
        //                            tempStr = GetNodeValue(temp);
        //                            tempList.Add(tempStr);
        //                        }
        //                        else if (temp.Name == "ready")
        //                        {
        //                            tempStr = GetNodeValue(temp);
        //                            tempList.Add(tempStr);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return tempList;
        //}

        //判断获取对方用户名id
        public static List<string> IsOtherUserInfoXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//是否保留空白符
            reXML_xml.LoadXml(xmlStr);//加载xml
            string tempStr = "";
            tempList.Add("OtherUserInfo");

            for (XmlNode tempNode = reXML_xml.FirstChild.FirstChild; tempNode != null; tempNode = tempNode.NextSibling)
            {
                if (tempNode.HasChildNodes)
                {
                    if (tempNode.ChildNodes.Count > 0)
                    {
                        for (XmlNode temp = tempNode.FirstChild; temp != null; temp = temp.NextSibling)
                        {
                            if (temp.NodeType == XmlNodeType.Element)
                            {
                                if (temp.Name == "userName")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                                else if(temp.Name == "userId")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                            }
                        }
                    }
                }
            }
            return tempList;
        }

        //判断获取组局成功信息
        public static List<string> IsPlaySuccessXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//是否保留空白符
            reXML_xml.LoadXml(xmlStr);//加载xml
            string tempStr = "";
            tempList.Add("PlaySuccess");

            for (XmlNode tempNode = reXML_xml.FirstChild.FirstChild; tempNode != null; tempNode = tempNode.NextSibling)
            {
                if (tempNode.HasChildNodes)
                {
                    if (tempNode.ChildNodes.Count > 0)
                    {
                        for (XmlNode temp = tempNode.FirstChild; temp != null; temp = temp.NextSibling)
                        {
                            if (temp.NodeType == XmlNodeType.Element)
                            {
                                if (temp.Name == "success")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                            }
                        }
                    }
                }
            }
            return tempList;
        }

        //判断获取食物位置信息
        public static List<string> IsFoodPositionXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;
            reXML_xml.LoadXml(xmlStr);
            string tempStr = "";
            tempList.Add("FoodPosition");

            for (XmlNode tempNode = reXML_xml.FirstChild.FirstChild; tempNode != null; tempNode = tempNode.NextSibling)
            {
                if (tempNode.HasChildNodes)
                {
                    if (tempNode.ChildNodes.Count > 0)
                    {
                        for (XmlNode temp = tempNode.FirstChild; temp != null; temp = temp.NextSibling)
                        {
                            if (temp.NodeType == XmlNodeType.Element)
                            {
                                if (temp.Name == "position")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                            }
                        }
                    }
                }
            }
            return tempList;
        }

        //判断获取食物移位信息
        public static List<string> IsFoodTransXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;
            reXML_xml.LoadXml(xmlStr);
            string tempStr = "";
            tempList.Add("FoodTrans");

            for (XmlNode tempNode = reXML_xml.FirstChild.FirstChild; tempNode != null; tempNode = tempNode.NextSibling)
            {
                if (tempNode.HasChildNodes)
                {
                    if (tempNode.ChildNodes.Count > 0)
                    {
                        for (XmlNode temp = tempNode.FirstChild; temp != null; temp = temp.NextSibling)
                        {
                            if (temp.NodeType == XmlNodeType.Element)
                            {
                                if (temp.Name == "name")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                                else if(temp.Name == "x")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                                else if (temp.Name == "y")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                            }
                        }
                    }
                }
            }
            return tempList;
        }

        //判断获取蛇信息 速度
        //public static List<string> IsSnakeMsgXml(string xmlStr)
        //{
        //    List<string> tempList = new List<string>();
        //    XmlDocument reXML_xml = new XmlDocument();
        //    reXML_xml.PreserveWhitespace = false;//是否保留空白符
        //    reXML_xml.LoadXml(xmlStr);//加载xml
        //    string tempStr = "";
        //    tempList.Add("SnakeMsg");

        //    for (XmlNode tempNode = reXML_xml.FirstChild.FirstChild; tempNode != null; tempNode = tempNode.NextSibling)
        //    {
        //        if (tempNode.HasChildNodes)
        //        {
        //            if (tempNode.ChildNodes.Count > 0)
        //            {
        //                for (XmlNode temp = tempNode.FirstChild; temp != null; temp = temp.NextSibling)
        //                {
        //                    if (temp.NodeType == XmlNodeType.Element)
        //                    {
        //                        if (temp.Name == "userId")
        //                        {
        //                            tempStr = GetNodeValue(temp);
        //                            tempList.Add(tempStr);
        //                        }
        //                        else if(temp.Name == "length")
        //                        {
        //                            tempStr = GetNodeValue(temp);
        //                            tempList.Add(tempStr);
        //                        }
        //                        else if (temp.Name == "vecx")
        //                        {
        //                            tempStr = GetNodeValue(temp);
        //                            tempList.Add(tempStr);
        //                        }
        //                        else if (temp.Name == "vecy")
        //                        {
        //                            tempStr = GetNodeValue(temp);
        //                            tempList.Add(tempStr);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return tempList;
        //}

        //判断获取蛇信息 位置
        public static List<string> IsSnakeMsgXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//是否保留空白符
            reXML_xml.LoadXml(xmlStr);//加载xml
            string tempStr = "";
            tempList.Add("SnakeMsg");

            for (XmlNode tempNode = reXML_xml.FirstChild.FirstChild; tempNode != null; tempNode = tempNode.NextSibling)
            {
                if (tempNode.HasChildNodes)
                {
                    if (tempNode.ChildNodes.Count > 0)
                    {
                        for (XmlNode temp = tempNode.FirstChild; temp != null; temp = temp.NextSibling)
                        {
                            if (temp.NodeType == XmlNodeType.Element)
                            {
                                if (temp.Name == "userId")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                                else if (temp.Name == "angle")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                                else if (temp.Name == "x")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                                else if (temp.Name == "y")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                                else if(temp.Name == "length")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                            }
                        }
                    }
                }
            }
            return tempList;
        }

        //判断获取蛇死亡信息
        public static List<string> IsSnakeDeathXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//是否保留空白符
            reXML_xml.LoadXml(xmlStr);//加载xml
            string tempStr = "";
            tempList.Add("SnakeDeath");
            for (XmlNode tempNode = reXML_xml.FirstChild.FirstChild; tempNode != null; tempNode = tempNode.NextSibling)
            {
                if (tempNode.HasChildNodes)
                {
                    if (tempNode.ChildNodes.Count > 0)
                    {
                        for (XmlNode temp = tempNode.FirstChild; temp != null; temp = temp.NextSibling)
                        {
                            if (temp.NodeType == XmlNodeType.Element)
                            {
                                if (temp.Name == "userId")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                            }
                        }
                    }
                }
            }
            return tempList;
        }

        //判断获取游戏时间信息
        public static List<string> IsGameTimeToClientXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//是否保留空白符
            reXML_xml.LoadXml(xmlStr);//加载xml
            string tempStr = "";
            tempList.Add("GameTimeToClient");
            for (XmlNode tempNode = reXML_xml.FirstChild.FirstChild; tempNode != null; tempNode = tempNode.NextSibling)
            {
                if (tempNode.HasChildNodes)
                {
                    if (tempNode.ChildNodes.Count > 0)
                    {
                        for (XmlNode temp = tempNode.FirstChild; temp != null; temp = temp.NextSibling)
                        {
                            if (temp.NodeType == XmlNodeType.Element)
                            {
                                if (temp.Name == "gameTime")
                                {
                                    tempStr = GetNodeValue(temp);
                                    tempList.Add(tempStr);
                                }
                            }
                        }
                    }
                }
            }
            return tempList;
        } 

        //判断获取游戏结束信息
        public static List<string> IsGameOverToClientXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//是否保留空白符
            reXML_xml.LoadXml(xmlStr);//加载xml
            //string tempStr = "";
            tempList.Add("GameOverToClient");

            for (XmlNode tempNode = reXML_xml.FirstChild.FirstChild; tempNode != null; tempNode = tempNode.NextSibling)
            {
                if (tempNode.HasChildNodes)
                {
                    if (tempNode.ChildNodes.Count > 0)
                    {
                        for (XmlNode temp = tempNode.FirstChild; temp != null; temp = temp.NextSibling)
                        {
                            if (temp.NodeType == XmlNodeType.Element)
                            {
                                if (temp.Name == "winChairId")
                                {
    
                                }
                            }
                        }
                    }
                }
            }
            return tempList;
        }

        //判断信息类型并获取信息列表
        public static List<string> DataHelper(string xmlStr)
        {
            List<string> tempList = null;
            if (xmlStr != null && xmlStr != "")
            {
                XDocument xmlDoc = XDocument.Parse(xmlStr);
                string messageQuFEn = xmlDoc.Root.Name.ToString();
                if (true)
                {
                    switch (messageQuFEn)
                    {
                        case "LoginSuccess":
                            tempList = IsLoginInfoXml(xmlStr);
                            break;
                        case "YiDiLoginMsg":
                            tempList = IsYiDiLoginMsgXml(xmlStr);
                            break;
                        case "AutoSitInfo":
                            tempList = IsAutoSitInfoXml(xmlStr);
                            break;
                        case "RankList":
                            tempList = IsRankListXml(xmlStr);
                            break;
                        //case "SendUserReadyInfoToClient":
                        //    tempList = IsSendUserReadyInfoToClientXml(xmlStr);
                        //    break;
                        case "OtherUserInfo":
                            tempList = IsOtherUserInfoXml(xmlStr);
                            break;
                        case "PlaySuccess":
                            tempList = IsPlaySuccessXml(xmlStr);
                            break;
                        case "FoodPosition":
                            tempList = IsFoodPositionXml(xmlStr);
                            break;
                        case "FoodTrans":
                            tempList = IsFoodTransXml(xmlStr);
                            break;
                        case "SnakeMsg":
                            tempList = IsSnakeMsgXml(xmlStr);
                            break;
                        case "SnakeDeath":
                            tempList = IsSnakeDeathXml(xmlStr);
                            break;
                        case "GameTimeToClient":
                            tempList = IsGameTimeToClientXml(xmlStr);
                            break;
                        case "GameOverToClient":
                            tempList = IsGameOverToClientXml(xmlStr);
                            break;

                        default:
                            break;
                    }
                }
            }

            return tempList;
        }

    }
}
