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
    public class QuestRepository : Repository<Domain.Models.Quest>, IQuestRepository
    {
        public QuestRepository(QuestDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Domain.Models.Quest>> GetAvailableQuestsForPlayerAsync(int playerId)
        {
            // Пример логики: получить игрока и его квесты, а затем выбрать доступные квесты
            var player = await _context.Players
                .Include(p => p.PlayerQuests)
                .FirstOrDefaultAsync(p => p.Id == playerId);

            if (player == null)
                return Enumerable.Empty<Domain.Models.Quest>();

            var completedQuestIds = player.PlayerQuests
                .Where(pq => pq.Status == QuestStatus.Finished)
                .Select(pq => pq.QuestId);

            return await _dbSet
                .Where(q => q.MinPlayerLevel <= player.Level && !completedQuestIds.Contains(q.Id))
                .ToListAsync();
        }
    }
}
