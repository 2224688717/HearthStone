﻿using Card.Client;
using Card.Server;
using System.Collections.Generic;
namespace Card.Effect
{
    public static class CrystalEffect
    {
        /// <summary>
        /// 对法力水晶的法术实施
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Ability"></param>
        public static List<string> RunEffect(EffectDefine singleEffect, GameManager game)
        {
            List<string> Result = new List<string>();
            string[] Op = singleEffect.AddtionInfo.Split("/".ToCharArray());
            int point = 0;
            //±N/±N	增加减少 可用水晶 / 增加减少 空水晶
            //可用水晶
            if (Op[0].Substring(1, 1) != "0")
            {
                point = int.Parse(Op[0].Substring(1, 1));
                if (Op[0].Substring(0, 1) == "+")
                {
                    for (int i = 0; i < point; i++)
                    {
                        if (singleEffect.EffectTargetSelectDirect == CardUtility.TargetSelectDirectEnum.本方)
                        {
                            game.MySelf.RoleInfo.crystal.AddCurrentPoint();
                        }
                        else
                        {
                            game.YourInfo.crystal.AddCurrentPoint();
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < point; i++)
                    {
                        if (singleEffect.EffectTargetSelectDirect == CardUtility.TargetSelectDirectEnum.本方)
                        {
                            game.MySelf.RoleInfo.crystal.ReduceCurrentPoint();
                        }
                        else
                        {
                            game.YourInfo.crystal.ReduceCurrentPoint();
                        }
                    }
                }
            }
            //空水晶
            if (Op[1].Substring(1, 1) != "0")
            {
                point = int.Parse(Op[1].Substring(1, 1));
                if (Op[1].Substring(0, 1) == "+")
                {
                    for (int i = 0; i < point; i++)
                    {
                        if (singleEffect.EffectTargetSelectDirect == CardUtility.TargetSelectDirectEnum.本方)
                        {
                            game.MySelf.RoleInfo.crystal.AddFullPoint();
                        }
                        else
                        {
                            game.YourInfo.crystal.AddFullPoint();
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < point; i++)
                    {
                        if (singleEffect.EffectTargetSelectDirect == CardUtility.TargetSelectDirectEnum.本方)
                        {
                            game.MySelf.RoleInfo.crystal.ReduceFullPoint();
                        }
                        else
                        {
                            game.YourInfo.crystal.ReduceFullPoint();
                        }
                    }
                }
            }
            //Crystal#ME#4#4
            if (singleEffect.EffectTargetSelectDirect == CardUtility.TargetSelectDirectEnum.本方)
            {
                Result.Add(ActionCode.strCrystal + CardUtility.strSplitMark + CardUtility.strMe + CardUtility.strSplitMark + game.MySelf.RoleInfo.crystal.CurrentRemainPoint + CardUtility.strSplitMark + game.MySelf.RoleInfo.crystal.CurrentFullPoint);
            }
            else
            {
                Result.Add(ActionCode.strCrystal + CardUtility.strSplitMark + CardUtility.strYou + CardUtility.strSplitMark + game.YourInfo.crystal.CurrentRemainPoint + CardUtility.strSplitMark + game.YourInfo.crystal.CurrentFullPoint);
            }
            return Result;
        }
    }
}
