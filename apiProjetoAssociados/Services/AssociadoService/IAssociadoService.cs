using apiProjetoAssociados.Models.AssociadoModels;
using apiProjetoAssociados.Models;

namespace apiProjetoAssociados.Services.AssociadoService
{
    public interface IAssociadoService
    {
        Task<ServiceResponse<List<AssociadoModel>>> GetAssociados();
        Task<ServiceResponse<AssociadoModel>> GetAssociadoById(int id);
        Task<ServiceResponse<List<AssociadoModel>>> CreateAssociado(AssociadoViewModel novaAssociado);
        Task<ServiceResponse<AssociadoViewModel>> UpdateAssociado(AssociadoViewModel editadoAssociado);
        Task<ServiceResponse<List<AssociadoModel>>> DeleteAssociado(int id);
        Task<ServiceResponse<List<CheckBoxViewModel>>> GetEmpresasAssociado(int IdAssociado);
    }
}
