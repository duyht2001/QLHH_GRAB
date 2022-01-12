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
    public partial class frmChiTietHD : Form
    {
        public static string ConnentionString = @"Data Source=DUYHT;Initial Catalog=QLHH_GRAB;Integrated Security=True";
       
        public static string State = "-1";
        public frmChiTietHD()
        {
            InitializeComponent();
            SetControl("Reset");
            GetData();
            CbbSoHD();
            CbbMaMH();
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

            string query = "SELECT* FROM CHITIETDATHANG";

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
            cmmSoHD.Text = selectedRow.Cells["SOHOADON"].Value.ToString();
            cbbMaMH.Text = selectedRow.Cells["MAHANG"].Value.ToString();
            txtSoLuong.Text = selectedRow.Cells["SOLUONG"].Value.ToString();
            txtGiaban.Text = selectedRow.Cells["GIABAN"].Value.ToString();
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
                cmmSoHD.Items.Add(pb[0].ToString());
            }
        }
        public void CbbMaMH()
        {
            SqlConnection conn = new SqlConnection(ConnentionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "SELECT MAHANG FROM MATHANG";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader pb = cmd.ExecuteReader();

            while (pb.Read())
            {
                cbbMaMH.Items.Add(pb[0].ToString());
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnGhi.Enabled = true;

            cmmSoHD.Text = "";
            cbbMaMH.Text = "";
            txtSoLuong.Text = "";
            txtGiaban.Text = "";

            cmmSoHD.Focus();
            State = "Insert";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnGhi.Enabled = true;

            cmmSoHD.Text = "";
            cbbMaMH.Text = "";
            txtSoLuong.Text = "";
            txtGiaban.Text = "";

            State = "Update";
            cmmSoHD.Focus();
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
                    string query = "DELETE FROM CHITIETDATHANG where SOHOADON = '" + cmmSoHD.Text.Trim() + "'";
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
                if (cmmSoHD.Text == "") { MessageBox.Show("Chưa nhập thông tin số hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); cmmSoHD.Focus(); return; }
                if (cbbMaMH.Text == "") { MessageBox.Show("Chưa nhập thông tin mã mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); cbbMaMH.Focus(); return; }
                if (txtSoLuong.Text == "") { MessageBox.Show("Chưa nhập thông tin số lượng bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtSoLuong.Focus(); return; }
                if (txtGiaban.Text == "") { MessageBox.Show("Chưa nhập thông tin số lượng bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); txtGiaban.Focus(); return; }

                if (State == "Insert")
                {
                    //Thuc hien ghi du lieu
                    SqlConnection conn = new SqlConnection(ConnentionString);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    string query = "INSERT INTO CHITIETDATHANG(SOHOADON,MAHANG,SOLUONG,GIABAN) VALUES " +
                        "('" + cmmSoHD.Text.Trim() + "'," +
                        "'" + cbbMaMH.Text.Trim() + "'," +
                        "'" + txtGiaban.Text.Trim() + "'," +
                        "'" + txtSoLuong.Text.Trim() + "')";

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

                    string query = "UPDATE CHITIETDATHANG SET SOHOADON = '" + cmmSoHD.Text.Trim() + "', " +
                        "MAHANG = '" + cbbMaMH.Text.Trim() + "'," +
                        "GIABAN = '" + txtGiaban.Text.Trim() + "'," +
                        "SOLUONG = '" + txtSoLuong.Text.Trim() + "'" +
                        " WHERE SOHOADON = '" + cmmSoHD.Text.Trim() + "'";

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
