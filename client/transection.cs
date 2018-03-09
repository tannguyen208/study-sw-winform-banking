using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class transection
    {
        public int Id { get; set; }
        public DateTime TransDate { get; set; }

        public int AccFrom { get; set; }
        public int AccTo { get; set; }
        public int Amount { get; set; }
        public string Message { get; set; }
    }
}
