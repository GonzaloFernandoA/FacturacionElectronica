using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParaProbar
{
    public class RespuestaCae
    {
        public string Cae { get; set; }
        public List<string> Problemas { get; set; }

        public RespuestaCae()
        {
            this.Problemas = new List<string>();
        }
    }
}
