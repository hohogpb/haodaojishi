using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDaoJiShi.Project
{
    public class DaoJiShiDoc 
    {
        public DaoJiShiDoc()
        {

        }

        /// <summary>
        /// 计时长度
        /// </summary>
        public TimeSpan length { get; set; }

        /// <summary>
        /// 提示1
        /// </summary>        
        public TimeSpan alertTS1 { get; set; }

        /// <summary>
        /// 提示2
        /// </summary>        
        public TimeSpan alertTS2 { get; set; }

        /// <summary>
        /// 提示声音
        /// </summary>
        public String alertMusic { get; set; }

        /// <summary>
        /// 结束声音
        /// </summary>
        public String endMusic { get; set; }

    }
}
