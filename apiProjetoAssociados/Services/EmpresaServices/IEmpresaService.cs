using apiProjetoAssociados.Models;
using apiProjetoAssociados.Models.EmpresaModels;

namespace apiProjetoAssociados.Services.EmpresaServices
{
    public interface IEmpresaService
    {
        Task<ServiceResponse<List<EmpresaModel>>> GetEmpresas();
        Task<ServiceResponse<EmpresaModel>> GetEmpresaById(int id);
        Task<ServiceResponse<List<EmpresaModel>>> CreateEmpresa(EmpresaModel novaEmpresa);        
        Task<ServiceResponse<List<EmpresaModel>>> UpdateEmpresa(EmpresaModel editadoEmpresa);
        Task<ServiceResponse<List<EmpresaModel>>> DeleteEmpresa(int id);
    }
}
