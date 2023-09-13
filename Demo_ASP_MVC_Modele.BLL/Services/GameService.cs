using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GameService : IGameService
    {
        private IGameRepository _Repository;
        public GameService(IGameRepository repository)
        {
            _Repository = repository;
        }
        public int Add(Game entity)
        {
            return _Repository.Add(entity.ToDAL());
        }

        public bool Delete(int id)
        {
            return _Repository.Delete(id);
        }

        public IEnumerable<Game> GetAll()
        {
            return _Repository.GetAll().Select(g => g.ToBLL());
        }

        public Game GetById(int id)
        {
            return _Repository.GetById(id).ToBLL();
        }

        public bool Update(Game entity)
        {
            return _Repository.Update(entity.ToDAL());
        }

        public IEnumerable<Game> GetFavoriteByMemberId(int id)
        {

            IEnumerable<Game> games = _Repository.GetFavoriteByMemberId(id).Select(g => g.ToBLL());
            return _Repository.GetFavoriteByMemberId(id).Select(g => g.ToBLL());
        }

        public bool AddFavoriteToMember(int idMember, int idGame)
        {
            return _Repository.AddFavoriteToMember(idMember, idGame);
        }
        public bool DeleteFavoriteToMember(int idMember, int idGame)
        {
            return _Repository.DeleteFavoriteToMember(idMember, idGame);
        }
    }
}
