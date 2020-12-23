using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models;

namespace Test.Controllers
{
    [ApiController]
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {
            var Categoria = await context.categories.ToListAsync();
            return Categoria;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post([FromServices] DataContext context, [FromBody] Category category)
        {
            if(ModelState.IsValid)
            {
                context.categories.Add(category);
                await context.SaveChangesAsync();
                return category;
            }
            else{return BadRequest(ModelState);}
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Put([FromServices] DataContext context, [FromBody] Category category, int id)
        {
            if(category.Id != id)
            {
                return BadRequest();
            }

            context.Entry(category).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return category;            
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Delete([FromServices] DataContext context, int id)
        {
            var categor = await context.categories.FirstOrDefaultAsync(x => x.Id == id);
            if(categor.Id != id)
            {
                return BadRequest();
            }

            context.categories.Remove(categor);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}