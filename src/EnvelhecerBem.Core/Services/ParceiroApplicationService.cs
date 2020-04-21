using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnvelhecerBem.Core.Data;
using EnvelhecerBem.Core.Domain;
using Serilog;

namespace EnvelhecerBem.Core.Services
{
    public class ParceiroApplicationService
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<ParceiroApplicationService>();
        private readonly IParceiroRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ParceiroApplicationService(IParceiroRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Parceiro> ObterParceiro(Guid id)
        {
            try
            {
                var parceiro = await _repository.Load(id);
                Log.Debug("{@Parceiro} recuperado", parceiro);
                return parceiro;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocorreu um erro ao obter o pareceiro {ParceiroId}", id);
                throw new ApplicationException("Erro ao obter o parceiro.", ex);
            }
        }

        public async Task RegistrarParceiro(Parceiro parceiro)
        {
            try
            {
                if (parceiro == null) throw new ArgumentNullException(nameof(parceiro));
                parceiro.Id = parceiro.Id == Guid.Empty ? Guid.NewGuid() : parceiro.Id;
                parceiro.DataRegistro = DateTime.Now;
                await _repository.Add(parceiro);
                await _unitOfWork.Commit();
                Log.Debug("{@Parceiro} adicionado", parceiro);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocorreu um erro ao adicionar o {@Parceiro}", parceiro);
                throw new ApplicationException("Erro ao adicionar o parceiro.", ex);
            }
        }

        public async Task AlterarParceiro(Parceiro parceiro)
        {
            try
            {
                if (parceiro == null) throw new ArgumentNullException(nameof(parceiro));
                await _repository.Update(parceiro);
                await _unitOfWork.Commit();
                Log.Debug("{@Parceiro} alterado", parceiro);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocorreu um erro ao alterar o {@Parceiro}", parceiro);
                throw new ApplicationException("Erro ao alterar o parceiro.", ex);
            }
        }

        public async Task RemoverParceiro(Guid parceiroId)
        {
            try
            {
                if (parceiroId.Equals(Guid.Empty)) throw new ArgumentException(nameof(parceiroId));
                await _repository.Delete(parceiroId);
                await _unitOfWork.Commit();
                Log.Debug("{ParceiroId} removido", parceiroId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ocorreu um erro ao remover parceiro {parceiroId}", parceiroId);
                throw new ApplicationException("Erro ao remover o parceiro.", ex);
            }
        }

        public async Task<IEnumerable<Parceiro>> ListarParceiros()
        {
            try
            {
                var list = await _repository.ListAll();
                Log.Debug("Listando parceiros");
                return list;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ocorreu um erro ao listar parceiros");
                throw new ApplicationException("Erro ao listar parceiros", ex);
            }
        }

        public async Task<IEnumerable<Parceiro>> Procurar(Expression<Func<Parceiro, bool>> expression)
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