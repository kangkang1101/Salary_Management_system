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
    public partial class Attendance2 : Form
    {
        public Attendance2()
        {
            InitializeComponent();
        }

      

        private void Attendance2_Load(object sender, EventArgs e)
        {
            //设置日期格式
            dateTimePicker1.CustomFormat = "yyyy年MM月";   //修改只显示年月
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.ShowUpDown = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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
                    int id = int.Parse(Program.mainUser);
                    String sql = "select staff.sid,staff.name,attendance.oday,attendance.aday,convert(varchar(7),attendance.time,120)from attendance,staff where staff.sid=attendance.sid and staff.sid="+id+ "and convert(varchar(7),attendance.time,120)='" + date +"'"; // 查询语句staff表除了部门编号的全部内容，department的名称
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
    }
}
