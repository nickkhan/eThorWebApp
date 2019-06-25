﻿using System.Collections.Generic;
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
    public class eThorTestEntitiesController : ControllerBase
    {
        private IWindsorContainer Container { get; set; }
        public eThorTestEntitiesController(IWindsorContainer container)
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

        // GET: api/eThorTestEntities
        [HttpGet]
        public async Task<IEnumerable<eThorTestEntity>> GeteThorTestEntites()
        {
            return await ethorService.GetAll();
        }

        // GET: api/eThorTestEntities/5
        [HttpGet("{id}")]
        public async Task<string> GeteThorTestEntity(int id)
        {
            var eThorTestEntity = await ethorService.Get(id);
            return JsonConvert.SerializeObject(eThorTestEntity);
        }

        // PUT: api/eThorTestEntities/5
        [HttpPut("{id}")]
        public async Task PuteThorTestEntity(eThorTestEntity eThorTestEntity)
        {
            await ethorService.Update(eThorTestEntity);
        }

        // POST: api/eThorTestEntities
        [HttpPost]
        public async Task PosteThorTestEntity(eThorTestEntity eThorTestEntity)
        {
            await ethorService.Add(eThorTestEntity);
        }

        // DELETE: api/eThorTestEntities/5
        [HttpDelete("{id}")]
        public async Task DeleteeThorTestEntity(int Id)
        {
            await ethorService.Delete(Id);
        }
    }
}
