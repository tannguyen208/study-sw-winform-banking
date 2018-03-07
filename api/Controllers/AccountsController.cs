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


        // GET: api/Accounts/5
        [ResponseType(typeof(Account))]
        public IHttpActionResult GetAccount([FromUri] int accid, [FromUri] string token)
        {
            var acc = (from a in db.Accounts
                       where a.Id == accid && 
                             a.Token.Equals(token) && 
                             !a.Token.Equals("")
                       select a).FirstOrDefault();
            if (acc != null)
            {
                acc.Token = "";
                db.SaveChanges();

                return Ok();
            }
            else
            {
                return NotFound();
            }

            return Ok(acc);
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
            }

            return Ok(acc);
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