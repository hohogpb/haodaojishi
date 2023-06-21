using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HaoDaoJiShi.Project
{
    class SessionLoaderSaver
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal static void Load()
        {
            Session session = null;

            // 从文件加载会话
            try
            {
                session = Utils.utils.JsonToObject<Session>("session");
            }
            catch (Exception ex)
            {
                log.Error(ex);

                // 异常可能下面两种
                // FileNotFoundException
                // Jil.DeserializationException
            }

            if (session == null)
                session = new Session();

            // 如果会话加载成功，尝试从会话指定路径加载上次的方案
            
            // 加载文档
            var doc = DocLoaderSaver.Load(session.docFilePath);
                
            // 如果文档加载失败，手动创建一枚
            if (doc == null)
            {
                doc = new DaoJiShiDoc
                {
                    length = new TimeSpan(0, 0, 30),
                    alertTS1 = new TimeSpan(0, 0, 20),
                    alertTS2 = new TimeSpan(0, 0, 10),
                    alertMusic = "wav/提示音.wav",
                    endMusic = "wav/终止音.wav"
                };
            }

            // ok 文档处理完毕，设置回去
            session.doc = doc;

            // 正常窗口设置

            // 全屏窗口设置

            // mini窗口设置


            App.Inst.session = session;
        }

        internal static void Save()
        {
            try
            {
                Utils.utils.ObjectToJson<Session>(App.Inst.session, "session");
            }
            catch (Exception ex)
            {
                log.Error(ex);                
            }
        }
    }


}
