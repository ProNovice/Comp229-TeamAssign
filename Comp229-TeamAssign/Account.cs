using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comp229_TeamAssign
{
    public class Account
    {
        public int MemberID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int LoanedCount { get; set; }
        public string Position { get; set; }
    }
}