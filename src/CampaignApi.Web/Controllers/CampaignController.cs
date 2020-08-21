using CampaignApi.Core.Models;
using CampaignApi.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;


namespace CampaignApi.Web.Controllers
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
            _campaignRepository.Create(campaign);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Guid id)
        {
            _campaignRepository.Edit(id);
            
            return RedirectToAction("Index");
        }
        
        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _campaignRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }  
}  