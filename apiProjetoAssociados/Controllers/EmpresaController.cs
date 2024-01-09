using apiProjetoAssociados.Models;
using apiProjetoAssociados.Models.EmpresaModels;
using apiProjetoAssociados.Services.EmpresaServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiProjetoAssociados.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<EmpresaModel>>> GetEmpresaById(int id)
        {
            ServiceResponse<EmpresaModel> serviceResponse = await _empresaInterface.GetEmpresaById(id);
            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<EmpresaModel>>>> CreateEmpresa(EmpresaModel novaEmpresa)
        {
            var empresas = await _empresaInterface.CreateEmpresa(novaEmpresa);
            return Ok(empresas);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<EmpresaModel>>>> UpdateEmpresa(EmpresaModel novaEmpresa)
        {
            ServiceResponse<List<EmpresaModel>> empresas = await _empresaInterface.UpdateEmpresa(novaEmpresa);
            return Ok(empresas);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<EmpresaModel>>>> DeleteEmpresa(int id)
        {
            ServiceResponse<List<EmpresaModel>> empresas = await _empresaInterface.DeleteEmpresa(id);
            return Ok(empresas);
        }



    }
}
