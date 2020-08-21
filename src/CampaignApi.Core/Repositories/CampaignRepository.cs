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

        public void Add(Campaign campaign)
        {
            _campaigns.Add(campaign);
        }

        public void CostsSum()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Campaign> GetAll()
            => _campaigns;

        public void Remove(Guid id)
        {
            var campaign = Get(id);
            _campaigns.Remove(campaign);
        }

        public void Update(Campaign campaign)
        {
            throw new System.NotImplementedException();
        }
    }
}