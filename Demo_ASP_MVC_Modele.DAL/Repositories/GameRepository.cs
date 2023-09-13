using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GameRepository : RepositoryBase<int,Game>, IGameRepository
    {

        public GameRepository(IDbConnection connection) : base(connection,"Game","Id"){}
                
        protected override Game Convert(IDataRecord dataRecord)
        {
            return new Game
            {
                Id = (int)dataRecord["Id"],
                Name = (string)dataRecord["Name"],
                Description = dataRecord["Description"] == DBNull.Value ? null : (string)dataRecord["Description"],
                Nb_Player_Min = (int)dataRecord["Nb_Player_Min"],
                Nb_Player_Max = (int)dataRecord["Nb_Player_Max"],
                Age = dataRecord["Age"] == DBNull.Value ? null : (int)dataRecord["Age"],
                IsCoop = (bool)dataRecord["Coop"]
            };
        }
        #region CRUD
        public override int Add(Game entity)
        {
            IDbCommand command = _Connection.CreateCommand();
            command.CommandText = "INSERT INTO Game ([Name],[Description],[Nb_Player_min],[Nb_Player_Max],[Age],[Coop]) " +
                                  "OUTPUT inserted.[Id] " +
                                  "VALUES(@Name,@Desc,@NbPlayerMin,@NbPlayerMax,@Age,@Coop)";
            GenerateParameter(command, "@Name", entity.Name);
            GenerateParameter(command, "@Desc", entity.Description);
            GenerateParameter(command, "@NbPlayerMin", entity.Nb_Player_Min);
            GenerateParameter(command, "@NbPlayerMax", entity.Nb_Player_Max);
            GenerateParameter(command, "@Age", entity.Age);
            GenerateParameter(command, "@Coop", entity.IsCoop);
            _Connection.Open();
            int result = (int)command.ExecuteScalar();
            _Connection.Close();
            return result;
        }
        public override bool Update(Game entity)
        {
            using (IDbCommand command = _Connection.CreateCommand())
            {
                command.CommandText = "UPDATE Game " +
                                      "SET [Name] = @Name, " +
                                      "[Description] = @Desc, " +
                                      "[Nb_Player_min] = @NbPlayerMin, " +
                                      "[Nb_Player_Max] = @NbPlayerMax, " +
                                      "[Age] = @Age, " +
                                      "[Coop] = @Coop " +
                                      "WHERE Id = @Id";
                GenerateParameter(command, "@Name", entity.Name);
                GenerateParameter(command, "@Desc", entity.Description);
                GenerateParameter(command, "@NbPlayerMin", entity.Nb_Player_Min);
                GenerateParameter(command, "@NbPlayerMax", entity.Nb_Player_Max);
                GenerateParameter(command, "@Age", entity.Age);
                GenerateParameter(command, "@Coop", entity.IsCoop);
                GenerateParameter(command, "@Id", entity.Id);
                _Connection.Open();
                return command.ExecuteNonQuery() == 1;
            }
        }
        #endregion
        public IEnumerable<Game> GetFavoriteByMemberId(int id)
        {
            using (IDbCommand command = _Connection.CreateCommand())
            {
                command.CommandText = "SELECT G.* FROM Game G JOIN Favorite F ON G.Id = F.IdGame " +
                                      "WHERE F.IdMember = @id";
                GenerateParameter(command, "@id", id);
                _Connection.Open();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return Convert(reader);
                    }
                }
                _Connection.Close();
            }
        }
        public bool AddFavoriteToMember(int idMember,int idGame)
        {
            using(IDbCommand command = _Connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Favorite (IdMember,IdGame) " +
                                      "VALUES (@idMember,@idGame)";
                GenerateParameter(command, "idMember", idMember);
                GenerateParameter(command, "idGame", idGame);

                _Connection.Open();
                return command.ExecuteNonQuery() == 1;
            }
        }
        public bool DeleteFavoriteToMember(int idMember, int idGame)
        {
            using (IDbCommand command = _Connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Favorite " +
                                      "WHERE IdMember = @idMember AND IdGame = @IdGame";
                GenerateParameter(command, "idMember", idMember);
                GenerateParameter(command, "idGame", idGame);

                _Connection.Open();
                return command.ExecuteNonQuery() == 1;
            }
        }
    }
}
