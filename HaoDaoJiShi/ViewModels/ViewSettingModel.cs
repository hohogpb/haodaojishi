using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaoDaoJiShi.Project;

namespace HaoDaoJiShi.ViewModels
{
    class ViewSettingModel : ViewModelBase
    {
        // 会话~ 
        public Project.Session session{get { return Project.App.Inst.session; }}

        public DaoJiShiDocEditVm doc_edit_vm;
        public DaoJiShiDocEditVm docEditVM 
        {
            get { return doc_edit_vm; } 
            set
            {
                doc_edit_vm = value;
                RaisePropertyChanged("docEditVM");
            } 
        }

        // 文档筛选器
        const string djs_file_filter = "倒计时文件(*.djs)|*.djs|所有文件(*.*)|*.*";

        // 构造函数
        public ViewSettingModel()
        {
            // 初始化时 填充零时结构
            RefreshTmp();

            cmdChooseProject = new RelayCommand<object>(this.OnCmdChooseProject);
            cmdSaveProject = new RelayCommand<object>(this.OnCmdSaveProject);
            cmdSaveAsProject = new RelayCommand<object>(this.OnCmdSaveAsProject);  
        }

        private void RefreshTmp()
        {            
            var new_vm = new DaoJiShiDocEditVm();

            new_vm.tmpdoc = new DaoJiShiDoc{
                length = session.doc.length,
                alertTS1 = session.doc.alertTS1,
                alertTS2 = session.doc.alertTS2,
                alertMusic = session.doc.alertMusic,
                endMusic = session.doc.endMusic
            };

            docEditVM = new_vm;
        }


        public RelayCommand<object> cmdChooseProject { get; set; }
        public RelayCommand<object> cmdSaveProject { get; set; }
        public RelayCommand<object> cmdSaveAsProject { get; set; }


        // 选择文件
        private void OnCmdChooseProject(object obj)
        {
            var msg = new NotificationMessageAction<string>(this, djs_file_filter, (r) =>
            {
                if (r == null)
                    return;

                // 加载文档
                var doc = Project.DocLoaderSaver.Load(r);
                // 设置回去
                if (doc != null)
                {
                    Project.App.Inst.session.docFilePath = r;
                    Project.App.Inst.session.doc = doc;

                    RefreshTmp();                    
                }

            });

            Messenger.Default.Send(msg, "choose-open-file");
            //Messenger.Default.Send(msg, "close");
        }

        // 保存文件
        private void OnCmdSaveProject(object obj)
        {
            // 保存文档内容
            Project.App.Inst.session.doc = docEditVM.tmpdoc;

            // 若路径为空，提示选择保存路径
            if (Project.App.Inst.session.docFilePath == null || 
                !File.Exists(Project.App.Inst.session.docFilePath))
            {
                var msg = new NotificationMessageAction<string>(this, djs_file_filter, (r) =>
                {
                    if (r == null)
                        return;

                    Project.App.Inst.session.docFilePath = r;
                });

                Messenger.Default.Send(msg, "choose-save-file");
            }

            // 若用户取消选择，直接返回不保存文档文件
            if (Project.App.Inst.session.docFilePath == null)
                return;
            
            // 保存文档到文件
            Project.DocLoaderSaver.Save(
                Project.App.Inst.session.docFilePath, Project.App.Inst.session.doc);
        }

        // 另存文件
        private void OnCmdSaveAsProject(object obj)
        {
            var msg = new NotificationMessageAction<string>(this, djs_file_filter, (r) =>
            {
                if (r == null)
                    return;

                // 保存文档
                Project.App.Inst.session.doc = docEditVM.tmpdoc; 
                Project.App.Inst.session.docFilePath = r;
                Project.DocLoaderSaver.Save(r, Project.App.Inst.session.doc);
            });

            Messenger.Default.Send(msg, "choose-save-file");
        }
    }

    class DaoJiShiDocEditVm : ViewModelBase
    {
        const string wav_file_filter = "wav文件(*.wav)|*.wav|所有文件(*.*)|*.*";

        public DaoJiShiDocEditVm()
        {
            cmdChooseAlertMusic = new RelayCommand<object>(this.OnCmdChooseAlertMusic);
            cmdTestAlertMusic = new RelayCommand<object>(this.OnCmdTestAlertMusic);
            cmdChooseEndMusic = new RelayCommand<object>(this.OnCmdChooseEndMusic);
            cmdTestEndMusic = new RelayCommand<object>(this.OnCmdTestEndMusic);  
        }

        public DaoJiShiDoc tmpdoc { get; set; }

        // 倒计时长度 - 时
        public string lengthHour
        {
            get { return tmpdoc.length.ToString("hh"); }
            set
            {
                int d = 0;
                int.TryParse(value, out d);

                tmpdoc.length = new TimeSpan(
                    d, tmpdoc.length.Minutes, tmpdoc.length.Seconds);
                RaisePropertyChanged("lengthHour");
            }
        }

