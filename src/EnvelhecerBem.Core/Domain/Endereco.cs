using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace EnvelhecerBem.Core.Domain
{
    [DataContract]
    public class Endereco : ValueObject<Endereco>
    {
        protected Endereco()
        {
        }

        public Endereco(string logradouro, string numero, string complemento, string cidade, string uf, string cep)
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cidade = cidade;
            Uf = uf;
            Cep = cep;
        }

        [Required]
        [MaxLength(128)]
        public string Logradouro { get; set; }

        [MaxLength(6)]
        public string Numero { get; set; }

        [MaxLength(128)]
        public string Complemento { get; set; }

        [Required]
        [MaxLength(64)]
        public string Cidade { get; set; }

        [Required]
        [MaxLength(2)]
        public string Uf { get; set; }

        [Required]
        [RegularExpression(@"\d{8}")]
        [MaxLength(8)]
        public string Cep { get; set; }
    }
}