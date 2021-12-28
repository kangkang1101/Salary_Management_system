using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gongziguanli
{
    static class Program
    {
        public static string mainUser;
        public static Login1 log;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login1());

            //一个系统只需要一个登录实例
            log = new Login1();

            while (true)
            {
                //显示登录窗体
                log.ShowDialog();
                //判断登录界面给通过了没有，运行主系统
                if (log.DialogResult == DialogResult.OK)
                {
                    yonghu mf = new yonghu(mainUser);
                    Application.Run(mf);
                }
                else if (log.DialogResult == DialogResult.Cancel)
                {
                    log.Dispose();
                    return;
                }
            }
        }
    }
}
