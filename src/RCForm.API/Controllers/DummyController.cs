using Microsoft.AspNetCore.Mvc;
using RCForm.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCForm.API.Controllers
{
    public class DummyController : Controller
    {
        private RCFormContext _ctx;

        public DummyController(RCFormContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/testdatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
