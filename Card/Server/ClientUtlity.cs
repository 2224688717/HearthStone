﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Card.Server
{
    public static class ClientUtlity
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public static String strIP = "127.0.0.1";
        /// <summary>
        /// 新建游戏
        /// </summary>
        /// <param name="NickName"></param>
        public static String CreateGame(String NickName)
        {
            String requestInfo = Card.Server.Communication.RequestType.新建游戏.GetHashCode().ToString("D3") + NickName;
            return Card.Server.Communication.Request(requestInfo, strIP);
        }
        /// <summary>
        /// 加入游戏
        /// </summary>
        /// <param name="GameId"></param>
        /// <param name="NickName"></param>
        /// <returns></returns>
        public static String JoinGame(int GameId, String NickName)
        {
            String requestInfo = Card.Server.Communication.RequestType.加入游戏.GetHashCode().ToString("D3") + GameId.ToString(GameServer.GameIdFormat) + NickName;
            return Card.Server.Communication.Request(requestInfo, strIP);
        }
        /// <summary>
        /// 等待游戏列表
        /// </summary>
        /// <param name="NickName"></param>
        public static String GetWatiGameList()
        {
            String requestInfo = Card.Server.Communication.RequestType.等待游戏列表.GetHashCode().ToString("D3");
            return Card.Server.Communication.Request(requestInfo, strIP);
        }
        /// <summary>
        /// 确认游戏状态
        /// </summary>
        /// <param name="NickName"></param>
        public static Boolean IsGameStart(String GameId)
        {
            String requestInfo = Card.Server.Communication.RequestType.游戏启动状态.GetHashCode().ToString("D3") + GameId;
            return Card.Server.Communication.Request(requestInfo, strIP) == CardUtility.strTrue;
        }

        /// <summary>
        /// 确认先后手
        /// </summary>
        /// <param name="NickName"></param>
        public static Boolean IsFirst(String GameId, Boolean IsHost)
        {
            String requestInfo = Card.Server.Communication.RequestType.先后手状态.GetHashCode().ToString("D3") + GameId + (IsHost ? CardUtility.strTrue : CardUtility.strFalse);
            return Card.Server.Communication.Request(requestInfo, strIP) == CardUtility.strTrue;
        }
        /// <summary>
        /// 抽牌
        /// </summary>
        /// <param name="GameId"></param>
        /// <param name="IsFirst"></param>
        /// <param name="CardCount"></param>
        /// <returns></returns>
        public static List<String> DrawCard(String GameId, bool IsFirst, int CardCount)
        {
            String requestInfo = Card.Server.Communication.RequestType.抽牌.GetHashCode().ToString("D3") + GameId + (IsFirst ? CardUtility.strTrue : CardUtility.strFalse) + CardCount.ToString("D1");
            List<String> CardList = new List<string>();
            foreach (var card in Card.Server.Communication.Request(requestInfo, strIP).Split("|".ToArray()))
            {
                CardList.Add(card);
            }
            return CardList;
        }
        /// <summary>
        /// TurnEnd
        /// </summary>
        /// <param name="GameId"></param>
        public static void TurnEnd(String GameId)
        {
            var t =  new List<String>();
            t.Add(ActionCode.strEndTurn);
            WriteAction(GameId,t);
        }
        /// <summary>
        /// 添加指令
        /// </summary>
        /// <param name="GameId"></param>
        public static void WriteAction(String GameId, List<String> Action)
        {
            String Transform = String.Empty;
            foreach (var item in Action)
            {
                Transform += item + "|";
            }
            Transform = Transform.TrimEnd("|".ToCharArray());
            String requestInfo = Card.Server.Communication.RequestType.写入行动.GetHashCode().ToString("D3") + GameId + Transform;
            Card.Server.Communication.Request(requestInfo, strIP);
        }
        /// <summary>
        /// 读取行动
        /// </summary>
        /// <param name="GameId"></param>
        /// <returns></returns>
        public static String ReadAction(String GameId)
        {
            String requestInfo = Card.Server.Communication.RequestType.读取行动.GetHashCode().ToString("D3") + GameId;
            return Card.Server.Communication.Request(requestInfo, strIP);
        }
        /// <summary>
        /// 是否触发了奥秘
        /// </summary>
        /// <returns></returns>
        public static String IsSecretHit(String GameId, bool IsFirst)
        {
            String requestInfo = Card.Server.Communication.RequestType.奥秘判定.GetHashCode().ToString("D3") + GameId + (IsFirst ? CardUtility.strTrue : CardUtility.strFalse);
            return Card.Server.Communication.Request(requestInfo, strIP);
        }
    }
}
