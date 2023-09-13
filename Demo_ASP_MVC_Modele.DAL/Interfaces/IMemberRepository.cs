using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IMemberRepository : IRepository<int,Member>
    {
        string GetHashByPseudo(string pseudo);
        Member GetByPseudo(string pseudo);
        bool VerifyExistingMember(Member member);
    }
}
