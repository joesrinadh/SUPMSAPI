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
    public class CompanyController : ApiController
    {
        private string strConnectionString = ConfigurationManager.ConnectionStrings["supmsconnection"].ConnectionString;
        private SqlCommand _sqlCommand;
        private SqlDataAdapter _sqlDataAdapter;
        DataSet _dtSet;

        private Entities db = new Entities();

        // GET: api/Company
        public IQueryable<Company> GetCompanies()
        {
            return db.Companies;
        }

        // GET: api/Company/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult GetCompany(int id)
        {
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // PUT: api/Company/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompany(int id, Company company)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != company.CompanyId)
            //{
            //    return BadRequest();
            //}

            //db.Entry(company).State = EntityState.Modified;

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CompanyExists(id))
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
            _sqlCommand.CommandText = "stp_Company_Update";
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.Parameters.AddWithValue("@CompanyId", company.CompanyId);
            _sqlCommand.Parameters.AddWithValue("@CompanyName", company.CompanyName);
            _sqlCommand.Parameters.AddWithValue("@OwnerName", company.OwnerName);
            int result = Convert.ToInt32(_sqlCommand.ExecuteNonQuery());
            _sqlCommand.Connection.Close();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Company
        [ResponseType(typeof(Company))]
        public IHttpActionResult PostCompany(Company company)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //db.Companies.Add(company);
            //db.SaveChanges();

            //return CreatedAtRoute("DefaultApi", new { id = company.CompanyId }, company);

            // ADO.NET Code
            SqlConnection _sqlConnection = new SqlConnection(strConnectionString);
            _sqlCommand = new SqlCommand();
            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.Connection.Open();
            _sqlCommand.CommandText = "stp_Company_Insert";
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.Parameters.AddWithValue("@CompanyName", company.CompanyName);
            _sqlCommand.Parameters.AddWithValue("@OwnerName", company.OwnerName);
            int result = Convert.ToInt32(_sqlCommand.ExecuteNonQuery());
            _sqlCommand.Connection.Close();

            return CreatedAtRoute("DefaultApi", new { id = result }, company);

        }

        // DELETE: api/Company/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult DeleteCompany(int id)
        {
            //Company company = db.Companies.Find(id);
            //if (company == null)
            //{
            //    return NotFound();
            //}

            //db.Companies.Remove(company);
            //db.SaveChanges();

            // ADO.NET Code
            SqlConnection _sqlConnection = new SqlConnection(strConnectionString);
            _sqlCommand = new SqlCommand();
            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.Connection.Open();
            _sqlCommand.CommandText = "stp_Company_Delete";
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.Parameters.AddWithValue("@CompanyId", id);
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

        private bool CompanyExists(int id)
        {
            return db.Companies.Count(e => e.CompanyId == id) > 0;
        }
    }
}