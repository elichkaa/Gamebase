using Gamebase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Data.Services.Contracts
{
    public interface IDeveloperService
    {
        public Developer GetDeveloper(string name);
    }
}
