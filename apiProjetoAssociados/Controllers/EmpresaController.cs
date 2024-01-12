using apiProjetoAssociados.Models;
using apiProjetoAssociados.Models.EmpresaModels;
using apiProjetoAssociados.Services.EmpresaServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apiProjetoAssociados.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaInterface;

        public EmpresaController(IEmpresaService empresaInterface)
        {
            _empresaInterface = empresaInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<EmpresaModel>>>> GetEmpresas()
        {
            var empresas = await _empresaInterface.GetEmpresas();
            return Ok(empresas);
        }

        [HttpGet("GetEmpresasAssociado")]
        public async Task<ServiceResponse<List<AssociadoModelEmpresaModel>>> GetEmpresasAssociado()
        {
            var resultado = await _empresaInterface.GetEmpresasAssociado();
            return resultado;
        }

        [HttpGet("GetAssociadosEmpresa/{idEmpresa}")]
        public async Task<ActionResult<ServiceResponse<List<CheckBoxViewModel>>>> GetAssociadosEmpresa(int IdEmpresa)
        {
            var resultado = await _empresaInterface.GetAssociadosEmpresa(IdEmpresa);
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<EmpresaModel>>> GetEmpresaById(int id)
        {
            var serviceResponse = await _empresaInterface.GetEmpresaById(id);
            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<EmpresaModel>>>> CreateEmpresa(EmpresaViewModel empresa)
        {
            var empresas = await _empresaInterface.CreateEmpresa(empresa);
            return Ok(empresas);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<EmpresaViewModel>>> UpdateEmpresa(int id, EmpresaViewModel editadoEmpresa)
        {
            editadoEmpresa.Id = id;
            var empresas = await _empresaInterface.UpdateEmpresa(editadoEmpresa);
            return Ok(empresas);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<EmpresaModel>>>> DeleteEmpresa(int id)
        {
            var empresas = await _empresaInterface.DeleteEmpresa(id);
            return Ok(empresas);
        }



    }
}
