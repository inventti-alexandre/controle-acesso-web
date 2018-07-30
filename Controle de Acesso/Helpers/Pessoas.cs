using ControleAcesso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleAcesso.Helpers
{
    public static class Pessoas
    {

        public static Pessoa RemoverPontosETracos(Pessoa pessoa)
        {
            var pessoaReturn = pessoa;

            if (!string.IsNullOrEmpty(pessoaReturn.Telefone))
                pessoaReturn.Telefone = new String(pessoa.Telefone.Where(Char.IsDigit).ToArray());
            if (!string.IsNullOrEmpty(pessoaReturn.Celular))
                pessoaReturn.Celular = new String(pessoa.Celular.Where(Char.IsDigit).ToArray());
            if (!string.IsNullOrEmpty(pessoaReturn.CPF))
                pessoaReturn.CPF = new String(pessoa.CPF.Where(Char.IsDigit).ToArray());
            if (!string.IsNullOrEmpty(pessoaReturn.CEP))
                pessoaReturn.CEP = new String(pessoa.CEP.Where(Char.IsDigit).ToArray());

            return pessoaReturn;
        }


    }
}
