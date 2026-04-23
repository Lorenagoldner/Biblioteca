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

builder.Services.AddScoped<INucleoRepository, NucleoRepository>();
builder.Services.AddScoped<INucleoService, NucleoService>();

builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();

builder.Services.AddScoped<IExemplaresService, ExemplaresService>();
builder.Services.AddScoped<IExemplaresRepository, ExemplaresRepository>();

builder.Services.AddScoped<IEstatisticasP2Repository, EstatisticasP2Repository>();
builder.Services.AddScoped<IEstatisticasP2Service, EstatisticasP2Service>();

builder.Services.AddScoped<IImagemLivroService, ImagemLivroService>();
builder.Services.AddScoped<IImagemLivroRepository, ImagemLivroRepository>();

// AppDbContext (na pasta Data) não é utilizado neste projeto,
// pois a aplicação utiliza DALPro (ADO.NET) para acesso direto à base de dados.
// Entity Framework não está implementado aqui.

Biblioteca.ADONet.DALPro.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
?? throw new Exception("Connection string não encontrada.");  // 2. ERRO: Possible null (ConnectionString): Problema: GetConnectionString pode retornar null


//adicionar aqui os services para os respositories

var app = builder.Build();

//Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// IMG 
app.UseRouting();


//Endpoints:

//------------------------------ Endpoints para Obras ------------------------------//

app.MapGet("/api/obras/buscar", (IObraService service) =>
{
    return Results.Ok(service.GetAll());
});


