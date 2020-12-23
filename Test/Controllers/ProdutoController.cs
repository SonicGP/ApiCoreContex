using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models;

namespace Test.Controllers
{
    [ApiController]
    [Route("v1/produto")]
    public class ProdutoController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Production>>> Get([FromServices] DataContext context)
        {
            var produto = await context.productions.Include(c => c.Category).ToListAsync();
            return produto;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Production>> GetById([FromServices] DataContext context, int id)
        {
            var produto = await context.productions
            .Include(c => c.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
            return produto;
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        public async Task<ActionResult<List<Production>>> GetByCategory([FromServices] DataContext context, int id)
        {
            var produtoCat = await context.productions
            .Include(c => c.Category)
            .AsNoTracking()
            .Where(x => x.CategoryId == id)
            .ToListAsync();

            return produtoCat;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Production>> Post([FromServices] DataContext context, [FromBody] Production production)
        {
            if(ModelState.IsValid)
            {
                context.productions.Add(production);
                await context.SaveChangesAsync();
                return production;
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Production>> Put([FromServices] DataContext context, [FromBody] Production production, int id)
        {
            if(production.Id != id)
            {
                return BadRequest();
            }

            context.Entry(production).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return production;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Production>> Delete([FromServices] DataContext context, int id)
        {
            var produto = await context.productions.FirstOrDefaultAsync(x => x.Id == id);

            if(produto.Id != id)
            {
                return BadRequest();
            }

            context.productions.Remove(produto);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}