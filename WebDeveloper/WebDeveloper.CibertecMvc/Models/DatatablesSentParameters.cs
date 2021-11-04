using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeveloper.CibertecMvc.Models
{
    public class DatatablesSentParametersSearch
    {
        public string Value { get; set; }
    }
    public class DatatablesSentParameters
    {
        public int Draw { get; set; }
        // El indice a partir del cual datatables esta pidiendo la data
        public int Start { get; set; }
        // La cantidad de elementos que datatables quiere mostrar
        public int Length { get; set; }
        public DatatablesSentParametersSearch Search { get; set; }
    }
}
