using Biblioteca.ADONet;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using BibliotecaAPI.Services;
using System.Data;
using BibliotecaAPI.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Configurações de Serviços
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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

Biblioteca.ADONet.DALPro.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
?? throw new Exception("Connection string não encontrada."); 

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
builder.Services.AddScoped<IHistoricoRepository, HistoricoRepository>();
builder.Services.AddScoped<EstatisticasRepository>();

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

// IMG 
app.UseRouting();


//Endpoints para Obras

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
    var id = service.Add(dto);

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
var resultado = service.PesquisarObra(texto);


if (resultado == null || !resultado.Any())

{

    return Results.NotFound(new
    {

        mensagem = "Nenhuma obra encontrada"
    });

}


return Results.Ok(new
{

    mensagem = "Obra(s) encontrada(s) com sucesso"
});

});

//Endpoints para Exemplares

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


//Endpoints para Núcleos
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


app.MapGet("/api/exemplares/nucleo/{id}", (int id, IExemplaresService service) =>
{
    var result = service.GetByNucleo(id);
    return Results.Ok(result);
});


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

//Endpoints para Imagens de Obras
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

app.MapGet("/api/stats/top-obras",

    (DateTime dataInicio, DateTime dataFim, IEstatisticasP2Service service) =>
    {
        return Results.Ok(service.TopObras(dataInicio, dataFim));
    })
.WithDescription("Formato das datas: yyyy-MM-dd (ex: 2026-01-01)");


app.MapGet("/api/stats/nucleos",
    (DateTime dataInicio, DateTime dataFim, IEstatisticasP2Service service) =>
    {
        return Results.Ok(service.Nucleos(dataInicio, dataFim));
    })
.WithDescription("Formato das datas: yyyy-MM-dd (ex: 2026-01-01)");


app.MapGet("/api/stats/generos", (IEstatisticasP2Service service) =>
{
    return Results.Ok(service.Generos());
});
app.Run();