using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Domain.Models
{
    public class PlayerQuest
    {
        /// <summary>
        /// Уникальный идентификатор квеста игрока
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Внешний ключ на игрока
        /// </summary>
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        /// <summary>
        /// Внешний ключ на квест
        /// </summary>
        public int QuestId { get; set; }
        public Quest Quest { get; set; }

        /// <summary>
        /// Статус квеста
        /// </summary>
        public QuestStatus Status { get; set; } // Статус квеста

        /// <summary>
        /// Прогресс выполнения условий квеста
        /// </summary>
        public string Progress { get; set; }

        /// <summary>
        /// Дата принятия квеста
        /// </summary>
        public DateTime AcceptedAt { get; set; }

        /// <summary>
        /// Дата завершения квеста (если завершен)
        /// </summary>
        public DateTime? CompletedAt { get; set; }
    }

    public enum QuestStatus
    {
        Accepted,
        InProgress,
        Completed,
        Finished
    }
}
