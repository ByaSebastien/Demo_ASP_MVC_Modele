using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IGameService : IRepository<int, Game>
    {
        public IEnumerable<Game> GetFavoriteByMemberId(int id);
        public bool AddFavoriteToMember(int idMember, int idGame);
        public bool DeleteFavoriteToMember(int idMember, int idGame);
    }
}
