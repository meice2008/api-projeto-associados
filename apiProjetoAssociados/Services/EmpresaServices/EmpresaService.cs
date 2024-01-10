﻿using apiProjetoAssociados.Data;
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

        public async Task<ServiceResponse<List<EmpresaModel>>> CreateEmpresa(EmpresaViewModel novaEmpresa)
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

                var empresa = new EmpresaModel()
                {
                    Nome = novaEmpresa.Nome,
                    Cnpj = novaEmpresa.Cnpj
                };

                _context.Add(empresa);
                _context.SaveChanges();

                CadastrarSociedade(empresa.Id, novaEmpresa.Associados);

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
                EmpresaModel empresa = _context.Empresas.AsNoTracking().FirstOrDefault(x => x.Id == id);

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
                        
        public async Task<ServiceResponse<EmpresaViewModel>> UpdateEmpresa(EmpresaViewModel editadoEmpresa)
        {
            ServiceResponse<EmpresaViewModel> serviceResponse = new ServiceResponse<EmpresaViewModel>();

            try
            {
                //EmpresaModel empresa = _context.Empresas.AsNoTracking().FirstOrDefault(x => x.Id == editadoEmpresa.Id);
                var empresaSelecionada = GetEmpresaById(editadoEmpresa.Id).Result.Dados;

                if (empresaSelecionada == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Empresa NÃO encontrada!";
                    serviceResponse.Sucesso = false;
                    
                    return serviceResponse;

                }

                empresaSelecionada.Nome = editadoEmpresa.Nome;
                empresaSelecionada.Cnpj = editadoEmpresa.Cnpj;

                _context.Empresas.Update(empresaSelecionada);
                _context.SaveChanges();

                var associadosEmpresa = GetEmpresasAssociado().Result;

                foreach (var item in associadosEmpresa)
                {
                    if (item.EmpresaId == editadoEmpresa.Id)
                    {
                        _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    }
                }

                CadastrarSociedade(editadoEmpresa.Id, editadoEmpresa.Associados);
                

                //serviceResponse.Dados = _context.Empresas.ToList();

                serviceResponse.Dados = editadoEmpresa;

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;



            //throw new NotImplementedException();
        }

        private async Task<IEnumerable<AssociadoModelEmpresaModel>> GetEmpresasAssociado()
        {
            var associadosEmpresa = _context.AssociadosEmpresa;
            return associadosEmpresa;
        }

        public async Task<ServiceResponse<List<EmpresaModel>>> DeleteEmpresa(int id)
        {
            ServiceResponse<List<EmpresaModel>> serviceResponse = new ServiceResponse<List<EmpresaModel>>();

            try
            {

                EmpresaModel empresa = GetEmpresaById(id).Result.Dados;  //_context.Empresas.FirstOrDefault(x => x.Id == id);

                if (empresa == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Empresa NÃO encontrada!";
                    serviceResponse.Sucesso = false;
                }

                _context.Empresas.Remove(empresa);
                _context.SaveChanges();

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


        private void CadastrarSociedade(int IdEmpresa, List<CheckBoxViewModel> sociedade)
        {

            try
            {

                foreach (var item in sociedade)
                {

                    if (item.Checked)
                    {

                        var associar = new AssociadoModelEmpresaModel()
                        {
                            EmpresaId = IdEmpresa,
                            AssociadoId = item.Id
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

        public  List<CheckBoxViewModel> GetAssociadosEmpresa(int IdEmpresa)
        {
            var lstAssociados = new List<CheckBoxViewModel>();

            try
            {

                var AssociadosEmpresa = from c in _context.Associados
                                        select new CheckBoxViewModel
                                        {
                                            Id = c.Id,
                                            Nome = c.Nome,
                                            Checked = _context.AssociadosEmpresa
                                                        .Any(ce => ce.EmpresaId == IdEmpresa && ce.AssociadoId == c.Id)
                                        };

                lstAssociados = AssociadosEmpresa.ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return lstAssociados;
        }


    }
}
