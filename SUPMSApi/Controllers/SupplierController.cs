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
using System.Configuration;
using System.Data.SqlClient;

namespace SUPMSApi.Controllers
{
    public class SupplierController : ApiController
    {
        private string strConnectionString = ConfigurationManager.ConnectionStrings["supmsconnection"].ConnectionString;
        private SqlCommand _sqlCommand;
        private SqlDataAdapter _sqlDataAdapter;
        DataSet _dtSet;

        private Entities db = new Entities();

        // GET: api/Supplier
        public IQueryable<Supplier> GetSuppliers()
        {
            return db.Suppliers;
        }

        // GET: api/Supplier/5
        [ResponseType(typeof(Supplier))]
        public IHttpActionResult GetSupplier(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        // PUT: api/Supplier/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSupplier(int id, Supplier supplier)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != supplier.SupplierId)
            //{
            //    return BadRequest();
            //}

            //db.Entry(supplier).State = EntityState.Modified;

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!SupplierExists(id))
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
            _sqlCommand.CommandText = "stp_Supplier_Update";
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.Parameters.AddWithValue("@SupplierId", supplier.SupplierId);
            _sqlCommand.Parameters.AddWithValue("@SupplierName", supplier.SupplierName);
            _sqlCommand.Parameters.AddWithValue("@Address", supplier.Address);
            _sqlCommand.Parameters.AddWithValue("@GSTNumber", supplier.GSTNumber);
            _sqlCommand.Parameters.AddWithValue("@OtherDetails", supplier.OtherDetails);
            _sqlCommand.Parameters.AddWithValue("@Active", supplier.Active);
            int result = Convert.ToInt32(_sqlCommand.ExecuteNonQuery());
            _sqlCommand.Connection.Close();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Supplier
        [ResponseType(typeof(Supplier))]
        public IHttpActionResult PostSupplier(Supplier supplier)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //db.Suppliers.Add(supplier);
            //db.SaveChanges();

            // ADO.NET Code
            SqlConnection _sqlConnection = new SqlConnection(strConnectionString);
            _sqlCommand = new SqlCommand();
            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.Connection.Open();
            _sqlCommand.CommandText = "stp_Supplier_Insert";
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.Parameters.AddWithValue("@SupplierName", supplier.SupplierName);
            _sqlCommand.Parameters.AddWithValue("@Address", supplier.Address);
            _sqlCommand.Parameters.AddWithValue("@GSTNumber", supplier.GSTNumber);
            _sqlCommand.Parameters.AddWithValue("@OtherDetails", supplier.OtherDetails);
            _sqlCommand.Parameters.AddWithValue("@Active", supplier.Active);
            int result = Convert.ToInt32(_sqlCommand.ExecuteNonQuery());
            _sqlCommand.Connection.Close();

            return CreatedAtRoute("DefaultApi", new { id = result }, supplier);
        }

        // DELETE: api/Supplier/5
        [ResponseType(typeof(Supplier))]
        public IHttpActionResult DeleteSupplier(int id)
        {
            //Supplier supplier = db.Suppliers.Find(id);
            //if (supplier == null)
            //{
            //    return NotFound();
            //}

            //db.Suppliers.Remove(supplier);
            //db.SaveChanges();

            // ADO.NET Code
            SqlConnection _sqlConnection = new SqlConnection(strConnectionString);
            _sqlCommand = new SqlCommand();
            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.Connection.Open();
            _sqlCommand.CommandText = "stp_Supplier_Delete";
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.Parameters.AddWithValue("@SupplierId", id);
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

        private bool SupplierExists(int id)
        {
            return db.Suppliers.Count(e => e.SupplierId == id) > 0;
        }
    }
}