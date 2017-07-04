using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Views
{

    public class IndexPageMenuItem
    {
        public IndexPageMenuItem()
        {
            TargetType = typeof(IndexPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}