        // 倒计时长度 - 分
        public string lengthMinute
        {
            get { return tmpdoc.length.ToString("mm"); }
            set
            {
                int d = 0;
                int.TryParse(value, out d);

                tmpdoc.length = new TimeSpan(
                    tmpdoc.length.Hours, d, tmpdoc.length.Seconds);
                RaisePropertyChanged("lengthMinute");
            }
        }

        // 倒计时长度 - 秒
        public string lengthSecond
        {
            get { return tmpdoc.length.ToString("ss"); }
            set
            {
                int d = 0;
                int.TryParse(value, out d);

                tmpdoc.length = new TimeSpan(
                    tmpdoc.length.Hours, tmpdoc.length.Minutes, d);
                RaisePropertyChanged("lengthSecond");
            }
        }

        // 提示音1 - 时
        public string alter1Hour
        {
            get { return tmpdoc.alertTS1.ToString("hh"); }
            set
            {
                int d = 0;
                int.TryParse(value, out d);

                tmpdoc.alertTS1 = new TimeSpan(
                    d, tmpdoc.alertTS1.Minutes, tmpdoc.alertTS1.Seconds);
                RaisePropertyChanged("alter1Hour");
            }
        }

        // 提示音1 - 分
        public string alter1Minute
        {
            get { return tmpdoc.alertTS1.ToString("mm"); }
            set
            {
                int d = 0;
                int.TryParse(value, out d);

                tmpdoc.alertTS1 = new TimeSpan(
                    tmpdoc.alertTS1.Hours, d, tmpdoc.alertTS1.Seconds);
                RaisePropertyChanged("alter1Minute");
            }
        }

        // 提示音1 - 秒
        public string alter1Second
        {
            get { return tmpdoc.alertTS1.ToString("ss"); }
            set
            {
                int d = 0;
                int.TryParse(value, out d);

                tmpdoc.alertTS1 = new TimeSpan(
                    tmpdoc.alertTS1.Hours, tmpdoc.alertTS1.Minutes, d);
                RaisePropertyChanged("alter1Second");
            }
        }

        // 提示音2 - 小时
        public string alter2Hour
        {
            get { return tmpdoc.alertTS2.ToString("hh"); }
            set
            {
                int d = 0;
                int.TryParse(value, out d);

                tmpdoc.alertTS2 = new TimeSpan(
                    d, tmpdoc.alertTS2.Minutes, tmpdoc.alertTS2.Seconds);
                RaisePropertyChanged("alter2Hour");
            }
        }

        // 提示音2 - 分
        public string alter2Minute
        {
            get { return tmpdoc.alertTS2.ToString("mm"); }
            set
            {
                int d = 0;
                int.TryParse(value, out d);

                tmpdoc.alertTS2 = new TimeSpan(
                    tmpdoc.alertTS2.Hours, d, tmpdoc.alertTS2.Seconds);
                RaisePropertyChanged("alter2Minute");
            }
        }

        // 提示音2 - 秒
        public string alter2Second
        {
            get { return tmpdoc.alertTS2.ToString("ss"); }
            set
            {
                int d = 0;
                int.TryParse(value, out d);

                tmpdoc.alertTS2 = new TimeSpan(
                    tmpdoc.alertTS2.Hours, tmpdoc.alertTS2.Minutes, d);
                RaisePropertyChanged("alter2Second");
            }
        }

        // 提示音
        public string alertMusic
        {
            get { return tmpdoc.alertMusic; }
            set
            {
                tmpdoc.alertMusic = value;
                RaisePropertyChanged("alertMusic");
            }
        }

        // 终止音
        public string endMusic
        {
            get { return tmpdoc.endMusic; }
            set
            {
                tmpdoc.endMusic = value;
                RaisePropertyChanged("endMusic");
            }
        }

        public RelayCommand<object> cmdChooseAlertMusic { get; set; }
        public RelayCommand<object> cmdTestAlertMusic { get; set; }
        public RelayCommand<object> cmdChooseEndMusic { get; set; }
        public RelayCommand<object> cmdTestEndMusic { get; set; }

        // 选择提示音
        private void OnCmdChooseAlertMusic(object obj)
        {
            var msg = new NotificationMessageAction<string>(this, wav_file_filter, (r) =>
            {
                if (r == null)
                    return;

                alertMusic = r;
            });

            Messenger.Default.Send(msg, "choose-open-file");
        }

        // 测试提示音
        private void OnCmdTestAlertMusic(object obj)
        {
            Utils.PlayMusic.Play(alertMusic);
        }

        // 选择终结音
        private void OnCmdChooseEndMusic(object obj)
        {
            var msg = new NotificationMessageAction<string>(this, wav_file_filter, (r) =>
            {
                if (r == null)
                    return;

                endMusic = r;
            });

            Messenger.Default.Send(msg, "choose-open-file");
        }

        // 测试终结音
        private void OnCmdTestEndMusic(object obj)
        {
            Utils.PlayMusic.Play(endMusic);
        }


    }
}
