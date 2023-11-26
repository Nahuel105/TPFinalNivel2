using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio 
{
    public  class Categoria
    {
        private int id;
        private string descripcion;
        public override string ToString()
        {
            return Descripcion;
        }

        public int Id { get => id; set => id = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
