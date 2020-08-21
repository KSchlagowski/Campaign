using System;

namespace CampaignApi.Core.Models
{
    public class Campaign
    {
        private double _value;
        public double Cost 
        { 
            get { return _value; } 
            set {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(value)} must be greater than 0.");
                _value = value;
            }
        }
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string SubjectOfAdvertisement { get; set; }
        public string Target { get; set; }
        public string MediaType { get; set; }
        public int Intensity { get; set; }
    }
}