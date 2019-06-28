using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using eThorWebApp.Shared.Data;
using eThorWebApp.Shared.Models;
using eThorWebApp.Shared.Interfaces;
using Newtonsoft.Json;
using Castle.Windsor;

namespace eThorWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class eThorEntityController : ControllerBase
    {
        private IWindsorContainer Container { get; set; }
        public eThorEntityController(IWindsorContainer container)
        {
            Container = container;
        }

        public IethorService ethorService
        {
            get
            {
                return Container.Resolve<IethorService>();
            }
        }

        // GET: api/eThorTestEntity
        [HttpGet]
        public async Task<IEnumerable<eThorTestEntity>> Get()
        {
            return await ethorService.GetAll();
        }

        // GET: api/eThorTestEntity/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            var eThorTestEntity = await ethorService.Get(id);
            return JsonConvert.SerializeObject(eThorTestEntity);
        }

        // PUT: api/eThorTestEntity/5
        [HttpPut("{id}")]
        public async Task Put(eThorTestEntity eThorTestEntity)
        {
            await ethorService.Update(eThorTestEntity);
        }

        // POST: api/eThorTestEntity
        [HttpPost]
        public async Task Post(eThorTestEntity eThorTestEntity)
        {
            await ethorService.Add(eThorTestEntity);
        }

        // DELETE: api/eThorTestEntity/5
        [HttpDelete("{id}")]
        public async Task Delete(int Id)
        {
            await ethorService.Delete(Id);
        }
    }
}
