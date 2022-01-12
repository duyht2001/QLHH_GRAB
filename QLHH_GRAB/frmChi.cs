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
    public partial class frmChi : Form
    {
        public static string ConnentionString = @"Data Source=LAPTOP-G4I1QCAJ\SQLEXPRESS;Initial Catalog=QLCF;Integrated Security=True";
        public static string Quyen = "-1";
        public static string State = "-1";
        public frmChi()
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

            string query = "SELECT distinct  month(NgayNhap) from NhapHang";

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

            string query = "SELECT distinct year(NgayNhap) FROM NhapHang";

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

            string query = "select sum(SoLuong) as 'Số lượng mặt mặt hàng mua',sum(SoLuong*DonGia) as 'Tổng chi' from NhapHang where month(NgayNhap) = '" + cbbThang.Text.Trim() + "' and year(NgayNhap) = '" + cbbNam.Text.Trim() + "'";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dtgMain.DataSource = ds.Tables[0];
            }
        }
    }
}
