﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NeoModules.Core;
using NeoModules.KeyPairs;
using NeoModules.NEP6;
using NeoModules.Rest.DTOs.NeoNotifications;
using NeoModules.Rest.DTOs.NeoScan;
using NeoModules.RPC.DTOs;
using NeoModules.RPC.Services;
using PhantasmaMail.Services.Authentication;
using PhantasmaMail.ViewModels.Base;

namespace PhantasmaMail.Services
{
    public class WalletService
    {
        public AccountSignerTransactionManager AccountManager => (AccountSignerTransactionManager)ActiveUser.GetDefaultAccount().TransactionManager;
        private User ActiveUser => Locator.Instance.Resolve<IAuthenticationService>().AuthenticatedUser;

        public async Task<string> TransferNep5(string toAddress, decimal amount, string tokenScriptHash)
        {
            var tokenScriptHashBytes = UInt160.Parse(tokenScriptHash).ToArray();
            var toAddressBytes = toAddress.ToScriptHash().ToArray();
            var tx = await AccountManager.TransferNep5(toAddressBytes, amount, tokenScriptHashBytes);
            return tx.Hash.ToString();
        }

        public async Task<string> SendAsset(string toAddress, string symbol, decimal amount)
        {
            var tx = await AccountManager.SendAsset(toAddress, symbol, amount);
            return tx.Hash.ToString();
        }

        public async Task<List<AccountBalance>> GetAssets()
        {
            var assetService = new NeoApiAccountService(AppSettings.RpcClient);
            var assetBalance = await assetService.GetAccountState.SendRequestAsync(ActiveUser.GetUserDefaultAddress());
            return assetBalance.Balance;
        }

        public async Task<AddressBalance> GetAccountBalance()
        {
            var balance = await AppSettings.RestService.GetBalanceAsync(ActiveUser.GetUserDefaultAddress());
            return balance;
        }

        public async Task<TokenResult> GetAllNep5Tokens()
        {
            var result = await AppSettings.NotificationsService.GetTokens();
            return result;
        }

        public async Task<List<AbstractEntry>> GetTransactionHistory()
        {
            var transactionHistory = await AppSettings.RestService.GetAddressAbstracts(ActiveUser.GetUserDefaultAddress(), 0);
            return transactionHistory.Entries.ToList();
        }

        public string GetUserAddress()
        {
            return ActiveUser.GetUserDefaultAddress();
        }
    }
}
