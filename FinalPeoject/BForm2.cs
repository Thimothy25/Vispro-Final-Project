using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

namespace FinalPeoject
{
    public partial class BForm2 : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;
        private DataSet ds = new DataSet();
        private string alamat, query;
        public BForm2()
        {

            alamat = "server=localhost; database=db_playpad; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BCourt frmMain = new BCourt();
            frmMain.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtname.Text != "" && txttelp.Text != "" && txttanggal.Text != "" && CBmulai.Text != "" && CBselesai.Text != "")
                {
                    query = string.Format("INSERT INTO lapangan2 (nama, no_tlp, tanggal, jam_mulai, jam_selesai) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');",
                      txtname.Text, txttelp.Text, txttanggal.Text, CBmulai.Text, CBselesai.Text);

                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    int res = perintah.ExecuteNonQuery();
                    koneksi.Close();

                    if (res == 1)
                    {
                        MessageBox.Show("Insert Data Sukses ...");
                        BForm2_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Gagal Insert Data...");
                    }
                }
                else
                {
                    MessageBox.Show("Data Tidak Lengkap!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BForm2_Load(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                query = string.Format("select *  from lapangan2");
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                ds.Clear();
                adapter.Fill(ds);
                koneksi.Close();
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[0].HeaderText = "ID Booking";
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[1].HeaderText = "Nama";
                dataGridView1.Columns[2].Width = 120;
                dataGridView1.Columns[2].HeaderText = "no telp";
                dataGridView1.Columns[3].Width = 120;
                dataGridView1.Columns[3].HeaderText = "tanggal";
                dataGridView1.Columns[4].Width = 120;
                dataGridView1.Columns[4].HeaderText = "Mulai";
                dataGridView1.Columns[5].Width = 120;
                dataGridView1.Columns[5].HeaderText = "Selesai";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
