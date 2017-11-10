﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Service.Dtos.Partner;

namespace Service.Dtos.Account
{
    public class AccountDto
    {
        public int Id { get; set; }

        [DisplayName("Account Name")]
        public string Name { get; set; }

        [DisplayName("Account Number")]
        public string Number { get; set; }

        [DisplayName("Opened Date")]
        public DateTime DateOpened { get; set; }

        [DisplayName("Closed Date")]
        public DateTime DateClosed { get; set; }
        
        public virtual ICollection<PartnerDto> Partners { get; set; }
    }
}