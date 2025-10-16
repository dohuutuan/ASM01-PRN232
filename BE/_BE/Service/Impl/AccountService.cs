using _BE.Models;
using _BE.Repositories.Interface;
using _BE.Service.Interface;

namespace _BE.Service.Impl
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<SystemAccount> _accountRepo;
        private readonly IGenericRepository<NewsArticle> _newsRepo;

        public AccountService(ISystemAccountRepository accountRepo,
                              INewsArticleRepository newsRepo)
        {
            _accountRepo = accountRepo;
            _newsRepo = newsRepo;
        }

        public IQueryable<SystemAccount> GetAccounts()
        {
            return _accountRepo.GetAll().AsQueryable();
        }

        public SystemAccount CreateAccount(SystemAccount account)
        {
            if (_accountRepo.GetAll().Any(a => a.AccountEmail == account.AccountEmail))
                throw new Exception("Email already exists.");
            var maxId = _accountRepo.GetAll().Max(a => (int?)a.AccountId) ?? 0;
            account.AccountId = (short)(maxId + 1);

            _accountRepo.Add(account);
            _accountRepo.Save();
            return account;
        }

        public SystemAccount UpdateAccount(short id, SystemAccount update, string? currentPassword = null)
        {
            var existing = _accountRepo.GetById(id);
            if (existing == null) throw new Exception("Account not found");

            if (_accountRepo.GetAll().Any(a => a.AccountEmail == update.AccountEmail && a.AccountId != id))
                throw new Exception("Email already exists.");

            //if (!string.IsNullOrEmpty(update.AccountPassword))
            //{
            //    if (currentPassword == null || currentPassword != existing.AccountPassword)
            //        throw new Exception("Current password is incorrect.");
            //    existing.AccountPassword = update.AccountPassword;
            //}

            existing.AccountName = update.AccountName;
            existing.AccountEmail = update.AccountEmail;
            existing.AccountRole = update.AccountRole;

            _accountRepo.Update(existing);
            _accountRepo.Save();
            return existing;
        }

        public void DeleteAccount(short id)
        {
            var account = _accountRepo.GetById(id);
            if (account == null) throw new Exception("Account not found");

            if (_newsRepo.GetAll().Any(n => n.CreatedById == id))
                throw new Exception("Cannot delete account that has created news articles.");

            _accountRepo.Delete(account);
            _accountRepo.Save();
        }
    }

}
