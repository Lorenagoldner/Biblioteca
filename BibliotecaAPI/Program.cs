//using BibliotecaAPI.Repositories;
using Biblioteca.ADONet;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

//Configurações de Serviços
//Teste de github
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

//Endpoints

//criei só pra verificar se estava tudo ok, pode apagar
app.MapGet("/status-conexao", () => {
    return Results.Ok(new { mensagem = "API conectada ao DALPro com sucesso!" });
});


app.Run();