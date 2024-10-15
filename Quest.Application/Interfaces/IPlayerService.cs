using Quest.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Application.Interfaces
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerDto>> GetAllPlayersAsync();
        Task<PlayerDto> GetPlayerByIdAsync(int id);
        Task<PlayerDto> CreatePlayerAsync(PlayerDto playerDto);
        Task UpdatePlayerAsync(PlayerDto playerDto);
        Task DeletePlayerAsync(int playerId);
    }
}
