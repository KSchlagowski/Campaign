using CampaignApi.Core.Models;
using CampaignApi.Core.Repositories.Interfaces;
using CampaignApi.Db.Models;
using Microsoft.AspNetCore.Mvc;
using System;  
using System.Collections.Generic;  
using System.Configuration;  
using System.Data;  
using System.Data.SqlClient;  
using System.Web.Mvc;  
using System.Linq;
using System.Web;

   
namespace MvcDatabaseConnectivity_Demo.Controllers  
{  
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignController : Controller
    {  
        string connectionString = @"Data Source = (local)\sqle2012; Initial Catalog = MvcCrudDB; Integrated Security=True";
       
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Product",sqlCon);
                sqlDa.Fill(dtblProduct);
            }
            return View(dtblProduct);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Campaign());
        }

        [HttpPost]
        public ActionResult Create(Campaign campaign)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "INSERT INTO Product VALUES(@Name,@Id,@Cost,@Intensity,@MediaType,@SubjectOfAdvertisement,@Target)";
                SqlCommand sqlCmd = new SqlCommand(query,sqlCon);
                sqlCmd.Parameters.AddWithValue("@Name", campaign.Name);
                sqlCmd.Parameters.AddWithValue("@Id", campaign.Id);
                sqlCmd.Parameters.AddWithValue("@Cost", campaign.Cost);
                sqlCmd.Parameters.AddWithValue("@Intensity", campaign.Intensity);
                sqlCmd.Parameters.AddWithValue("@MediaType", campaign.MediaType);
                sqlCmd.Parameters.AddWithValue("@SubjectOfAdvertisement", campaign.SubjectOfAdvertisement);
                sqlCmd.Parameters.AddWithValue("@Target", campaign.Target);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Campaign campaign)
        {
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE FROM Campaign Where CampaignId = @Id";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Id", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }  
}  