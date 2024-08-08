using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace com.beiyou.snake.gameclient.socketdata
{
    //����xml����
    public class XmlDataHelper : MonoBehaviour
    {
        //��ȡ�ڵ�ֵ
        private static string GetNodeValue(XmlNode tempNode)
        {
            string res = "";
            if (tempNode.FirstChild != null)
            {
                res = tempNode.FirstChild.Value;
            }
            return res;
        }

        //�жϻ�ȡ��¼��Ϣ
        public static List<string> IsLoginInfoXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//�Ƿ����հ׷�
            reXML_xml.LoadXml(xmlStr);//����xml
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

        //�жϻ�ȡ��ص�¼��Ϣ
        public static List<string> IsYiDiLoginMsgXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//�Ƿ����հ׷�
            reXML_xml.LoadXml(xmlStr);//����xml
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

        //�жϻ�ȡ�Զ�������Ϣ
        public static List<string> IsAutoSitInfoXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//�Ƿ����հ׷�
            reXML_xml.LoadXml(xmlStr);//����xml
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

        //�жϻ�ȡ���а���Ϣ
        public static List<string> IsRankListXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//�Ƿ����հ׷�
            reXML_xml.LoadXml(xmlStr);//����xml
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
        //    reXML_xml.PreserveWhitespace = false;//�Ƿ����հ׷�
        //    reXML_xml.LoadXml(xmlStr);//����xml
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

        //�жϻ�ȡ�Է��û���id
        public static List<string> IsOtherUserInfoXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//�Ƿ����հ׷�
            reXML_xml.LoadXml(xmlStr);//����xml
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

        //�жϻ�ȡ��ֳɹ���Ϣ
        public static List<string> IsPlaySuccessXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//�Ƿ����հ׷�
            reXML_xml.LoadXml(xmlStr);//����xml
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

        //�жϻ�ȡʳ��λ����Ϣ
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

        //�жϻ�ȡʳ����λ��Ϣ
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

        //�жϻ�ȡ����Ϣ �ٶ�
        //public static List<string> IsSnakeMsgXml(string xmlStr)
        //{
        //    List<string> tempList = new List<string>();
        //    XmlDocument reXML_xml = new XmlDocument();
        //    reXML_xml.PreserveWhitespace = false;//�Ƿ����հ׷�
        //    reXML_xml.LoadXml(xmlStr);//����xml
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

        //�жϻ�ȡ����Ϣ λ��
        public static List<string> IsSnakeMsgXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//�Ƿ����հ׷�
            reXML_xml.LoadXml(xmlStr);//����xml
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

        //�жϻ�ȡ��������Ϣ
        public static List<string> IsSnakeDeathXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//�Ƿ����հ׷�
            reXML_xml.LoadXml(xmlStr);//����xml
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

        //�жϻ�ȡ��Ϸʱ����Ϣ
        public static List<string> IsGameTimeToClientXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//�Ƿ����հ׷�
            reXML_xml.LoadXml(xmlStr);//����xml
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

        //�жϻ�ȡ��Ϸ������Ϣ
        public static List<string> IsGameOverToClientXml(string xmlStr)
        {
            List<string> tempList = new List<string>();
            XmlDocument reXML_xml = new XmlDocument();
            reXML_xml.PreserveWhitespace = false;//�Ƿ����հ׷�
            reXML_xml.LoadXml(xmlStr);//����xml
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

        //�ж���Ϣ���Ͳ���ȡ��Ϣ�б�
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
