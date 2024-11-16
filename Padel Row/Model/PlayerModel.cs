using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padel_Row.Model
{
    public class PlayerModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; } = 0; // Puntaje inicializado en 0
    }
}
