﻿using Card.Effect;
using System;
namespace Card
{
    /// <summary>
    /// 武器卡牌
    /// </summary>
    [Serializable]
    public class WeaponCard : CardBasicInfo
    {
        /// <summary>
        /// 攻击力（标准）
        /// </summary>
        public int StandardAttackPoint = -1;
        /// <summary>
        /// 攻击力（实际）
        /// </summary>
        public int 实际攻击力 = -1;
        /// <summary>
        /// 耐久（标准）
        /// </summary>
        public int 标准耐久度 = -1;
        /// <summary>
        /// 耐久（实际）
        /// </summary>
        public int 实际耐久度 = -1;
        /// <summary>
        /// 武器的附加效果
        /// 真银圣剑：每当你的英雄进攻时，为其恢复2点生命值。
        /// </summary>
        public EffectDefine AdditionEffect = new EffectDefine();
        /// <summary>
        /// 设置初始状态
        /// </summary>
        public new void Init()
        {
            实际耐久度 = 标准耐久度;
            实际攻击力 = StandardAttackPoint;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new string GetInfo()
        {
            return Name + "：" + 实际攻击力 + "/" + 实际耐久度;
        }
    }
}
