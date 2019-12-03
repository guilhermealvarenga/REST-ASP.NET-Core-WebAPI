using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Api.ViewModels;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        // validação de notificações de erro

        // validação de modelstate

        // validação de operação de negócios
    }

    [Route("api/[fornecedores]")]
    public class FornecedoresController : MainController
    {

        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedoresController(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task<ActionResult<IEnumerable<FornecedorViewModel>>> ObterTodos()
        {
            var fornecedores = await _fornecedorRepository.ObterTodos();

            return Ok(fornecedores);
        }
    }
}
