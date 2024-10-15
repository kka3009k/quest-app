using Microsoft.AspNetCore.Mvc;
using Quest.Application.Interfaces;
using Quest.Application.Models;

namespace Quest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestController : ControllerBase
    {
        private readonly IQuestService _questService;

        public QuestController(IQuestService questService)
        {
            _questService = questService;
        }

        [HttpGet("available/{playerId}")]
        public async Task<ActionResult<IEnumerable<QuestDto>>> GetAvailableQuests(int playerId)
        {
            var quests = await _questService.GetAvailableQuestsForPlayerAsync(playerId);
            return Ok(quests);
        }

        [HttpPost("accept/{playerId}/{questId}")]
        public async Task<ActionResult> AcceptQuest(int playerId, int questId)
        {
            try
            {
                await _questService.AcceptQuestAsync(playerId, questId);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("progress/{playerId}/{questId}")]
        public async Task<ActionResult> UpdateQuestProgress(int playerId, int questId, [FromBody] QuestProgressDto progressDto)
        {
            try
            {
                await _questService.UpdateQuestProgressAsync(playerId, questId, progressDto);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("complete/{playerId}/{questId}")]
        public async Task<ActionResult> CompleteQuest(int playerId, int questId)
        {
            try
            {
                await _questService.CompleteQuestAsync(playerId, questId);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
