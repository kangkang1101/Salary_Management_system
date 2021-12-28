using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Data.SqlClient;
using SQLCONN;

namespace gongziguanli
{
    public partial class add : Form
    {
        public add()
        {
            InitializeComponent();
        }

        private void add_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(SQLServer.connsql))
                {
                    conn.Open(); // 打开数据库连接
                    String sql = "select sex,count(sid) from staff where jointime between convert(datetime,'2021-01-01',120) and convert(datetime,'2021-12-31',120) group by sex";
                    SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                    DataTable dt = new DataTable(); // 实例化数据表
                    myda.Fill(dt); // 保存数据 
                    textBox1.Text = (((float.Parse(dt.Rows[0][1].ToString()) / float.Parse(dt.Rows[1][1].ToString())))*100).ToString() + "%";


                    dataGridView1.DataSource = dt; // 设置到DataGridView中
                    SQLServer.AutoSizeColumn(dataGridView1);  //宽度自适应
                    this.dataGridView1.Columns[0].HeaderText = "性别";   //设置列名
                    this.dataGridView1.Columns[1].HeaderText = "人数";
                    dataGridView1.AllowUserToAddRows = false;  //关闭（最后一行空白行）
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
