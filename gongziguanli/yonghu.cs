using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gongziguanli
{
    public partial class yonghu : Form
    {
        string user;
        public yonghu(String user1)
        {
            this.user = user1;
            InitializeComponent();
        }

        private void 出勤记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Attendance2 attendance2 = new Attendance2();
            attendance2.Show();
        }

        private void 工资统计ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Pay2 pay2 = new Pay2();
            pay2.Show();
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认退出登录？（记得保存更改哦！）", "退出登录？", MessageBoxButtons.OKCancel
                , MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.OK)
            {
                this.Dispose();
                Login1 login1 = new Login1();
                login1.Show();
            }

            else
                return;
        }

        private void 工资明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pay3 pay3 = new Pay3();
            pay3.Show();
        }

        private void 修改个人信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateuser updateuser = new updateuser();
            updateuser.Show();

        }

        private void 退出系统ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认退出？（记得保存更改哦！）", "退出系统？", MessageBoxButtons.OKCancel
                , MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.OK)
                System.Environment.Exit(0);
        }
    }
}
