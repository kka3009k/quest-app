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
    public class QuestService : IQuestService
    {
        private readonly IQuestRepository _questRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IQuestPlayerRepository _questPlayerRepository;

        public QuestService(IQuestRepository questRepository, IPlayerRepository playerRepository, IQuestPlayerRepository questPlayerRepository)
        {
            _questRepository = questRepository;
            _playerRepository = playerRepository;
            _questPlayerRepository = questPlayerRepository;
        }

        public async Task<IEnumerable<QuestDto>> GetAvailableQuestsForPlayerAsync(int playerId)
        {
            var quests = await _questRepository.GetAvailableQuestsForPlayerAsync(playerId);
            return quests.Select(q => new QuestDto
            {
                Id = q.Id,
                Title = q.Title,
                Description = q.Description,
                Rewards = q.Rewards,
                MinPlayerLevel = q.MinPlayerLevel
            });
        }

        public async Task AcceptQuestAsync(int playerId, int questId)
        {
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player == null)
            {
                throw new KeyNotFoundException($"Player with ID {playerId} not found.");
            }

            var quest = await _questRepository.GetByIdAsync(questId);
            if (quest == null)
            {
                throw new KeyNotFoundException($"Quest with ID {questId} not found.");
            }

            if (player.Level < quest.MinPlayerLevel)
            {
                throw new InvalidOperationException("Player does not meet the level requirement for this quest.");
            }

            var activeQuests = await _questPlayerRepository.GetActiveQuestsForPlayerAsync(playerId);
            if (activeQuests.Count() >= 10)
            {
                throw new InvalidOperationException("Player has reached the maximum number of active quests.");
            }

            var questPlayer = new PlayerQuest
            {
                PlayerId = playerId,
                QuestId = questId,
                Status = QuestStatus.Accepted,
                Progress = "finish"
            };

            await _questPlayerRepository.AddAsync(questPlayer);
            await _questPlayerRepository.SaveChangesAsync();
        }

        public async Task UpdateQuestProgressAsync(int playerId, int questId, QuestProgressDto progressDto)
        {
            var questPlayer = await _questPlayerRepository.GetByPlayerAndQuestIdAsync(playerId, questId);
            if (questPlayer == null)
            {
                throw new KeyNotFoundException($"No progress found for player {playerId} on quest {questId}.");
            }

            if (questPlayer.Status == QuestStatus.Completed || questPlayer.Status == QuestStatus.Finished)
            {
                return;
            }

            if (questPlayer.Progress != null)
            {
                questPlayer.Status = QuestStatus.Completed;
            }
            else if (questPlayer.Status == QuestStatus.Accepted)
            {
                questPlayer.Status = QuestStatus.InProgress;
            }

            await _questPlayerRepository.UpdateAsync(questPlayer);
            await _questPlayerRepository.SaveChangesAsync();
        }

        public async Task CompleteQuestAsync(int playerId, int questId)
        {
            var questPlayer = await _questPlayerRepository.GetByPlayerAndQuestIdAsync(playerId, questId);
            if (questPlayer == null)
            {
                throw new KeyNotFoundException($"No progress found for player {playerId} on quest {questId}.");
            }

            if (questPlayer.Status != QuestStatus.Completed)
            {
                throw new InvalidOperationException("Quest must be completed before it can be finished.");
            }


            questPlayer.Status = QuestStatus.Finished;

            await _questPlayerRepository.UpdateAsync(questPlayer);
            await _questPlayerRepository.SaveChangesAsync();
        }
    }
}
