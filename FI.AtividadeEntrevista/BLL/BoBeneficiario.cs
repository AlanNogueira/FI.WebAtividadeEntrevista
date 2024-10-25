﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiarios bene = new DAL.DaoBeneficiarios();
            return bene.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public void Alterar(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiarios bene = new DAL.DaoBeneficiarios();
            bene.Alterar(beneficiario);
        }

        /// <summary>
        /// Consulta o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public DML.Beneficiario Consultar(long id)
        {
            DAL.DaoBeneficiarios bene = new DAL.DaoBeneficiarios();
            return bene.Consultar(id);
        }

        /// <summary>
        /// Excluir o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoBeneficiarios bene = new DAL.DaoBeneficiarios();
            bene.Excluir(id);
        }

        /// <summary>
        /// Lista os beneficiarios
        /// </summary>
        public List<DML.Beneficiario> Listar()
        {
            DAL.DaoBeneficiarios bene = new DAL.DaoBeneficiarios();
            return bene.Listar();
        }

        /// <summary>
        /// Lista os beneficiarios
        /// </summary>
        public List<DML.Beneficiario> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoBeneficiarios bene = new DAL.DaoBeneficiarios();
            return bene.Pesquisa(iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistenciaUpdate
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistenciaUpdate(string CPF, long Id)
        {
            DAL.DaoBeneficiarios bene = new DAL.DaoBeneficiarios();
            return bene.VerificarExistenciaUpdate(CPF, Id);
        }

        public List<DML.Beneficiario> ObterBeneficiariosPorCliente(long clienteId)
        {
            DAL.DaoBeneficiarios bene = new DAL.DaoBeneficiarios();
            return bene.ObterBeneficiariosPorCliente(clienteId);
        }
    }
}