using System;
namespace election_night_api.Models
{
    public class CandidateDetails
    {
        public CandidateDetails()
        {
        }

        public string Candidate { get; set; }
        public string Votes { get; set; }
    }
}
