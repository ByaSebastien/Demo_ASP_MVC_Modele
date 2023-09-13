using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IMemberService
    {
        Member Register(Member member);
        Member Login(string pseudo, string password);
    }
}
