using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Emergence_WPF.Comm
{
    public class AttachmentPage : Page
    {
        public FileStream OpenUploadDialog(string name)
        {
            #region choose import file
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Microsoft Excel 2013|*.xlsx";
            var dlgResult = dialog.ShowDialog();
            if (dlgResult != DialogResult.OK && dlgResult != DialogResult.Yes)
            {
                return null;
            }
            #endregion
            #region check file if exists
            FileStream stream = null;
            try
            {
                //stream = new FileStream(dialog.FileName, FileMode.Open);
                stream = File.OpenRead(dialog.FileName);

                return stream;
            }
            catch (IOException ex)
            {
                System.Windows.MessageBox.Show("上传"+ name + "失败，请联系管理员！");
                return null;
            }
            #endregion
        }

        public void GetDataFromExcel(FileStream fs)
        {
            using (ExcelPackage package = new ExcelPackage(fs))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets[1];
                #region check excel format
                if (sheet == null)
                {
                    MessageBox.Show("Excel format error!");
                    return;
                }
                if (CheckExcelIsCorrect(sheet))
                {
                    MessageBox.Show("Excel format error!");
                    return;
                }
                #endregion

                #region get last row index
                int lastRow = sheet.Dimension.End.Row;
                while (sheet.Cells[lastRow, 1].Value == null)
                {
                    lastRow--;
                }
                #endregion
            }
        }

        public virtual bool CheckExcelIsCorrect(ExcelWorksheet sheet) {
            return true;
        }

        public virtual void WriteExcelDataToList(ExcelWorksheet sheet, int lastRow)
        {
        }

        public virtual void SendExcelDataToService()
        {
        }

    }
}