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
    public partial class frmLogin : Form
    {
        public static string ConnentionString = @"Data Source=DUYHT;Initial Catalog=QLHH_GRAB;Integrated Security=True";
  
        public frmLogin()
        {
            InitializeComponent();
            lblError.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text != null && txtTaiKhoan.Text.Trim() != "") { }
            else
            {
                MessageBox.Show("Chưa nhập thông tin tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTaiKhoan.Focus();
                return;
            }
            if (txtMatKhau.Text != null && txtMatKhau.Text.Trim() != "") { }
            else
            {
                MessageBox.Show("Chưa nhập thông tin mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMatKhau.Focus();
                return;
            }
            SqlConnection conn = new SqlConnection(ConnentionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string TaiKhoan = txtTaiKhoan.Text.Trim();
            string MatKhau = txtMatKhau.Text.Trim();
            string query = "SELECT* FROM TAIKHOAN WHERE TK = '" + TaiKhoan + "' AND MK = '" + MatKhau + "'";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                frmMain _frmMain = new frmMain();
                _frmMain.Show();
                this.Hide();
            }
            else
            {
                lblError.Text = "Tài khoản hoặc mật khẩu không chính xác!";
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (thoat == DialogResult.OK)
                Application.Exit();
        }
    }
}
