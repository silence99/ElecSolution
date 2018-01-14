using Busniess.Services;
using Emergence.Common.Model;
using Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busniess.ViewModel
{
    public class CameraManagementViewModel : NotificationObject
    {
        public virtual int PageSize { get; set; }
        public virtual int PageIndex { get; set; }
        public virtual int TotalCount { get; set; }
        public virtual int TotalPage { get; set; }
        public virtual ObservableCollection<CameraModel> camerasList { get; set; }
        public virtual CameraService cameraService { get; set; }

        public CameraManagementViewModel()
        {
            PageIndex = 1;
            PageSize = 10;
            TotalCount = 0;
            TotalPage = 0;
            cameraService = new CameraService();
        }

        public void SyncCameras()
        {
            var thisVop = this.AopWapper as CameraManagementViewModel;
            var cameras = cameraService.GetCameras(PageIndex, PageSize);
            if (cameras != null)
            {
                thisVop.camerasList = new ObservableCollection<CameraModel>(cameras.Data.Select(o => o.CreateAopProxy()));
                thisVop.PageIndex = cameras.PageIndex;
                thisVop.PageSize = cameras.PageSize;
                thisVop.TotalCount = cameras.Count;
                thisVop.TotalPage = cameras.PageSize == 0 ? 0 : (int)Math.Ceiling((double)cameras.Count / cameras.PageSize);
            }
        }
        public void DeleteCameras()
        {
            var thisVop = this.AopWapper as CameraManagementViewModel;
            var cameras = thisVop.camerasList.Where(a => a.IsSelected).Select(a => a.ID.ToString()).ToList();
            if (cameras.Count() <= 0)
            {
                System.Windows.MessageBox.Show("没有要删除的摄像头！");
            }
            else
            {
                var result = cameraService.DeleteCamera(cameras);
                if (result)
                {
                    System.Windows.MessageBox.Show("摄像头删除成功！");
                }
                else
                {
                    System.Windows.MessageBox.Show("摄像头删除失败，请联系管理员！");
                }
                SyncCameras();
            }
        }
    }
}
