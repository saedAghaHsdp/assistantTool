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
            switch (stepId)
            {
                case 1:
                    return new Step
                    {
                        Text = "What is the issue?",
                        Options = new Option[]
                        {
                            new Option {Text = "Login issues", NextStep = 2},
                            new Option {Text = "Worklist issues", NextStep = 3}
                        }
                    };
                case 2:
                    return new Step
                    {
                        Text = "Login issues",
                        Options = new Option[]
                        {
                            new Option {Text = "Username", NextStep = 4},
                            new Option {Text = "Permission", NextStep = 5}
                        }
                    };
                case 3:
                    return new Step
                    {
                        Text = "Worklist issues",
                        Options = new Option[]
                        {
                            new Option {Text = "New exam not loading", NextStep = 6},
                            new Option {Text = "Assignment not updated", NextStep = 7},
                            new Option {Text = "Can't create new worklist", NextStep = 8}
                        }
                    };
                default:
                    return new Step
                    {
                        Text = "...",
                        Options = new Option[]
                        {
                            new Option {Text = "Call your administrator", NextStep = 1},
                        }
                    };
            }
           
        }
    }
}
