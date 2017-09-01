﻿using System;
using System.Collections.Generic;
using Core.Domain.Partners;

namespace Core.Domain.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseAccount<T> : BaseEntity<T> where T : BaseEntity<T>, new()
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Number { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime OpenedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ClosedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<Partner> Partners { get; set; }
        #endregion

        #region Constructor

        protected BaseAccount()
        {
            Partners = null;
        }

        public void OpenNewAccount(string name, string number, Partner partner)
        {
            if (name == null || number == null || partner == null)
            {
                throw new ArgumentException();
            }

            Name = name;
            Number = number;
            OpenedDate = DateTime.Today;
            Partners.Add(partner);
        }
        #endregion
    }
}
