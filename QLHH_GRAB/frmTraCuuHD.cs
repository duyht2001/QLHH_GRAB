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
    public partial class frmTraCuuHD : Form
    {
        public static string ConnentionString = @"Data Source=DUYHT;Initial Catalog=QLHH_GRAB;Integrated Security=True";
        public static string State = "-1";
        public frmTraCuuHD()
        {
            InitializeComponent();
            CbbSoHD();
        }
        public void CbbSoHD()
        {
            SqlConnection conn = new SqlConnection(ConnentionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "SELECT SOHOADON FROM DONDATHANG";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader pb = cmd.ExecuteReader();

            while (pb.Read())
            {
                comboBox1.Items.Add(pb[0].ToString());
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "") { MessageBox.Show("Chưa nhập số hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); comboBox1.Focus(); return; }      

            SqlConnection conn = new SqlConnection(ConnentionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "select hd.SoHD,hd.NgayLapHD as 'Ngày lập hóa đơn',nv.HoTenNV as 'Nhân viên lập hóa đơn',b.SoBan as 'Số bàn',COUNT(ct.SoHD) as 'Số mặt hàng',STRING_AGG(mh.TenMH,', ') as 'Tên mặt hàng',STRING_AGG(ct.SoLuong,', ') as 'Số lượng', sum(ct.SoLuong*mh.GiaBan) as 'Tổng tiền' from HoaDon hd join ChiTietHoaDon ct on ct.SoHD = hd.SoHD join MatHang mh on ct.MaMH = mh.MaMH join Ban b on b.MaBan = hd.MaBan join NhanVien nv on nv.MaNV = hd.MaNV where hd.SoHD = '" + comboBox1.Text.Trim() + "' group by hd.SoHD,b.SoBan,hd.NgayLapHD,nv.HoTenNV";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dtgMain.DataSource = ds.Tables[0]; 
            }
        }

        private void dtgMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
