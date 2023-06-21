using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace HaoDaoJiShi.Project
{

    class DaoJiShi : ViewModelBase
    {
        // 关联文档，session负责每次doc改变时同时改变这里        
        private DaoJiShiDoc _doc;
        public DaoJiShiDoc doc
        {
            get { return _doc; }
            set
            {
                _doc = value;
                RaisePropertyChanged("doc");
                ResetCount();
            }
        }

        // 高精度计数器
        private Stopwatch stopwatch { get; set; }

        // 一般timer
        private Timer timer { get; set; }

        // 当前时间 
        private DateTime current_datetime;
        public DateTime currentDatetime
        {
            get { return current_datetime; }
            set
            {
                current_datetime = value;
                RaisePropertyChanged("currentDatetime");
            }
        }

        // 计时开始时间
        public DateTime start_datetime;
        public DateTime startDatetime
        {
            get { return start_datetime; }
            set
            {
                start_datetime = value;
                RaisePropertyChanged("startDatetime");
            }
        }

        // 计时预计结束时间
        public DateTime end_datetime;
        public DateTime endDatetime
        {
            get { return end_datetime; }
            set
            {
                end_datetime = value;
                RaisePropertyChanged("endDatetime");
            }
        }

        // 当前的时间计数
        private TimeSpan _current;
        public TimeSpan current 
        {
            get { return _current; }
            set
            {
                _current = value;
                RaisePropertyChanged("current");
            }
        }

        private List<TimeSpan> playMusicList { get; set; }

        // 构造
        public DaoJiShi()
        {
            StartBasicTimer();

            playMusicList = new List<TimeSpan>();
        }

        // 析构
        ~DaoJiShi()
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }

            if (stopwatch != null)
            {
                stopwatch.Stop();
                stopwatch = null;
            }

        }

        private void StartBasicTimer()
        {
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Interval = 10;
            timer.Enabled = true;              
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            currentDatetime = DateTime.Now;
            if (stopwatch != null)
            {
                current = doc.length.Subtract( stopwatch.Elapsed);
                // current = current.Subtract(TimeSpan.FromMilliseconds(10));

                for (int i = playMusicList.Count - 1; i >= 0; i--)
                {
                    if (current.Ticks <= playMusicList[i].Ticks)
                    {
                        playMusicList.RemoveAt(i);
                        Utils.PlayMusic.Play(doc.alertMusic);
                    }
                }

                

                // 结束 播放终止音
                if (current.Ticks <= 0)
                {
                    StopCount();
                    
                    Utils.PlayMusic.Play(doc.endMusic);
                }
            }       
        }

        // 启动
        internal void StartCount()
        {
            ResetCount();

            Task.Delay(1500).ContinueWith(t => 
            {
                playMusicList.Clear();
                playMusicList.Add(doc.alertTS1);
                playMusicList.Add(doc.alertTS2);

                startDatetime = DateTime.Now;
                endDatetime = startDatetime + doc.length;  

                stopwatch = new Stopwatch();
                stopwatch.Start();
            });            
        }

        // 重置
        internal void ResetCount()
        {
            if (stopwatch != null)
            {
                stopwatch.Reset();
                stopwatch = null;
            }
            
            startDatetime = new DateTime();
            endDatetime = new DateTime();
            
            current = doc.length;
        }

        // 暂停或者继续
        internal void PauseContinueCount()
        {
            if (stopwatch == null)
                return;

            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
            }
            else
            {
                endDatetime = DateTime.Now + current;
                stopwatch.Start();                
            }
        }

        // 停止计数
        private void StopCount()
        {
            if (stopwatch != null)
            {
                stopwatch.Reset();
                stopwatch = null;
            }

            startDatetime = new DateTime();
            endDatetime = new DateTime();
            
            current = new TimeSpan();
        }
    }
}
