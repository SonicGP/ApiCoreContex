using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Campo {0} é Obrigatorio")]
        [Display(Name="Categoria")]
        public string Title { get; set; }
        
    }
}