using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnvelhecerBem.Core.Data;
using EnvelhecerBem.Core.Domain;
using Serilog;

namespace EnvelhecerBem.Core.Services
{
    public class ClienteApplicationService
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<ClienteApplicationService>();
        private readonly IClienteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ClienteApplicationService(IClienteRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Cliente> ObterCliente(Guid id)
        {
            try
            {
                var cliente = await _repository.Load(id);
                Log.Debug("{@Cliente} recuperado", cliente);
                return cliente;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocorreu um erro ao obter o pareceiro {ClienteId}", id);
                throw new ApplicationException("Erro ao obter o cliente.", ex);
            }
        }

        public async Task RegistrarCliente(Cliente cliente)
        {
            try
            {
                if (cliente == null) throw new ArgumentNullException(nameof(cliente));
                cliente.Id = Guid.NewGuid();
                cliente.DataRegistro = DateTime.Now;
                await _repository.Add(cliente);
                await _unitOfWork.Commit();
                Log.Debug("{@Cliente} adicionado", cliente);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocorreu um erro ao adicionar o {@Cliente}", cliente);
                throw new ApplicationException("Erro ao adicionar o cliente.", ex);
            }
        }

        public async Task AlterarCliente(Cliente cliente)
        {
            try
            {
                if (cliente == null) throw new ArgumentNullException(nameof(cliente));
                await _repository.Update(cliente);
                await _unitOfWork.Commit();
                Log.Debug("{@Cliente} alterado", cliente);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocorreu um erro ao alterar o {@Cliente}", cliente);
                throw new ApplicationException("Erro ao alterar o cliente.", ex);
            }
        }

        public async Task RemoverCliente(Guid clienteId)
        {
            try
            {
                if (clienteId.Equals(Guid.Empty)) throw new ArgumentException(nameof(clienteId));
                await _repository.Delete(clienteId);
                await _unitOfWork.Commit();
                Log.Debug("{ClienteId} removido", clienteId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ocorreu um erro ao remover cliente {clienteId}", clienteId);
                throw new ApplicationException("Erro ao remover o cliente.", ex);
            }
        }

        public Task<IEnumerable<Cliente>> ListarClientes()
        {
            try
            {
                var list = _repository.ListAll();
                Log.Debug("Listando clientes");
                return list;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ocorreu um erro ao listar clientes");
                throw new ApplicationException("Erro ao listar clientes", ex);
            }
        }

        public async Task<IEnumerable<Cliente>> Procurar(Expression<Func<Cliente, bool>> expression)
        {
            try
            {
                var list = await _repository.Find(expression);
                Log.Debug("Listando parceiros");
                return list;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ocorreu um erro ao listar parceiros");
                throw new ApplicationException("Erro ao listar parceiros", ex);
            }
        }
    }
}