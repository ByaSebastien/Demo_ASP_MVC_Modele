using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MemberRepository : RepositoryBase<int, Member>, IMemberRepository
    {
        public MemberRepository(IDbConnection connection) : base(connection, "Member", "Id") { }

        protected override Member Convert(IDataRecord dataRecord)
        {
            return new Member
            {
                Id = (int)dataRecord["Id"],
                Pseudo = (string)dataRecord["Pseudo"],
                Email = (string)dataRecord["Email"],
                Password_Hash = (string)dataRecord["Password_Hash"]
            };
        }

        public override int Add(Member entity)
        {
            IDbCommand command = _Connection.CreateCommand();
            command.CommandText = "INSERT INTO Member ([Pseudo],[Email],[Password_Hash]) " +
                                  "OUTPUT inserted.[Id] " +
                                  "VALUES(@Pseudo,@Email,@Password_Hash)";
            GenerateParameter(command, "@Pseudo", entity.Pseudo);
            GenerateParameter(command, "@Email", entity.Email);
            GenerateParameter(command, "@Password_Hash", entity.Password_Hash);
            _Connection.Open();
            int result = (int)command.ExecuteScalar();
            _Connection.Close();
            return result;
        }

        public override bool Update(Member entity)
        {
            using (IDbCommand command = _Connection.CreateCommand())
            {
                command.CommandText = "UPDATE Member " +
                                      "SET [Pseudo] = @Pseudo, " +
                                      "[Email] = @Email, " +
                                      "[Password_Hash] = @Password_Hash, " +
                                      $"WHERE Id = @Id";
                GenerateParameter(command, "@Pseudo", entity.Pseudo);
                GenerateParameter(command, "@Email", entity.Email);
                GenerateParameter(command, "@Password_Hash", entity.Password_Hash);
                GenerateParameter(command, "@Id", entity.Id);
                _Connection.Open();
                return command.ExecuteNonQuery() == 1;
            }
        }

        public string GetHashByPseudo(string pseudo)
        {
            using (IDbCommand command = _Connection.CreateCommand())
            {
                command.CommandText = $"SELECT Password_Hash FROM {TableName} WHERE pseudo = @pseudo";
                GenerateParameter(command, "@pseudo", pseudo);

                _Connection.Open();
                object result = command.ExecuteScalar();
                _Connection.Close();

                return result is DBNull ? null : (string)result;
            }
        }

        public Member GetByPseudo(string pseudo)
        {
            using (IDbCommand command = _Connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM {TableName} WHERE pseudo = @pseudo";
                GenerateParameter(command, "@pseudo", pseudo);

                _Connection.Open();
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return Convert(reader);
                    return null;
                }
            }
        }

        public bool VerifyExistingMember(Member member)
        {
            using (IDbCommand command = _Connection.CreateCommand())
            {
                command.CommandText = $"SELECT COUNT(Id) FROM {TableName} WHERE pseudo = @pseudo OR email = @email";
                GenerateParameter(command, "@pseudo", member.Pseudo);
                GenerateParameter(command, "@email", member.Email);

                _Connection.Open();
                int result = (int)command.ExecuteScalar();
                _Connection.Close();
                return result == 1;
            }
        }        
    }
}
