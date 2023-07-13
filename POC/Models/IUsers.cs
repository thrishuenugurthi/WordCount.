using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace POC.Models
{
    public interface IUsers
    {

        bool Verify(string email, string password);

        bool Register(Users user);

        bool ValidateEmail(string email);
    }
}
