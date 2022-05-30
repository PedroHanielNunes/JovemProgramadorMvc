﻿using JovemProgramadorMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JovemProgramadorMvc.Data.repositorio.interfaces
{
    public interface IAlunoRepositorio 
    {
        AlunoModel Inserir(AlunoModel aluno);

        List<AlunoModel> BuscarAlunos();
        AlunoModel BuscarId(int id);
        bool Atualizar(AlunoModel aluno);

        bool Excluir(int id);

        List<AlunoModel> FiltroIdade(int idade, string operacao);

        List<AlunoModel> FiltroNome(string nome);

        List<AlunoModel> FiltroContato(string Contato);

    }
}
