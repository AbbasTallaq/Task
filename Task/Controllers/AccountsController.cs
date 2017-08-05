using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Task.Models;

namespace Task.Controllers
{
    public class AccountsController : Controller
    {
        /// <summary>
        /// database connection
        /// </summary>
        private Taskcontext db = new Taskcontext();
        //Port, it should change based on testing machine
        private string port = "62539";



        //list all accounts in method Index
        // GET: Accounts2
        public ActionResult Index()
        {
            IEnumerable<TaskDTO> Taskcxt = null;

            using (var client = new HttpClient())
            {
                //initiate connection web api and retrieved the data 
                client.BaseAddress = new Uri("http://localhost:"+ port + "/api/");
                //HTTP GET
                var responseTask = client.GetAsync("AccountsApi");
                responseTask.Wait();
                //check api response 
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //api retrun data 
                    var readTask = result.Content.ReadAsAsync<IList<TaskDTO>>();
                    readTask.Wait();

                    Taskcxt = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    Taskcxt = Enumerable.Empty<TaskDTO>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(Taskcxt);
        }

        //list account details
        public ActionResult Detail(int id)
        {
            //run automapper
            var Accounts = db.Accounts.Include("AccountClass").Include("AccountType").Where(x => x.ID == id).SingleOrDefault();

            var LoadData = AutoMapper.Mapper.Map<Accounts, TaskDTO>(Accounts);
        

            return View(LoadData);
        }

        //create a new account
        public ActionResult Create()
        {
            //load data into ddl 
            var AccountClass = from v in db.AccountClass
                        select v;
            var AccountType = from c in db.AccountType select c;

            IEnumerable<SelectListItem> AccountClasslist = new SelectList(AccountClass, "Id", "AccountClassName");
            ViewBag.AccountClasslist = AccountClasslist;
            IEnumerable<SelectListItem> AccountTypeist = new SelectList(AccountType, "id", "AccountTypeDesc");
            ViewBag.AccountTypeist = AccountTypeist;

            return View();
        }


        [HttpPost]
        public ActionResult Create(Accounts Accounts)
        {
            using (var client = new HttpClient())
            {           
                //initiate connection web api and retrieved the data 

                client.BaseAddress = new Uri("http://localhost:"+port+"/api/Accounts");

               // HTTP POST
                var postTask = client.PostAsJsonAsync<Accounts>("AccountsApi", Accounts);
                postTask.Wait();
                //checking respose
                var result = postTask.Result;

    
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            //api error 
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(Accounts);
        
    }

        public ActionResult Edit(int id)
        {
            //load data into ddl 


            TaskDTO Accounts = null;
            var AccountClass = from v in db.AccountClass
                               select v;
            var AccountType = from c in db.AccountType select c;

            var i = db.Accounts.Find(id);

            IEnumerable<SelectListItem> AccountClasslist = new SelectList(AccountClass, "Id", "AccountClassName",i.AccountClassID);
            ViewBag.AccountClasslist = AccountClasslist;
            IEnumerable<SelectListItem> AccountTypeist = new SelectList(AccountType, "id", "AccountTypeDesc",i.AccountTypeCode);
            ViewBag.AccountTypeist = AccountTypeist;
            using (var client = new HttpClient())
            {
                //initiate connection web api and retrieved the data 
                client.BaseAddress = new Uri("http://localhost:"+port+"/api/");
                //HTTP GET
                var responseTask = client.GetAsync("AccountsApi?ID=" + id.ToString());
                responseTask.Wait();
                //checking respose
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //return api data 
                    var readTask = result.Content.ReadAsAsync<TaskDTO>();
                    readTask.Wait();

                    Accounts = readTask.Result;
                }
            }

            return View(Accounts);
        }

       [HttpPost]
        public ActionResult Edit(Accounts accounts)
        {
            using (var client = new HttpClient())
            {               //initiate connection web api and retrieved the data 
                client.BaseAddress = new Uri("http://localhost:"+port+"/api/Accounts");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Accounts>("AccountsApi", accounts);
                putTask.Wait();
                //checking respose

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(accounts);

        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {            //initiate connection web api and retrieved the data 
                client.BaseAddress = new Uri("http://localhost:"+port+"/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("AccountsApi/" + id.ToString());
                deleteTask.Wait();
                //checking respose
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }



    }
}