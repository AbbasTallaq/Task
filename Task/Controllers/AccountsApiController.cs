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
using Task.Models;

namespace Task.Controllers
{

    public class AccountsApiController : ApiController
    {
        private Taskcontext db = new Taskcontext();
        /// <summary>
        /// Load all account
        /// </summary>
        /// <returns></returns>


        public IHttpActionResult GetAllAccounts()
        {
            //list all avialable accounts in db
            var Accounts = db.Accounts.Include("AccountClass").Include("AccountType").ToList();
            //run automapper
            var LoadData = AutoMapper.Mapper.Map<IEnumerable <Accounts>  , IEnumerable<TaskDTO>>(Accounts);
            if (Accounts.Count == 0)
            {
                return NotFound();
            }

            return Ok(LoadData);
        }

        // GET: api/Accounts1/5
        
        [ResponseType(typeof(Accounts))]
        public IHttpActionResult GetAccounts(int id)
        {
            Accounts accounts = db.Accounts.Find(id);
            if (accounts == null)
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        /// <summary>
        /// Edit and update current record
        /// </summary>
        /// <param name="TaskDTO"></param>
        /// <returns></returns>

        // PUT: api/Accounts1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccounts(TaskDTO TaskDTO)
        {

            //check status 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //run automapper

            var LoadData = AutoMapper.Mapper.Map<TaskDTO, Accounts>(TaskDTO);
            //run update
          db.Entry(LoadData).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
               
                    throw;
                
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        /// <summary>
        /// Insert a new record
        /// </summary>
        /// <param name="TaskDTO"></param>
        /// <returns></returns>

        // POST: api/Accounts1
        [ResponseType(typeof(TaskDTO))]
        public IHttpActionResult PostAccounts(TaskDTO TaskDTO)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //run automapper

            var InsertData = AutoMapper.Mapper.Map<TaskDTO, Accounts>(TaskDTO);
            //insert the data
            db.Accounts.Add(InsertData);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = TaskDTO.ID }, TaskDTO);
        }


/// <summary>
/// Delete record from accounts db
/// </summary>
/// <param name="id"></param>
/// <returns></returns>

        // DELETE: api/Accounts1/5
        [ResponseType(typeof(Accounts))]
        public IHttpActionResult DeleteAccounts(int id)
        {
            Accounts accounts = db.Accounts.Find(id);
            if (accounts == null)
            {
                return NotFound();
            }

            db.Accounts.Remove(accounts);
            db.SaveChanges();

            return Ok(accounts);
        }
        /// <summary>
        /// clearing and closing and remaing slots 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// check wether  account available
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool AccountsExists(int id)
        {
            return db.Accounts.Count(e => e.ID == id) > 0;
        }
    }
}
