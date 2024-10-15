using Moq;
using Quest.Application.Models;
using Quest.Application.Services;
using Quest.Domain.Models;
using Quest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Tests
{
    public class QuestServiceTests
    {
        private readonly Mock<IQuestPlayerRepository> _questPlayerRepositoryMock;
        private readonly Mock<IQuestRepository> _questRepositoryMock;
        private readonly Mock<IPlayerRepository> _playerRepositoryMock;
        private readonly QuestService _questService;

        public QuestServiceTests()
        {
            _questPlayerRepositoryMock = new Mock<IQuestPlayerRepository>();
            _questRepositoryMock = new Mock<IQuestRepository>();
            _playerRepositoryMock = new Mock<IPlayerRepository>();
            _questService = new QuestService(_questRepositoryMock.Object, _playerRepositoryMock.Object, _questPlayerRepositoryMock.Object);
        }

        [Fact]
        public async Task AcceptQuestAsync_QuestIsAvailable_AcceptsQuest()
        {
            // Arrange
            int playerId = 1;
            int questId = 1;
            var quest = new Domain.Models.Quest { Id = questId, MinPlayerLevel = 1 };

            _questRepositoryMock.Setup(r => r.GetByIdAsync(questId)).ReturnsAsync(quest);
            _questPlayerRepositoryMock.Setup(r => r.GetActiveQuestsForPlayerAsync(playerId)).ReturnsAsync(new List<PlayerQuest>());

            // Act
            await _questService.AcceptQuestAsync(playerId, questId);

            // Assert
            _questPlayerRepositoryMock.Verify(r => r.AddAsync(It.IsAny<PlayerQuest>()), Times.Once);
            _questPlayerRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateQuestProgressAsync_ValidProgress_UpdatesProgress()
        {
            // Arrange
            int playerId = 1;
            int questId = 1;
            var progressDto = new QuestProgressDto { ItemsCollected = 5 };

            var questPlayer = new PlayerQuest { PlayerId = playerId, QuestId = questId, Status = QuestStatus.Accepted };
            _questPlayerRepositoryMock.Setup(r => r.GetByPlayerAndQuestIdAsync(playerId, questId)).ReturnsAsync(questPlayer);

            // Act
            await _questService.UpdateQuestProgressAsync(playerId, questId, progressDto);

            // Assert
            _questPlayerRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<PlayerQuest>()), Times.Once);
            _questPlayerRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
