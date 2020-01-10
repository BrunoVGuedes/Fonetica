﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lider.DPVAT.APIFonetica.UI.VewModel
{
    public class ParametrosEntradaFonetica
    {
        /// <summary>
        /// Palavra que vai ter Fonetica
        /// </summary>
        /// <example> João da Silva </example>
        [Required]
        public string palavra { get; set; }

    }
}
