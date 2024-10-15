using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Application.Models
{
    public class QuestProgressDto
    {
        public int PlayerId { get; set; }
        public int QuestId { get; set; }
        public int ItemsCollected { get; set; }
        public int MonstersDefeated { get; set; }
        public bool LocationVisited { get; set; }
    }
}
