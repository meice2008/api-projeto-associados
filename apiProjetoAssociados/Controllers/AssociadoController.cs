using apiProjetoAssociados.Models.AssociadoModels;
using apiProjetoAssociados.Models;
using Microsoft.AspNetCore.Mvc;
using apiProjetoAssociados.Services.AssociadoService;
using Microsoft.AspNetCore.Authorization;

namespace apiProjetoAssociados.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AssociadoController : ControllerBase
    {
        private readonly IAssociadoService _AssociadoInterface;

        public AssociadoController(IAssociadoService AssociadoInterface)
        {
            _AssociadoInterface = AssociadoInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<AssociadoModel>>>> GetAssociados()
        {
            var Associados = await _AssociadoInterface.GetAssociados();
            return Ok(Associados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<AssociadoModel>>> GetAssociadoById(int id)
        {
            ServiceResponse<AssociadoModel> serviceResponse = await _AssociadoInterface.GetAssociadoById(id);
            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<AssociadoModel>>>> CreateAssociado(AssociadoModel novaAssociado)
        {
            var Associados = await _AssociadoInterface.CreateAssociado(novaAssociado);
            return Ok(Associados);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<AssociadoModel>>>> UpdateAssociado(AssociadoModel novaAssociado)
        {
            ServiceResponse<List<AssociadoModel>> Associados = await _AssociadoInterface.UpdateAssociado(novaAssociado);
            return Ok(Associados);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<AssociadoModel>>>> DeleteAssociado(int id)
        {
            ServiceResponse<List<AssociadoModel>> Associados = await _AssociadoInterface.DeleteAssociado(id);
            return Ok(Associados);
        }
    }
}
