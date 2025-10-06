using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories
{
    public class UpdateCategoryRequest : Request
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Titulo invalido")]
        [MaxLength(80, ErrorMessage = "O titulo deve conter ate 80 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descricao invalida")]
        public string Description { get; set; } = string.Empty;
    }
}
