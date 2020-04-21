using System.Text;

namespace EnvelhecerBem.Mvc.Models
{
    public static class OutputHelper
    {
        public static string FormatarEndereco(IEnderecoViewModel enderecoViewModel)
        {
            var sb = new StringBuilder();
            sb.Append(enderecoViewModel.Logradouro);
            if (!string.IsNullOrWhiteSpace(enderecoViewModel.Numero)) sb.Append($", {enderecoViewModel.Numero}");
            if (!string.IsNullOrWhiteSpace(enderecoViewModel.Complemento)) sb.Append($" - {enderecoViewModel.Complemento}");
            sb.AppendLine()
              .AppendLine($"{enderecoViewModel.Cidade} - {enderecoViewModel.Uf}")
              .Append(enderecoViewModel.Cep);
            return sb.ToString();
        }
    }
}