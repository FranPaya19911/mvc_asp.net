//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MilistaSeries.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CRITICA
    {
        public int IdCritica { get; set; }
        public Nullable<int> Nota { get; set; }
        public string Comentario { get; set; }
        public int fk_Usuario { get; set; }
        public int Fk_Producto { get; set; }
    
        public virtual PRODUCTO PRODUCTO { get; set; }
        public virtual USUARIOS USUARIOS { get; set; }
    }
}