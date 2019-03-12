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
using SUPMSApi.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace SUPMSApi.Controllers
{
    public class ItemController : ApiController
    {
        private string strConnectionString = ConfigurationManager.ConnectionStrings["supmsconnection"].ConnectionString;
        private SqlCommand _sqlCommand;
        private SqlDataAdapter _sqlDataAdapter;
        DataSet _dtSet;

        private Entities db = new Entities();

        // GET: api/Item
        public IQueryable<Item> GetItems()
        {
            return db.Items;
        }

        // GET: api/Item/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult GetItem(int id)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Item/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItem(int id, Item item)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != item.ItemId)
            //{
            //    return BadRequest();
            //}

            //db.Entry(item).State = EntityState.Modified;

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ItemExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            // ADO.NET Code
            SqlConnection _sqlConnection = new SqlConnection(strConnectionString);
            _sqlCommand = new SqlCommand();
            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.Connection.Open();
            _sqlCommand.CommandText = "stp_Item_Update";
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.Parameters.AddWithValue("@ItemId", item.ItemId);
            _sqlCommand.Parameters.AddWithValue("@ItemName", item.ItemName);
            int result = Convert.ToInt32(_sqlCommand.ExecuteNonQuery());
            _sqlCommand.Connection.Close();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Item
        [ResponseType(typeof(Item))]
        public IHttpActionResult PostItem(Item item)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //db.Items.Add(item);
            //db.SaveChanges();

            // ADO.NET Code
            SqlConnection _sqlConnection = new SqlConnection(strConnectionString);
            _sqlCommand = new SqlCommand();
            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.Connection.Open();
            _sqlCommand.CommandText = "stp_Item_Insert";
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.Parameters.AddWithValue("@ItemName", item.ItemName);
            int result = Convert.ToInt32(_sqlCommand.ExecuteNonQuery());
            _sqlCommand.Connection.Close();

            return CreatedAtRoute("DefaultApi", new { id = result }, item);
        }

        // DELETE: api/Item/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult DeleteItem(int id)
        {
            //Item item = db.Items.Find(id);
            //if (item == null)
            //{
            //    return NotFound();
            //}

            //db.Items.Remove(item);
            //db.SaveChanges();

            // ADO.NET Code
            SqlConnection _sqlConnection = new SqlConnection(strConnectionString);
            _sqlCommand = new SqlCommand();
            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.Connection.Open();
            _sqlCommand.CommandText = "stp_Item_Delete";
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.Parameters.AddWithValue("@ItemId", id);
            int result = Convert.ToInt32(_sqlCommand.ExecuteNonQuery());
            _sqlCommand.Connection.Close();

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(int id)
        {
            return db.Items.Count(e => e.ItemId == id) > 0;
        }
    }
}