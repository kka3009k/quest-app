using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Application.Models
{
    public class QuestDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Rewards { get; set; }
        public int MinPlayerLevel { get; set; }
    }
}
