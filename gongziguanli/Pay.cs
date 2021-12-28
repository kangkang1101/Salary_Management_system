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
    public partial class Pay : Form
    {
        String salaryDate, attendanceDate;  //日期 xx-xx (年-月)
        public Pay()
        {
            InitializeComponent();
        }

        private void Pay_Load(object sender, EventArgs e)
        {   //设置日期格式
            dateTimePicker1.CustomFormat = "yyyy年MM月";   //修改只显示年月
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.ShowUpDown = true;

            dateTimePicker2.CustomFormat = "yyyy年MM月";   //修改只显示年月
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.ShowUpDown = true;

            dateTimePicker3.CustomFormat = "yyyy年MM月";   //修改只显示年月
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.ShowUpDown = true;
            try
            {
                //实现自动释放托管资源。
                using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                {
                    conn.Open(); // 打开数据库连接
                                 // 左外连接查询语句，只保留左边关系salsry中的悬浮元组
                    String sql = "select salary.sid,staff.name,department.name,salary.shouldsalary,salary.realsalary,convert(varchar(7),salary.time,120) from salary left join staff on staff.sid=salary.sid left join department on department.did=staff.did and salary.sid=staff.sid";
                    SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器天（执行sql语句）
                    DataTable dt = new DataTable(); // 实例化数据表（初始化返回值保存表）
                    myda.Fill(dt); // 保存数据 (将查询返回值保存在DataTable中）
                    dataGridView1.DataSource = dt; // 设置到DataGridView中（数据源传输）
                    this.dataGridView1.ClearSelection();   //取消默认选择的第一行
                    SQLServer.AutoSizeColumn(dataGridView1);  //宽度自适应
                    this.dataGridView1.Columns[0].HeaderText = "员工编号";   //设置列名
                    this.dataGridView1.Columns[1].HeaderText = "姓名";
                    this.dataGridView1.Columns[2].HeaderText = "部门";
                    this.dataGridView1.Columns[3].HeaderText = "应发工资（元）";
                    this.dataGridView1.Columns[4].HeaderText = "实发工资（元）";
                    this.dataGridView1.Columns[5].HeaderText = "时间";
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

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim() != string.Empty)  //通过工号查找
            {
                String time = dateTimePicker2.Value.Date.ToString();
                String year = dateTimePicker2.Value.Year.ToString();
                String month = dateTimePicker2.Value.Month.ToString();
                String date = year + "-0" + month;                  //查询改员工当月的出勤信息与代扣信息
                String sid = textBox3.Text;
                try
                {
                    using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                    {
                        //SQLServer.conn.ConnectionString = connsql;
                        conn.Open(); // 打开数据库连接
                        String sql = "select salary.sid,staff.name,department.name,salary.shouldsalary,salary.realsalary,convert(varchar(7),salary.time,120) from salary,department,staff where staff.sid=" + sid + " and department.did=staff.did and salary.sid=" + sid + " and convert(varchar(7),salary.time,120) like '" + date + "'"; // 查询语句
                        SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                        DataTable dt = new DataTable(); // 实例化数据表
                        myda.Fill(dt); // 保存数据 
                        dataGridView1.DataSource = dt; // 设置到datagridview中
                        this.dataGridView1.Columns[0].HeaderText = "员工编号";   //设置列名
                        this.dataGridView1.Columns[1].HeaderText = "姓名";
                        this.dataGridView1.Columns[2].HeaderText = "部门";
                        this.dataGridView1.Columns[3].HeaderText = "应发工资（元）";
                        this.dataGridView1.Columns[4].HeaderText = "实发工资（元）";
                        this.dataGridView1.Columns[5].HeaderText = "时间";

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
                MessageBox.Show("警告信息：请输入需要查询的工号！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String time = dateTimePicker3.Value.Date.ToString();
            String year = dateTimePicker3.Value.Year.ToString();
            String month = dateTimePicker3.Value.Month.ToString();
            String date = year + "-0" + month;                  //查询改员工当月的出勤信息与代扣信息
            try
            {
                using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                {
                    //SQLServer.conn.ConnectionString = connsql;
                    conn.Open(); // 打开数据库连接
                    String sql = "select salary.sid,staff.name,department.name,salary.shouldsalary,salary.realsalary,convert(varchar(7),salary.time,120) from salary,department,staff where department.did=staff.did and salary.sid=staff.sid and convert(varchar(7),salary.time,120) like '" + date + "'"; // 查询语句
                    SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                    DataTable dt = new DataTable(); // 实例化数据表
                    myda.Fill(dt); // 保存数据 
                    dataGridView1.DataSource = dt; // 设置到datagridview中
                    this.dataGridView1.Columns[0].HeaderText = "员工编号";   //设置列名
                    this.dataGridView1.Columns[1].HeaderText = "姓名";
                    this.dataGridView1.Columns[2].HeaderText = "部门";
                    this.dataGridView1.Columns[3].HeaderText = "应发工资（元）";
                    this.dataGridView1.Columns[4].HeaderText = "实发工资（元）";
                    this.dataGridView1.Columns[5].HeaderText = "时间";

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

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            String time1 = dateTimePicker3.Value.Date.ToString();
            String year1 = dateTimePicker3.Value.Year.ToString();
            String month1 = dateTimePicker3.Value.Month.ToString();
            String date1 = year1 + "-0" + month1;
            if (textBox6.Text.Trim() != string.Empty && textBox5.Text.Trim() != string.Empty && textBox8.Text.Trim() != string.Empty && textBox5.Text.Trim() != string.Empty)
            {
                String sid = textBox6.Text.Trim();
                float shouldsalary = float.Parse(textBox5.Text.Trim());//类型转换
                float jiangli = float.Parse(textBox8.Text.Trim());
                float koukuan = float.Parse(textBox7.Text.Trim());
                float realsalary;
                float min, oday, aday, tax, gold;
                SQLHelper helper = new SQLHelper();
                string sql0 = "select sid from salary where sid=" + "'" + sid + "'" + "and time=" + "'" + time1 + "'";
                int count = 0;
                count = helper.SqlServerRecordCount(sql0);
                if (count > 0)
                {
                    using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                    {
                        String sql2 = "select attendance.sid,attendance.oday,attendance.aday,rate.gold, rate.tax,rate.min from attendance,rate where convert(varchar(7),attendance.time,120)='" + date1 + "'  and attendance.sid=" + sid + " and rate.id in (select rid from withhold where convert(varchar(7),withhold.time,120)='" + date1 + "' and withhold.sid=" + sid + ")";
                        SqlDataAdapter myda2 = new SqlDataAdapter(sql2, conn); // 实例化适配器
                        DataTable dt2 = new DataTable(); // 实例化数据表
                        myda2.Fill(dt2); // 保存数据 
                        oday = float.Parse(dt2.Rows[0]["oday"].ToString());
                        aday = float.Parse(dt2.Rows[0]["aday"].ToString());
                        gold = float.Parse(dt2.Rows[0]["gold"].ToString());
                        tax = float.Parse(dt2.Rows[0]["tax"].ToString());
                        min = float.Parse(dt2.Rows[0]["min"].ToString());

                        if (shouldsalary >= min)  //应发金额大于纳税最低金额才扣税！
                        {
                            realsalary = shouldsalary * (1 - tax / 100);//扣除总税
                        }
                        else
                        {
                            realsalary = shouldsalary;
                        }
                        realsalary = realsalary + oday * jiangli - aday * koukuan;  //最后的实发工资(基本工资+加班工资-缺勤工资）
                                                                                    //将工资信息插入工资表
                        String sql = "update salary set shouldsalary=" + shouldsalary + ",realsalary=" + realsalary + " where sid="+sid+"and time='"+time1+"'"; // 插入语句 
                        SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                        DataTable dt = new DataTable(); // 实例化数据表
                        myda.Fill(dt); // 保存数据 
                        float jiangli1 = float.Parse(textBox8.Text.Trim());
                        float koukuan1 = float.Parse(textBox7.Text.Trim());
                        String sql5 = "update mingxi set shouldsalary=" + shouldsalary + ",realsalary=" + realsalary + " ,jiang=" + jiangli1 + " ,fa=" + koukuan1 + "where sid=" + sid + "and time='" + time1 + "'";
                        SqlDataAdapter myda5 = new SqlDataAdapter(sql5, conn); // 实例化适配器
                        DataTable dt5 = new DataTable(); // 实例化数据表
                        myda5.Fill(dt5); // 保存数据 

                        String sql1 = "select salary.sid,staff.name,department.name,salary.shouldsalary,salary.realsalary,convert(varchar(7),salary.time,120) from salary left join staff on staff.sid=salary.sid left join department on department.did=staff.did and salary.sid=staff.sid"; // 查询语句
                        SqlDataAdapter myda1 = new SqlDataAdapter(sql1, conn); // 实例化适配器
                        DataTable dt1 = new DataTable(); // 实例化数据表
                        myda1.Fill(dt1); // 保存数据 
                        dataGridView1.DataSource = dt1; // 设置到DataGridView中

                        conn.Close(); // 关闭数据库连接
                        MessageBox.Show("修改成功！");
                    }
                }
                else
                {
                    MessageBox.Show("不存在匹配项，请重新输入！");
                }
            }
            else
            {
                MessageBox.Show("不能有空项！");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox6.Text.Trim() != string.Empty)  //通过工号查找
            {
                String time1 = dateTimePicker3.Value.Date.ToString();
                String year1 = dateTimePicker3.Value.Year.ToString();
                String month1 = dateTimePicker3.Value.Month.ToString();
                String date1 = year1 + "-0" + month1;
                SQLHelper helper = new SQLHelper();
                String sid = textBox6.Text.Trim();
                string sql0 = "select sid from salary where sid=" + int.Parse(sid) + "and convert(varchar(7),salary.time,120) like" + "'" + date1 + "'";
                int count = 0;
                count = helper.SqlServerRecordCount(sql0);
                if (count > 0)
                {
                    String sql1 = "delete from salary where sid=" + "'" + sid + "'" + "and convert(varchar(7),salary.time,120) like" + "'" + date1 + "'";
                    using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                    {
                        SqlDataAdapter myda1 = new SqlDataAdapter(sql1, conn);
                        DataTable dt1 = new DataTable();
                        myda1.Fill(dt1);
                        String sql2 = "delete from mingxi where sid=" + "'" + sid + "'" + "and convert(varchar(7),mingxi.time,120) like" + "'" +date1 + "'";
                        SqlDataAdapter myda2 = new SqlDataAdapter(sql2, conn);
                        DataTable dt2 = new DataTable();
                        myda1.Fill(dt2);
                        MessageBox.Show("删除成功！");
                        String sql = "select salary.sid,staff.name,department.name,salary.shouldsalary,salary.realsalary,convert(varchar(7),salary.time,120) from salary left join staff on staff.sid=salary.sid left join department on department.did=staff.did and salary.sid=staff.sid";
                        SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器天（执行sql语句）
                        DataTable dt = new DataTable(); // 实例化数据表（初始化返回值保存表）
                        myda.Fill(dt); // 保存数据 (将查询返回值保存在DataTable中）
                        dataGridView1.DataSource = dt; // 设置到DataGridView中（数据源传输）
                        this.dataGridView1.ClearSelection();   //取消默认选择的第一行
                        SQLServer.AutoSizeColumn(dataGridView1);  //宽度自适应
                        this.dataGridView1.Columns[0].HeaderText = "员工编号";   //设置列名
                        this.dataGridView1.Columns[1].HeaderText = "姓名";
                        this.dataGridView1.Columns[2].HeaderText = "部门";
                        this.dataGridView1.Columns[3].HeaderText = "应发工资（元）";
                        this.dataGridView1.Columns[4].HeaderText = "实发工资（元）";
                        this.dataGridView1.Columns[5].HeaderText = "时间";
                        dataGridView1.AllowUserToAddRows = false;  //关闭用户增加行（最后一行空白行）
                        conn.Close(); // 关闭数据库连接
                    }
                }
                else
                {
                    MessageBox.Show("没有匹配的项！");
                }

            }
            else
            {
                MessageBox.Show("请输入要删除薪资的员工工号");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if ((textBox6.Text.Trim() == string.Empty))
            {
                MessageBox.Show("警告信息：请输入工号！");
            }
            else
            {
                if ((textBox5.Text.Trim() != string.Empty) && (textBox8.Text.Trim() != string.Empty) && (textBox7.Text.Trim() != string.Empty))
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                        {
                            conn.Open(); // 打开数据库连接

                            String sid = textBox6.Text.Trim();
                            float shouldsalary = float.Parse(textBox5.Text.Trim());//类型转换
                            float jiangli = float.Parse(textBox8.Text.Trim());
                            float koukuan = float.Parse(textBox7.Text.Trim());
                            float realsalary;
                            float min, oday, aday, tax, gold;
                            String time = dateTimePicker1.Value.Date.ToString();
                            String year = dateTimePicker1.Value.Year.ToString();
                            String month = dateTimePicker1.Value.Month.ToString();
                            String date = year + "-0" + month;


                            SQLHelper helper = new SQLHelper();
                            string sql0 = "select sid,time  from salary where sid=" + "'" + sid + "'" + "and time=" + "'" + time + "'";
                            int count = 0;
                            count = helper.SqlServerRecordCount(sql0);
                            if (count == 1)
                            {
                                MessageBox.Show("该工号所对应员工该月已有工资记录，请不要重复发工资！");
                                return;
                            }
                            else
                            {

                                //查询改员工当月的出勤信息与代扣信息(oday加班天数，aday缺勤天数）
                                //先通过工资代扣表查出对应的员工保险税收编号，再从出勤表、税收表根据出勤工号、对应日期、税收编号查出员工当月的出勤信息与代扣信息
                                String sql2 = "select attendance.sid,attendance.oday,attendance.aday,rate.gold, rate.tax,rate.min from attendance,rate where convert(varchar(7),attendance.time,120)='" + date + "'  and attendance.sid=" + sid + " and rate.id in (select rid from withhold where convert(varchar(7),withhold.time,120)='" + date + "' and withhold.sid=" + sid + ")";
                                SqlDataAdapter myda2 = new SqlDataAdapter(sql2, conn); // 实例化适配器
                                DataTable dt2 = new DataTable(); // 实例化数据表
                                myda2.Fill(dt2); // 保存数据 
                                oday = float.Parse(dt2.Rows[0]["oday"].ToString());
                                aday = float.Parse(dt2.Rows[0]["aday"].ToString());
                                gold = float.Parse(dt2.Rows[0]["gold"].ToString());
                                tax = float.Parse(dt2.Rows[0]["tax"].ToString());
                                min = float.Parse(dt2.Rows[0]["min"].ToString());

                                if (shouldsalary >= min)  //应发金额大于纳税最低金额才扣税！
                                {
                                    realsalary = shouldsalary * (1 - tax / 100);//扣除总税
                                }
                                else
                                {
                                    realsalary = shouldsalary;
                                }
                                realsalary = realsalary + oday * jiangli - aday * koukuan;  //最后的实发工资(基本工资+加班工资-缺勤工资）
                                                                                            //将工资信息插入工资表
                                String sql = "insert into salary(sid,shouldsalary,realsalary,time) values(" + sid + "," + shouldsalary + "," + realsalary + ",'" + time + "')"; // 插入语句 
                                SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                                DataTable dt = new DataTable(); // 实例化数据表
                                myda.Fill(dt); // 保存数据 

                                String sql5 = "insert into mingxi values(" + sid + "," + oday + "," + aday + "," + gold + "," +
                                    tax + "," + min + "," + jiangli + "," + koukuan + "," + shouldsalary + "," + realsalary + ",'" + time + "')";
                                SqlDataAdapter myda5 = new SqlDataAdapter(sql5, conn); // 实例化适配器
                                DataTable dt5 = new DataTable(); // 实例化数据表
                                myda5.Fill(dt5); // 保存数据 

                                String sql1 = "select salary.sid,staff.name,department.name,salary.shouldsalary,salary.realsalary,convert(varchar(7),salary.time,120) from salary left join staff on staff.sid=salary.sid left join department on department.did=staff.did and salary.sid=staff.sid"; // 查询语句
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
                        if (ex.GetType().ToString() == "System.IndexOutOfRangeException")
                        {
                            MessageBox.Show("警告信息：此员工此月份还没设置出勤信息、工资代扣信息，请填写后再进行计算！", "出现错误!");
                        }
                        else
                        {
                            MessageBox.Show("警告信息：" + ex.Message, "出现错误!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("警告信息：请输入需要写入的工号！");
                }
            }
        }
    }
}
