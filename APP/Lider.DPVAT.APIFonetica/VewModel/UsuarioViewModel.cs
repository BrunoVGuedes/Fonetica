using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lider.DPVAT.APIFonetica.UI.VewModel
{
    [DisplayColumn("Parametros de entrada")]
    public class UsuarioViewModel
    {
        /// <summary>
        /// Nome do Usuario para autenticar na Api
        /// </summary>
        /// <example> Joao </example>
        [Required]        
        public string Nome { get; set; }

        /// <summary>
        /// Senha do usuario para autenticar na API
        /// </summary>
        /// <example> XXXXXX </example>
        [Required]       
        public string Senha { get; set; }
    }
}
