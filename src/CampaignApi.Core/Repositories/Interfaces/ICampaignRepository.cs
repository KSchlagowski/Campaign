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
        void Create(Campaign campaign);
        void Delete(Guid id);
        void Edit(Guid id);
    }
}