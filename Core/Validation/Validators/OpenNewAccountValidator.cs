﻿using System.Collections.Generic;
using Core.Domain.Accounts;
using Core.Interfaces.Validation;

namespace Core.Validation.Validators
{
    public class OpenNewAccountValidator : IValidator
    {
        public OpenNewAccountValidator(Account account)
        {
            AccountValidated = account;
        }

        public ICollection<ValidationRule> BrokenRules => GetBrokenRules();
        public Account AccountValidated { get; set; }

        public bool IsValid()
        {
            return BrokenRules.Count < 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICollection<ValidationRule> GetBrokenRules()
        {
            if (string.IsNullOrEmpty(AccountValidated.Name))
            {
                BrokenRules.Add(new ValidationRule
                {
                    Name = "",
                    Message = "Please specify the Name of this Account"
                });
            }

            if (AccountValidated.Partners.Count <= 0)
            {
                BrokenRules.Add(new ValidationRule
                {
                    Name = "",
                    Message = "The Account must belong to at least one Partner"
                });
            }

            return BrokenRules;
        }
    }
}
