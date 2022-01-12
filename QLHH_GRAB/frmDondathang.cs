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
    public partial class frmDondathang : Form
    {
        public static string ConnentionString = @"Data Source=DUYHT;Initial Catalog=QLHH_GRAB;Integrated Security=True";
        public static string State = "-1";
        public frmDondathang()
        {
            InitializeComponent();
            SetControl("Reset");
            GetData();
            CbbMNV();
            CbbMKH();
        }
        #region Public Function
        public void SetControl(string State)
        {
            switch (State)
            {
                case "Reset":
                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    btnGhi.Enabled = false;
                    btnHuy.Enabled = false;

                    lblError.Text = "";
                    break;
                default:
                    break;
            }
        }
        public void GetData()
        {
            SqlConnection conn = new SqlConnection(ConnentionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "SELECT *  FROM DONDATHANG ";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dtgMain.DataSource = ds.Tables[0];
                lblTongSo.Text = "Số bản ghi: " + (dtgMain.Rows.Count - 1);
            }
            else
            {
                //Không có dữ liệu
                lblTongSo.Text = "Số bản ghi: 0";
            }
        }
        #endregion

        private void dtgMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dtgMain.Rows[index];
            txtHoadon.Text = selectedRow.Cells["SOHOADON"].Value.ToString();
            cbbMaKH.Text = selectedRow.Cells["MAKHACHHANG"].Value.ToString();
            cbbMaNV.Text = selectedRow.Cells["MANHANVIEN"].Value.ToString();
            dtNgayNhap.Text = selectedRow.Cells["NGAYDATHANG"].Value.ToString();
            txtNoigiao.Text = selectedRow.Cells["NOIGIAOHANG"].Value.ToString();
        }
        public void CbbMNV()
        {
            SqlConnection conn = new SqlConnection(ConnentionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "SELECT MANHANVIEN FROM NHANVIEN";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader pb = cmd.ExecuteReader();

            while (pb.Read())
            {
                cbbMaNV.Items.Add(pb[0].ToString());
            }
        }
        public void CbbMKH()
        {
            SqlConnection conn = new SqlConnection(ConnentionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "SELECT MAKHACHHANG FROM KHACHHANG";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader pb = cmd.ExecuteReader();

            while (pb.Read())
            {
                cbbMaKH.Items.Add(pb[0].ToString());
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnGhi.Enabled = true;

            txtHoadon.Text = "";
            cbbMaKH.Text = "";
            cbbMaNV.Text = "";
            dtNgayNhap.Text = "";
            txtNoigiao.Text = "";

            
            State = "Insert";
            cbbMaKH.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnGhi.Enabled = true;

            txtHoadon.Text = "";
            cbbMaKH.Text = "";
            cbbMaNV.Text = "";
            dtNgayNhap.Text = "";
            txtNoigiao.Text = "";

            State = "Update";
            cbbMaKH.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnentionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DialogResult xoa = MessageBox.Show("Bạn có chắc chắn muốn xóa dữ liệu này", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (xoa == DialogResult.Yes)
                {
                    string query = "DELETE FROM DONDATHANG where SOHOADON = '" + txtHoadon.Text.Trim() + "'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi xóa dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtHoadon.Text == "") { MessageBox.Show("Chưa nhập thông tin mã nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtHoadon.Focus(); return; }
                if (cbbMaKH.Text == "") { MessageBox.Show("Chưa nhập thông tin mã mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); cbbMaKH.Focus(); return; }
                if (dtNgayNhap.Text == "") { MessageBox.Show("Chưa nhập thông tin ngày nhập hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); dtNgayNhap.Focus(); return; }
                if (cbbMaNV.Text == "") { MessageBox.Show("Chưa nhập thông tin số lượng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); cbbMaNV.Focus(); return; }
                if (txtNoigiao.Text == "") { MessageBox.Show("Chưa nhập thông tin đơn giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtNoigiao.Focus(); return; }

                if (State == "Insert")
                {
                    //Thuc hien ghi du lieu
                    SqlConnection conn = new SqlConnection(ConnentionString);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    string query = "INSERT INTO NhapHang(MaNCC,MaMH,NgayNhap,SoLuong,DonGia) VALUES " +
                        "('" + txtHoadon.Text.Trim() + "'," +
                        "'" + cbbMaKH.Text.Trim() + "'," +
                        "'" + dtNgayNhap.Text.Trim() + "'," +
                        "'" + cbbMaNV.Text.Trim() + "'," +
                        "'" + txtNoigiao.Text.Trim() + "')";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Thêm dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi ghi dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if (State == "Update")
                {
                    //Thuc hien cap nhat du lieu
                    SqlConnection conn = new SqlConnection(ConnentionString);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    string query = "UPDATE DONDATHANG SET SOHOADON = '" + txtHoadon.Text.Trim() + "', " +
                        "MAKHACHHANG = '" + cbbMaKH.Text.Trim() + "'," +
                        "NGAYDATHANG = '" + dtNgayNhap.Text.Trim() + "'," +
                        "MANHANVIEN = '" + cbbMaNV.Text.Trim() + "'," +
                        "DonGia = N'" + txtNoigiao.Text.Trim() + "'" +
                        " WHERE SOHOADON = '" + txtHoadon.Text.Trim() + "'";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi cập nhật dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetControl("Reset");
        }
    }
}
