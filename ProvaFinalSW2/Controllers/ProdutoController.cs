using Microsoft.AspNetCore.Mvc;

namespace ProvaFinalSW2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProdutoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Produto
        [HttpGet]
        public IActionResult GetProdutos()
        {
            var produtos = _context.Produtos.Include(p => p.UsuarioCadastro).Include(p => p.UsuarioUpdate).ToList();
            return Ok(produtos);
        }

        // GET: api/Produto/1
        [HttpGet("{id}")]
        public IActionResult GetProduto(int id)
        {
            var produto = _context.Produtos.Include(p => p.UsuarioCadastro).Include(p => p.UsuarioUpdate).FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // POST: api/Produto
        [HttpPost]
        public IActionResult PostProduto([FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
        }

        // PUT: api/Produto/1
        [HttpPut("{id}")]
        public IActionResult PutProduto(int id, [FromBody] Produto produto)
        {
            if (!ModelState.IsValid || id != produto.Id)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Produtos.Any(p => p.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Produto/1
        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
    }


}
