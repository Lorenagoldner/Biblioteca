using Biblioteca.ADONet;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using BibliotecaAPI.Services;

var builder = WebApplication.CreateBuilder(args);

//Configurações de Serviços
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Biblioteca.ADONet.DALPro.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");


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

// buscar usuario por id
app.MapGet("/usuarios/{id}", (int id, LeitorService service) =>
{
    try
    {
        var user = service.GetById(id);

        if (user == null)
            return Results.NotFound("Usuário não encontrado");

        return Results.Ok(user);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// suspender usuario
app.MapPut("/usuarios/{id}/suspender", (int id, LeitorService service) =>
{
    try
    {
        service.Suspender(id);
        return Results.Ok("Usuário suspenso");
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// reativar usuario
app.MapPut("/usuarios/{id}/reativar", (int id, LeitorService service) =>
{
    try
    {
        service.Reativar(id);
        return Results.Ok("Usuário reativado");
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// cancelar usuario
app.MapDelete("/usuarios/{id}", (int id, LeitorService service) =>
{
    try
    {
        service.Cancelar(id);
        return Results.Ok("Usuário cancelado");
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.Run();