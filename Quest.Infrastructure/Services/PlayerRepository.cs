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
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(QuestDbContext context) : base(context)
        {
        }

        public async Task<Player> GetPlayerWithQuestsAsync(int playerId)
        {
            return await _dbSet
                .Include(p => p.PlayerQuests)
                .ThenInclude(pq => pq.Quest)
                .FirstOrDefaultAsync(p => p.Id == playerId);
        }
    }
}
