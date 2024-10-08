﻿
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Payment : BaseEntity
    {
        public int FK_ORDER {  get; set; }
        public int FK_TYPE_PAYMENT { get; set; }
        public string? ACCOUNT_PAYMENT { get; set; }
        public int? FK_BANK_ACCOUNT { get; set; }
        public decimal AMOUNT { get; set; }
        
        public string? ACCOUNT_NUMBER { get; set; }
        public bool? NOTE_CREDIT { get; set; }

        //nav props
        public List<AccountDebit>? AccountDebit { get; set; }
	}
}
