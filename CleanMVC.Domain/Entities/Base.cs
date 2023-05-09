using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMVC.Domain.Entities
{
    public abstract class Base
    {
        public int Id { get; protected set; }
    }
}
