using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFT_Podcasts.Models {
    public class Podcast {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Autor { get; set; }

        public string Descricao { get; set; }

        public string Link { get; set; }

        public string Imagem { get; set; }

        public int CategoriaId { get; set; }
        [ForeignKey(nameof(CategoriaId))] public Categoria Categoria { get; set; }
        
        public virtual IEnumerable<Episodio> Episodios { get; set; }
    }
}