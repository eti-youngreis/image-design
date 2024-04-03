using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public class DesignTemplate
    {
        public int Id { get; set; }

        public string Color { get; set; }

        public virtual User User { get; set; }
    }
}
