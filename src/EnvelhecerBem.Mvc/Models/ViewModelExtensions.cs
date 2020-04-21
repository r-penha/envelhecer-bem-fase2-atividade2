using System;
using System.Globalization;
using EnvelhecerBem.Core.Domain;

namespace EnvelhecerBem.Mvc.Models
{
    public static class ViewModelExtensions
    {
        #region Parceiro

        public static Parceiro ToDomain(this ViewModels.Parceiro self)
        {
            return new Parceiro
                   {
                       Id = self.Id,
                       Cnpj = self.Cnpj,
                       RazaoSocial = self.RazaoSocial,
                       Contato = self.Contato,
                       Telefone = self.Telefone,
                       DataRegistro = self.DataRegistro != null ? DateTime.Parse(self.DataRegistro, CultureInfo.GetCultureInfo("pt-BR")) : DateTime.MinValue,
                       Endereco = new Endereco(self.Logradouro,
                                               self.Numero,
                                               self.Complemento,
                                               self.Cidade,
                                               self.Uf,
                                               self.Cep)
                   };
        }

        public static ViewModels.Parceiro ToViewModel(this Parceiro self)
        {
            return new ViewModels.Parceiro
                   {
                       Id = self.Id,
                       Cnpj = self.Cnpj,
                       RazaoSocial = self.RazaoSocial,
                       Contato = self.Contato,
                       Telefone = self.Telefone,
                       DataRegistro = self.DataRegistro.ToString("dd/MM/yyyy"),
                       Logradouro = self.Endereco.Logradouro,
                       Numero = self.Endereco.Numero,
                       Complemento = self.Endereco.Complemento,
                       Cidade = self.Endereco.Cidade,
                       Uf = self.Endereco.Uf,
                       Cep = self.Endereco.Cep
                   };
        }

        #endregion

        #region Cliente

        public static Cliente ToDomain(this ViewModels.Cliente self)
        {
            return new Cliente
                   {
                       Id = self.Id,
                       Cpf = self.Cpf,
                       Nome = self.Nome,
                       Email = self.Email,
                       Telefone = self.Telefone,
                       Sexo = self.Sexo,
                       Plano = self.Plano,
                       DataNascimento = DateTime.Parse(self.DataNascimento, CultureInfo.GetCultureInfo("pt-BR")),
                       DataRegistro = self.DataRegistro != null ? DateTime.Parse(self.DataRegistro, CultureInfo.GetCultureInfo("pt-BR")) : DateTime.MinValue,
                       Endereco = new Endereco(self.Logradouro,
                                               self.Numero,
                                               self.Complemento,
                                               self.Cidade,
                                               self.Uf,
                                               self.Cep)
                   };
        }

        public static ViewModels.Cliente ToViewModel(this Cliente self)
        {
            return new ViewModels.Cliente
                   {
                       Id = self.Id,
                       Cpf = self.Cpf,
                       Nome = self.Nome,
                       Email = self.Email,
                       Telefone = self.Telefone,
                       Sexo = self.Sexo,
                       Plano = self.Plano,
                       DataNascimento = self.DataNascimento.ToString("dd/MM/yyyy"),
                       DataRegistro = self.DataRegistro.ToString("dd/MM/yyyy"),
                       Logradouro = self.Endereco.Logradouro,
                       Numero = self.Endereco.Numero,
                       Complemento = self.Endereco.Complemento,
                       Cidade = self.Endereco.Cidade,
                       Uf = self.Endereco.Uf,
                       Cep = self.Endereco.Cep
                   };
        }

        #endregion

        #region Endereco

        private static Endereco ToDomain(this ViewModels.Endereco self)
        {
            if (self == null) return null;
            return new Endereco(self.Logradouro,
                                self.Numero,
                                self.Complemento,
                                self.Cidade,
                                self.Cidade,
                                self.Cep);
        }

        public static ViewModels.Endereco ToViewModel(this Endereco self)
        {
            return new ViewModels.Endereco()
                   {
                       Logradouro = self.Logradouro,
                       Numero = self.Numero,
                       Complemento = self.Complemento,
                       Cidade = self.Cidade,
                       Uf = self.Uf,
                       Cep = self.Cep
                   };
        }

        #endregion
    }
}