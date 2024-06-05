using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NUnit_practice.DataTypes
{
    public class ApiData
    {
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, this.GetType());
        }
    }
}
