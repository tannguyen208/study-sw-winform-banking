using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using api.Models;

namespace api.Controllers
{
    public class TransactionsController : ApiController
    {
        private ApiTutorialsBankEntities db = new ApiTutorialsBankEntities();

        // GET: api/Transactions
        public IHttpActionResult GetTransactions([FromUri] int accid, [FromUri] string token)
        {
            // check
            var acc = (from a in db.Accounts
                       where a.Id == accid && a.Token.Equals(token) && !a.Token.Equals("")
                       select a).FirstOrDefault();
            if (acc != null)
            {
                var list = from s in db.Transactions
                           where s.AccFrom == accid || s.AccTo == accid
                           select new {
                               s.Id,
                               s.TransDate,
                               AccFrom = s.Account.AccName,
                               AccTo = s.Account1.AccName,
                               s.Amount,
                               s.Message
                           } ;
                return Ok(list);
            }
            else
            {
                return NotFound();
            }
        }

  
        // POST: api/Transactions
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult PostTransaction(Transaction transaction, [FromUri] int accid, [FromUri] string token)
        {
            // check
            var acc = (from a in db.Accounts
                       where a.Id == accid && a.Token.Equals(token) && !a.Token.Equals("")
                       select a).FirstOrDefault();
            if (acc != null)
            {
                // check
                if (transaction.Amount <= 0 || acc.Balance <= transaction.Amount || acc.Id != transaction.AccFrom || transaction.AccFrom == transaction.AccTo )
                {
                    return BadRequest();
                }
                else
                {
                    transaction.TransDate = DateTime.Now;

                    // acc ng nhận
                    var acc_to = db.Accounts.Where(a => a.Id == transaction.AccTo).SingleOrDefault();

                    // 3 lệnh phải thực hiện đúng
                    acc.Balance -= transaction.Amount;
                    acc_to.Balance += transaction.Amount;
                    db.Transactions.Add(transaction);

                    db.SaveChanges();

                    return Ok(transaction);
                }
            }
            else
            {
                return NotFound();
            }
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}