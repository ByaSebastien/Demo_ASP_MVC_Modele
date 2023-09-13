using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class Mappers
    {
        public static Game ToBLL(this DAL.Game game)
        {
            return new Game
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Age = game.Age,
                IsCoop = game.IsCoop,
                Nb_Player_Max = game.Nb_Player_Max,
                Nb_Player_Min = game.Nb_Player_Min,
            };
        }
        public static DAL.Game ToDAL(this Game game)
        {
            return new DAL.Game
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Age = game.Age,
                IsCoop = game.IsCoop,
                Nb_Player_Max = game.Nb_Player_Max,
                Nb_Player_Min = game.Nb_Player_Min,
            };
        }
        public static Member ToBLL(this DAL.Member member)
        {
            return new Member
            {
                Id = member.Id,
                Pseudo = member.Pseudo,
                Email = member.Email,
                Password = null
            };
        }
        public static DAL.Member ToDAL(this Member member)
        {
            return new DAL.Member
            {
                Id = member.Id,
                Pseudo = member.Pseudo,
                Email = member.Email,
                Password_Hash = null
            };
        }
    }
}
