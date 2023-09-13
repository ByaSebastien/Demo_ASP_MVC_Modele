using DAL;
using Isopoh.Cryptography.Argon2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MemberService : IMemberService
    {
        private IMemberRepository _Repository;
        public MemberService(IMemberRepository repository)
        {
            _Repository = repository;
        }

        public Member Login(string pseudo, string password)
        {
            string hash = _Repository.GetHashByPseudo(pseudo);
            if (string.IsNullOrWhiteSpace(hash))
                return null;
            if (Argon2.Verify(hash, password))
                return _Repository.GetByPseudo(pseudo).ToBLL();
            else
                return null;
        }

        public Member Register(Member member)
        {
            if (_Repository.VerifyExistingMember(member.ToDAL()))
                throw new Exception();
            else
            {
                string password = Argon2.Hash(member.Password);
                DAL.Member memberTemp = member.ToDAL();
                memberTemp.Password_Hash = password;
                int id = _Repository.Add(memberTemp);
                return _Repository.GetById(id).ToBLL();
            }
        }
    }
}
