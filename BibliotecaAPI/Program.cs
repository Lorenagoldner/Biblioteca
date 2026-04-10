using BibliotecaAPI.Models;
//using BibliotecaAPI.Repositories;
using Biblioteca.ADONet;

var builder = WebApplication.CreateBuilder(args);

//Configurações de Serviços
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Biblioteca.ADONet.DALPro.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

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