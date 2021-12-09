using System;
using System.Collections.Generic;

namespace election_night_api.Models
{
    public class ConstituencyVotes
    {
        public ConstituencyVotes()
        {
            CandidateDetails = new List<CandidateDetails>();
        }

        public string Constituency { get; set; }

        public List<CandidateDetails> CandidateDetails { get; set; }
    }
}
