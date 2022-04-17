﻿using System.Collections.Generic;
using System.Linq;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Data;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class UserManagementService : IUserManagementService
    {
        // Readonly means that the object/variable cannot be defined outside of the
        // constructor
        private readonly UserManagementDataAccess _userManagementDAO;
        public UserManagementService(UserManagementDataAccess userManagementDataAccess)
        {
            _userManagementDAO = userManagementDataAccess;

        }
        public ISet<AccountModel> RetrieveAllAccounts(AccountModel userAccount)
        {
            var accountEntities = _userManagementDAO.GetAllUsers();

            var userAccounts = accountEntities.Select(acct => new AccountModel()
            {
                AccountType = userAccount!.AccountType,
                Username = userAccount!.Username
            }).ToHashSet();
            return userAccounts;
        }
        public bool CreateAccount(DataStoreUser createdUser)
        {
            var dataStoreUser = new DataStoreUser()
            {
                _userType = createdUser!._userType,
                _username = createdUser!._username,
                _password = createdUser!._password,
                _email = createdUser!._email
            };
            return _userManagementDAO.InsertNewDataStoreAccountEntity(dataStoreUser);
        }
        public bool PerminateDeleteAccount(DeleteAccountModel deletedAccount)
        {
            var dataStoreUser = new DeleteAccountModel()
            {
                Username = deletedAccount!.Username,
                VerifiedPassword = deletedAccount!.VerifiedPassword
            };
            return _userManagementDAO.PerminateDeleteAccountEntity(dataStoreUser);
        }

        public bool KeepDeleteAccount(DeleteAccountModel deletedAccount)
        {
            var dataStoreUser = new DeleteAccountModel()
            {
                Username = deletedAccount!.Username,
                VerifiedPassword = deletedAccount!.VerifiedPassword
            };
            return _userManagementDAO.KeepDeleteAccountEntity(dataStoreUser);
        }

        //**********DO NOT DELETE BELOW***********
        //Account Recovery Functions needed later
        /*
        public bool ForgotUsername(ForgotUsernameModel forgottenUsername)
        {
            var dataStoreUser = new ForgotUsernameModel()
            {
                email = forgottenUsername!.email
            };
            return _userManagementDAO.ForgotUsernameEntity(dataStoreUser);
        }
        public bool ForgotPassword(ForgotPasswordModel forgottenPassword) //What is forgottenPassword supposed to be?
        {
            var dataStoreUser = new ForgotPasswordModel()
            {
                username = forgottenPassword!.username
            };
            return _userManagementDAO.ForgotPasswordEntity(dataStoreUser); //What does this do?
        }
        public bool ChangePassword(ChangePasswordModel changedPassword)
        {
            var dataStoreUser = new ChangePasswordModel()
            {
                newPassword = changedPassword!.newPassword,
                verifiedNewPassword = changedPassword!.verifiedNewPassword
            };
            return _userManagementDAO.ChangePasswordEntity(dataStoreUser);
        }
        */
    }
}
