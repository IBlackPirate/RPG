﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    /// <summary>
    /// Содержит в себе основные структуры, использующиеся в игре
    /// </summary>
    class MainGameStructures
    {
        /// <summary>
        /// Позиция объекта на карте
        /// </summary>
        public struct Position
        {
            public int X;
            public int Y;

            public bool Compare(Position position) =>
                this.X == position.X && this.Y == position.Y;
        }

        /// <summary>
        /// Аптечка, принадлежит игроку
        /// </summary>
        public struct MedicineKit
        {
            public int HpToAdd;

            /// <summary>
            /// Применяет аптечку
            /// </summary>
            /// <param name="player">Игрок, которому прибавляется HP</param>
            public void JoinKit(Player player) =>
                player.AddHP(HpToAdd);
        }
    }
}

