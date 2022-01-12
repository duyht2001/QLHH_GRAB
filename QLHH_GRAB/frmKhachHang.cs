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
    public partial class frmKhachHang : Form
    {
        public static string ConnentionString = @"Data Source=DUYHT;Initial Catalog=QLHH_GRAB;Integrated Security=True";
       
        public static string State = "-1";
        public frmKhachHang()
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

            string query = "SELECT* FROM KHACHHANG";

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
            txtMaKH.Text = selectedRow.Cells["MAKHACHHANG"].Value.ToString();
            txtTenKH.Text = selectedRow.Cells["TENKH"].Value.ToString();
            txtTenGD.Text = selectedRow.Cells["TENGIAODICH"].Value.ToString();
            txtDiachi.Text = selectedRow.Cells["DIACHI"].Value.ToString();
            txtEmail.Text = selectedRow.Cells["EMAIL"].Value.ToString();
            txtSdt.Text = selectedRow.Cells["DIENTHOAI"].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnGhi.Enabled = true;

            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtTenGD.Text = "";
            txtDiachi.Text = "";
            txtEmail.Text = "";
            txtSdt.Text = "";

            txtMaKH.Focus();
            State = "Insert";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnGhi.Enabled = true;

            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtTenGD.Text = "";
            txtDiachi.Text = "";
            txtEmail.Text = "";
            txtSdt.Text = "";

            State = "Update";
            txtMaKH.Focus();
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
                    string query = "DELETE FROM KHACHANG where MAKHACHHANG = '" + txtMaKH.Text.Trim() + "'";
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetControl("Reset");
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaKH.Text == "") { MessageBox.Show("Chưa nhập thông tin mã bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtMaKH.Focus(); return; }
                if (txtTenKH.Text == "") { MessageBox.Show("Chưa nhập thông tin số bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtTenKH.Focus(); return; }
                if (txtTenGD.Text == "") { MessageBox.Show("Chưa nhập thông tin mã bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtTenGD.Focus(); return; }
                if (txtDiachi.Text == "") { MessageBox.Show("Chưa nhập thông tin số bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDiachi.Focus(); return; }
                if (txtEmail.Text == "") { MessageBox.Show("Chưa nhập thông tin mã bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtEmail.Focus(); return; }
                if (txtSdt.Text == "") { MessageBox.Show("Chưa nhập thông tin số bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtSdt.Focus(); return; }
                if (State == "Insert")
                {
                    //Thuc hien ghi du lieu
                    SqlConnection conn = new SqlConnection(ConnentionString);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    string query = "INSERT INTO KHACHHANG(MAKHACHHANG,TENKH, TENGIAODICH, DIACHI, EMAIL, DIENTHOAI) VALUES " +
                        "('" + txtMaKH.Text.Trim() + "'," +
                        "'" + txtTenKH.Text.Trim() + "'," +
                         "('" + txtTenGD.Text.Trim() + "'," +
                        "'" + txtDiachi.Text.Trim() + "'," +
                         "('" + txtEmail.Text.Trim() + "'," +
                        "'" + txtSdt.Text.Trim() + "')";

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

                    string query = "UPDATE KHACHHANG SET MAKHACHHANG = '" + txtMaKH.Text.Trim() + "', " +
                        "TENKH = '" + txtTenKH.Text.Trim() + "'," +
                        "TENGIAODICH = '" + txtTenGD.Text.Trim() + "'," +
                        "DIACHI = '" + txtDiachi.Text.Trim() + "'," +
                        "EMAIL = '" + txtEmail.Text.Trim() + "'," +
                        "DIENTHOAI = '" + txtSdt.Text.Trim() + "'," +
                        " WHERE MAKHACHHANG = '" + txtMaKH.Text.Trim() + "'";

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

        private void lblError_Click(object sender, EventArgs e)
        {

        }

        private void frmBan_Load(object sender, EventArgs e)
        {

        }

        private void txtMaBan_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtSoBan_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblTongSo_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
