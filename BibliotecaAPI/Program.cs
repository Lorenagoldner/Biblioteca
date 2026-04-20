using Biblioteca.ADONet;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using BibliotecaAPI.Services;

var builder = WebApplication.CreateBuilder(args);

//Configurações de Serviços
//Teste de github
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Biblioteca.ADONet.DALPro.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
?? throw new Exception("Connection string não encontrada.");  // 2. ERRO: Possible null (ConnectionString): Problema: GetConnectionString pode retornar null


builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//adicionar aqui os services para os respositories
builder.Services.AddScoped<LeitorService>(); 

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

app.Run();