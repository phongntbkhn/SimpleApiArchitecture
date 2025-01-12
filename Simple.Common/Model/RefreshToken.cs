﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common.Model
{
    public class RefreshToken
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Os { get; set; }
        public string VersionApp { get; set; }
        public string IP { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
