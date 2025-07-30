namespace webBotica2.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ParametrosGenerales
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El RUC es obligatorio")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "El RUC debe tener 11 dígitos")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El RUC solo debe contener números")]
        public string ruc { get; set; }

        [Required(ErrorMessage = "El nombre de la empresa es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre de la empresa no puede tener más de 100 caracteres")]
        public string NombreEmporesa { get; set; }

        [Range(0, 1, ErrorMessage = "El IGV debe estar entre 0 y 1 (ej. 0.18 para 18%)")]
        public decimal Igv { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La ganancia mínima mensual debe ser mayor o igual a 0")]
        public decimal GananciaMinimaMensual { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La ganancia mínima anual debe ser mayor o igual a 0")]
        public decimal GananciaMinimaAnual { get; set; }

        [Range(0, 365, ErrorMessage = "Los días de vencimiento deben estar entre 0 y 365")]
        [Display(Name = "Días Mínimos para Vencimiento")]
        public int DiasVencimientoMinima { get; set; }

        public string? LogoSistema { get; set; }

        public bool modoOscuro { get; set; } = false;
    }

}
