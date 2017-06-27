using System;
using System.Collections.Generic;

namespace Pos.Models
{
    public class Account
    {
        public string Token { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public int TenantId { get; set; }
        public string TenantCode { get; set; }
        public string TenantName { get; set; }
        public IEnumerable<Spot> Spots { set; get; }
        public IEnumerable<string> Roles { get; set; }
        public long CurrentSpotId { get; set; }
        public int CurrentChannelId { get; set; }
    }
}