using apiProjetoAssociados.Data;
using apiProjetoAssociados.Models;
using apiProjetoAssociados.Models.AssociadoModels;
using Microsoft.EntityFrameworkCore;

namespace apiProjetoAssociados.Services.AssociadoService
{
    public class AssociadoService : IAssociadoService
    {
        private readonly ApplicationDbContext _context;
        public AssociadoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<AssociadoModel>>> CreateAssociado(AssociadoModel novaAssociado)
        {
            ServiceResponse<List<AssociadoModel>> serviceResponse = new ServiceResponse<List<AssociadoModel>>();

            try
            {
                if (novaAssociado == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Favor informar os dados!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                _context.Add(novaAssociado);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Associados.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;

            //throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<AssociadoModel>>> DeleteAssociado(int id)
        {
            ServiceResponse<List<AssociadoModel>> serviceResponse = new ServiceResponse<List<AssociadoModel>>();

            try
            {

                AssociadoModel Associado = _context.Associados.FirstOrDefault(x => x.Id == id);

                if (Associado == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Associado NÃO encontrada!";
                    serviceResponse.Sucesso = false;
                }

                _context.Associados.Remove(Associado);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Associados.ToList();


            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;

            //throw new NotImplementedException();
        }

        public async Task<ServiceResponse<AssociadoModel>> GetAssociadoById(int id)
        {
            ServiceResponse<AssociadoModel> serviceResponse = new ServiceResponse<AssociadoModel>();

            try
            {
                AssociadoModel Associado = _context.Associados.FirstOrDefault(x => x.Id == id);

                serviceResponse.Dados = Associado;

                if (serviceResponse.Dados == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Associado NÃO encontrada!";
                    serviceResponse.Sucesso = false;
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;



            //throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<AssociadoModel>>> GetAssociados()
        {
            ServiceResponse<List<AssociadoModel>> serviceResponse = new ServiceResponse<List<AssociadoModel>>();

            try
            {
                serviceResponse.Dados = _context.Associados.ToList();

                if (serviceResponse.Dados.Count() == 0)
                {
                    serviceResponse.Mensagem = "Nenhum dado encontrado!";
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;

            //throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<AssociadoModel>>> UpdateAssociado(AssociadoModel editadoAssociado)
        {
            ServiceResponse<List<AssociadoModel>> serviceResponse = new ServiceResponse<List<AssociadoModel>>();

            try
            {
                AssociadoModel Associado = _context.Associados.AsNoTracking().FirstOrDefault(x => x.Id == editadoAssociado.Id);
                //AssociadoModel Associado = GetAssociadoById(editadoAssociado.Id).Result.Dados;

                if (Associado == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Associado NÃO encontrada!";
                    serviceResponse.Sucesso = false;
                }

                _context.Associados.Update(editadoAssociado);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Associados.ToList();


            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;


            //throw new NotImplementedException();
        }
    }
}
