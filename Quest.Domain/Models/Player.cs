using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Domain.Models
{
    public class Player
    {
        /// <summary>
        /// Уникальный идентификатор игрока
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя игрока
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Уровень игрока
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Квесты игрока
        /// </summary>
        public ICollection<PlayerQuest> PlayerQuests { get; set; }
    }
}
