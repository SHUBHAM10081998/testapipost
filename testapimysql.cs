using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TESTAPI.Models;

namespace TESTAPI.Controllers
{
    public class TestController : ApiController
    {
        [System.Web.Http.HttpPost]
         public HttpResponseMessage POSTDATA([FromBody] List<TestModel> CustomerObj)
        {
            try
            {
                response objResponse = new response();
                DataTable Customer = new DataTable();

                Customer.Columns.Add("name", typeof(string));
                Customer.Columns.Add("sname", typeof(string));

                string name = "", sname = "";
                MySqlConnection constr = new MySqlConnection("server = 172.16.0.86; user id = root; database = test1; password = '1234';");
                {
                    for (int i = 0; i < CustomerObj.Count; i++)
                    {
                        name = CustomerObj[i].name;
                        sname = CustomerObj[i].sname;
                        
                        MySqlCommand cmd = new MySqlCommand("test", constr);
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))

                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@sname", sname);
                        constr.Open();
                        cmd.ExecuteNonQuery();
                        int ss = cmd.ExecuteNonQuery();
                        constr.Close();

                        if (ss > 0)
                        {
                            objResponse.MESSAGE_CODE = "001-" + name;
                            break;
                        }
                        else
                        {
                            objResponse.MESSAGE_CODE = "002-FAILED";
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, objResponse, "application/json");
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, objResponse, "application/json");
            }
            catch (Exception ex)
            {
                response objResponse = new response();
                objResponse.MESSAGE_CODE = "002-FAILED";
                return Request.CreateResponse(HttpStatusCode.InternalServerError, objResponse, "application/json");
            }
        }


    }
}






model


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TESTAPI.Models
{
    public class TestModel
    {
        public string name { get; set; }
        public string sname { get; set; }
    }
    public class response
    {
        public string MESSAGE_CODE { get; set; }
    }
}
