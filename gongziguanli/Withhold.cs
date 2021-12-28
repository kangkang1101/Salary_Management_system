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
    public partial class Withhold : Form
    {
        public Withhold()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Withhold_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "yyyy年MM月";   //修改只显示年月
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.ShowUpDown = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                {
                    conn.Open(); // 打开数据库连接
                                 //convert(varchar(7),attendance.time,120) 获取年月
                    String sql1 = "select * from rate"; // 初始化部门下拉框
                    SqlDataAdapter myda1 = new SqlDataAdapter(sql1, conn);
                    DataTable dt1 = new DataTable();
                    myda1.Fill(dt1);
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string strName = dt1.Rows[i]["id"].ToString();
                        comboBox1.Items.Add(new SQLServer.ComboxItem("编号" + strName, "Value1"));
                        //comboBox2.Items.Add(new SQLServer.ComboxItem(comboBox2.Items.IndexOf("Value1").ToString(), i.ToString()));
                    }

                    String sql = "select withhold.sid,staff.name,rate.tax,rate.gold,rate.min,convert(varchar(7),withhold.time,120) from withhold left join staff on staff.sid=withhold.sid left join rate on withhold.rid=rate.id"; // 查询语句staff表除了部门编号的全部内容，department的名称
                    SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                    DataTable dt = new DataTable(); // 实例化数据表
                    myda.Fill(dt); // 保存数据 
                    dataGridView1.DataSource = dt; // 设置到DataGridView中
                    this.dataGridView1.ClearSelection();   //取消默认选择的第一行
                    SQLServer.AutoSizeColumn(dataGridView1);  //宽度自适应
                    this.dataGridView1.Columns[0].HeaderText = "员工编号";   //设置列名
                    this.dataGridView1.Columns[1].HeaderText = "姓名";
                    this.dataGridView1.Columns[2].HeaderText = "个税税率（%）";
                    this.dataGridView1.Columns[3].HeaderText = "五险一金（元）";
                    this.dataGridView1.Columns[4].HeaderText = "最低纳税金额（元）";
                    this.dataGridView1.Columns[5].HeaderText = "日期";
                    dataGridView1.AllowUserToAddRows = false;  //关闭用户增加行（最后一行空白行）
                    conn.Close(); // 关闭数据库连接
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：" + ex.Message, "出现错误");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((comboBox1.Text.Trim() == string.Empty))
            {
                MessageBox.Show("警告信息：请输入选择保险税收编号！");
            }
            else
            {
                if ((textBox4.Text.Trim() != string.Empty))
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                        {
                            conn.Open(); // 打开数据库连接

                            String sid = textBox4.Text.Trim();
                            int rid = comboBox1.FindString(comboBox1.Text) + 1;
                            String time = dateTimePicker1.Value.Date.ToString();



                            SQLHelper helper = new SQLHelper();
                            string sql0 = "select sid,time  from withhold where sid=" + "'" + sid + "'" + "and time=" + "'" + time + "'";
                            int count = 0;
                            count = helper.SqlServerRecordCount(sql0);
                            if (count == 1)
                            {
                                MessageBox.Show("该工号所对应员工该月已有代扣工资记录，请不要重复代扣！");
                                return;
                            }


                            else
                            {


                                String sql = "insert into withhold(sid,rid,time) values(" + sid + "," + rid + ",'" + time + "')"; // 插入语句 comboBox2.FindString(comboBox2.Text).ToString()获取当前选项的索引值
                                SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                                DataTable dt = new DataTable(); // 实例化数据表
                                myda.Fill(dt); // 保存数据 

                                String sql1 = "select withhold.sid,staff.name,rate.tax,rate.gold,rate.min,convert(varchar(7),withhold.time,120) from withhold left join staff on staff.sid=withhold.sid left join rate on withhold.rid=rate.id"; // 查询语句staff表除了部门编号的全部内容，department的名称
                                SqlDataAdapter myda1 = new SqlDataAdapter(sql1, conn); // 实例化适配器
                                DataTable dt1 = new DataTable(); // 实例化数据表
                                myda1.Fill(dt1); // 保存数据 
                                dataGridView1.DataSource = dt1; // 设置到DataGridView中

                                conn.Close(); // 关闭数据库连接
                                MessageBox.Show("写入成功！");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("警告信息：" + ex.Message, "出现错误");
                    }
                }
                else
                {
                    MessageBox.Show("警告信息：请输入需要写入的工号！");
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
