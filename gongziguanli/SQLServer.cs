using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SQLCONN
{
    public class SQLServer
    {
        public static String connsql = "Server=localhost;Database=salary;Trusted_Connection=True;";// 数据库连接字符串,database设置为自己的数据库名
        public static void AutoSizeColumn(DataGridView dgViewFiles)
        {
            int width = 0;
            //使列自使用宽度
            //对于DataGridView的每一个列都调整
            for (int i = 0; i < dgViewFiles.Columns.Count; i++)
            {
                //将每一列都调整为自动适应模式
                dgViewFiles.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                //记录整个DataGridView的宽度
                width += dgViewFiles.Columns[i].Width;
            }
            //判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，
            //则将DataGridView的列自动调整模式设置为显示的列即可，
            //如果是小于原来设定的宽度，将模式改为填充。
            if (width > dgViewFiles.Size.Width)
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            //冻结某列 从左开始 0，1，2
            dgViewFiles.Columns[1].Frozen = true;
        }

        //comboBox2存储类
        public class ComboxItem
        {
            private string text;
            private string values;

            public string Text
            {
                get { return this.text; }
                set { this.text = value; }
            }

            public string Values
            {
                get { return this.values; }
                set { this.values = value; }
            }

            public ComboxItem(string _Text, string _Values)
            {
                Text = _Text;
                Values = _Values;
            }


            public override string ToString()
            {
                return Text;
            }
        }
    }
}
