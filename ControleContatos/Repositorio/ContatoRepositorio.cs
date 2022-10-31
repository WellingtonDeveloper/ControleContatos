using ControleContatos.Data;
using ControleContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _context;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;
        }
        public ContatoModel ListarPorId(int Id)
        {
            return _context.Contatos.FirstOrDefault(x => x.Id == Id);
        }
        public List<ContatoModel> BuscarTodos()
        {
            return _context.Contatos.ToList();
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
            //Gravar meu banco de dados - Contexto
            _context.Contatos.Add(contato);
            _context.SaveChanges(); //Commit dados de contatos
            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = ListarPorId(contato.Id);

            if (contatoDB == null) throw new SystemException("Houve um erro na atualizção do contato!");

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Telefone = contato.Telefone;

            _context.Contatos.Update(contatoDB);
            _context.SaveChanges();

            return contatoDB;
        }
    }
}
