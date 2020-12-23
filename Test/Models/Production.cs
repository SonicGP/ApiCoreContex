using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Production
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Campo {0} é Obrigatorio")]
        [Display(Name="Produto")]
        public string Title { get; set; }

        [Display(Name="Descrição")]
        public string Description { get; set; }

        [Display(Name="Preço")]
        public decimal Price { get; set; }

        [Required(ErrorMessage="Campo {0} é Obrigatorio")]
        [Display(Name="Categoria")]
        public int CategoryId { get; set; }

        public Category Category {get; set;}
    }
}