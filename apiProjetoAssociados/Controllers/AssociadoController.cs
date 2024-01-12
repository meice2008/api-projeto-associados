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
            var serviceResponse = await _AssociadoInterface.GetAssociados();
            return Ok(serviceResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<AssociadoModel>>> GetAssociadoById(int id)
        {
            var serviceResponse = await _AssociadoInterface.GetAssociadoById(id);
            return Ok(serviceResponse);
        }

        [HttpGet("GetEmpresasAssociado/{IdAssociado}")]
        public async Task<ActionResult<ServiceResponse<List<CheckBoxViewModel>>>> GetEmpresasAssociado(int IdAssociado)
        {
            var resultado = await _AssociadoInterface.GetEmpresasAssociado(IdAssociado);
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<AssociadoModel>>>> CreateAssociado(AssociadoViewModel novaAssociado)
        {
            var serviceResponse = await _AssociadoInterface.CreateAssociado(novaAssociado);
            return Ok(serviceResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<AssociadoViewModel>>> UpdateAssociado(int id, AssociadoViewModel editadoAssociado)
        {
            editadoAssociado.Id = id;
            var serviceResponse = await _AssociadoInterface.UpdateAssociado(editadoAssociado);
            return Ok(serviceResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<AssociadoModel>>>> DeleteAssociado(int id)
        {
            var serviceResponse = await _AssociadoInterface.DeleteAssociado(id);
            return Ok(serviceResponse);
        }
    }
}
