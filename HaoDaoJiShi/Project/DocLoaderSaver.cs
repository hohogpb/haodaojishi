using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HaoDaoJiShi.Project
{
    class DocLoaderSaver
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal static DaoJiShiDoc Load(string docFilePath)
        {
            DaoJiShiDoc doc = null;

            try
            {
                doc = Utils.utils.JsonToObject<DaoJiShiDoc>(docFilePath);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

            return doc;
        }

        internal static void Save(string docFilePath, DaoJiShiDoc doc)
        {
            try
            {
                Utils.utils.ObjectToJson<DaoJiShiDoc>(doc, docFilePath);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }


    }
}
