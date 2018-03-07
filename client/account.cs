using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class account
    {
        public int Id { get; set; }
        public string AccNo { get; set; }
        public string AccName { get; set; }
        public int Balance { get; set; }
        // public Nullable<System.DateTime> LastLogin { get; set; }
        public string Token { get; set; }
    }
}
