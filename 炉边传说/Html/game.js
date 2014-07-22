﻿//全局变量
//游戏ID
var GameId = "00000";
//主机
var IsHost = false;
//先手
var IsFirst = false;
var data;
var ResponseCode;
var BattleInfo;
var IsMyTurn;
var Interrupt;
var ActiveCardSN;
var strHost;

function onmessage(evt) {
    data = evt.data;
    if (!data) return;
    ResponseCode = data.toString().substr(0, 3);
    data = data.toString().substr(3);
    switch (ResponseCode) {
        case RequestType.开始游戏:
            CreateGameResponse();
            break;
        case RequestType.回合结束:
            EndTrunResponse();
            break;
        case RequestType.传送套牌:
            SendDeckResponse();
            break;
        case RequestType.初始化状态:
            InitPlayInfoResponse();
            break;
        case RequestType.使用手牌:
            UseHandCardResponse();
            break;
        case RequestType.战场状态:
            BattleInfoResponse();
            break;
        case RequestType.中断续行:
            ResumeResponse();
            break;
    }
}

var strTrue = "1";
var strFalse = "0";
var RequestType = {
    新建游戏: "000",
    传送套牌: "001",
    等待游戏列表: "002",
    加入游戏: "003",
    游戏启动状态: "004",
    先后手状态: "005",
    认输: "006",
    抽牌: "007",
    回合结束: "008",
    写入行动: "009",
    读取行动: "010",
    奥秘判定: "011",
    使用手牌: "012",
    战场状态: "013",
    开始游戏: "014",
    初始化状态: "015",
    中断续行: "016"
};



function CreateGameResponse() {
    GameId = data.toString().substr(0, 5);
    IsHost = data.toString().substr(5, 1) == strTrue;
    IsFirst = data.toString().substr(6, 1) == strTrue;
    var gameInfo = GameId;
    if (IsHost) {
        gameInfo = gameInfo + "Host";
    } else {
        gameInfo = gameInfo + "Guest";
    }
    if (IsFirst) {
        gameInfo = gameInfo + "First";
    } else {
        gameInfo = gameInfo + "Second";
    }
    document.getElementById("GameId").innerHTML = gameInfo;
    SendDeck();
}

function EndTrun() {
    IsMyTurn = false;
    var message = RequestType.回合结束 + GameId + strHost;
    socket.send(message);
}

function EndTrunResponse() {
    var IsHostEnd = data.toString().substr(0, 1) == strTrue;
    var message = RequestType.战场状态 + GameId + strHost;
    socket.send(message);
    if (IsHostEnd != IsHost) {
        IsMyTurn = true;
    }
}

function SendDeck() {
    if (IsHost) {
        strHost = strTrue;
    } else {
        strHost = strFalse;
    }
    var message = RequestType.传送套牌 + GameId + strHost + "M000017|M000018|M000021|M000003|M000024|M000026|M000027|M000035|M000037|M000047|M000043|M000041|M000040|M000059|M000058|M000057|M000054|M000061|M000064|M000065|M000067|M000076|M000077|M000082|M000088|M000087|M000085|M000084|M000068|M000076";
    socket.send(message);
}

function UserHandCard(CardSN) {
    ActiveCardSN = CardSN;
    var message = RequestType.使用手牌 + GameId + strHost + CardSN;
    socket.send(message);
}

function SendDeckResponse() {
    if (!IsHost) {
        var message = RequestType.初始化状态 + GameId;
        socket.send(message);
    }
}
function InitPlayInfoResponse() {
    if (IsHost) {
        strHost = strTrue;
    } else {
        strHost = strFalse;
    }
    var message = RequestType.战场状态 + GameId + strHost;
    socket.send(message);
    if (IsFirst) IsMyTurn = true;
}

var SessionData;
var Step;
var MinionPos;
var Currentrequest;
function UseHandCardResponse() {
    if (IsHost) {
        strHost = strTrue;
    } else {
        strHost = strFalse;
    }
    Interrupt = JSON.parse(data);

    switch (Interrupt.ActionName) {
        case "OK":
            var message = RequestType.战场状态 + GameId + strHost;
            socket.send(message);
            break;
        case "MINIONPOSITION":
            InitPutMinionDialog();
            Currentrequest = RequestType.使用手牌;
            Step = "2";
            SessionData = Interrupt.SessionData + "MINIONPOSITION:";
            MinionPosDialog.dialog("open");
            break;
        case "BATTLECRYPOSITION":
            //这个时候随从已经入场，需要刷新一下自己战场的信息，
            //但是由于还没有完成整个动作，所以暂时不刷新对方战场
            //位置选择在战场状态刷新后执行
            message = RequestType.战场状态 + GameId + strHost;
            socket.send(message);
            break;
        case "SPELLPOSITION":
            InitTargetDialog(Interrupt.ExternalInfo);
            Currentrequest = RequestType.使用手牌;
            Step = "2";
            SessionData = Interrupt.SessionData + "SPELLPOSITION:";
            TargetPosDialog.dialog("open");
            break;
        case "SPELLDECIDE":
            Currentrequest = RequestType.使用手牌;
            Step = "2";
            SessionData = Interrupt.SessionData + "SPELLDECIDE:";
            break;
    }
}
//随从入场位置选择后
function AfterPutMinionPos(MinionPos) {
    SessionData = SessionData + MinionPos + "|";
    var message = RequestType.中断续行 + GameId + strHost + Currentrequest + Step + ActiveCardSN + SessionData
    socket.send(message);
}
//位置选择后
function AfterTargetPos(IsMy, TargetPos) {
    var strPos;
    if (IsMy) {
        strPos = "ME#" + TargetPos;
    } else {
        strPos = "YOU#" + TargetPos;
    }
    SessionData = SessionData + strPos + "|";
    var message = RequestType.中断续行 + GameId + strHost + Currentrequest + Step + ActiveCardSN + SessionData
    socket.send(message);
}
//中断续行
function ResumeResponse() {
    Interrupt = JSON.parse(data);
    if (Interrupt.ActionName == "OK") {
        var message = RequestType.战场状态 + GameId + strHost;
        socket.send(message);
    }
}
