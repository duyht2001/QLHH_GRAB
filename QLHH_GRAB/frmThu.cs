using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLHH_GRAB
{
    public partial class frmThu : Form
    {
        public static string ConnentionString = @"Data Source=LAPTOP-G4I1QCAJ\SQLEXPRESS;Initial Catalog=QLCF;Integrated Security=True";
        public static string Quyen = "-1";
        public static string State = "-1";
        public frmThu()
        {
            InitializeComponent();
            CbbThang();
            CbbNam();
        }
        public void CbbThang()
        {
            SqlConnection conn = new SqlConnection(ConnentionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "SELECT distinct  month(NgayLapHD) from HoaDon";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader pb = cmd.ExecuteReader();

            while (pb.Read())
            {
                cbbThang.Items.Add(pb[0].ToString());
            }
        }
        public void CbbNam()
        {
            SqlConnection conn = new SqlConnection(ConnentionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "SELECT distinct year(NgayLapHD) FROM HoaDon";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader pb = cmd.ExecuteReader();

            while (pb.Read())
            {
                cbbNam.Items.Add(pb[0].ToString());
            }
        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            if (cbbNam.Text == "") { MessageBox.Show("Chưa nhập năm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); cbbNam.Focus(); return; }

            SqlConnection conn = new SqlConnection(ConnentionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "select sum(SoLuong) as 'Số lượng mặt hàng bán được',sum(SoLuong*GiaBan) as 'Tổng doanh thu' from ChiTietHoaDon ct join MatHang mh on ct.MaMH = mh.MaMH join HoaDon hd on ct.SoHD = hd.SoHD where month(hd.NgayLapHD) = '" + cbbThang.Text.Trim() + "' and year(hd.NgayLapHD) = '" + cbbNam.Text.Trim() + "'";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dtgmain.DataSource = ds.Tables[0];
            }
        }
    }
}
