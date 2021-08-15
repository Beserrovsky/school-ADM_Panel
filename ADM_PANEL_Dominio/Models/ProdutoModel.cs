using System.ComponentModel.DataAnnotations;

namespace FelipeB_App3BI.Models
{
    public class ProdutoModel
    {
        public int ID { get; set; } = 0;
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public int Quantidade { get; set; }
    }
}