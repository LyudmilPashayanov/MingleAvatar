using System;
using System.Runtime.Serialization;

namespace Mingle.Model
{
    [Serializable]
    public class Avatar
    {
        [IgnoreDataMember]
        public string Id { get; set; }
        public string Name { get; set; }
        public float Shoesize { get; set; }
        /// <summary>
        /// Color is string. Now it has 2 colors, but tomorrow it may have more.
        /// </summary>
        public string Color { get; set; }
        public bool CanMineUnobtainium { get; set; }
    }
}
