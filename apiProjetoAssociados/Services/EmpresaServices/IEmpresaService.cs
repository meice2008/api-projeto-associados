using apiProjetoAssociados.Models;
using apiProjetoAssociados.Models.EmpresaModels;

namespace apiProjetoAssociados.Services.EmpresaServices
{
    public interface IEmpresaService
    {
        Task<ServiceResponse<List<EmpresaModel>>> GetEmpresas();
        Task<ServiceResponse<EmpresaModel>> GetEmpresaById(int id);
        Task<ServiceResponse<List<EmpresaModel>>> CreateEmpresa(EmpresaViewModel novaEmpresa);        
        Task<ServiceResponse<EmpresaViewModel>> UpdateEmpresa(EmpresaViewModel editadoEmpresa);
        Task<ServiceResponse<List<EmpresaModel>>> DeleteEmpresa(int id);
        Task<ServiceResponse<List<CheckBoxViewModel>>> GetAssociadosEmpresa(int IdEmpresa);
        Task<ServiceResponse<List<AssociadoModelEmpresaModel>>> GetEmpresasAssociado();
    }
}
