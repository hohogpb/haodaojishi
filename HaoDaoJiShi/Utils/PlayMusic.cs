using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HaoDaoJiShi.Utils
{
    class PlayMusic
    {     
        static public void Play(string path)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                using (var player = new SoundPlayer())
                {
                    player.SoundLocation =path;
                    player.Load();
                    player.Play();
                }

                sw.Stop();                
            }
            catch (Exception ex)
            {
                string msg = string.Format("发生错误:{0}\n{1}", ex.Message, path);
                MessageBox.Show(msg);
            }                
        }
    }
}
