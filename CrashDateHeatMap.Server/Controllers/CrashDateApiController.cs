using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CrashDateHeatMap.Server.Models;

namespace CrashDateHeatMap.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CrashDateApiController : ControllerBase
    {
        private static CrashDateSchema _cached;

        static CrashDateApiController()
        {
            // After this runs successfully the parameters need to go into a .env file immediately.
            _cached = CrashDateController.LoadCrashData("E:\\CSharpProjects\\CrashDateHeatMap\\CrashDateHeatMap.Server\\Assets\\compressed_data.csv.gz","compressed_data.csv");
        }

        [HttpGet]
        public IEnumerable<CrashDateRecord> GetAll()
        {
            return _cached.RecordProducer();
        }
    }

}
