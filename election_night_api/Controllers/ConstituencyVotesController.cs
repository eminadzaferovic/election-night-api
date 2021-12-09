using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using election_night_api.Models;
using election_night_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace election_night_api.Controllers
{
    [Route("api/[controller]")]
    public class ConstituencyVotesController
    {
        private ConstituencyVotesService _constituencyVotesService;

        public ConstituencyVotesController()
        {
            _constituencyVotesService = new ConstituencyVotesService();
        }

        [HttpGet]
        [Route("getVotes/{fileUploadStatus}")]
        public List<ConstituencyVotes> GetConstituencyVotes(decimal fileUploadStatus)
        {
            var filePath = "";

            if (fileUploadStatus == 1)
            {
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload/Files/election_night_votes.csv");
            }
            else
            {
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload/Files/election_night_votes_override.csv");
            }

            List<ConstituencyVotes> values = File.ReadAllLines(filePath)
                                           .Select(value => _constituencyVotesService.ReadFromCsv(value))
                                           .ToList();
            return values;
        }

        [HttpPost]
        [Route("uploadFile")]
        public async Task<bool> UploadFile(IFormFile file)
        {
            if (_constituencyVotesService.CheckFileFormat(file))
            {
                string fileName;

                try
                {
                    var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                    fileName = "election_night_votes" + extension;

                    var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload/Files");

                    if (!Directory.Exists(pathBuilt))
                    {
                        Directory.CreateDirectory(pathBuilt);
                    }

                    if (File.Exists(pathBuilt + "/" + fileName))
                    {
                        fileName = "election_night_votes_override" + extension;
                    }

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload/Files",
                       fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                }
            }

            return true;
        }
    }
}