using apiProjetoAssociados.Data;
using apiProjetoAssociados.Models;
using apiProjetoAssociados.Models.EmpresaModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace apiProjetoAssociados.Services.EmpresaServices
{
    public class EmpresaService : IEmpresaService
    {
        private readonly ApplicationDbContext _context;
        public EmpresaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<EmpresaModel>>> GetEmpresas()
        {
            ServiceResponse<List<EmpresaModel>> serviceResponse = new ServiceResponse<List<EmpresaModel>>();

            try
            {
                serviceResponse.Dados = _context.Empresas.ToList();

                if(serviceResponse.Dados.Count() == 0)
                {
                    serviceResponse.Mensagem = "Nenhum dado encontrado!";
                }

            }catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;

            //throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<EmpresaModel>>> CreateEmpresa(EmpresaModel novaEmpresa)
        {
            ServiceResponse<List<EmpresaModel>> serviceResponse = new ServiceResponse<List<EmpresaModel>>();

            try
            {
                if(novaEmpresa == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Favor informar os dados!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                _context.Add(novaEmpresa);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Empresas.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;

            //throw new NotImplementedException();
        }

        public async Task<ServiceResponse<EmpresaModel>> GetEmpresaById(int id)
        {
            ServiceResponse<EmpresaModel> serviceResponse = new ServiceResponse<EmpresaModel>();

            try
            {
                EmpresaModel empresa = _context.Empresas.FirstOrDefault(x => x.Id == id);

                serviceResponse.Dados = empresa;

                if (serviceResponse.Dados == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Empresa NÃO encontrada!";
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
                        
        public async Task<ServiceResponse<List<EmpresaModel>>> UpdateEmpresa(EmpresaModel editadoEmpresa)
        {
            ServiceResponse<List<EmpresaModel>> serviceResponse = new ServiceResponse<List<EmpresaModel>>();

            try
            {
                EmpresaModel empresa = _context.Empresas.AsNoTracking().FirstOrDefault(x => x.Id == editadoEmpresa.Id);
                //EmpresaModel empresa = GetEmpresaById(editadoEmpresa.Id).Result.Dados;

                if (empresa == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Empresa NÃO encontrada!";
                    serviceResponse.Sucesso = false;
                }

                _context.Empresas.Update(editadoEmpresa);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Empresas.ToList();


            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;



            //throw new NotImplementedException();
        }


        public async Task<ServiceResponse<List<EmpresaModel>>> DeleteEmpresa(int id)
        {
            ServiceResponse<List<EmpresaModel>> serviceResponse = new ServiceResponse<List<EmpresaModel>>();

            try
            {                

                EmpresaModel empresa = _context.Empresas.FirstOrDefault(x => x.Id == id);

                if (empresa == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Empresa NÃO encontrada!";
                    serviceResponse.Sucesso = false;
                }

                _context.Empresas.Remove(empresa);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Empresas.ToList();


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
