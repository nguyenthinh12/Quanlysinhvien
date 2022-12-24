using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class txt : Form
    {
        string flag;
        DataTable dtSV;
        int index;
        public txt()
        {
            InitializeComponent();
        }
        public DataTable createTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaSV");
            dt.Columns.Add("TenSV");
            dt.Columns.Add("Diem");
            dt.Columns.Add("Lop");
            return dt;
        }
        // Khoá 
        public void LockControl()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;

            txtMaSV.ReadOnly = true;
            txtTenSV.ReadOnly = true;
            txtDiem.ReadOnly = true;
            txtLop.ReadOnly = true;

            btnThem.Focus();
        }
        // Mở
        public void UnlockControl()
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

            txtMaSV.ReadOnly = false;
            txtTenSV.ReadOnly = false;
            txtDiem.ReadOnly = false;
            txtLop.ReadOnly = false;

            txtMaSV.Focus();
        }
        //MAIN
        private void txt_Load(object sender, EventArgs e)
        {
            LockControl();
            dtSV = createTable();

        }
        // thêm sinh viên
        private void btnThem_Click(object sender, EventArgs e)
        {
            UnlockControl();
            flag = "add";

            txtMaSV.Text = "";
            txtTenSV.Text = "";
            txtLop.Text = "";
            txtDiem.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            UnlockControl();
            flag = "edit";
        }
        // lưu sinh viên
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(flag == "add")
            {
                if(checkData())
                {
                    dtSV.Rows.Add(txtMaSV.Text, txtTenSV.Text, txtDiem.Text, txtLop.Text);
                    dataGridSinhVien.DataSource = dtSV;
                    dataGridSinhVien.RefreshEdit();
                }
                else if (flag == "edit")
                {
                    if(checkData())
                    {
                        dtSV.Rows[index][0] = txtMaSV.Text;
                        dtSV.Rows[index][1] = txtTenSV.Text;
                        dtSV.Rows[index][2] = txtDiem.Text;
                        dtSV.Rows[index][3] = txtLop.Text;
                        dataGridSinhVien.DataSource = dtSV;
                        dataGridSinhVien.RefreshEdit();
                    }
                }
                LockControl();
            }
        }
        public bool checkData()
        {
            if(string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSV.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenSV.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSV.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDiem.Text))
            {
                MessageBox.Show("Bạn chưa nhập điểm sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiem.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtLop.Text))
            {
                MessageBox.Show("Bạn chưa nhập lớp sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLop.Focus();
                return false;
            }
            return true;
        }

        private void dataGridSinhVien_SelectionChanged(object sender, EventArgs e)
        {
             index = dataGridSinhVien.CurrentCell == null ? -1 : dataGridSinhVien.CurrentCell.RowIndex;
           
            if(index != -1) 
            {
                txtMaSV.Text = dataGridSinhVien.Rows[index].Cells[0].Value.ToString();
                txtTenSV.Text = dataGridSinhVien.Rows[index].Cells[1].Value.ToString();
                txtDiem.Text = dataGridSinhVien.Rows[index].Cells[2].Value.ToString();
                txtLop.Text = dataGridSinhVien.Rows[index].Cells[3].Value.ToString();
            }



        }
        // xoá sinh viên
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xoá sinh viên này? ", " Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                dtSV.Rows.RemoveAt(index);
                dataGridSinhVien.DataSource = dtSV;
                dataGridSinhVien.RefreshEdit();
            }
        }
    }
}








