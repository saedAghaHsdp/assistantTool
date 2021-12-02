using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RWSTroubleshooting.Domain;

namespace RWSTroubleshooting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StepsController
    {
        [HttpGet, Route("{stepId}")]
        public Step Get(int stepId)
        {
            return new Step
            {
                Text = "What is the issue?", Options = new Option[]
                {
                    new Option {Text = "Login issues", NextStep = 2},
                    new Option {Text = "Worklist issues", NextStep = 3}
                }.ToList()
            };
        }
    }
}
