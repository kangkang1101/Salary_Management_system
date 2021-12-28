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
    public partial class Pay2 : Form
    {
        public Pay2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String time = dateTimePicker1.Value.Date.ToString();
            String year = dateTimePicker1.Value.Year.ToString();
            String month = dateTimePicker1.Value.Month.ToString();
            String date = year + "-0" + month;                  //查询改员工当月的出勤信息与代扣信息
            int sid = int.Parse(Program.mainUser);
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

        private void Pay2_Load(object sender, EventArgs e)
        {
            //设置日期格式
            dateTimePicker1.CustomFormat = "yyyy年MM月";   //修改只显示年月
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.ShowUpDown = true;
        }
    }
}
