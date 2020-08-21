using System;
using System.Collections.Generic;
using System.Linq;
using CampaignApi.Core.Models;
using CampaignApi.Core.Repositories.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace CampaignApi.Core.Repositories
{
    public class CampaignRepository : ICampaignRepository
    {
        string connectionString = @"Data Source = (local)\sql; Initial Catalog = sample; Integrated Security=True";
        private readonly ICampaignRepository _campaignRepository;
        private static ISet<Campaign> _campaigns;
        
        public CampaignRepository(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public Campaign Get(Guid id)
            => _campaigns.Single(x => x.Id == id);

        public Campaign Get(string name)
            => _campaigns.Single(x => x.Name == name);

        public void Create(Campaign campaign)
        {
            _campaigns.Add(campaign);

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
        }

        public double Raport()
        {
            double CostsSum = 0;
            foreach (Campaign campaign in _campaigns)
            {
                CostsSum += campaign.Cost;
            }

            return CostsSum;
        }

        public IEnumerable<Campaign> GetAll()
            => _campaigns;

        public void Delete(Guid id)
        {
            var campaign = Get(id);
            _campaigns.Remove(campaign);

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE FROM Campaign Where CampaignId = @Id";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Id", id);
                sqlCmd.ExecuteNonQuery();
            }
        }

        public void Edit(Guid id)
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
            }
        }
    }
}