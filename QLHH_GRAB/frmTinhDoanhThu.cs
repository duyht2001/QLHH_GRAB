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
    public partial class frmTinhDoanhThu : Form
    {
        public static string ConnentionString = @"Data Source=LAPTOP-G4I1QCAJ\SQLEXPRESS;Initial Catalog=QLCF;Integrated Security=True";
        public static string Quyen = "-1";
        public static string State = "-1";
        public frmTinhDoanhThu()
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

            string query = "select month(nh.NgayNhap) as 'Tháng' from NhapHang nh join MatHang mh on mh.MaMH = nh.MaMH join ChiTietHoaDon ct on ct.MaMH = mh.MaMH join HoaDon hd on hd.SoHD = ct.SoHD group by month(nh.NgayNhap)";

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

            string query = "select year(nh.NgayNhap) as 'Năm' from NhapHang nh join MatHang mh on mh.MaMH = nh.MaMH join ChiTietHoaDon ct on ct.MaMH = mh.MaMH join HoaDon hd on hd.SoHD = ct.SoHD group by year(nh.NgayNhap)";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader pb = cmd.ExecuteReader();

            while (pb.Read())
            {
                cbbNam.Items.Add(pb[0].ToString());
            }
        }

        /*private void btnTinh_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConnentionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = 

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dtgMain.DataSource = ds.Tables[0];
            }
        }*/
    }
}
