using System;
using System.ComponentModel.DataAnnotations;

namespace EnvelhecerBem.Core.Domain
{
    public class Parceiro : Entity<Guid>, IEntity
    {
        [Required]
        [MaxLength(128)]
        public string RazaoSocial { get; set; }

        [Required]
        [RegularExpression(@"^\d{14,15}$")]
        public string Cnpj { get; set; }

        [Required]
        [MaxLength(64)]
        public string Contato { get; set; }

        [Required]
        [RegularExpression(@"^\d{10,11}$")]
        [MaxLength(11)]
        public string Telefone { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataRegistro { get; set; }

        [Required]
        public Endereco Endereco { get; set; }
    }
}