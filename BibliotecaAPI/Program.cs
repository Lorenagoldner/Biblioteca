using Biblioteca.ADONet;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using BibliotecaAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// 🔧 REGISTRO DE DEPENDÊNCIAS (Dependency Injection)
//
// 👉 Estamos dizendo ao .NET:
// "Sempre que alguém pedir IObrasRepository,
// entregue uma instância de ObrasRepository"
//
// ✔ Isso permite que o Service receba o Repository automaticamente
// ✔ Sem isso, a aplicação NÃO sabe como criar o objeto
// ❌ Resultado sem isso: erro em runtime (Cannot resolve service)


// Configurações de Serviços
//Teste de github
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ADD erika
builder.Services.AddScoped<IObraService, ObraService>();
builder.Services.AddScoped<IObrasRepository, ObrasRepository>();
builder.Services.AddScoped<IExemplaresRepository, ExemplaresRepository>();
builder.Services.AddScoped<INucleoRepository, NucleoRepository>();
builder.Services.AddScoped<IEmprestimoService, EmprestimoService>();
builder.Services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();

// AppDbContext (na pasta Data) não é utilizado neste projeto,
// pois a aplicação utiliza DALPro (ADO.NET) para acesso direto à base de dados.
// Entity Framework não está implementado aqui.

Biblioteca.ADONet.DALPro.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
?? throw new Exception("Connection string não encontrada.");  // 2. ERRO: Possible null (ConnectionString): Problema: GetConnectionString pode retornar null

//builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//adicionar aqui os services para os respositories

var app = builder.Build();

//Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//Endpoints:

//------------------------------ Endpoints para Obras ------------------------------//

app.MapGet("/api/obras/buscar", (IObraService service) =>
{
    return Results.Ok(service.GetAll());
});


app.MapGet("/api/obras/buscar/{id}", (int id, IObraService service) =>
{
    return Results.Ok(service.GetById(id));
});


app.MapPost("/api/obras/criar", (CriarObraDTO dto, IObraService service) =>
{
    var id = service.Add(dto); // 👈 tem de devolver ID

    return Results.Created($"/api/obras/{id}", new
    {
        mensagem = "Obra criada com sucesso",
        obraId = id
    });
});


app.MapPut("/api/obras/atualizar/{id}", (int id, AtualizarObraDTO dto, IObraService service) =>
{
    service.Update(id, dto);
    return Results.Ok();
});


app.MapDelete("/api/obras/deletar/{id}", (int id, IObraService service) =>
{
    service.Delete(id);
    return Results.Ok();
});


app.MapGet("/api/obras/detalhadas", (IObraService service) =>
{
    return Results.Ok(service.GetObrasDetalhadas());
});


app.MapGet("/api/obras/pesquisa", (string texto, IObraService service) =>
{
    return Results.Ok(service.PesquisarObra(texto));
});



// ----------------------- Endpoints para Emprestimos ------------------------//

app.MapGet("/api/emprestimos/buscar", (IEmprestimoService service) =>
{
    return Results.Ok(service.GetAll());
});


app.MapGet("/api/emprestimos/buscar/{id}", (int id, IEmprestimoService service) =>
{
    var emprestimo = service.GetById(id);

    if (emprestimo == null)
        return Results.NotFound();

    return Results.Ok(emprestimo);
});


app.MapPost("/api/emprestimos/criar", (int usuarioId, int exemplarId, IEmprestimoService service) =>
{
    service.NewEmprestimo(usuarioId, exemplarId);
    return Results.Ok();
});


app.MapPut("/api/emprestimos/atualizar/{id}", (int id, EmprestimoUpdateDTO emprestimo, IEmprestimoService service) =>
{
    service.UpdateEmprestimo(id, emprestimo);
    return Results.Ok("Empréstimo atualizado");
});


app.MapDelete("/api/emprestimos/{id}", (int id, IEmprestimoService service) =>
{
    service.DeleteEmprestimo(id);
    return Results.Ok("Empréstimo eliminado");
});


app.MapPut("/api/emprestimos/devolver/{id}", (int id, IEmprestimoService service) =>
{
    service.ReturnBook(id);
    return Results.Ok("Livro devolvido com sucesso");
});





//criei só pra verificar se estava tudo ok, pode apagar
app.MapGet("/status-conexao", () => {
    return Results.Ok(new { mensagem = "API conectada ao DALPro com sucesso!" });
});


app.Run();