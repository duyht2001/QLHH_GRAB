using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLHH_GRAB
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
           
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult dx = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dx == DialogResult.Yes)
            {
                this.Close();
                frmLogin _frmLogin = new frmLogin();
                _frmLogin.Show();
            }
        }

        private void mnuThoatHT_Click(object sender, EventArgs e)
        {
            DialogResult thoat = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes)
                Application.Exit();
        }

        private void mnuBan_Click(object sender, EventArgs e)
        {
            frmKhachHang _frmKH = new frmKhachHang();
            _frmKH.MdiParent = this;
            _frmKH.Show();
        }

        private void mnuNhaCungCap_Click(object sender, EventArgs e)
        {
            frmNhaCungCap _frmNhaCungCap = new frmNhaCungCap();
            _frmNhaCungCap.MdiParent = this;
            _frmNhaCungCap.Show();
        }

        private void mnuLoaiNhanVien_Click(object sender, EventArgs e)
        {
            frmLoaiNV _frmLoaiNhanVien = new frmLoaiNV();
            _frmLoaiNhanVien.MdiParent = this;
            _frmLoaiNhanVien.Show();
        }

        private void mnuLoaiMH_Click(object sender, EventArgs e)
        {
            frmLoaiMH _frmLoaiMH = new frmLoaiMH();
            _frmLoaiMH.MdiParent = this;
            _frmLoaiMH.Show();
        }

        private void mnuThongTinNV_Click(object sender, EventArgs e)
        {
        }

        private void mnuThongTinMH_Click(object sender, EventArgs e)
        {
            frmThongTinMH _frmThongTinMH = new frmThongTinMH();
            _frmThongTinMH.MdiParent = this;
            _frmThongTinMH.Show();
        }

        private void mnuNhapHang_Click(object sender, EventArgs e)
        {
            frmDondathang _frmDondathang = new frmDondathang();
            _frmDondathang.MdiParent = this;
            _frmDondathang.Show();
        }

        private void mnuHoaDon_Click(object sender, EventArgs e)
        {
            frmHoaDon _frmHoaDon = new frmHoaDon();
            _frmHoaDon.MdiParent = this;
            _frmHoaDon.Show();
        }

        private void mnuChiTienHD_Click(object sender, EventArgs e)
        {
            frmHoaDon _frmHoaDon = new frmHoaDon();
            _frmHoaDon.MdiParent = this;
            _frmHoaDon.Show();
        }

        private void mnuChiTietHD_Click(object sender, EventArgs e)
        {
            frmChiTietHD _frmChiTietHD = new frmChiTietHD();
            _frmChiTietHD.MdiParent = this;
            _frmChiTietHD.Show();
        }

        private void mnuTinhDoanhThu_Click(object sender, EventArgs e)
        {
            frmTinhDoanhThu _frmTinhDoanhThu = new frmTinhDoanhThu();
            _frmTinhDoanhThu.MdiParent = this;
            _frmTinhDoanhThu.Show();
        }

        private void mnuTraCuu1_Click(object sender, EventArgs e)
        {
            frmTraCuuHD _frmTraCuuHD = new frmTraCuuHD();
            _frmTraCuuHD.MdiParent = this;
            _frmTraCuuHD.Show();
        }

        private void mnuTienThu_Click(object sender, EventArgs e)
        {
            frmThu _frmThu = new frmThu();
            _frmThu.MdiParent = this;
            _frmThu.Show();
        }

        private void tiềnChiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChi _frmChi = new frmChi();
            _frmChi.MdiParent = this;
            _frmChi.Show();
        }

        private void mnuHeThong_Click(object sender, EventArgs e)
        {

        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmThongTinNV _frmThongTinNV = new frmThongTinNV();
            _frmThongTinNV.MdiParent = this;
            _frmThongTinNV.Show();
        }
    }
}
