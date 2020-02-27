using System;

namespace GFT_Podcasts.Models.ViewModels {
    /**
     * Classe que será retornada nas requisições HTTP da API.
     */
    public class ResultViewModel {
        public ResultViewModel(bool sucesso, string mensagem, object dado) {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dado = dado;
        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public Object Dado { get; set; }
    }
}