app.MapGet("/api/obras/buscar/{id}", (int id, IObraService service) =>
{
    var obra = service.GetById(id);

    if (obra == null)
        return Results.NotFound(new { message = "Obra não encontrada" });

    return Results.Ok(obra);
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


// ----------------------- Endpoints para Exemplares ------------------------//

app.MapGet("/api/exemplares/buscar", (IExemplaresService service) =>
{
    return Results.Ok(service.GetAll());
});



app.MapPost("/api/exemplares/criar", (CriarExemplarDTO dto, IExemplaresService service) =>
{
    var exemplar = new Exemplares
    {
        ObraID = dto.ObraID,
        NucleoID = dto.NucleoID,
        Disponivel = dto.Disponivel
    };

    var id = service.CriarExemplar(exemplar);

    return Results.Ok(new ExemplarCriadoDTO
    {
        Id = id,
        Message = "Exemplar criado com sucesso"
    });
});


app.MapPut("/api/exemplares/transferir", (int id, int nucleoId, IExemplaresService service) =>
{
    service.TransferirExemplarParaNucleo(id, nucleoId);
    return Results.Ok(new { message = "Transferência feita" });
});


app.MapPut("/api/exemplares/atualizar", (Exemplares exemplar, IExemplaresService service) =>
{
    service.AtualizarExemplar(exemplar);
    return Results.Ok(new { message = "Exemplar atualizado com sucesso" });
});


app.MapDelete("/api/exemplares/{id}", (int id, IExemplaresService service) =>
{
    service.EliminarExemplar(id);
    return Results.Ok(new { message = "Exemplar eliminado com sucesso" });
});


app.MapPut("/api/exemplares/{id}/disponibilizar", (int id, IExemplaresService service) =>
{
    service.Disponibilizar(id);
    return Results.Ok(new { message = "Exemplar disponível" });
});


app.MapPut("/api/exemplares/{id}/indisponibilizar", (int id, IExemplaresService service) =>
{
    service.Indisponibilizar(id);
    return Results.Ok(new { message = "Exemplar indisponível" });
});



// ----------------------- Endpoints para Nucleos ------------------------//
app.MapGet("/api/nucleos/{id}", (int id, INucleoRepository repo) =>
{
    var nucleo = repo.GetNucleoById(id);

    if (nucleo == null)
        return Results.NotFound(new { message = "Núcleo não encontrado" });

    return Results.Ok(nucleo);
});


app.MapGet("/api/nucleos", (INucleoService service) =>
{
    return Results.Ok(service.GetAll());
});


app.MapPost("/api/nucleos", (Nucleo nucleo, INucleoService service) =>
{
    service.Add(nucleo);

    return Results.Ok(new
    {
        message = "Núcleo criado com sucesso"
    });
});


app.MapPut("/api/nucleos", (Nucleo nucleo, INucleoService service) =>
{
    service.Update(nucleo);

    return Results.Ok(new
    {
        message = "Núcleo atualizado com sucesso"
    });
});


app.MapDelete("/api/nucleos/{id}", (int id, INucleoService service) =>
{
    service.Delete(id);

    return Results.Ok(new
    {
        message = "Núcleo eliminado com sucesso"
    });
});


// E ver exatamente/detalhado o que existe naquele núcleo.
app.MapGet("/api/exemplares/nucleo/{id}", (int id, IExemplaresService service) =>
{
    var result = service.GetByNucleo(id);
    return Results.Ok(result);
});


// E ver o TOTAL o que existe naquele núcleo.
app.MapGet("/api/exemplares/nucleo/{id}/por-obra", (int id, IExemplaresService service) =>
{
    var result = service.GetByNucleo(id);

    var resumo = result
        .GroupBy(x => new { x.ObraID, x.TituloObra })
        .Select(g => new
        {
            obraId = g.Key.ObraID,
            titulo = g.Key.TituloObra,
            totalExemplares = g.Count()
        });

    return Results.Ok(resumo);
});


// OBSs SOBRE A RELAÇÃO Endpoints para Nucleos: 2 e 3:

// 🔎 RESUMO IMPORTANTE SOBRE ESTE ENDPOINT (/resumo)
//
// Este endpoint NÃO cria nova lógica de negócio nem faz novas queries à base de dados.
// Ele reutiliza o método já existente "GetByNucleo", que já devolve todos os exemplares
// de um núcleo com as respetivas relações (Obra e Núcleo).
//
// A única responsabilidade deste endpoint é TRANSFORMAR os dados já obtidos,
// agrupando-os por ObraID e calculando a quantidade de exemplares por obra.
//
// ✔ Isto evita duplicação de código (reutilização de service/repository)
// ✔ Mantém a regra de acesso à BD centralizada
// ✔ Permite múltiplas formas de apresentação dos mesmos dados
//
// Em termos de arquitetura:
// - Repository → acesso à base de dados
// - Service → regras de negócio
// - Endpoint → apenas formatação/retorno da resposta
//
// 💡 Conclusão: o mesmo conjunto de dados pode (e deve) ser reutilizado
// para diferentes necessidades sem criar novas queries desnecessárias.


//----------------------- Endpoint de teste de upload-imagem ------------------------ //
app.MapPost("/api/obras/{obraId}/imagem",
(int obraId, IFormFile file, IImagemLivroService service) =>
{
    if (file == null || file.Length == 0)
        return Results.BadRequest(new { message = "Ficheiro inválido" });

    service.Upload(obraId, file);

    return Results.Ok(new
    {
        message = "Imagem guardada com sucesso"
    });
})
.DisableAntiforgery();


app.MapGet("/api/obras/{obraId}/imagem",
(int obraId, IImagemLivroService service) =>
{
    var img = service.Get(obraId);

    if (img is null)
    {
        return Results.NotFound();
    }

    return Results.File(img.Imagem, "image/jpeg"); // ou png conforme o caso
    // return Results.File(img.Imagem, "image/jpeg", $"obra_{obraId}.jpg");
});



// ----------------------- Endpoint de teste de Estatísticas ------------------------//
//app.MapGet("/api/stats/top-obras",

//    (DateTime dataInicio, DateTime dataFim, IEstatisticasP2Service service) =>
//    {
//        return Results.Ok(service.TopObras(dataInicio, dataFim));
//    })
//.WithDescription("Formato das datas: yyyy-MM-dd (ex: 2026-01-01)");



//app.MapGet("/api/stats/nucleos",
//    (DateTime dataInicio, DateTime dataFim, IEstatisticasP2Service service) =>
//    {
//        return Results.Ok(service.Nucleos(dataInicio, dataFim));
//    })
//.WithDescription("Formato das datas: yyyy-MM-dd (ex: 2026-01-01)");



//app.MapGet("/api/stats/generos", (IEstatisticasP2Service service) =>
//{
//    return Results.Ok(service.Generos());
//});



//----------------------- Endpoint de teste de conexão ------------------------ //

//criei só pra verificar se estava tudo ok, pode apagar
app.MapGet("/status-conexao", () => {
    return Results.Ok(new { mensagem = "API conectada ao DALPro com sucesso!" });
});






app.Run();