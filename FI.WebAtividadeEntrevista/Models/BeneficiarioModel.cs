using FI.AtividadeEntrevista.Validations;
using Microsoft.Ajax.Utilities;
using System.ComponentModel.DataAnnotations;

namespace FI.WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        [Required]
        public long Id { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [Required]
        [CPFValidator(ErrorMessage = "Insira um CPF válido para o beneficiário: ")]
        public string CPF { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Id do cliente relacionado a este beneficiário
        /// </summary>
        public long ClienteId { get; set; }
    }
}