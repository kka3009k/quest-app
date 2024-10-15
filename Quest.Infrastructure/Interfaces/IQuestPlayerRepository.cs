using Quest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Infrastructure.Interfaces
{
    public interface IQuestPlayerRepository
    {
        Task<PlayerQuest> GetByPlayerAndQuestIdAsync(int playerId, int questId);
        Task<IEnumerable<PlayerQuest>> GetActiveQuestsForPlayerAsync(int playerId);
        Task AddAsync(PlayerQuest questPlayer);
        Task UpdateAsync(PlayerQuest questPlayer);
        Task SaveChangesAsync();
    }
}
