using ATMApp.Domain.BankAccount;
using Microsoft.EntityFrameworkCore;

namespace ATMApp.Data.Seeds
{
    public class AccountSeed
    {
        public static void Up()
        {
            var checkingAccount = new Account
            {
                Name = "Personal Checking Advantage",
                AccountNumber = "01234567890",
                RoutingNumber = "012345678",
                Balance = 100.00m
            };
            var savingAccount = new Account
            {
                Name = "Personal Saving Advantage",
                AccountNumber = "04434567770",
                RoutingNumber = "015545003",
                Balance = 200.00m
            };
            CreateAccount(AccountType.Checking.ToString(), checkingAccount);
            CreateAccount(AccountType.Saving.ToString(), savingAccount);
        }

        public static void Down()
        {
            RemoveAccount("01234567890");
            RemoveAccount("04434567770");
        }

        private static void CreateAccount(string type, Account account)
        {
            using var _context = new DatabaseContext();
            var accountType = _context.EntityKinds.FirstOrDefault(x => x.Code == type);
            if (accountType != null)
            {
                if (!_context.Accounts.Any(x => x.Name == account.Name || x.AccountNumber == account.AccountNumber))
                {
                    account.AccountTypeId = accountType.Id;
                    _context.Accounts.Entry(account).State = EntityState.Added;
                    _context.SaveChanges();
                }
            }
        }

        private static void RemoveAccount(string accountNumber)
        {
            using var _context = new DatabaseContext();
            var account = _context.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                _context.SaveChanges();
            }
        }
    }
}
