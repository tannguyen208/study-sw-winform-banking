using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using api.Models;

namespace api.Controllers
{
    public class AccountsController : ApiController
    {
        private ApiTutorialsBankEntities db = new ApiTutorialsBankEntities();


        // GET: api/accounts/?accid=1&token=
        [ResponseType(typeof(Account))]
        public IHttpActionResult GetAccount([FromUri] int accid, [FromUri] string token)
        {
            var acc = (from   a in db.Accounts
                       where  a.Id == accid && 
                              a.Token.Equals(token) && 
                             !a.Token.Equals("")
                       select a).FirstOrDefault();
            if (acc != null)
            {
                acc.Token = "";
                db.SaveChanges();

                return Ok(acc);
            }
            else
            {
                return NotFound();
            }
        }




        // check
        // GET: api/accounts/id_to?accid=1&token=
        public IHttpActionResult GetAccount(string id, [FromUri] int accid, [FromUri] string token)
        {

            // string id : accno_to
            var acc = (from a in db.Accounts
                       where a.Id == accid &&
                              a.Token.Equals(token) &&
                             !a.Token.Equals("")
                       select a).FirstOrDefault();

            if (acc != null)
            {
                var acc_to = (from a in db.Accounts
                                        where a.AccNo.Equals(id)
                                        select new
                                        {
                                            a.Id,
                                            a.AccName
                                        }).FirstOrDefault();
                return Ok(acc_to);
            }
            else
            {
                return NotFound();
            }
        }




        // POST: api/Accounts
        [ResponseType(typeof(Account))]
        public IHttpActionResult PostAccount(Account account)
        {
            var acc = db.Accounts.Where(a=>a.AccNo.Equals(account.AccNo)).SingleOrDefault();

            if(acc != null )
            {
                acc.LastLogin = DateTime.Now;
                acc.Token = EncryptString(acc.LastLogin.Value.Ticks + acc.AccNo);

                db.SaveChanges();
                var x = ( from a in db.Accounts
                      where a.AccNo.Equals(account.AccNo)
                      select new
                      {
                          a.Id,
                          a.Balance,
                          a.AccNo,
                          a.Token,
                          a.AccName
                      }).SingleOrDefault();
                return Ok(x);
            }
            else
            {
                return Ok(acc);
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

        private bool AccountExists(int id)
        {
            return db.Accounts.Count(e => e.Id == id) > 0;
        }









        // token
        private string EncryptString(string s)
        {
            SHA256 sha = SHA256Managed.Create();
            byte[] d = sha.ComputeHash(Encoding.UTF8.GetBytes(s));
            return BitConverter.ToString(d).ToLower().Replace("-", "");
        }
    }
}