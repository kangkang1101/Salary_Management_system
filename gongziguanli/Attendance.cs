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
    public partial class Attendance : Form
    {
        public Attendance()
        {
            InitializeComponent();
        }

        private void Attendance_Load(object sender, EventArgs e)
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
                    String sql = "select staff.sid,staff.name,attendance.oday,attendance.aday,convert(varchar(7),attendance.time,120) from attendance left join staff on attendance.sid=staff.sid"; // 查询语句staff表除了部门编号的全部内容，department的名称
                    SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                    DataTable dt = new DataTable(); // 实例化数据表
                    myda.Fill(dt); // 保存数据 
                    dataGridView1.DataSource = dt; // 设置到DataGridView中
                    this.dataGridView1.ClearSelection();   //取消默认选择的第一行
                    SQLServer.AutoSizeColumn(dataGridView1);  //宽度自适应
                    this.dataGridView1.Columns[0].HeaderText = "员工编号";   //设置列名
                    this.dataGridView1.Columns[1].HeaderText = "姓名";
                    this.dataGridView1.Columns[2].HeaderText = "加班天数";
                    this.dataGridView1.Columns[3].HeaderText = "缺勤天数";
                    this.dataGridView1.Columns[4].HeaderText = "日期";
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
            if ((textBox3.Text.Trim() == string.Empty) || (textBox4.Text.Trim() == string.Empty))
            {
                MessageBox.Show("警告信息：请输入加班天数和缺勤天数！");
            }
            else
            {
                if ((textBox6.Text.Trim() != string.Empty))
                {


                    try
                    {
                        using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                        {
                            conn.Open(); // 打开数据库连接

                            String sid = textBox6.Text.Trim();
                            String oday = textBox4.Text.Trim();
                            String aday = textBox3.Text.Trim();
                            String time = dateTimePicker1.Value.Date.ToString();




                            SQLHelper helper = new SQLHelper();
                            string sql0 = "select sid,time  from attendance where sid=" + "'" + sid + "'" + "and time=" + "'" + time + "'";
                            int count = 0;
                            count = helper.SqlServerRecordCount(sql0);
                            if (count == 1)
                            {
                                MessageBox.Show("该工号所对应员工该月已有出勤记录，请不要重复插入！");
                                return;
                            }

                            else
                            {
                                String sql = "insert into attendance(sid,oday,aday,time) values('" + sid + "','" + oday + "','" + aday + "','" + time + "')"; // 插入语句 comboBox2.FindString(comboBox2.Text).ToString()获取当前选项的索引值
                                SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                                DataTable dt = new DataTable(); // 实例化数据表
                                myda.Fill(dt); // 保存数据 

                                String sql1 = "select staff.sid,staff.name,attendance.oday,attendance.aday,convert(varchar(7),attendance.time,120) from attendance left join staff on attendance.sid=staff.sid"; // 查询语句staff表除了部门编号的全部内容，department的名称
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

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox6.Text.Trim() != string.Empty)  //通过工号查找
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                    {
                        conn.Open(); // 打开数据库连接
                        String time = dateTimePicker1.Value.Date.ToString();
                        String year = dateTimePicker1.Value.Year.ToString();
                        String month = dateTimePicker1.Value.Month.ToString();
                        String date = year + "-0" + month;

                        String sql2 = "select sid from attendance where sid=" + int.Parse(textBox6.Text)+ " and convert(varchar(7),attendance.time,120)=" + "'"+ date+"'";
                        SQLHelper helper = new SQLHelper();
                        int count = 0;
                        count = helper.SqlServerRecordCount(sql2);
                        if (count < 1) {
                            MessageBox.Show("不存在匹配的项");
                        }
                        else{

                        String sql = "delete from attendance where sid=" +int.Parse(textBox6.Text)+ "and convert(varchar(7),attendance.time,120)=" + "'" + date + "'"; // 删除语句
                            SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                            DataTable dt = new DataTable(); // 实例化数据表
                            myda.Fill(dt); // 保存数据 

                        String sql3 = "delete from salary where sid=" + int.Parse(textBox6.Text) + "and convert(varchar(7),salary.time,120)=" + "'" + date + "'";
                            SQLHelper helper3 = new SQLHelper();
                            helper3.ExecuteScalar(sql3);

                        String sql4 = "delete from withhold where sid=" + int.Parse(textBox6.Text) + "and convert(varchar(7),withhold.time,120)=" + "'" + date + "'";
                            SQLHelper helper4 = new SQLHelper();
                            helper4.ExecuteScalar(sql4);

                            MessageBox.Show("当月出勤信息已删除，同时当月的代扣，工资记录已删除！");

                        }

                        String sql1 = "select staff.sid,staff.name,attendance.oday,attendance.aday,convert(varchar(7),attendance.time,120) from attendance left join staff on attendance.sid=staff.sid"; // 查询语句staff表除了部门编号的全部内容，department的名称
                        SqlDataAdapter myda1 = new SqlDataAdapter(sql1, conn); // 实例化适配器
                        DataTable dt1 = new DataTable(); // 实例化数据表
                        myda1.Fill(dt1); // 保存数据 
                        dataGridView1.DataSource = dt1; // 设置到DataGridView中
                        this.dataGridView1.ClearSelection();   //取消默认选择的第一行
                        SQLServer.AutoSizeColumn(dataGridView1);  //宽度自适应
                        this.dataGridView1.Columns[0].HeaderText = "员工编号";   //设置列名
                        this.dataGridView1.Columns[1].HeaderText = "姓名";
                        this.dataGridView1.Columns[2].HeaderText = "加班天数";
                        this.dataGridView1.Columns[3].HeaderText = "缺勤天数";
                        this.dataGridView1.Columns[4].HeaderText = "日期";
                        dataGridView1.AllowUserToAddRows = false;  //关闭用户增加行（最后一行空白行）
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
            else
            {
                MessageBox.Show("警告信息：请输入需要删除的员工工号！");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox6.Text.Trim() != string.Empty)  //通过id修改部门信息
            {
                if ((textBox4.Text.Trim() != string.Empty) && (textBox3.Text.Trim() != string.Empty) && (dateTimePicker1.Text.Trim() != string.Empty))
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                        {

                            conn.Open(); // 打开数据库连接
                            String oday = textBox4.Text.Trim();
                            String aday = textBox3.Text.Trim();
                            String time = dateTimePicker1.Value.Date.ToString();
                            String year = dateTimePicker1.Value.Year.ToString();
                            String month = dateTimePicker1.Value.Month.ToString();
                            String date = year + "-0" + month;

                            String sql2 = "select sid from attendance where sid=" + int.Parse(textBox6.Text) + " and convert(varchar(7),attendance.time,120)=" + "'" + date + "'";
                            SQLHelper helper = new SQLHelper();
                            int count = 0;
                            count = helper.SqlServerRecordCount(sql2);
                            if (count < 1)
                            {
                                MessageBox.Show("不存在匹配的项");
                            }
                            else
                            {
                                String sql = "update attendance set oday='" + textBox4.Text + "', aday='" + textBox3.Text + "', time='" + dateTimePicker1.Value.Date.ToString() + "' where sid=" + textBox6.Text + " and convert(varchar(7),attendance.time,120)=" + "'" + date + "'"; // 查询语句
                                String sql3 = "select sid from salary where sid=" + int.Parse(textBox6.Text) + "and convert(varchar(7),time,120)=" + "'" + date + "'";
                                int count1 = 0;
                                count1 = helper.SqlServerRecordCount(sql3);
                                if(count1>0)
                                {
                                    String sql5="select gold,tax,min,jiang,fa,shouldsalary from mingxi where sid=" + int.Parse(textBox6.Text) + "and convert(varchar(7),time,120)=" + "'" + date + "'";

                                    String sql4="update mingxi set oday='"+textBox4.Text+"',aday='"+textBox3.Text+"'where sid="+textBox6.Text+"and convert(varchar(7),time,120)=" + "'" + date + "'"; // 查询语句
                              
                                }
                                SQLHelper helper1 = new SQLHelper();
                                helper1.ExecuteScalar(sql);


                               



                                MessageBox.Show("修改成功！");

                            }


                            String sql1 = "select staff.sid,staff.name,attendance.oday,attendance.aday,convert(varchar(7),attendance.time,120) from attendance left join staff on attendance.sid=staff.sid"; // 查询语句staff表除了部门编号的全部内容，department的名称
                            SqlDataAdapter myda1 = new SqlDataAdapter(sql1, conn); // 实例化适配器
                            DataTable dt1 = new DataTable(); // 实例化数据表
                            myda1.Fill(dt1); // 保存数据 
                            dataGridView1.DataSource = dt1; // 设置到DataGridView中
                            this.dataGridView1.ClearSelection();   //取消默认选择的第一行
                            SQLServer.AutoSizeColumn(dataGridView1);  //宽度自适应
                            this.dataGridView1.Columns[0].HeaderText = "员工编号";   //设置列名
                            this.dataGridView1.Columns[1].HeaderText = "姓名";
                            this.dataGridView1.Columns[2].HeaderText = "加班天数";
                            this.dataGridView1.Columns[3].HeaderText = "缺勤天数";
                            this.dataGridView1.Columns[4].HeaderText = "日期";
                            dataGridView1.AllowUserToAddRows = false;  //关闭用户增加行（最后一行空白行）
                            conn.Close();
                            
                        }
                    }
                    catch (Exception ex)
                    {               
                            MessageBox.Show("查询错误：" + ex.Message, "请将本错误信息发送给管理员，请求协助解决！");                        
                    }
                }
                else
                {
                    MessageBox.Show("警告信息：请输入修改后的员工信息！");
                }
            }
            else
            {
                MessageBox.Show("警告信息：请输入需要修改的工号！");
            }
        }
    }
}
