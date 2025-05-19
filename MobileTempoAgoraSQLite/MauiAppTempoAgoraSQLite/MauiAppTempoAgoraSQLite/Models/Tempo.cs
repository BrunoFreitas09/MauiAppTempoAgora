using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiAppTempoAgoraSQLite.Models
{
    internal class Tempo
    {
        public class Produto
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
          
        }
    }
}
