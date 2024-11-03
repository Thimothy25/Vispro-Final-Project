using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalPeoject
{
    public partial class Lap2Mng : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;
        private DataSet ds = new DataSet();
        private string alamat, query;
        private object txtID;
        public Lap2Mng()
        {
            alamat = "server=localhost; database=db_playpad; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
        }

        private void Lap2Mng_Load(object sender, EventArgs e)
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
                dataGridView1.Columns[0].Width = 20;
                dataGridView1.Columns[0].HeaderText = "ID Booking";
                dataGridView1.Columns[1].Width = 50;
                dataGridView1.Columns[1].HeaderText = "Nama";
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[2].HeaderText = "No Telpon";
                dataGridView1.Columns[3].Width = 70;
                dataGridView1.Columns[3].HeaderText = "Tanggal";
                dataGridView1.Columns[4].Width = 50;
                dataGridView1.Columns[4].HeaderText = "Mulai";
                dataGridView1.Columns[5].Width = 50;
                dataGridView1.Columns[5].HeaderText = "Selesai";
                dataGridView1.Columns[6].Width = 70;
                dataGridView1.Columns[6].HeaderText = "Status";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Periksa apakah txtNama atau txttlp berisi data
                if (!string.IsNullOrEmpty(txtnama.Text) || !string.IsNullOrEmpty(txttlp.Text))
                {
                    // Query untuk mencari berdasarkan nama atau nomor telepon menggunakan parameter
                    query = "SELECT * FROM lapangan2 WHERE nama = @nama OR no_tlp = @no_tlp";

                    ds.Clear();
                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    perintah.Parameters.AddWithValue("@nama", txtnama.Text);
                    perintah.Parameters.AddWithValue("@no_tlp", txttlp.Text);

                    adapter = new MySqlDataAdapter(perintah);
                    adapter.Fill(ds); // Isi dataset tanpa perlu ExecuteNonQuery()

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        // Mengisi DataGridView dan TextBox
                        dataGridView1.DataSource = ds.Tables[0];

                        // Mengambil baris pertama hasil pencarian
                        DataRow kolom = ds.Tables[0].Rows[0];
                        txtnama.Text = kolom["nama"].ToString();
                        txttlp.Text = kolom["no_tlp"].ToString();
                        txtstatus.Text = kolom["status"].ToString();

                        txtnama.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Data Tidak Ada!");
                        Lap2Mng_Load(null, null); // Refresh data jika tidak ditemukan
                    }
                }
                else
                {
                    MessageBox.Show("Data Yang Anda Pilih Tidak Ada!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                {
                    koneksi.Close(); // Pastikan koneksi ditutup
                }
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtnama.Text) && !string.IsNullOrEmpty(txttlp.Text) && !string.IsNullOrEmpty(txtstatus.Text))
                {

                    query = string.Format("UPDATE lapangan2 SET nama = '{0}', no_tlp = '{1}', status = '{2}' WHERE nama = '{3}'",
                                          txtnama.Text, txttlp.Text, txtstatus.Text, txtnama.Text);

                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    int res = perintah.ExecuteNonQuery();
                    koneksi.Close();

                    if (res == 1)
                    {
                        MessageBox.Show("Update Data Sukses...");
                        Lap2Mng_Load(null, null); // Refresh data setelah update
                    }
                    else
                    {
                        MessageBox.Show("Data tidak ditemukan atau gagal di-update.");
                    }
                }
                else
                {
                    MessageBox.Show("Data tidak lengkap! Pastikan Nama, No. Telepon, dan Status diisi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtnama.Text != "")
                {
                    if (MessageBox.Show("Anda Yakin Menghapus Data Ini ??", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        query = string.Format("Delete from tbl_Lapangan2 where id_pengguna = '{0}'", txtnama.Text);
                        ds.Clear();
                        koneksi.Open();
                        perintah = new MySqlCommand(query, koneksi);
                        adapter = new MySqlDataAdapter(perintah);
                        int res = perintah.ExecuteNonQuery();
                        koneksi.Close();
                        if (res == 1)
                        {
                            MessageBox.Show("Delete Data Suksess ...");
                        }
                        else
                        {
                            MessageBox.Show("Gagal Delete data");
                        }
                    }
                    Lap2Mng_Load(null, null);
                    txtnama.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Data Yang Anda Pilih Tidak Ada !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BForm frmMain = new BForm();
            frmMain.Show();
            this.Hide();
        }

        private void txtnama_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
