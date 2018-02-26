using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace api.Controllers
{
  [Route("/configs")]
  public class ConfigsController : Controller
  {
    private readonly IConfiguration _configuration;
    public ConfigsController(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    [HttpGet]
    public Configuration Get()
    {
      return new Configuration {
        ConfigOne = _configuration["CustomConfigurationSection:ConfigOne"],
        ConfigTwo = _configuration["CustomConfigurationSection:ConfigTwo"]
      };
    }
  }
}
