using Basilisk.Business.Interfaces;
using Basilisk.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Business.Repositories;
public class AuthRepository : IAuthRepository
{
    private readonly BasiliskTfContext _context;

    public AuthRepository(BasiliskTfContext context)
    {
        _context = context;
    }

    public void RegisterAccount(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
    }


    public Account GetAccount(string name)
    {
        return _context.Accounts.FirstOrDefault(u => u.Username.Equals(name))
            ?? throw new KeyNotFoundException("Username salah atau belum terdaftar");
    }

    public List<Account> GetAllAccount()
    {
        var query = from account in _context.Accounts
                    select account;
        return query.ToList();
    }

    public void ChangePassword(Account newAccount)
    {
        _context.Accounts.Update(newAccount);
        _context.SaveChanges();
    }


}

