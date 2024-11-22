using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosHenrryGA.Entities
{
    public class ClienteCLS
    {
        public int Id { get; set; }



        // VALIDACIONES

        [Required(ErrorMessage = "Debes introducir un nombre")]
        [StringLength(50, ErrorMessage = "La longitud máxima del nombre es 50")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚüÜ ]*$", ErrorMessage = "Solo se permiten letras")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Debes introducir el apellido paterno")]
        [StringLength(50, ErrorMessage = "La longitud máxima del apellido es 50")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚüÜ ]*$", ErrorMessage = "Solo se permiten letras")]
        public string? ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "Debes introducir el apellido paterno")]
        [StringLength(50, ErrorMessage = "La longitud máxima del apellido es 50")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚüÜ ]*$", ErrorMessage = "Solo se permiten letras")]
        public string? ApellidoMaterno { get; set; }
    }
}
