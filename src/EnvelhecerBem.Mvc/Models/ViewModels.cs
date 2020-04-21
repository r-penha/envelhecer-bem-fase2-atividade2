using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EnvelhecerBem.Core.Domain;

namespace EnvelhecerBem.Mvc.Models
{
    public static class ViewModels
    {
        public class Parceiro : IEnderecoViewModel
        {
            public Guid Id { get; set; }

            [DisplayName("Razão social")]
            [Required(ErrorMessage = "Informe um valor.")]
            [MaxLength(128, ErrorMessage = "Número de caracteres máximo excedido.")]
            public string RazaoSocial { get; set; }

            [DisplayName("CNPJ")]
            [Required(ErrorMessage = "Informe um valor.")]
            [RegularExpression(@"^\d{14,15}$", ErrorMessage = "CNPJ inválido.")]
            [MaxLength(15, ErrorMessage = "Número de caracteres máximo excedido.")]
            public string Cnpj { get; set; }

            [DisplayName("Nome do responsável")]
            [Required(ErrorMessage = "Informe um valor.")]
            [MaxLength(64, ErrorMessage = "Número de caracteres máximo excedido.")]
            public string Contato { get; set; }

            [DisplayName("Telefone")]
            [Required(ErrorMessage = "Informe um valor.")]
            [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Telefone inválido.")]
            [MaxLength(11, ErrorMessage = "Número de caracteres máximo excedido.")]
            public string Telefone { get; set; }

            [DisplayName("Logradouro")]
            [Required(ErrorMessage = "Informe um valor.")]
            [MaxLength(128)]
            public string Logradouro { get; set; }

            [DisplayName("Número")]
            [MaxLength(6)]
            public string Numero { get; set; }

            [DisplayName("Complemento")]
            [MaxLength(128)]
            public string Complemento { get; set; }

            [DisplayName("Cidade")]
            [Required(ErrorMessage = "Informe um valor.")]
            [MaxLength(64)]
            public string Cidade { get; set; }

            [DisplayName("Estado")]
            [Required(ErrorMessage = "Informe um valor.")]
            [MaxLength(2)]
            public string Uf { get; set; }

            [DisplayName("CEP")]
            [Required(ErrorMessage = "Informe um valor.")]
            [RegularExpression(@"^\d{8}$", ErrorMessage = "CEP inválido.")]
            [MaxLength(8)]
            public string Cep { get; set; }

            public string DataRegistro { get; set; }
        }

        public class Cliente : IEnderecoViewModel
        {
            public Guid Id { get; set; }

            [DisplayName("Nome")]
            [Required(ErrorMessage = "Informe um valor.")]
            [MaxLength(128, ErrorMessage = "Número de caracteres máximo excedido.")]
            public string Nome { get; set; }

            [DisplayName("CPF")]
            [Required(ErrorMessage = "Informe um valor.")]
            [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF inválido.")]
            [MaxLength(11, ErrorMessage = "Número de caracteres máximo excedido.")]
            public string Cpf { get; set; }

            [DisplayName("Email")]
            [Required(ErrorMessage = "Informe um valor.")]
            [EmailAddress(ErrorMessage = "E-mail inválido.")]
            [MaxLength(64, ErrorMessage = "Número de caracteres máximo excedido.")]
            public string Email { get; set; }

            [DisplayName("Telefone")]
            [Required(ErrorMessage = "Informe um valor.")]
            [RegularExpression(@"\d{10,11}", ErrorMessage = "Telefone inválido.")]
            [MaxLength(11, ErrorMessage = "Número de caracteres máximo excedido.")]
            public string Telefone { get; set; }

            [DisplayName("Sexo")]
            [Required(ErrorMessage = "Informe um valor.")]
            public Sexo Sexo { get; set; }

            [DisplayName("Plano")]
            [Required(ErrorMessage = "Informe um valor.")]
            public TipoPlano Plano { get; set; }

            [DisplayName("Logradouro")]
            [Required(ErrorMessage = "Informe um valor.")]
            [MaxLength(128, ErrorMessage = "Número de caracteres máximo excedido.")]
            public string Logradouro { get; set; }

            [DisplayName("Número")]
            [MaxLength(6, ErrorMessage = "Número de caracteres máximo excedido.")]
            public string Numero { get; set; }

            [DisplayName("Complemento")]
            [MaxLength(128, ErrorMessage = "Número de caracteres máximo excedido.")]
            public string Complemento { get; set; }

            [DisplayName("Cidade")]
            [Required(ErrorMessage = "Informe um valor.")]
            [MaxLength(64, ErrorMessage = "Número de caracteres máximo excedido.")]
            public string Cidade { get; set; }

            [DisplayName("Estado")]
            [Required(ErrorMessage = "Informe um valor.")]
            [MaxLength(2, ErrorMessage = "Número de caracteres máximo excedido.")]
            public string Uf { get; set; }

            [DisplayName("CEP")]
            [Required(ErrorMessage = "Informe um valor.")]
            [RegularExpression(@"^\d{8}$", ErrorMessage = "CEP inválido.")]
            [MaxLength(8, ErrorMessage = "Número de caracteres máximo excedido.")]
            public string Cep { get; set; }

            [DisplayName("Data de nascimento")]
            [Required(ErrorMessage = "Informe um valor.")]
            [MaxLength(10)]
            [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "Data inválida.")]
            public string DataNascimento { get; set; }

            public string DataRegistro { get; set; }
        }

        public class Endereco
        {
            [DisplayName("Logradouro")]
            [Required(ErrorMessage = "Informe um valor.")]
            [MaxLength(128)]
            public string Logradouro { get; set; }

            [DisplayName("Número")]
            [MaxLength(6)]
            public string Numero { get; set; }

            [DisplayName("Complemento")]
            [MaxLength(128)]
            public string Complemento { get; set; }

            [DisplayName("Cidade")]
            [Required(ErrorMessage = "Informe um valor.")]
            [MaxLength(64)]
            public string Cidade { get; set; }

            [DisplayName("Estado")]
            [Required(ErrorMessage = "Informe um valor.")]
            [MaxLength(2)]
            public string Uf { get; set; }

            [DisplayName("CEP")]
            [Required(ErrorMessage = "Informe um valor.")]
            [RegularExpression(@"^\d{8}$", ErrorMessage = "Formato de CNPJ inválido.")]
            [MaxLength(8)]
            public string Cep { get; set; }
        }
    }

    public interface IEnderecoViewModel
    {
        string Logradouro { get; }
        string Numero { get; }
        string Complemento { get; }
        string Cidade { get; }
        string Uf { get; }
        string Cep { get; }
    }
}