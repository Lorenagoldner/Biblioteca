using Biblioteca.ADONet;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using BibliotecaAPI.Services;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

//Configurações de Serviços
//Teste de github
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


Biblioteca.ADONet.DALPro.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
?? throw new Exception("Connection string não encontrada.");  // 2. ERRO: Possible null (ConnectionString): Problema: GetConnectionString pode retornar null


builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
builder.Services.AddScoped<IHistoricoRepository, HistoricoRepository>();
builder.Services.AddScoped<EstatisticasRepository>();

//adicionar aqui os services para os respositories
builder.Services.AddScoped<LeitorService>();
builder.Services.AddScoped<EmprestimoService>();
builder.Services.AddScoped<EstatisticasService>();

var app = builder.Build();

//Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Endpoints

//criei só pra verificar se estava tudo ok, pode apagar
app.MapGet("/status-conexao", () => {
    return Results.Ok(new { mensagem = "API conectada ao DALPro com sucesso!" });
});

//Endpoint para criar um leitor (pode ser adaptado para criar outros tipos de usuários)
app.MapGet("/usuarios", (LeitorService service) =>
{
    return Results.Ok(service.GetAll());
});

// Endpoint para criar um leitor 
app.MapPost("/leitores", (CriarUsuarioDTO dto, LeitorService service) =>
{
    try
    {
        service.CriarLeitor(dto);
        return Results.Ok("Leitor criado com sucesso");
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// Endpoint para login
app.MapPost("/login", (LoginDTO dto, LeitorService service) => 
{
    try
    {
        var user = service.Login(dto);

        return Results.Ok(new
        {
            mensagem = "Login realizado com sucesso",
            usuario = user
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});


//Endpoint para verificar a situação detalhada do leitor (
app.MapGet("/emprestimos/situacao/{usuarioId}", (int usuarioId, EmprestimoService service) =>
{
    // Agora o service já entrega a lista prontinha!
    return Results.Ok(service.ConsultarSituacaoDetalhada(usuarioId));
});

//Endpoint histórico
app.MapGet("/historico", (int? nucleoId, DateTime? inicio, DateTime? fim, EmprestimoService service) =>
{
    var historico = service.ConsultarHistorico(nucleoId, inicio, fim);
    return Results.Ok(historico);
});

//Endpoint para fazer um novo empréstimo
app.MapPost("/emprestimos/requisicao", (int usuarioId, int exemplarId, EmprestimoService service) =>
{
    try
    {
        service.RequisitarLivro(usuarioId, exemplarId);
        return Results.Ok("Requisição efetuada com sucesso!");
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

//Endpoint para devolução de livro
app.MapPut("/emprestimos/devolucao/{emprestimoId}", (int emprestimoId, EmprestimoService service) =>
{
    try
    {
        service.DevolverLivro(emprestimoId);

        return Results.Ok(new
        {
            mensagem = "Livro devolvido com sucesso!",
            data = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Erro ao processar devolução: {ex.Message}");
    }
});

//Estatisticas

app.MapGet("/estatisticas/resumo", (EstatisticasService service) =>
{
    try
    {
        var resumo = service.ObterResumoGeral();
        return Results.Ok(resumo);
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Erro ao obter resumo: {ex.Message}");
    }
});


app.MapGet("/estatisticas/top10-mes-anterior", (EstatisticasService service) =>
{
    try
    {
        var top10 = service.ObterTop10MesAnterior();
        return Results.Ok(top10);
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Erro ao obter ranking do mês anterior: {ex.Message}");
    }
});


app.Run();