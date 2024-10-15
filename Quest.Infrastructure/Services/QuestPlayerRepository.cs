using Microsoft.EntityFrameworkCore;
using Quest.Domain.Models;
using Quest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Infrastructure.Services
{
    public class QuestPlayerRepository : IQuestPlayerRepository
    {
        private readonly QuestDbContext _context;

        public QuestPlayerRepository(QuestDbContext context)
        {
            _context = context;
        }

        public async Task<PlayerQuest> GetByPlayerAndQuestIdAsync(int playerId, int questId)
        {
            return await _context.Set<PlayerQuest>()
                .FirstOrDefaultAsync(qp => qp.PlayerId == playerId && qp.QuestId == questId);
        }

        public async Task<IEnumerable<PlayerQuest>> GetActiveQuestsForPlayerAsync(int playerId)
        {
            return await _context.Set<PlayerQuest>()
                .Where(qp => qp.PlayerId == playerId && qp.Status != QuestStatus.Finished)
                .ToListAsync();
        }

        public async Task AddAsync(PlayerQuest questPlayer)
        {
            await _context.Set<PlayerQuest>().AddAsync(questPlayer);
        }

        public async Task UpdateAsync(PlayerQuest questPlayer)
        {
            _context.Set<PlayerQuest>().Update(questPlayer);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
