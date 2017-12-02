﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class EnemyGoblin : Enemy
    {
        public EnemyGoblin(int level)
        {
            this.Name = "Goblin";
            this.PowerAttack = 20 + level * level / 2;
            this.IsLive = true;
            this.Health = 48 + 2 * level * level;
        }

        public static Enemy[] CreateEnemyGoblin(int playerLevel)
        {
            int count = Program.Random.Next(2, 4);
            int level = Program.Random.Next(1, 101);
            var enemyes = new Enemy[count];
            for (int i = 0; i < count; i++)
            {
                if (level <= 70) enemyes[i] = new EnemyGoblin(playerLevel);
                else if (level <= 80) enemyes[i] = new EnemyGoblin(playerLevel + 1);
                else if (level <= 90) enemyes[i] = new EnemyGoblin(playerLevel - 1);
                else if (level <= 95) enemyes[i] = new EnemyGoblin(playerLevel + 2);
                else if (level <= 98) enemyes[i] = new EnemyGoblin(playerLevel - 2);
                else enemyes[i] = new EnemyWolf(playerLevel + 3);
            }
            return enemyes;
        }
    }
}
