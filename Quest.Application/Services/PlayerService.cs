using Quest.Application.Interfaces;
using Quest.Application.Models;
using Quest.Domain.Models;
using Quest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Application.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<IEnumerable<PlayerDto>> GetAllPlayersAsync()
        {
            var players = await _playerRepository.GetAllAsync();
            return players.Select(p => new PlayerDto
            {
                Id = p.Id,
                Name = p.Name,
                Level = p.Level
            });
        }

        public async Task<PlayerDto> GetPlayerByIdAsync(int id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if (player == null)
            {
                return null;
            }

            return new PlayerDto
            {
                Id = player.Id,
                Name = player.Name,
                Level = player.Level
            };
        }

        public async Task<PlayerDto> CreatePlayerAsync(PlayerDto playerDto)
        {
            var player = new Player
            {
                Name = playerDto.Name,
                Level = playerDto.Level
            };

            await _playerRepository.AddAsync(player);
            await _playerRepository.SaveChangesAsync();

            playerDto.Id = player.Id;
            return playerDto;
        }

        public async Task UpdatePlayerAsync(PlayerDto playerDto)
        {
            var player = await _playerRepository.GetByIdAsync(playerDto.Id);
            if (player == null)
            {
                throw new KeyNotFoundException($"Player with ID {playerDto.Id} not found.");
            }

            player.Name = playerDto.Name;
            player.Level = playerDto.Level;

            await _playerRepository.UpdateAsync(player);
            await _playerRepository.SaveChangesAsync();
        }

        public async Task DeletePlayerAsync(int playerId)
        {
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player == null)
            {
                throw new KeyNotFoundException($"Player with ID {playerId} not found.");
            }

            await _playerRepository.DeleteAsync(player);
            await _playerRepository.SaveChangesAsync();
        }
    }
}
