using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.Utils;
using FI.WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
            BoBeneficiario boBeneficiario = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                if (bo.VerificarExistencia(Utils.RemoverNaoNumerico(model.CPF)))
                {
                    Response.StatusCode = 400;
                    return Json("Já existe um cliente cadastrado com este CPF.");
                }

                model.Id = bo.Incluir(new Cliente()
                {
                    CPF = Utils.RemoverNaoNumerico(model.CPF),
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone
                });

                foreach(BeneficiarioModel beneficiario in model.Beneficiarios)
                {
                    if (boBeneficiario.VerificarExistenciaUpdate(Utils.RemoverNaoNumerico(model.CPF), model.Id))
                    {
                        Response.StatusCode = 400;
                        return Json("Já existe um beneficiário cadastrado com este CPF: " + model.CPF + "para este cliente.");
                    }

                    boBeneficiario.Incluir(new Beneficiario
                    {
                        Nome = beneficiario.Nome,
                        CPF = Utils.RemoverNaoNumerico(beneficiario.CPF),
                        IdCliente = model.Id
                    });
                }

           
                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
            BoBeneficiario boBeneficiario = new BoBeneficiario();
            List<Beneficiario> beneficiarioList = boBeneficiario.ObterBeneficiariosPorCliente(model.Id);

            var excluir = beneficiarioList.Where(x => !model.Beneficiarios.Select(y => y.Id).ToList().Contains(x.Id)).ToList();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                if (bo.VerificarExistenciaUpdate(Utils.RemoverNaoNumerico(model.CPF), model.Id))
                {
                    Response.StatusCode = 400;
                    return Json("Já existe um cliente cadastrado com este CPF.");
                }

                bo.Alterar(new Cliente()
                {
                    Id = model.Id,
                    CEP = Utils.RemoverNaoNumerico(model.CEP),
                    CPF = Utils.RemoverNaoNumerico(model.CPF),
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = Utils.RemoverNaoNumerico(model.Telefone)
                });

                if(model.Beneficiarios != null)
                {
                    foreach (BeneficiarioModel beneficiario in model.Beneficiarios.Where(x => x.Id != 0))
                    {
                        if (boBeneficiario.VerificarExistenciaUpdate(Utils.RemoverNaoNumerico(model.CPF), model.Id))
                        {
                            Response.StatusCode = 400;
                            return Json("Já existe um beneficiário cadastrado com este CPF: " + model.CPF + "para este cliente.");
                        }

                        boBeneficiario.Alterar(new Beneficiario
                        {
                            Id = beneficiario.Id,
                            CPF = beneficiario.CPF,
                            Nome = beneficiario.Nome,
                        });
                    }

                    foreach (BeneficiarioModel beneficiario in model.Beneficiarios.Where(x => x.Id == 0))
                    {
                        if (boBeneficiario.VerificarExistenciaUpdate(Utils.RemoverNaoNumerico(model.CPF), model.Id))
                        {
                            Response.StatusCode = 400;
                            return Json("Já existe um beneficiário cadastrado com este CPF: " + model.CPF + "para este cliente.");
                        }

                        boBeneficiario.Incluir(new Beneficiario
                        {
                            Id = beneficiario.Id,
                            CPF = beneficiario.CPF,
                            Nome = beneficiario.Nome,
                            IdCliente = model.Id
                        });
                    }

                    foreach (Beneficiario beneficiarioExcluir in excluir)
                    {
                        boBeneficiario.Excluir(beneficiarioExcluir.Id);
                    }
                }
                               
                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            BoBeneficiario boBeneficiario = new BoBeneficiario();

            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;

            List<Beneficiario> beneficiarioList = boBeneficiario.ObterBeneficiariosPorCliente(cliente.Id);

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    CPF = cliente.CPF,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    Beneficiarios = beneficiarioList.Select(x => new BeneficiarioModel { Id = x.Id, Nome = x.Nome, CPF = x.CPF, ClienteId = x.IdCliente }).ToList(),
                };

            
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}