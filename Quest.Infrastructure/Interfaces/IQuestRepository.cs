using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Infrastructure.Interfaces
{
    public interface IQuestRepository : IRepository<Domain.Models.Quest>
    {
        Task<IEnumerable<Domain.Models.Quest>> GetAvailableQuestsForPlayerAsync(int playerId);
    }
}
