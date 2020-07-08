using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 
 namespace GFT_Podcasts.Models.ViewModels.CategoriaViewModels {
     public class CategoriaCadastroViewModel {
         
         [Display(Name = "Id")]
         public int Id { get; set; }
 
         [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
         [MinLength(3, ErrorMessage = " O campo {0} deve ter no mínimo {1} caracteres")]
         [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
         public string Nome { get; set; }
 
     }
 }