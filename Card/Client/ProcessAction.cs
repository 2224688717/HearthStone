﻿using Card.Server;
using System;

namespace Card.Client
{
    public static class ProcessAction
    {
        #region"处理对方的动作"
        /// <summary>
        /// 处理对方的动作
        /// </summary>
        /// <param name="item"></param>
        /// <param name="game"></param>
        public static void Process(string item, GameManager game)
        {
            var actField = item.Split(CardUtility.strSplitMark.ToCharArray());
            switch (Card.Server.ActionCode.GetActionType(item))
            {
                case ActionCode.ActionType.UseWeapon:
                    game.AgainstInfo.Weapon = (Card.WeaponCard)Card.CardUtility.GetCardInfoBySN(actField[1]);
                    break;
                case ActionCode.ActionType.UseMinion:
                    int Pos = int.Parse(actField[2]);
                    var minion = (Card.MinionCard)Card.CardUtility.GetCardInfoBySN(actField[1]);
                    minion.Init();
                    game.AgainstInfo.BattleField.PutToBattle(Pos, minion);
                    game.AgainstInfo.BattleField.ResetBuff();
                    break;
                case ActionCode.ActionType.UseAbility:
                    break;
                case ActionCode.ActionType.Health:
                    //HEALTH#ME#1#2
                    //Me代表对方 YOU代表自己，必须反过来
                    if (actField[1] == CardUtility.strYou)
                    {
                        if (actField[2] == "0")
                        {
                            game.MySelf.RoleInfo.HealthPoint = int.Parse(actField[3]);
                        }
                        else
                        {
                            //位置从1开始，数组从0开始
                            game.MySelf.RoleInfo.BattleField.BattleMinions[int.Parse(actField[2]) - 1].ActualHealthPoint = int.Parse(actField[3]);
                        }
                    }
                    else
                    {
                        if (actField[2] == "0")
                        {
                            game.AgainstInfo.HealthPoint = int.Parse(actField[3]);
                        }
                        else
                        {
                            //位置从1开始，数组从0开始
                            game.AgainstInfo.BattleField.BattleMinions[int.Parse(actField[2]) - 1].ActualHealthPoint = int.Parse(actField[3]);
                        }
                    }
                    break;
                case ActionCode.ActionType.Status:
                    //STATUS#ME#1#FREEZE
                    //Me代表对方 YOU代表自己，必须反过来
                    if (actField[1] == CardUtility.strYou)
                    {
                        if (actField[2] == "0")
                        {
                            switch (actField[3])
                            {
                                case Card.Effect.StatusEffect.strFreeze:
                                    game.MySelf.RoleInfo.冰冻状态 = CardUtility.EffectTurn.效果命中;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            //位置从1开始，数组从0开始
                            switch (actField[3])
                            {
                                case Card.Effect.StatusEffect.strFreeze:
                                    game.MySelf.RoleInfo.BattleField.BattleMinions[int.Parse(actField[2]) - 1].冰冻状态 = CardUtility.EffectTurn.效果命中;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (actField[2] == "0")
                        {
                            switch (actField[3])
                            {
                                case Card.Effect.StatusEffect.strFreeze:
                                    game.AgainstInfo.冰冻状态 = CardUtility.EffectTurn.效果命中;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            //位置从1开始，数组从0开始
                            switch (actField[3])
                            {
                                case Card.Effect.StatusEffect.strFreeze:
                                    game.AgainstInfo.BattleField.BattleMinions[int.Parse(actField[2]) - 1].冰冻状态 = CardUtility.EffectTurn.效果命中;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                case ActionCode.ActionType.Crystal:
                    //Crystal#ME#4#4
                    //Me代表对方 YOU代表自己，必须反过来
                    if (actField[1] == CardUtility.strMe)
                    {
                        game.AgainstInfo.crystal.CurrentRemainPoint = int.Parse(actField[2]);
                        game.AgainstInfo.crystal.CurrentFullPoint = int.Parse(actField[3]);
                    }
                    else
                    {
                        game.MySelf.RoleInfo.crystal.CurrentRemainPoint = int.Parse(actField[2]);
                        game.MySelf.RoleInfo.crystal.CurrentFullPoint = int.Parse(actField[3]);
                    }
                    break;
                case ActionCode.ActionType.Transform:
                    //TRANSFORM#ME#1#M9000001
                    //Me代表对方 YOU代表自己，必须反过来
                    if (actField[1] == CardUtility.strYou)
                    {
                        game.MySelf.RoleInfo.BattleField.BattleMinions[int.Parse(actField[2]) - 1] = (Card.MinionCard)Card.CardUtility.GetCardInfoBySN(actField[3]);
                    }
                    else
                    {
                        game.MySelf.RoleInfo.BattleField.BattleMinions[int.Parse(actField[2]) - 1] = (Card.MinionCard)Card.CardUtility.GetCardInfoBySN(actField[3]);
                    }
                    break;
                case ActionCode.ActionType.Attack:
                    //ATTACK#ME#POS#AP
                    //Me代表对方 YOU代表自己，必须反过来
                    int AttackPoint = int.Parse(actField[3]);
                    if (actField[1] == CardUtility.strYou)
                    {
                        if (actField[2] == "0")
                        {
                            game.MySelf.RoleInfo.HealthPoint -= AttackPoint;
                        }
                        else
                        {
                            //位置从1开始，数组从0开始
                            game.MySelf.RoleInfo.BattleField.BattleMinions[int.Parse(actField[2]) - 1].AfterBeAttack(AttackPoint);
                        }
                    }
                    else
                    {
                        if (actField[2] == "0")
                        {
                            game.AgainstInfo.HealthPoint -= AttackPoint;
                        }
                        else
                        {
                            //位置从1开始，数组从0开始
                            game.AgainstInfo.BattleField.BattleMinions[int.Parse(actField[2]) - 1].AfterBeAttack(AttackPoint);
                        }
                    }
                    break;
                case ActionCode.ActionType.Fight:
                    //FIGHT#1#2
                    game.Fight(int.Parse(actField[2]), int.Parse(actField[1]),true);
                    break;
                case ActionCode.ActionType.UnKnown:
                    break;
            }
            game.Settle();
        }
        #endregion

    }
}
