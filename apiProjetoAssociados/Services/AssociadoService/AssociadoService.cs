using apiProjetoAssociados.Data;
using apiProjetoAssociados.Models;
using apiProjetoAssociados.Models.AssociadoModels;
using apiProjetoAssociados.Services.EmpresaServices;

namespace apiProjetoAssociados.Services.AssociadoService
{
    public class AssociadoService : IAssociadoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmpresaService _empresaService;
        public AssociadoService(ApplicationDbContext context, IEmpresaService empresaService)
        {
            _context = context;
            _empresaService = empresaService;
        }

        public async Task<ServiceResponse<List<AssociadoModel>>> CreateAssociado(AssociadoViewModel novaAssociado)
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

                var associado = new AssociadoModel()
                {
                    Nome = novaAssociado.Nome,
                    Cpf = novaAssociado.Cpf
                    //DtNascimento = novaAssociado.DtNascimento
                };

                _context.Add(associado);
                _context.SaveChanges();

                CadastrarSociedade(associado.Id, novaAssociado.Empresas);

                serviceResponse.Dados = _context.Associados.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<AssociadoModel>>> DeleteAssociado(int id)
        {
            ServiceResponse<List<AssociadoModel>> serviceResponse = new ServiceResponse<List<AssociadoModel>>();

            try
            {
                AssociadoModel Associado = GetAssociadoById(id).Result.Dados;

                if (Associado == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Associado NÃO encontrada!";
                    serviceResponse.Sucesso = false;
                }

                _context.Associados.Remove(Associado);
                _context.SaveChanges();

                serviceResponse.Dados = _context.Associados.ToList();


            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;

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

        }

        public async Task<ServiceResponse<AssociadoViewModel>> UpdateAssociado(AssociadoViewModel editadoAssociado)
        {
            ServiceResponse<AssociadoViewModel> serviceResponse = new ServiceResponse<AssociadoViewModel>();

            try
            {
                AssociadoModel associadoSelecionado = GetAssociadoById(editadoAssociado.Id).Result.Dados;

                if (associadoSelecionado == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Associado NÃO encontrada!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                associadoSelecionado.Nome = editadoAssociado.Nome;
                associadoSelecionado.Cpf = editadoAssociado.Cpf;
                //associadoSelecionado.DtNascimento = editadoAssociado.DtNascimento;

                _context.Associados.Update(associadoSelecionado);
                _context.SaveChanges();

                var associadosEmpresa = _empresaService.GetEmpresasAssociado().Result.Dados;

                foreach (var item in associadosEmpresa)
                {
                    if (item.AssociadoId == editadoAssociado.Id)
                    {
                        _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    }
                }

                CadastrarSociedade(editadoAssociado.Id, editadoAssociado.Empresas);

                serviceResponse.Dados = editadoAssociado;

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;


        }

        private void CadastrarSociedade(int IdAssociado, List<CheckBoxViewModel> sociedade)
        {

            try
            {

                foreach (var item in sociedade)
                {

                    if (item.Checked)
                    {

                        var associar = new AssociadoModelEmpresaModel()
                        {
                            EmpresaId = item.Id,
                            AssociadoId = IdAssociado
                        };

                        _context.AssociadosEmpresa.AddRange(associar);

                    }
                }

                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ServiceResponse<List<CheckBoxViewModel>>> GetEmpresasAssociado(int IdAssociado)
        {

            ServiceResponse<List<CheckBoxViewModel>> response = new ServiceResponse<List<CheckBoxViewModel>>();
            List<CheckBoxViewModel> lstEmpresas = new List<CheckBoxViewModel>();

            try
            {

                var empresasAssociado = from c in _context.Empresas
                                        select new CheckBoxViewModel
                                        {
                                            Id = c.Id,
                                            Nome = c.Nome,
                                            Checked = _context.AssociadosEmpresa
                                                        .Any(ce => ce.AssociadoId == IdAssociado && ce.EmpresaId == c.Id)
                                        };

                lstEmpresas = empresasAssociado.ToList();
                response.Dados = lstEmpresas;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

    }
}
