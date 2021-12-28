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
using System.Windows.Forms;
using SQLCONN;

namespace gongziguanli
{
    public partial class Addstaff : Form
    {
        public Addstaff()
        {
            InitializeComponent();
        }

        private void Addstaff_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                {
                    conn.Open(); // 打开数据库连接

                    String sql1 = "select name from department"; // 初始化部门下拉框
                    SqlDataAdapter myda1 = new SqlDataAdapter(sql1, conn); 
                    DataTable dt1 = new DataTable(); 
                    myda1.Fill(dt1);
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string strName = dt1.Rows[i]["name"].ToString();
                        comboBox2.Items.Add(new SQLServer.ComboxItem(strName, "Value1"));
                        //comboBox2.Items.Add(new SQLServer.ComboxItem(comboBox2.Items.IndexOf("Value1").ToString(), i.ToString()));
                    }

                    String sql = "select staff.sid,staff.name,staff.sex,department.name,staff.level,staff.jointime from staff left join department on staff.did=department.did"; // 查询语句staff表除了部门编号的全部内容，department的名称
                    SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                    DataTable dt = new DataTable(); // 实例化数据表
                    myda.Fill(dt); // 保存数据 
                    dataGridView1.DataSource = dt; // 设置到DataGridView中
                    SQLServer.AutoSizeColumn(dataGridView1);  //宽度自适应
                    this.dataGridView1.Columns[0].HeaderText = "员工编号";   //设置列名
                    this.dataGridView1.Columns[1].HeaderText = "姓名";
                    this.dataGridView1.Columns[2].HeaderText = "性别";
                    this.dataGridView1.Columns[3].HeaderText = "部门";
                    this.dataGridView1.Columns[4].HeaderText = "级别";
                    this.dataGridView1.Columns[5].HeaderText = "加入时间";
                    dataGridView1.AllowUserToAddRows = false;  //关闭用户增加行（最后一行空白行）
                    conn.Close(); // 关闭数据库连接
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：" + ex.Message, "出现错误");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != string.Empty)  //通过工号查找
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                    {
                        //SQLServer.conn.ConnectionString = connsql;
                        conn.Open(); // 打开数据库连接
                        //查询员工基本信息
                        String sql = "select staff.sid,staff.name,staff.sex,department.name,staff.level,staff.jointime from staff,department where staff.sid=" + textBox1.Text + " and staff.did=department.did"; // 查询语句
                        SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                        DataTable dt = new DataTable(); // 实例化数据表
                        myda.Fill(dt); // 保存数据 
                        dataGridView1.DataSource = dt; // 设置到datagridview中
                        this.dataGridView1.Columns[0].HeaderText = "员工编号";   //设置列名
                        this.dataGridView1.Columns[1].HeaderText = "姓名";
                        this.dataGridView1.Columns[2].HeaderText = "性别";
                        this.dataGridView1.Columns[3].HeaderText = "部门";
                        this.dataGridView1.Columns[4].HeaderText = "级别";
                        this.dataGridView1.Columns[5].HeaderText = "加入时间";
                        textBox1.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();  //将查询结果显示在textbox中
                        textBox2.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                        textBox3.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
                        comboBox1.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                        comboBox2.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();

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
                if (textBox2.Text.Trim() != string.Empty)  //通过名字查找
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                        {
                            //SQLServer.conn.ConnectionString = connsql;
                            conn.Open(); // 打开数据库连接

                            String sql = "select staff.sid,staff.name,staff.sex,department.name,staff.level,staff.jointime from staff,department where staff.name='" + textBox2.Text + "' and staff.did=department.did"; // 查询语句
                            SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                            DataTable dt = new DataTable(); // 实例化数据表
                            myda.Fill(dt); // 保存数据 
                            dataGridView1.DataSource = dt; // 设置到datagridview中
                            this.dataGridView1.Columns[0].HeaderText = "员工编号";   //设置列名
                            this.dataGridView1.Columns[1].HeaderText = "姓名";
                            this.dataGridView1.Columns[2].HeaderText = "性别";
                            this.dataGridView1.Columns[3].HeaderText = "部门";
                            this.dataGridView1.Columns[4].HeaderText = "级别";
                            this.dataGridView1.Columns[5].HeaderText = "加入时间";
                            textBox1.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();  //将查询结果显示在textbox中
                            textBox2.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                            textBox3.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
                            comboBox1.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                            comboBox2.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();

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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((textBox2.Text.Trim() != string.Empty) && (comboBox1.Text.Trim() != string.Empty) && textBox1.Text.Trim() != string.Empty)  //新增员工
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                    {
                        //SQLServer.conn.ConnectionString = connsql;
                        conn.Open(); // 打开数据库连接

                        int did = comboBox2.FindString(comboBox2.Text) + 1;
                        int id = int.Parse(textBox1.Text);

                        SQLHelper helper = new SQLHelper();
                        string sql0 = "select sid from staff where sid=" + "'" + id + "'";
                        int count = 0;
                        count = helper.SqlServerRecordCount(sql0);
                        if (count == 1)
                        {
                            MessageBox.Show("该工号已经存在该用户，请分配其他工号！");
                            return;
                        }


                        else
                        {
                            String sql = "insert into staff(sid,did,jointime,level,name,sex) values(" + id + "," + did + ",'" + dateTimePicker1.Value.Date.ToString() + "','" + textBox3.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "')"; // 插入语句 comboBox2.FindString(comboBox2.Text).ToString()获取当前选项的索引值
                            SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                            DataTable dt = new DataTable(); // 实例化数据表
                            myda.Fill(dt); // 保存数据 

                            String sql5 = "insert into [user](uid,uname,passwd,quanxian) values(" +
                                  id + ",'" + textBox2.Text + "','" + "123456" + "','" + "职工" + "')";
                            SqlDataAdapter myda5 = new SqlDataAdapter(sql5, conn); // 实例化适配器
                            DataTable dt5 = new DataTable(); // 实例化数据表
                            myda5.Fill(dt5); // 保存数据


                            String sql3 = "update department set number=number+1 where department.did=" + did; // 更新语句 将员工所在部门的人数+1
                            SqlDataAdapter myda3 = new SqlDataAdapter(sql3, conn); // 实例化适配器
                            DataTable dt3 = new DataTable(); // 实例化数据表
                            myda3.Fill(dt3); // 保存数据 

                            String sql1 = "select staff.sid,staff.name,staff.sex,department.name,staff.level,staff.jointime from staff left join department on staff.did=department.did"; // 更新dataGridView1数据
                            SqlDataAdapter myda1 = new SqlDataAdapter(sql1, conn);
                            DataTable dt1 = new DataTable();
                            myda1.Fill(dt1);

                            dataGridView1.DataSource = dt1; // 设置到datagridview中
                            this.dataGridView1.Columns[0].HeaderText = "员工编号";   //设置列名
                            this.dataGridView1.Columns[1].HeaderText = "姓名";
                            this.dataGridView1.Columns[2].HeaderText = "性别";
                            this.dataGridView1.Columns[3].HeaderText = "部门";
                            this.dataGridView1.Columns[4].HeaderText = "级别";
                            this.dataGridView1.Columns[5].HeaderText = "加入时间";
                            conn.Close(); // 关闭数据库连接
                            MessageBox.Show("员工新增成功！");
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
                MessageBox.Show("请输入需要新增的部门名称！");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != string.Empty)  //通过工号查找
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                    {
                        conn.Open(); // 打开数据库连接;

                        //修改部门人数
                        String sql6 = "update department set number=number-1 where department.did in(select staff.did from staff where staff.sid=" + textBox1.Text + ")"; // 更新语句 将员工所在部门的人数+1
                        SqlDataAdapter myda6 = new SqlDataAdapter(sql6, conn); // 实例化适配器
                        DataTable dt6 = new DataTable(); // 实例化数据表
                        myda6.Fill(dt6); // 保存数据

                        String sql = "delete from staff where sid=" + textBox1.Text; // 删除语句
                        SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                        DataTable dt = new DataTable(); // 实例化数据表
                        myda.Fill(dt); // 保存数据 

                        String sql1 = "select staff.sid,staff.name,staff.sex,department.name,staff.level,staff.jointime from staff left join department on staff.did=department.did"; // 更新dataGridView1数据
                        SqlDataAdapter myda1 = new SqlDataAdapter(sql1, conn);
                        DataTable dt1 = new DataTable();

                        //删除员工的出勤记录
                        String sql2 = "delete from attendance where sid=" + textBox1.Text; // 更新dataGridView1数据
                        SqlDataAdapter myda2 = new SqlDataAdapter(sql2, conn);
                        DataTable dt2 = new DataTable();
                        myda2.Fill(dt2);

                        //删除员工的代扣
                        String sql3 = "delete from withhold where sid=" + textBox1.Text; // 更新dataGridView1数据
                        SqlDataAdapter myda3 = new SqlDataAdapter(sql3, conn);
                        DataTable dt3 = new DataTable();
                        myda3.Fill(dt3);

                        //删除员工的代扣
                        String sql4 = "delete from salary where sid=" + textBox1.Text; // 更新dataGridView1数据
                        SqlDataAdapter myda4 = new SqlDataAdapter(sql4, conn);
                        DataTable dt4 = new DataTable();
                        myda4.Fill(dt4);

                        //删除员工工资明细
                        String sql5 = "delete from mingxi where sid=" + textBox1.Text; // 更新dataGridView1数据
                        SqlDataAdapter myda5 = new SqlDataAdapter(sql5, conn);
                        DataTable dt5 = new DataTable();
                        myda5.Fill(dt5);

                        //删除user表
                        String sql7 = "delete from [user] where uid=" + textBox1.Text; // 更新dataGridView1数据
                        SqlDataAdapter myda7 = new SqlDataAdapter(sql7, conn);
                        DataTable dt7 = new DataTable();
                        myda7.Fill(dt7);

                        myda1.Fill(dt1);
                        dataGridView1.DataSource = dt1; // 设置到datagridview中
                        this.dataGridView1.Columns[0].HeaderText = "员工编号";   //设置列名
                        this.dataGridView1.Columns[1].HeaderText = "姓名";
                        this.dataGridView1.Columns[2].HeaderText = "性别";
                        this.dataGridView1.Columns[3].HeaderText = "部门";
                        this.dataGridView1.Columns[4].HeaderText = "级别";
                        this.dataGridView1.Columns[5].HeaderText = "加入时间";
                        textBox1.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();  //将查询结果显示在textbox中
                        textBox2.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                        textBox3.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
                        comboBox1.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                        comboBox2.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();

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

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != string.Empty)  //通过id修改部门信息
            {
                if ((textBox2.Text.Trim() != string.Empty) && (textBox3.Text.Trim() != string.Empty) && (comboBox1.Text.Trim() != string.Empty) && (comboBox2.Text.Trim() != string.Empty) && (dateTimePicker1.Text.Trim() != string.Empty))
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                        {
                            //SQLServer.conn.ConnectionString = connsql;
                            conn.Open(); // 打开数据库连接

                            //修改部门人数
                            String sql6 = "update department set number=number-1 where department.did in(select staff.did from staff where staff.sid=" + textBox1.Text + ")"; // 更新语句 将员工所在部门的人数+1
                            SqlDataAdapter myda6 = new SqlDataAdapter(sql6, conn); // 实例化适配器
                            DataTable dt6 = new DataTable(); // 实例化数据表
                            myda6.Fill(dt6); // 保存数据

                            int did = comboBox2.FindString(comboBox2.Text) + 1;
                            String sql = "update staff set name='" + textBox2.Text + "', sex='" + comboBox1.Text + "', did=" + did+ ", level='" + textBox3.Text + "', jointime='" + dateTimePicker1.Value.Date.ToString() + "' where sid=" + textBox1.Text; // 查询语句
                            SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                            DataTable dt = new DataTable(); // 实例化数据表
                            myda.Fill(dt); // 保存数据 

                            //修改部门人数
                            String sql3 = "update department set number=number+1 where department.did=" + did; // 更新语句 将员工所在部门的人数+1
                            SqlDataAdapter myda3 = new SqlDataAdapter(sql3, conn); // 实例化适配器
                            DataTable dt3 = new DataTable(); // 实例化数据表
                            myda3.Fill(dt3); // 保存数据 

                            String sql1 = "select staff.sid,staff.name,staff.sex,department.name,staff.level,staff.jointime from staff left join department on staff.did=department.did"; // 查询语句 // 更新dataGridView1数据
                            SqlDataAdapter myda1 = new SqlDataAdapter(sql1, conn);
                            DataTable dt1 = new DataTable();
                            myda1.Fill(dt1);
                            dataGridView1.DataSource = dt1; // 设置到datagridview中
                            this.dataGridView1.Columns[0].HeaderText = "员工编号";   //设置列名
                            this.dataGridView1.Columns[1].HeaderText = "姓名";
                            this.dataGridView1.Columns[2].HeaderText = "性别";
                            this.dataGridView1.Columns[3].HeaderText = "部门";
                            this.dataGridView1.Columns[4].HeaderText = "级别";
                            this.dataGridView1.Columns[5].HeaderText = "加入时间";
                            textBox1.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();  //将查询结果显示在textbox中
                            textBox2.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                            textBox3.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
                            comboBox1.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                            comboBox2.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                            MessageBox.Show("工号为" + textBox1.Text + "的信息已修改！");
                            conn.Close(); // 关闭数据库连接
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.GetType().ToString() == "System.ArgumentOutOfRangeException")
                        {
                            MessageBox.Show("查询错误：修改出错，请确认部号是否存在！");
                        }
                        else
                        {
                            MessageBox.Show("查询错误：" + ex.Message, "请将本错误信息发送给管理员，请求协助解决！");
                        }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
