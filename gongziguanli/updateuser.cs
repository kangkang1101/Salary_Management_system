using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SQLCONN;

namespace gongziguanli
{
    public partial class updateuser : Form
    {
        public updateuser()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Trim()==string.Empty)
            {
                MessageBox.Show("警告信息：请输入原密码！");
            }
            else if (textBox2.Text.Trim() == string.Empty)
            {
                MessageBox.Show("警告信息：请输入新密码！");
            }
            else if (textBox3.Text.Trim() == string.Empty)
            {
                MessageBox.Show("警告信息：请再次输入新密码！");
            }
            else
            {
                int uid = int.Parse(Program.mainUser);
                String passwd1 = textBox1.Text.Trim();
                String passwd2 = textBox2.Text.Trim();
                String passwd3 = textBox3.Text.Trim();
                try
                {
                    using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                    {
                        //SQLServer.conn.ConnectionString = connsql;
                        conn.Open(); // 打开数据库连接
                        String sql = "select passwd from [user] where uid=" + uid; // 查询语句
                        SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                        DataTable dt = new DataTable(); // 实例化数据表
                        myda.Fill(dt); // 保存数据 
                        if (passwd1.Equals(dt.Rows[0]["passwd"].ToString()))
                        {
                            if(passwd2.Equals(passwd3))
                            {
                                String sql1 = "update [user] set passwd=" + passwd2 + "where uid=" + uid; // 更新语句
                                SqlDataAdapter myda1 = new SqlDataAdapter(sql1, conn); // 实例化适配器
                                DataTable dt1 = new DataTable(); // 实例化数据表
                                myda1.Fill(dt1); // 保存数据 
                                MessageBox.Show("密码修改成功！");
                            }
                            else
                            {
                                MessageBox.Show("两次输入密码不一致，请重新输入！");
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("您输入的密码不正确！");
                        }
                        conn.Close(); // 关闭数据库连接
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetType().ToString() == "System.ArgumentOutOfRangeException")
                    {
                        MessageBox.Show("查询错误：查询不到相关员工，请确认员工是否存在！");
                    }
                    else
                    {
                        MessageBox.Show("查询错误：" + ex.Message, "请将本错误信息发送给管理员，请求协助解决！");
                    }
                }

            }
            
        }

        private void updateuser_Load(object sender, EventArgs e)
        {
            textBox1.PasswordChar = '*';
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';
        }
    }
}
