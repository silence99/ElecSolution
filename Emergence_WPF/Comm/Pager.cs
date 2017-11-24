using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Emergence_WPF.Comm
{
    public class Pager : Control
    {
        #region 静态构造
        static Pager()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pager), new FrameworkPropertyMetadata(typeof(Pager)));

            InitCommand();
        }
        #endregion

        #region 依赖属性

        #region 总记录数

        public int TotalCount
        {
            get
            {
                return (int)GetValue(TotalCountProperty);
            }
            set
            {
                SetValue(TotalCountProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for TotalCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalCountProperty =
            DependencyProperty.Register("TotalCount", typeof(int), typeof(Pager), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion

        #region 每页大小

        public int PageSize
        {
            get
            {
                return (int)GetValue(PageSizeProperty);
            }
            set
            {
                SetValue(PageSizeProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for PageSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(Pager), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        #endregion

        #region 当前页

        public int PageIndex
        {
            get
            {
                return (int)GetValue(PageIndexProperty);
            }
            set
            {
                SetValue(PageIndexProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for PageIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register("PageIndex", typeof(int), typeof(Pager), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        #endregion

        #region 总页数

        public int TotalPage
        {
            get
            {
                return (int)GetValue(TotalPageProperty);
            }
            set
            {
                SetValue(TotalPageProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for TotalPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalPageProperty =
            DependencyProperty.Register("TotalPage", typeof(int), typeof(Pager), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion

        #region 跳转页数



        public int PageNum
        {
            get
            {
                return (int)GetValue(PageNumProperty);
            }
            set
            {
                SetValue(PageNumProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for PageNum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageNumProperty =
            DependencyProperty.Register("PageNum", typeof(int), typeof(Pager), new PropertyMetadata(0));



        #endregion

        #endregion

        #region 命令
        //首页命令
        public static readonly RoutedCommand FirstPageCommand = new RoutedCommand("FirstPage", typeof(Pager));

        //上一页命令
        public static readonly RoutedCommand PrePageCommand = new RoutedCommand("PrePage", typeof(Pager));

        //下一页命令
        public static readonly RoutedCommand NextPageCommand = new RoutedCommand("NextPage", typeof(Pager));

        //最后一页命令
        public static readonly RoutedCommand LastPageCommand = new RoutedCommand("LastPage", typeof(Pager));

        //跳转命令
        public static readonly RoutedCommand GoPageCommand = new RoutedCommand("GoPage", typeof(Pager));
        #endregion

        #region 路由事件

        public static readonly RoutedEvent PageChangedEvent = EventManager.RegisterRoutedEvent("PageChanged",
            RoutingStrategy.Bubble, typeof(EventHandler<PageChangedEventArg>), typeof(Pager));

        public event EventHandler<PageChangedEventArg> PageChanged
        {
            add
            {
                this.AddHandler(PageChangedEvent, value);
            }
            remove
            {
                this.RemoveHandler(PageChangedEvent, value);
            }
        }

        protected virtual void OnPageChanged()
        {
            var pageChangedEventArg = new PageChangedEventArg(PageIndex);
            pageChangedEventArg.RoutedEvent = PageChangedEvent;
            pageChangedEventArg.Source = this;
            this.RaiseEvent(pageChangedEventArg);
        }

        #endregion

        #region 方法

        #region 初始化命令

        private static void InitCommand()
        {
            CommandManager.RegisterClassCommandBinding(typeof(Pager), new CommandBinding(FirstPageCommand, PagerCommandHandler));
            CommandManager.RegisterClassCommandBinding(typeof(Pager), new CommandBinding(PrePageCommand, PagerCommandHandler));
            CommandManager.RegisterClassCommandBinding(typeof(Pager), new CommandBinding(NextPageCommand, PagerCommandHandler));
            CommandManager.RegisterClassCommandBinding(typeof(Pager), new CommandBinding(LastPageCommand, PagerCommandHandler));
            CommandManager.RegisterClassCommandBinding(typeof(Pager), new CommandBinding(GoPageCommand, PagerCommandHandler));
        }

        #endregion

        public void OnPageGo()
        {
            if (PageNum >= 1 && PageNum <= TotalPage)
            {
                PageIndex = PageNum;

                OnPageChanged();
            }
        }

        public void OnLastPage()
        {
            PageIndex = TotalPage;

            OnPageChanged();
        }

        public void OnNextPage()
        {
            if (PageIndex < TotalPage)
            {
                PageIndex++;

                OnPageChanged();
            }
        }

        public void OnPrePage()
        {
            if (PageIndex > 1)
            {
                PageIndex--;

                OnPageChanged();
            }
        }

        public void OnFirstPage()
        {
            PageIndex = 1;

            OnPageChanged();
        }

        #endregion

        #region 私有方法

        private static void PagerCommandHandler(object obj, ExecutedRoutedEventArgs e)
        {
            var pager = obj as Pager;

            if (pager == null) return;

            if (e.Command == FirstPageCommand)
            {
                pager.OnFirstPage();
            }
            else if (e.Command == PrePageCommand)
            {
                pager.OnPrePage();
            }
            else if (e.Command == NextPageCommand)
            {
                pager.OnNextPage();
            }
            else if (e.Command == LastPageCommand)
            {
                pager.OnLastPage();
            }
            else if (e.Command == GoPageCommand)
            {
                pager.OnPageGo();
            }
        }

        

        #endregion
    }
}
