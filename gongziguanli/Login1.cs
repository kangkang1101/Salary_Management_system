using System;
using System.IO;
using System.Data.SqlClient;
using SQLCONN;
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
    public partial class Login1 : Form
    {
        public Login1()
        {
            InitializeComponent();
        }

        private void Login1_Load(object sender, EventArgs e)
        {
            //如果存在 记住密码文件

            if (File.Exists("user.bin"))
            {
                checkBox1.Checked = true;

                string contend1 = "";
                string contend2 = "";
                try
                {
                    //从user、password 文件取出数据
                    StreamReader sreader1 = new StreamReader("user.bin", Encoding.Default);
                    StreamReader sreader2 = new StreamReader("password.bin", Encoding.Default);
                    contend1 += sreader1.ReadLine() + Environment.NewLine;
                    contend2 += sreader2.ReadLine() + Environment.NewLine;

                    sreader1.Close();
                    sreader2.Close();
                }
                catch { }
                //让两个输入框显示账号密码
                this.textBox1.Text = contend1.Trim();
                this.textBox2.Text = contend2.Trim();
                textBox2.PasswordChar = '*';




               /* //如果存在 自动登录文件
                if (File.Exists("autologin.bin"))
                {
                    checkBox2.Checked = true;
                    button1.PerformClick();
                }*/
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = "";
            string password = "";
            //当选中记住密码时，将密码保存到本地
            user = this.textBox1.Text.Trim();
            password = this.textBox2.Text.Trim();

            if (checkBox1.Checked == true)
            {

                FileStream fs1 = new FileStream("user.bin", FileMode.Create);
                StreamWriter sw1 = new StreamWriter(fs1);
                sw1.Write(user);
                sw1.Flush();
                fs1.Close();

                FileStream fs2 = new FileStream("password.bin", FileMode.Create);
                StreamWriter sw2 = new StreamWriter(fs2);
                sw2.Write(password);
                sw2.Flush();
                fs2.Close();
            }
            //当不需要记住密码时，删除文件
            if (checkBox1.Checked == false)
            {
                if (File.Exists("password.bin") && File.Exists("user.bin"))
                {
                    File.Delete("password.bin");
                    File.Delete("user.bin");
                }

            }
            //===================检查是否自动登录=======================
            /*if (checkBox2.Checked == true)
            {

                FileStream fs = new FileStream("autologin.bin", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write("");
                sw.Flush();
                fs.Close();
            }
            if (checkBox2.Checked == false)
            {
                if (File.Exists("autologin.bin"))
                    File.Delete("autologin.bin");
            }*/

            //MessageBox.Show("点击了按钮！");
            //=========================进行登录（账号、密码）判断=====================

            if(textBox1.Text.Trim()==string.Empty)
            {
                MessageBox.Show("警告信息：请输入用户ID！");
            }
            else if(textBox2.Text.Trim()==string.Empty)
            {
                MessageBox.Show("警告信息：请输入密码！");
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                    {
                        conn.Open(); // 打开数据库连接

                        int uid =int.Parse(textBox1.Text.Trim());
                        String passwd = textBox2.Text.Trim();
                        String sql2 = "select [user].passwd,[user].quanxian from [user] where [user].uid=" + uid ;
                        SqlDataAdapter myda2 = new SqlDataAdapter(sql2, conn); // 实例化适配器
                        DataTable dt2 = new DataTable(); // 实例化数据表
                        myda2.Fill(dt2); // 保存数据 
                        if (passwd.Equals(dt2.Rows[0]["passwd"].ToString()) && dt2.Rows[0]["quanxian"].ToString().Equals("管理员"))
                        {
                            Form1 form = new Form1();
                            form.Show();
                            this.DialogResult = DialogResult.OK;
                            this.Hide();
                        }
                        else if (passwd.Equals(dt2.Rows[0]["passwd"].ToString()) && dt2.Rows[0]["quanxian"].ToString().Equals("职工"))
                        {
                            Program.mainUser = user;
                            yonghu yonghu = new yonghu(Program.mainUser);
                            yonghu.Show();
                            this.DialogResult = DialogResult.OK;
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("用户名或密码出现错误！！");
                        }
                    }
                }
                catch (Exception ex)
                {
                  MessageBox.Show("警告信息：" + ex.Message, "出现错误!");
                }
            }
        }
    }
}
