using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Newtonsoft.Json;

namespace GFT_Podcasts.Models {
    public class Categoria {

        public int Id { get; set; }
        
        public string Nome { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Podcast> Podcasts { get; set; }
        
    }
}