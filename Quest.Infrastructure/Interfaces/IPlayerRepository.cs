using Quest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Infrastructure.Interfaces
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<Player> GetPlayerWithQuestsAsync(int playerId);
    }
}
