using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class functions
    {
        static string url_acc = "http://localhost:12345/api/accounts";
        static string url_trans = "http://localhost:12345/api/transactions";


        // check login
        async public static Task<account> check(string acc_no) {
            try
            {
                using(var client = new HttpClient())
                {
                    StringContent data = new StringContent(
                            JsonConvert.SerializeObject(new account { AccNo = acc_no}),
                            Encoding.UTF8,
                            "application/json"
                        );
                    using (var res = await client.PostAsync(url_acc, data))
                    {
                        string json = await res.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<account>(json);
                    }
                }
            }
            catch
            {
                return null;
            }
        }


        // history
        async public static Task<List<transection>> history(account acc)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var res = await client.GetAsync( url_trans + "?accid=" + acc.Id + "&token=" + acc.Token ))
                    {
                        string json = await res.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<transection>>(json);
                    }
                }
            }
            catch{ return null; }
        }


        // transfer
        async public static Task<transection> transfer(account acc, int acc_to, int amount, string message )
        {
            try
            {
                transection transection = new transection()
                {
                    AccFrom = acc.Id,
                    AccTo = acc_to,
                    Amount = amount,
                    Message = message
                };
                using (var client = new HttpClient())
                {
                    StringContent data = new StringContent(
                            JsonConvert.SerializeObject(transection),
                            Encoding.UTF8,
                            "application/json"
                        );
                    using (var res = await client.PostAsync(url_trans + "?accid=" + acc.Id + "&token=" + acc.Token, data))
                    {
                        string json = await res.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<transection>(json);
                    }
                }
            }
            catch
            {
                return null;
            }
        }

    }
}
