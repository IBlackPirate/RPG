﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class Battle
    {
        public static bool IsEnemy = false;
        public static bool TheBattleWas = false;
        public static ObjectStructures.Position enemyPosition = new ObjectStructures.Position { X = 0, Y = 0 };
        public static ObjectStructures.Position theLastBattlePosition = new ObjectStructures.Position { X = 0, Y = 0 };

        public static int[] GetEnemyIndexs()
        {
            Console.WriteLine("Введите индексы противников через пробел");
            var indexs = Console.ReadLine().Split(' ');
            var result = new int[indexs.Length];
            for (int i = 0; i < indexs.Length; i++)
                result[i] = Program.Parse(indexs[i]);
            return result;
        }

        public static void GoBattle(Player player)
        {
            if (MayNewBattle(theLastBattlePosition, player.Position) || !TheBattleWas)
            {
                if (!IsEnemy || Enemy.IsEnemyFar(enemyPosition, player.Position))
                {
                    enemyPosition = Enemy.EnemyGenerationPosition(player.Position);
                    IsEnemy = true;
                }
                else
                {
                    if (Enemy.IsEnemyNear(enemyPosition, player.Position))
                    {

                        var enemy = Enemy.CreateEnemy(player.Level);
                        var reward = Enemy.GetReward(enemy);
                        while (Enemy.IsEnemyLive(enemy) && player.IsLive)
                        {
                            Window.PrintEnemy(enemy);
                            Console.WriteLine(player.CurrentHealth);

                            var type = player.SelectType();
                            Window.PrintArray(player.GetCharacteristicsOfWeapons(type));
                            int index = Program.Parse(Console.ReadLine());
                            var bow = player.Bow;
                            var sword = player.GetSword(index < player.Swords.Count ? index : 0);
                            var spell = player.GetSpell(index < player.Spells.Count ? index : 0);
                            var enInd = type == Weapons.WeaponsType.Sword ? GetEnemyIndexs() : new[] { 0 };
                            var numberOnEnemyAtsck = player.Attack(
                                enemy.Count, type, bow, spell, sword, enInd);

                            Enemy.OnEnemyAtack(enemy, numberOnEnemyAtsck);

                            Window.PrintOnEnemyAtack();
                            Window.PrintEnemy(enemy);

                            for (int i = 0; i < enemy.Count; i++)
                                player.ApplyDamage(enemy[i].EnemyAttack());


                            if (Enemy.IsEnemyLive(enemy))
                            {
                                Window.PrintEnemyAtack();
                                Window.PrintEnemy(enemy);
                            }
                        }

                        Window.ClearMap(Window.BattleMap);
                        Window.IsBattle = false;
                        //var city = City.IsSityNear(player.Position);
                        //if (city.Item1) Window.DrowCity(city.Item2, player.Position);

                        if (player.IsLive)
                        {
                            player.AddMoney(reward.Item1);
                            player.BattleSkill += reward.Item2;
                            while (player.Level * 100 <= player.BattleSkill)
                                player.ChangeBattleLevel();
                        }
                        TheBattleWas = true;
                        IsEnemy = false;
                        theLastBattlePosition = player.Position;
                    }
                }

            }
        }

        public static bool MayNewBattle(ObjectStructures.Position lastPosition, ObjectStructures.Position newPosition)
        {
            return (int)(Math.Sqrt((lastPosition.X - newPosition.X) * (lastPosition.X - newPosition.X) +
                (newPosition.Y - lastPosition.Y) * (newPosition.Y - lastPosition.Y))) > 10;
        }

    }
}
