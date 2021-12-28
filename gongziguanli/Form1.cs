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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 员工信息ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Addstaff f = new Addstaff();
            f.Show();
        }

        private void 部门信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin f = new Admin();
            f.Show();
        }

        private void 添加部门ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin f = new Admin();
            f.Show();
            f.tabControl1.SelectedIndex = 1;
    
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void 工资统计ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Pay f = new Pay();
            f.Show();
        }

        private void 出勤记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Attendance f = new Attendance();
            f.Show();
        }

        private void 基础设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Withhold f = new Withhold();
            f.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            add f = new add();
            f.Show();
        }

        private void 统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            add f = new add();
            f.Show();
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
            Pay4 pay4 = new Pay4();
            pay4.Show();
        }

        private void 关闭系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认退出？（记得保存更改哦！）", "退出系统？", MessageBoxButtons.OKCancel
                , MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.OK)
                System.Environment.Exit(0);
        }
    }
}
