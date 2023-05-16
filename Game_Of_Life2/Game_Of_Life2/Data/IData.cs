using System;
using System.Collections.Generic;
using System.Text;

namespace Game_Of_Life2.Data
{
    public interface IData
    {
        public List<Map> Maps { get; set; }
        public Map GetMap(string id);
        public List<Map> ReadMaps();
    }
}
