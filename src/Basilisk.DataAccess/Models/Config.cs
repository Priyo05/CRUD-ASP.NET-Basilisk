using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.DataAccess.Models
{
    public partial class Config
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

}
