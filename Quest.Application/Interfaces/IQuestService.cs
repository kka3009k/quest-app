using Quest.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Application.Interfaces
{
    public interface IQuestService
    {
        Task<IEnumerable<QuestDto>> GetAvailableQuestsForPlayerAsync(int playerId);
        Task AcceptQuestAsync(int playerId, int questId);
        Task UpdateQuestProgressAsync(int playerId, int questId, QuestProgressDto progressDto);
        Task CompleteQuestAsync(int playerId, int questId);
    }
}
