using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheapListBackEnd.Interfaces
{
    public interface Iupdateable
    {
        int Id { get; set; }
        string Column2update { get; set; }
        string NewValue { get; set; }

    }
}
