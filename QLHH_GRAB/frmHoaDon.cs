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
    public partial class frmHoaDon : Form
    {
        public static string ConnentionString = @"Data Source=LAPTOP-G4I1QCAJ\SQLEXPRESS;Initial Catalog=QLCF;Integrated Security=True";
        public static string Quyen = "-1";
        public static string State = "-1";
        public frmHoaDon()
        {
            InitializeComponent();
            GetData();
            CbbMaNV();
            CbbMaBan();
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

            string query = "SELECT * FROM HoaDon";

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
            txtMaHD.Text = selectedRow.Cells["SoHD"].Value.ToString();
            dtNgayLap.Text = selectedRow.Cells["NgayLapHD"].Value.ToString();
            cbbMaBan.Text = selectedRow.Cells["MaBan"].Value.ToString();
            cbbMaNV.Text = selectedRow.Cells["MaNV"].Value.ToString();
        }
        public void CbbMaBan()
        {
            SqlConnection conn = new SqlConnection(ConnentionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "SELECT MaBan FROM Ban";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader pb = cmd.ExecuteReader();

            while (pb.Read())
            {
                cbbMaBan.Items.Add(pb[0].ToString());
            }
        }
        public void CbbMaNV()
        {
            SqlConnection conn = new SqlConnection(ConnentionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "SELECT MaNV FROM NhanVien";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader pb = cmd.ExecuteReader();

            while (pb.Read())
            {
                cbbMaNV.Items.Add(pb[0].ToString());
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnGhi.Enabled = true;

            txtMaHD.Text = "";
            dtNgayLap.Text = "";
            cbbMaBan.Text = "";
            cbbMaNV.Text = "";

            txtMaHD.Focus();
            State = "Insert";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnGhi.Enabled = true;

            txtMaHD.Text = "";
            dtNgayLap.Text = "";
            cbbMaBan.Text = "";
            cbbMaNV.Text = "";

            State = "Update";
            txtMaHD.Focus();
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
                    string query = "DELETE FROM HoaDon where SoHD= '" + txtMaHD.Text.Trim() + "'";
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
                if (txtMaHD.Text == "") { MessageBox.Show("Chưa nhập thông tin mã hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtMaHD.Focus(); return; }
                if (dtNgayLap.Text == "") { MessageBox.Show("Chưa nhập thông tin ngày lập hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); dtNgayLap.Focus(); return; }
                if (cbbMaBan.Text == "") { MessageBox.Show("Chưa nhập thông tin mã bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); cbbMaBan.Focus(); return; }
                if (cbbMaNV.Text == "") { MessageBox.Show("Chưa nhập thông tin nhân viên lập hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); cbbMaNV.Focus(); return; }
                
                if (State == "Insert")
                {
                    //Thuc hien ghi du lieu
                    SqlConnection conn = new SqlConnection(ConnentionString);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    string query = "INSERT INTO HoaDon(SoHD,NgayLapHD,MaBan,MaNV) VALUES " +
                        "('" + txtMaHD.Text.Trim() + "'," +
                        "'" + dtNgayLap.Text.Trim() + "'," +
                        "'" + cbbMaBan.Text.Trim() + "'," +
                        "'" + cbbMaNV.Text.Trim() + "')";

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

                    string query = "UPDATE HoaDon SET SoHD = '" + txtMaHD.Text.Trim() + "', " +
                        "NgayLapHD = '" + dtNgayLap.Text.Trim() + "'," +
                        "MaBan = '" + cbbMaBan.Text.Trim() + "'," +
                        "MaNV = '" + cbbMaNV.Text.Trim() + "'" +
                        " WHERE SoHD = '" + txtMaHD.Text.Trim() + "'";

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
