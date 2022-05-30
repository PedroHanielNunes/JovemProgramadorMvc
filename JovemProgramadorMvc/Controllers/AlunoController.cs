using JovemProgramadorMvc.Data.repositorio.interfaces;
using JovemProgramadorMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JovemProgramadorMvc.Controllers
{
    public class AlunoController: Controller
    {
       public readonly IConfiguration _configuration;
       public readonly IAlunoRepositorio _alunoRepositorio;
            
        
        public AlunoController(IConfiguration configuration, IAlunoRepositorio alunoRepositorio)
        {
            _configuration = configuration;
            _alunoRepositorio = alunoRepositorio;

        }
        public IActionResult Index(AlunoModel filtroAluno)
        {
            List<AlunoModel> aluno = new();


            if (filtroAluno.Idade > 0)
            {
                aluno = _alunoRepositorio.FiltroIdade(filtroAluno.Idade, filtroAluno.Operacao);
            }
            if (filtroAluno.Nome != null)
            {
                aluno = _alunoRepositorio.FiltroNome(filtroAluno.Nome);
            }
            if (filtroAluno.Contato != null)
            {
                aluno = _alunoRepositorio.FiltroContato(filtroAluno.Contato);
            }
            if (filtroAluno.Id ==  0 )
            {
                aluno = _alunoRepositorio.BuscarAlunos();
            }

            
            return View(aluno);
        }
        public IActionResult Adicionar()
        {
            return View();
        }
        public IActionResult AlunoAdicionado()
        {
            return View();

        }
        public async Task<IActionResult> BuscarEndereco(string cep)
        {
            EnderecoModel enderecoModel = new();
            try
            {

                cep = cep.Replace("-", "");
                using var client = new HttpClient();
                var result = await client.GetAsync(_configuration.GetSection("ApiCep")["BaseUrl"] + cep + "/json");


                if (result.IsSuccessStatusCode)
                {
                    enderecoModel = JsonSerializer.Deserialize<EnderecoModel>(
                        await result.Content.ReadAsStringAsync(), new JsonSerializerOptions() { });


                    if (enderecoModel.complemento == "")
                    {
                        enderecoModel.complemento = "Nenhum complemento";
                    }
                    if (enderecoModel.logradouro == "")
                    {
                        enderecoModel.logradouro = "CEP geral";
                    }
                    if (enderecoModel.bairro == "")
                    {
                        enderecoModel.bairro = "ÀS imediações de " + enderecoModel.localidade;
                    }

                }
                else
                {
                    ViewData["Mensagem"] = "Erro na busca do endereço!";
                    return View("Index");
                }
            }

            catch (Exception e)
            {

            }
            return View("BuscarEndereco", enderecoModel);

        }
        [HttpPost]
        public ActionResult Inserir(AlunoModel aluno)
        {
            _alunoRepositorio.Inserir(aluno);
            return RedirectToAction("AlunoAdicionado");
        }
        public IActionResult Editar(int id)
        {
            var aluno = _alunoRepositorio.BuscarId(id);
            return View("Editar",aluno);
        }

        public IActionResult Atualizar(AlunoModel aluno)
        {
            var retorno = _alunoRepositorio.Atualizar(aluno);
            return RedirectToAction("Index");
        }
        public IActionResult Excluir(int id)
        {
            var result = _alunoRepositorio.Excluir(id);
            

            if ( result == true)
            {

                TempData["Mensagem3"] = "Aluno excluído com sucesso!";
            }
            else
            {
                TempData["Mensagem3"] = "Aluno não foi excluído";                
            }   
            return RedirectToAction("index");
        }
        

    }   

   
}
