using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mingle.Entinies
{
    public class Avatar
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float Shoesize { get; set; }
        /// <summary>
        /// Color is string, as now it has 2 colors, but tomorrow it may have more.
        /// </summary>
        public string Color { get; set; }
        public bool CanMineUnobtainium { get; set; }
    }
}
