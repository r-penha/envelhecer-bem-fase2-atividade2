using System;
using System.ComponentModel.DataAnnotations;

namespace EnvelhecerBem.Core.Domain
{
    public class Cliente : Entity<Guid>, IEntity
    {
        [Required]
        [RegularExpression(@"^\d{11}$")]
        [MaxLength(11)]
        public string Cpf { get;  set; }
     
        [Required]
        [MaxLength(128)]
        public string Nome { get;  set; }

        [Required]
        [EmailAddress]
        [MaxLength(64)]
        public string Email { get;  set; }

        [Required]
        [RegularExpression(@"^\d{10,11}$")]
        [MaxLength(11)]
        public string Telefone { get;  set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get;  set; }

        [DataType(DataType.DateTime)]
        public DateTime DataRegistro { get; set; }

        [Required]
        public Sexo Sexo { get;  set; }
        
        [Required]
        public TipoPlano Plano { get; set; }
        
        [Required]
        public Endereco Endereco { get;  set; }
    }
}