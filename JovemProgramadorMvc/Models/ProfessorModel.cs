using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JovemProgramadorMvc.Models
{
    public class ProfessorModel
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Idade { get; set; }
        public string Contato { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public ProfessorModel(int id,string nome, string idade, string contato, string email, string cep)
        {
            Id = id;
            Nome = nome;
            Idade = idade;
            Contato = contato;
            Email = email;
            Cep = cep;
        }

    }
}
