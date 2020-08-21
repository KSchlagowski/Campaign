using System;
using System.Collections.Generic;
using CampaignApi.Core.Models;

namespace CampaignApi.Core.Repositories.Interfaces
{
    public interface ICampaignRepository
    {
        Campaign Get(Guid id);
        Campaign Get(string name);
        IEnumerable<Campaign> GetAll();
        double Raport();
        void Add(Campaign campaign);
        void Remove(Guid id);
        void Edit(Campaign campaign);
    }
}