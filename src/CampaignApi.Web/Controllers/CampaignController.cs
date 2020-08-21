using CampaignApi.Core.Models;
using CampaignApi.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;


namespace MvcDatabaseConnectivity_Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignController : Controller
    {  
        string connectionString = @"Data Source = (local)\sql; Initial Catalog = sample; Integrated Security=True";
        private readonly ICampaignRepository _campaignRepository;
        
        public CampaignController(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }
       
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Campaign",sqlCon);
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
                string query = "INSERT INTO Campaign VALUES(@Name,@Id,@Cost,@Intensity,@MediaType,@SubjectOfAdvertisement,@Target)";
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
        public ActionResult Edit(Guid id)
        {
            Campaign campaign = new Campaign();
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * FROM Campaign Where Id = @Id";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query,sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@Id",id);
                sqlDa.Fill(dtblProduct);
            }
            if (dtblProduct.Rows.Count == 1)
            {
                campaign.Name = dtblProduct.Rows[0][0].ToString();
                campaign.Id = Guid.Parse(dtblProduct.Rows[0][1].ToString());
                campaign.Cost = Convert.ToDouble(dtblProduct.Rows[0][2].ToString());
                campaign.Intensity = Convert.ToInt32(dtblProduct.Rows[0][3].ToString());
                campaign.MediaType = dtblProduct.Rows[0][4].ToString();
                campaign.SubjectOfAdvertisement = dtblProduct.Rows[0][5].ToString();
                campaign.Target = dtblProduct.Rows[0][6].ToString();

                return View(campaign);
            }
            
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