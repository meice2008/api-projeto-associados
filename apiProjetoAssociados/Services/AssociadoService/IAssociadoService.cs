using apiProjetoAssociados.Models.AssociadoModels;
using apiProjetoAssociados.Models;

namespace apiProjetoAssociados.Services.AssociadoService
{
    public interface IAssociadoService
    {
        Task<ServiceResponse<List<AssociadoModel>>> GetAssociados();
        Task<ServiceResponse<AssociadoModel>> GetAssociadoById(int id);
        Task<ServiceResponse<List<AssociadoModel>>> CreateAssociado(AssociadoModel novaAssociado);
        Task<ServiceResponse<List<AssociadoModel>>> UpdateAssociado(AssociadoModel editadoAssociado);
        Task<ServiceResponse<List<AssociadoModel>>> DeleteAssociado(int id);
    }
}
