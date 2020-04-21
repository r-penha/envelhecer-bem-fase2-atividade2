## Plataforma e tecnologias utilizadas

A atividade foi desenvolvida utilizando-se as seguintes tecnologias

- .NET Core 3.1
- ASP.NET Core
- Microsoft SQL Server
- Bootstrap
- Entity Framework Core

## Principais detalhes de implementação

- Bootstrap customizado através de SASS
- Lógica de negócio encapsulada em _Application Services_ acionados pelos _Controllers_
- _View Models_ utilizados como protocolo de dados para as requisições
- Validação declarativa implementada através de atributos nas _View Models_
- _Extension methods_ criados para o mapeamento das _View Models_ para os _Domain Models_
- Criada a classe abstrata _Repository<T, TId>_ para encapsular toda lógica comum de acesso a dados
```c#
public abstract class Repository<T, TId> : IRepository<T, TId> 
    where T : class
{
    public DbSet<T> Collection { get; }

    protected Repository(DbSet<T> dbSet)
    {
        Collection = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
    }

    public async Task<T> Load(TId id)
    {
        return await Collection.FindAsync(id);
    }

    public async Task Add(T entity)
    {
        await Collection.AddAsync(entity);
    }

    public async Task Update(T entity)
    {
        await Task.FromResult(Collection.Update(entity));
    }

    public async Task Delete(TId entityId)
    {
        var entity = await Collection.FindAsync(entityId);
        if (entity == null) return;
        await Task.FromResult(Collection.Remove(entity));
    }

    public Task<IEnumerable<T>> ListAll()
    {
        return Task.FromResult(Collection.AsEnumerable());
    }

    public Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
    {
        return Task.FromResult(Collection.Where(expression).AsEnumerable());
    }
}
```
- Criada a interface _IUnitOfWork_ e a implementação _EfCoreUnitOfWork_ para tratar controlar o contexto transacional das requisições
```c#
public interface IUnitOfWork
{
    Task Commit();
}

public class EfCoreUnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;

    public EfCoreUnitOfWork(AppDbContext dbContext) => _dbContext = dbContext;

    public Task Commit() => _dbContext.SaveChangesAsync();
}
```
- Criado um _extension method_ para iniciar o contexto da aplicação
```c#
public static class AppModule
{
	public static IMvcBuilder AddAppModule(this IMvcBuilder builder, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection");
		
		builder.AddApplicationPart(typeof(AppModule).Assembly);

		builder.Services
			   .AddDbContext<AppDbContext>(o => o.UseSqlServer(connectionString))
			   .AddScoped<IDbConnection>(c => new SqlConnection(connectionString))
			   .AddScoped<IUnitOfWork, EfCoreUnitOfWork>()
			   .AddScoped<IParceiroRepository, ParceiroRepository>()
			   .AddScoped<IClienteRepository, ClienteRepository>()
			   .AddScoped<ParceiroApplicationService>()
			   .AddScoped<ClienteApplicationService>();

		return builder;
	}
}
```

## Imagens de algumas interfaces

![Edição de cliente](https://github.com/r-penha/envelhecer-bem/blob/master/docs/images/edicao_cliente.png)

![Edição de parceiro](https://github.com/r-penha/envelhecer-bem/blob/master/docs/images/edicao_parceiro.png)

![Lista vazia](https://github.com/r-penha/envelhecer-bem/blob/master/docs/images/lista_clientes_vazia.png)

![Lista de clientes](https://github.com/r-penha/envelhecer-bem/blob/master/docs/images/listagem_cliente.png)

![Lista de parceiros](https://github.com/r-penha/envelhecer-bem/blob/master/docs/images/listagem_parceiros.png)

![Lista mobile](https://github.com/r-penha/envelhecer-bem/blob/master/docs/images/lista_parceiros_mobile.png)

![Menu mobile](https://github.com/r-penha/envelhecer-bem/blob/master/docs/images/lista_parceiros_menu_mobile.png)

![Form mobile](https://github.com/r-penha/envelhecer-bem/blob/master/docs/images/registro_parceiro_mobile.png)
