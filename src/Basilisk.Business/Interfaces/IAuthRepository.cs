using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Business.Interfaces;
public interface IAuthRepository
{
    public void RegisterAccount(Account account);
    public Account GetAccount(string name);
    public List<Account> GetAllAccount();
    public void ChangePassword(Account newAccount);
}

