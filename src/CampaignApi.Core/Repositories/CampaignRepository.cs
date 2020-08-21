using System;
using System.Collections.Generic;
using System.Linq;
using CampaignApi.Core.Models;
using CampaignApi.Core.Repositories.Interfaces;

namespace CampaignApi.Core.Repositories
{
    public class CampaignRepository : ICampaignRepository
    {
        private static ISet<Campaign> _campaigns;

        public Campaign Get(Guid id)
            => _campaigns.Single(x => x.Id == id);

        public Campaign Get(string name)
            => _campaigns.Single(x => x.Name == name);

        public void Add(Campaign campaign)
        {
            _campaigns.Add(campaign);
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

        public void Remove(Guid id)
        {
            var campaign = Get(id);
            _campaigns.Remove(campaign);
        }

        public void Edit(Campaign campaign)
        {
            throw new System.NotImplementedException();
        }
    }
}