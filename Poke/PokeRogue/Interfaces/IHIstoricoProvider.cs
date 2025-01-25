using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeRogue.Interfaces
{
    public interface IHistoricoProvider<T>
    {
        public IEnumerable<T> Load(string filePath);
        public void Save(string filePath, IEnumerable<T> data);
    }
}
