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

        [HttpGet]
        public async Task<IEnumerable<eThorTestEntity>> Get()
        {
            return await ethorService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<eThorTestEntity> Get(int id)
        {
            var eThorTestEntity = await ethorService.Get(id);
            return eThorTestEntity;
        }

        [HttpPut]
        public async Task Put(object eThorTestEntity)
        {
            eThorTestEntity o = JsonConvert.DeserializeObject<eThorTestEntity>(eThorTestEntity.ToString());
            await ethorService.Update(o);
        }

        [HttpPost]
        public async Task Post(eThorTestEntity eThorTestEntity)
        {
            await ethorService.Add(eThorTestEntity);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int Id)
        {
            await ethorService.Delete(Id);
        }
    }
}
