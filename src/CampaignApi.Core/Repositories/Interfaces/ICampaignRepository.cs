using System;
using System.Collections.Generic;
using CampaignApi.Core.Models;

namespace CampaignApi.Core.Repositories.Interfaces
{
    public interface ICampaignRepository
    {
        IEnumerable<Campaign> GetAll();
        void CostsSum();
        void Add(Campaign campaign);
        void Remove(Guid id);
        void Update(Campaign campaign);
    }
}