using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Framework.Save2File
{
    /// <summary>
    /// 保存本地文件类
    /// </summary>
    public class SaveFile
    {
        private string FileName;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="FileName"></param>
        public SaveFile(string FileName)
        {
            this.FileName = FileName;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="sBuffer"></param>
        public void Save(string sBuffer)
        {
            FileInfo fileinfo = new FileInfo(FileName);
            if (Directory.Exists(Path.GetDirectoryName(FileName)))
            {
                if (!fileinfo.Exists)
                {
                    FileStream fs = File.Create(FileName);
                    fs.Flush();
                    fs.Close();
                }
                StreamWriter sw = new StreamWriter(FileName, true);
                sw.Write(sBuffer);
                sw.Flush();
                sw.Close();
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FileName));
                FileStream fs = File.Create(FileName);
                fs.Flush();
                fs.Close();
                StreamWriter sw = new StreamWriter(FileName);
                sw.Write(sBuffer);
                sw.Flush();
                sw.Close();

            }
        }
    }
}
