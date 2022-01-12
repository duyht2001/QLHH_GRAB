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
    public partial class frmThongTinMH : Form
    {
        public static string ConnentionString = @"Data Source=DUYHT;Initial Catalog=QLHH_GRAB;Integrated Security=True";
        public static string State = "-1";
        public frmThongTinMH()
        {
            InitializeComponent();
            SetControl("Reset");
            GetData();
            CbbMaLNV();
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

            string query = "SELECT* FROM MatHang";

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
        public void CbbMaLNV()
        {
            SqlConnection conn = new SqlConnection(ConnentionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "SELECT MALOAIHANG FROM LOAIHANG";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader pb = cmd.ExecuteReader();

            while (pb.Read())
            {
                cbbMaLMH.Items.Add(pb[0].ToString());
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtgMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dtgMain.Rows[index];
            txtMaMH.Text = selectedRow.Cells["MAMATHANG"].Value.ToString();
            cbbMaLMH.Text = selectedRow.Cells["MALOAIHANG"].Value.ToString();
            txtTenMH.Text = selectedRow.Cells["TENHANG"].Value.ToString();
            txtCty.Text = selectedRow.Cells["MACONGTY"].Value.ToString();
            txtGiaban.Text = selectedRow.Cells["GIAHANG"].Value.ToString();
            txtSoluong.Text = selectedRow.Cells["SOLUONG"].Value.ToString();
            txtDonvitinh.Text = selectedRow.Cells["DONVITINH"].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnGhi.Enabled = true;

            txtMaMH.Text = "";
            cbbMaLMH.Text = "";
            txtTenMH.Text = "";
            txtGiaban.Text = "";
            txtSoluong.Text = "";
            txtDonvitinh.Text = "";
            txtCty.Text = "";

            txtMaMH.Focus();
            State = "Insert";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnGhi.Enabled = true;

            txtMaMH.Text = "";
            cbbMaLMH.Text = "";
            txtTenMH.Text = "";
            txtGiaban.Text = "";
            txtSoluong.Text = "";
            txtDonvitinh.Text = "";
            txtCty.Text = "";

            State = "Update";
            txtMaMH.Focus();
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
                    string query = "DELETE FROM MATHANG where MAHANG = '" + txtMaMH.Text.Trim() + "'";
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
                if (txtMaMH.Text == "") { MessageBox.Show("Chưa nhập thông tin mã mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtMaMH.Focus(); return; }
                if (cbbMaLMH.Text == "") { MessageBox.Show("Chưa nhập thông tin mã loại mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); cbbMaLMH.Focus(); return; }
                if (txtTenMH.Text == "") { MessageBox.Show("Chưa nhập thông tin tên mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtTenMH.Focus(); return; }
                if (txtGiaban.Text == "") { MessageBox.Show("Chưa nhập thông tin giá bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtGiaban.Focus(); return; }
                if (txtSoluong.Text == "") { MessageBox.Show("Chưa nhập thông tin số lượng hàng tồn kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtSoluong.Focus(); return; }
                if (txtDonvitinh.Text == "") { MessageBox.Show("Chưa nhập thông tin số lượng hàng tồn kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDonvitinh.Focus(); return; }
                if (txtCty.Text == "") { MessageBox.Show("Chưa nhập thông tin số lượng hàng tồn kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCty.Focus(); return; }

                if (State == "Insert")
                {
                    //Thuc hien ghi du lieu
                    SqlConnection conn = new SqlConnection(ConnentionString);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    string query = "INSERT INTO MATHANG(MAHANG,TENHANG, MACONGTY, MALOAIHANG, SOLUONG, DONVITINH, GIAHANG) VALUES " +
                        "('" + txtMaMH.Text.Trim() + "'," +
                        "'" + cbbMaLMH.Text.Trim() + "'," +
                        "N'" + txtTenMH.Text.Trim() + "'," +
                        "'" + txtGiaban.Text.Trim() + "'," +
                        "'" + txtCty.Text.Trim() + "'," +
                        "'" + txtDonvitinh.Text.Trim() + "'," +
                        "'" + txtSoluong.Text.Trim() + "')";

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

                    string query = "UPDATE MATHANG SET MAHANG = '" + txtMaMH.Text.Trim() + "', " +
                        "MALOAIHANG = '" + cbbMaLMH.Text.Trim() + "'," +
                        "TENHANG = N'" + txtTenMH.Text.Trim() + "'," +
                        "GIABAN = '" + txtGiaban.Text.Trim() + "'," +
                        "SOLUONG = N'" + txtSoluong.Text.Trim() + "'" +
                        "DONVITINH = N'" + txtDonvitinh.Text.Trim() + "'" +
                        "MACONGTY = N'" + txtCty.Text.Trim() + "'" +
                        " WHERE MaMH = '" + txtMaMH.Text.Trim() + "'";

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
