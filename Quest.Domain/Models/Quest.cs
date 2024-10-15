using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Domain.Models
{
    public class Quest
    {
        /// <summary>
        ///  Уникальный идентификатор квеста
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название квеста
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание квеста
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Условия выполнения квеста
        /// </summary>
        public string Requirements { get; set; }

        /// <summary>
        /// Награды за выполнение квеста
        /// </summary>
        public string Rewards { get; set; }

        /// <summary>
        /// Минимальный уровень игрока для доступа к квесту
        /// </summary>
        public int MinPlayerLevel { get; set; }

        /// <summary>
        /// Квесты игроков
        /// </summary>
        public ICollection<PlayerQuest> PlayerQuests { get; set; } // Квесты игроков
    }
}
