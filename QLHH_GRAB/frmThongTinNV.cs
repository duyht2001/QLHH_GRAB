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
    public partial class frmThongTinNV : Form
    {
        public static string ConnentionString = @"Data Source=DUYHT;Initial Catalog=QLHH_GRAB;Integrated Security=True";

        public static string State = "-1";
        public frmThongTinNV()
        {
            InitializeComponent();
            SetControl("Reset");
            GetData();

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

            string query = "SELECT* FROM NhanVien";

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dtgMain.Rows[index];
            txtMaNV.Text = selectedRow.Cells["MANHANVIEN"].Value.ToString();
            txtHo.Text = selectedRow.Cells["HO"].Value.ToString();
            txtTenNV.Text = selectedRow.Cells["TEN"].Value.ToString();
            dtNgaySinh.Text = selectedRow.Cells["NGAYSINH"].Value.ToString();
            txtDiaChi.Text = selectedRow.Cells["DIACHI"].Value.ToString();
            txtSdt.Text = selectedRow.Cells["DEINTHOAI"].Value.ToString();
            txtLuong.Text = selectedRow.Cells["LUONG"].Value.ToString();
        }
       

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnGhi.Enabled = true;

            txtMaNV.Text = "";
            txtHo.Text = "";
            txtTenNV.Text = "";
            dtNgaySinh.Text = "";
           
            txtDiaChi.Text = "";
            txtSdt.Text = "";

            txtMaNV.Focus();
            State = "Insert";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnGhi.Enabled = true;

            txtMaNV.Text = "";
            txtHo.Text = "";
            txtTenNV.Text = "";
            dtNgaySinh.Text = "";
          
            txtDiaChi.Text = "";
            txtSdt.Text = "";

            State = "Update";
            txtMaNV.Focus();
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
                    string query = "DELETE FROM NHANVIEN where MANHANVIEN = '" + txtMaNV.Text.Trim() + "'";
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
                if (txtMaNV.Text == "") { MessageBox.Show("Chưa nhập thông tin mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtMaNV.Focus(); return; }
                if (txtHo.Text == "") { MessageBox.Show("Chưa nhập thông tin mã loại nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtHo.Focus(); return; }
                if (txtTenNV.Text == "") { MessageBox.Show("Chưa nhập thông tin tên nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtTenNV.Focus(); return; }
                if (dtNgaySinh.Text == "") { MessageBox.Show("Chưa nhập thông tin ngày sinh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); dtNgaySinh.Focus(); return; }
                
                if (txtDiaChi.Text == "") { MessageBox.Show("Chưa nhập thông tin địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDiaChi.Focus(); return; }
                if (txtSdt.Text == "") { MessageBox.Show("Chưa nhập thông tin số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtSdt.Focus(); return; }
                if (txtLuong.Text == "") { MessageBox.Show("Chưa nhập thông tin số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtLuong.Focus(); return; }
                if (State == "Insert")
                {
                    //Thuc hien ghi du lieu
                    SqlConnection conn = new SqlConnection(ConnentionString);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    string query = "INSERT INTO NHANVIEN(MANHANVIEN,HO,TEN,NGAYSINH,DIACHI,DIENTHOAI,LUONG) VALUES " +
                        "('" + txtMaNV.Text.Trim() + "'," +
                        "'" + txtHo.Text.Trim() + "'," +
                        "N'" + txtTenNV.Text.Trim() + "'," +
                        "'" + dtNgaySinh.Text.Trim() + "'," +
                        "N'" + txtDiaChi.Text.Trim() + "'," +
                        "'" + txtLuong.Text.Trim() + "'," +
                        "N'" + txtSdt.Text.Trim() + "')";

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

                    string query = "UPDATE NHANVIEN SET MANHANVIEN = '" + txtMaNV.Text.Trim() + "', " +
                        "HO = '" + txtHo.Text.Trim() + "'," +
                        "TEN = N'" + txtTenNV.Text.Trim() + "'," +
                        "NGAYSINH = '" + dtNgaySinh.Text.Trim() + "'," +
                        "DIACHI = N'" + txtDiaChi.Text.Trim() + "'," +
                        "DIENTHOAI = '" + txtSdt.Text.Trim() + "'," +
                        "LUONG = '" + txtLuong.Text.Trim() + "'" +
                        " WHERE MANHANVIEN = '" + txtMaNV.Text.Trim() + "'";

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

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSdt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
