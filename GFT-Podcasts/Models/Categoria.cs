using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace GFT_Podcasts.Models {
    public class Categoria {

        public int Id { get; set; }
        
        public string Nome { get; set; }

        public virtual IEnumerable<Podcast> Podcasts { get; set; }
        
    }
}