using System;
using System.Collections.Generic;
using election_night_api.Models;
using Microsoft.AspNetCore.Http;

namespace election_night_api.Services
{
    public class ConstituencyVotesService
    {
        public ConstituencyVotesService()
        {
        }

        public bool CheckFileFormat(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".csv");
        }

        public ConstituencyVotes ReadFromCsv(string csvLine)
        {
            string[] csvValues = csvLine.Split(',');

            var candidateName = new List<string>();
            ConstituencyVotes constituencyVotes = new ConstituencyVotes();
            constituencyVotes.Constituency = csvValues[0];

            for (int i = 1; i < csvValues.Length - 1; i = i + 2)
            {
                CandidateDetails candidateDetails = new CandidateDetails();

                candidateDetails.Candidate = getCandidateFullName(csvValues[i + 1]);
                candidateDetails.Votes = csvValues[i];

                if (candidateDetails.Candidate != "" && candidateDetails.Votes != "")
                {
                    constituencyVotes.CandidateDetails.Add(candidateDetails);
                    candidateName.Add(candidateDetails.Candidate);
                }
            }

            return constituencyVotes;
        }

        public string getCandidateFullName(string candidate)
        {
            var fullName = "";
            switch (candidate)
            {
                case "DT":
                    fullName = "Donald Trump";
                    break;
                case "HC":
                    fullName = "Hillary Clinton";
                    break;
                case "JB":
                    fullName = "Joe Biden";
                    break;
                case "JFK":
                    fullName = "John F. Kennedy";
                    break;
                case "JR":
                    fullName = "Jack Randall";
                    break;
            }
            return fullName;
        }
    }
}

