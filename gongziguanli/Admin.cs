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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox6.Text.Trim() != string.Empty)  //通过id修改部门信息
            {
                if(textBox5.Text.Trim() != string.Empty)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                        {
                            //SQLServer.conn.ConnectionString = connsql;
                            conn.Open(); // 打开数据库连接

                            String sql = "update department set name='" + textBox5.Text + "' where did='" + textBox6.Text + "'"; // 查询语句

                            SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器

                            DataTable dt = new DataTable(); // 实例化数据表
                            myda.Fill(dt); // 保存数据 

                            String sql1 = "select * from department"; // 更新dataGridView1数据

                            SqlDataAdapter myda1 = new SqlDataAdapter(sql1, conn);

                            DataTable dt1 = new DataTable();
                            myda1.Fill(dt1);

                            dataGridView1.DataSource = dt1; // 设置到datagridview中
                            this.dataGridView1.Columns[0].HeaderText = "部门编号";   //设置列名
                            this.dataGridView1.Columns[1].HeaderText = "部门名称";
                            this.dataGridView1.Columns[2].HeaderText = "部门人数";
                            textBox7.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();  //将查询结果显示在textbox中
                            textBox5.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                            textBox6.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
                            MessageBox.Show("部号为" + textBox6.Text + "的部门名称成功修改为" + textBox5.Text);
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
                    MessageBox.Show("警告信息：请输入修改后的部门名称！");
                }
            }
            else
            {
                MessageBox.Show("警告信息：请输入需要修改的部号！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox6.Text.Trim() != string.Empty)  //通过id查找
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                    {
                        //SQLServer.conn.ConnectionString = connsql;
                        conn.Open(); // 打开数据库连接

                        String sql = "select * from department where did=" + textBox6.Text; // 查询语句

                        SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器

                        DataTable dt = new DataTable(); // 实例化数据表
                        myda.Fill(dt); // 保存数据 
                        dataGridView1.DataSource = dt; // 设置到datagridview中
                        this.dataGridView1.Columns[0].HeaderText = "部门编号";   //设置列名
                        this.dataGridView1.Columns[1].HeaderText = "部门名称";
                        this.dataGridView1.Columns[2].HeaderText = "部门人数";
                        textBox7.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();  //将查询结果显示在textbox中
                        textBox5.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                        textBox6.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();

                        conn.Close(); // 关闭数据库连接
                    }
                }
                catch (Exception ex)
                {   
                    if(ex.GetType().ToString() == "System.ArgumentOutOfRangeException")
                    {
                        MessageBox.Show("查询错误：查询不到相关部门，请确认部门是否存在！");
                    }
                    else
                    {
                        MessageBox.Show("查询错误：" + ex.Message, "请将本错误信息发送给管理员，请求协助解决！");
                    }
                }
            }
            if (textBox5.Text.Trim() != string.Empty)  //通过名称查找
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                    {
                        //SQLServer.conn.ConnectionString = connsql;
                        conn.Open(); // 打开数据库连接

                        String sql = "select * from department where name='" + textBox5.Text + "'"; // 查询语句

                        SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器

                        DataTable dt = new DataTable(); // 实例化数据表
                        myda.Fill(dt); // 保存数据 

                        dataGridView1.DataSource = dt; // 设置到DataGridView中
                        textBox7.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();  //将查询结果显示在textbox中
                        textBox5.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                        textBox6.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
                        conn.Close(); // 关闭数据库连接
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetType().ToString() == "System.ArgumentOutOfRangeException")
                    {
                        MessageBox.Show("查询错误：查询不到相关部门，请确认部门是否存在！");
                    }
                    else
                    {
                        MessageBox.Show("查询错误：" + ex.Message, "请将本错误信息发送给管理员，请求协助解决！");
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim() != string.Empty)  //通过id查找
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                    {
                        //SQLServer.conn.ConnectionString = connsql;
                        conn.Open(); // 打开数据库连接

                        String sql = "select * from rate where id=" + textBox3.Text; // 查询语句

                        SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器

                        DataTable dt = new DataTable(); // 实例化数据表
                        myda.Fill(dt); // 保存数据 
                        dataGridView1.DataSource = dt; // 设置到datagridview中
                        this.dataGridView1.Columns[0].HeaderText = "编号";   //设置列名
                        this.dataGridView1.Columns[1].HeaderText = "五险一金（元）";
                        this.dataGridView1.Columns[2].HeaderText = "个税税率（%）";
                        this.dataGridView1.Columns[3].HeaderText = "最低纳税金额（元）";
                        textBox3.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();  //将查询结果显示在textbox中
                        textBox1.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                        textBox2.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                        textBox4.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();

                        conn.Close(); // 关闭数据库连接
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetType().ToString() == "System.ArgumentOutOfRangeException")
                    {
                        MessageBox.Show("查询错误：查询不到相关保险税收信息，请确认信息是否存在！");
                    }
                    else
                    {
                        MessageBox.Show("查询错误：" + ex.Message, "请将本错误信息发送给管理员，请求协助解决！");
                    }
                }
            }
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                {
                    conn.Open(); // 打开数据库连接

                    String sql = "select * from department"; // 查询语句

                    SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器

                    DataTable dt = new DataTable(); // 实例化数据表
                    myda.Fill(dt); // 保存数据 

                    dataGridView1.DataSource = dt; // 设置到DataGridView中
                    SQLServer.AutoSizeColumn(dataGridView1);  //宽度自适应
                    this.dataGridView1.Columns[0].HeaderText = "部门编号";   //设置列名
                    this.dataGridView1.Columns[1].HeaderText = "部门名称";
                    this.dataGridView1.Columns[2].HeaderText = "部门人数";
                    dataGridView1.AllowUserToAddRows = false;  //关闭用户增加行（最后一行空白行）
                    conn.Close(); // 关闭数据库连接
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：" + ex.Message, "出现错误");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox6.Text.Trim() != string.Empty)
            {
                MessageBox.Show("警告信息：部门ID请留空，新增部门只需填写部门名称！");
            }
            else
            {
                if (textBox5.Text.Trim() != string.Empty)  //新增部门
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                        {
                            //SQLServer.conn.ConnectionString = connsql;
                            conn.Open(); // 打开数据库连接

                            String sql = "insert into department(name,number) values('" + textBox5.Text + "', 0)"; // 插入语句

                            SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器

                            DataTable dt = new DataTable(); // 实例化数据表
                            myda.Fill(dt); // 保存数据 

                            String sql1 = "select * from department"; // 更新dataGridView1数据

                            SqlDataAdapter myda1 = new SqlDataAdapter(sql1, conn); 

                            DataTable dt1 = new DataTable(); 
                            myda1.Fill(dt1); 

                            dataGridView1.DataSource = dt1; // 设置到datagridview中
                            this.dataGridView1.Columns[0].HeaderText = "部门编号";   //设置列名
                            this.dataGridView1.Columns[1].HeaderText = "部门名称";
                            this.dataGridView1.Columns[2].HeaderText = "部门人数";
                            conn.Close(); // 关闭数据库连接
                            MessageBox.Show("部门新增成功！");
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
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim() != string.Empty)
            {
                MessageBox.Show("警告信息：编号请留空，新增信息只需填其他内容！");
            }
            else
            {
                if ((textBox4.Text.Trim() != string.Empty) && (textBox1.Text.Trim() != string.Empty) && (textBox2.Text.Trim() != string.Empty))  //新增部门
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                        {
                            conn.Open(); // 打开数据库连接

                            String sql = "insert into rate(gold,tax, min) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "')"; // 插入语句

                            SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器

                            DataTable dt = new DataTable(); // 实例化数据表
                            myda.Fill(dt); // 保存数据 

                            String sql1 = "select * from rate"; // 更新dataGridView1数据

                            SqlDataAdapter myda1 = new SqlDataAdapter(sql1, conn);

                            DataTable dt1 = new DataTable();
                            myda1.Fill(dt1);

                            dataGridView1.DataSource = dt1; // 设置到datagridview中
                            this.dataGridView1.Columns[0].HeaderText = "编号";   //设置列名
                            this.dataGridView1.Columns[1].HeaderText = "五险一金（元）";
                            this.dataGridView1.Columns[2].HeaderText = "个税税率（%）";
                            this.dataGridView1.Columns[3].HeaderText = "最低纳税金额（元）";
                            conn.Close(); // 关闭数据库连接
                            MessageBox.Show("新增成功！");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("警告信息：" + ex.Message, "出现错误");
                    }
                }
                else
                {
                    MessageBox.Show("请输入个税税率、五险一金、最低纳税金额信息！");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim() != string.Empty)  //通过id修改保险税收
            {
                if ((textBox2.Text.Trim() != string.Empty) && (textBox1.Text.Trim() != string.Empty) && (textBox4.Text.Trim() != string.Empty))
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                        {
                            //SQLServer.conn.ConnectionString = connsql;
                            conn.Open(); // 打开数据库连接

                            String sql = "update rate set gold='" + textBox1.Text + "', tax='" + textBox2.Text + "', min='" + textBox4.Text + "' where id=" + textBox3.Text + ""; // 查询语句

                            SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器

                            DataTable dt = new DataTable(); // 实例化数据表
                            myda.Fill(dt); // 保存数据 

                            String sql1 = "select * from rate"; // 更新dataGridView1数据

                            SqlDataAdapter myda1 = new SqlDataAdapter(sql1, conn);

                            DataTable dt1 = new DataTable();
                            myda1.Fill(dt1);

                            dataGridView1.DataSource = dt1; // 设置到datagridview中
                            this.dataGridView1.Columns[0].HeaderText = "部门编号";   //设置列名
                            this.dataGridView1.Columns[1].HeaderText = "部门名称";
                            this.dataGridView1.Columns[2].HeaderText = "部门人数";
                            textBox7.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();  //将查询结果显示在textbox中
                            textBox5.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                            textBox6.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
                            MessageBox.Show("编号为" + textBox3.Text + "的个税税率已修改为" + textBox2.Text + ",五险一金已修改为" + textBox1.Text + ",最低纳税金额已修改为" + textBox4.Text);
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
                    MessageBox.Show("警告信息：请输入修改后个税税率、五险一金、最低纳税金额！");
                }
            }
            else
            {
                MessageBox.Show("警告信息：请输入需要修改的编号！");
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
