using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
using System.Collections;

namespace FinalPeoject
{
    public partial class Lap3Mng : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;
        private DataSet ds = new DataSet();
        private string alamat, query;
        public Lap3Mng()
        {
            alamat = "server=localhost; database=db_playpad; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
            txtIDB.Visible = false;
            for (int hour = 8; hour <= 21; hour++)
            {
                CBmulai.Items.Add($"{hour}:00");
                CBselesai.Items.Add($"{hour}:00");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CBmulai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtname.Text != "" && txttelp.Text != "" && txttanggal.Text != "" && CBmulai.Text != "" && CBselesai.Text != "")
                {
                    int jamMulai = int.Parse(CBmulai.Text.Split(':')[0]);
                    int jamSelesai = int.Parse(CBselesai.Text.Split(':')[0]);

                    // Hitung durasi dalam jam
                    int durasi = jamSelesai - jamMulai;

                    if (durasi > 0)
                    {
                        // Cek apakah ada booking dengan tanggal dan waktu yang sama
                        string checkBookingQuery = string.Format("SELECT COUNT(*) FROM lapangan3 WHERE tanggal = '{0}' " +
                            "AND ((jam_mulai <= '{1}' AND jam_selesai > '{1}') OR (jam_mulai < '{2}' AND jam_selesai >= '{2}') OR " +
                            "(jam_mulai >= '{1}' AND jam_selesai <= '{2}'))",
                            txttanggal.Text, CBmulai.Text, CBselesai.Text);

                        koneksi.Open();
                        perintah = new MySqlCommand(checkBookingQuery, koneksi);
                        int bookingCount = Convert.ToInt32(perintah.ExecuteScalar());
                        koneksi.Close();

                        if (bookingCount > 0)
                        {
                            // Jika ada booking dengan tanggal dan jam yang sama
                            MessageBox.Show("Jam sudah dibooking pada tanggal yang sama.");
                        }
                        else
                        {
                            // Hitung biaya
                            int biaya = durasi * 40000;

                            // Update booking di database
                            string updateQuery = string.Format("UPDATE lapangan3 SET nama = '{0}', tanggal = '{1}', jam_mulai = '{2}', jam_selesai = '{3}', biaya = {4} WHERE no_tlp = '{5}';",
                                                                txtname.Text, txttanggal.Text, CBmulai.Text, CBselesai.Text, biaya, txttelp.Text);

                            koneksi.Open();
                            perintah = new MySqlCommand(updateQuery, koneksi);
                            int res = perintah.ExecuteNonQuery();
                            koneksi.Close();

                            if (res == 1)
                            {
                                MessageBox.Show("Update Data Sukses ...");
                                Lap3Mng_Load(null, null);
                            }
                            else
                            {
                                MessageBox.Show("Gagal Update Data...");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Waktu selesai harus lebih besar dari waktu mulai.");
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

        private void Lap3Mng_Load(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                query = string.Format("select *  from lapangan3");
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                ds.Clear();
                adapter.Fill(ds);
                koneksi.Close();
                dataGridView2.DataSource = ds.Tables[0];
                dataGridView2.Columns[0].Width = 20;
                dataGridView2.Columns[0].HeaderText = "ID Booking";
                dataGridView2.Columns[1].Width = 50;
                dataGridView2.Columns[1].HeaderText = "Nama";
                dataGridView2.Columns[2].Width = 80;
                dataGridView2.Columns[2].HeaderText = "No Telpon";
                dataGridView2.Columns[3].Width = 70;
                dataGridView2.Columns[3].HeaderText = "Tanggal";
                dataGridView2.Columns[4].Width = 50;
                dataGridView2.Columns[4].HeaderText = "Mulai";
                dataGridView2.Columns[5].Width = 50;
                dataGridView2.Columns[5].HeaderText = "Selesai";
                dataGridView2.Columns[6].Width = 70;
                dataGridView2.Columns[6].HeaderText = "Status";
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
                if (!string.IsNullOrEmpty(txtIDB.Text))
                {
                    if (MessageBox.Show("Anda Yakin Menghapus Data Ini ??", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Gunakan parameter dalam query untuk keamanan dan menghindari SQL Injection
                        query = "DELETE FROM lapangan3 WHERE id_booking = @id_booking";

                        koneksi.Open();
                        perintah = new MySqlCommand(query, koneksi);
                        perintah.Parameters.AddWithValue("@id_booking", txtIDB.Text);

                        int res = perintah.ExecuteNonQuery();
                        koneksi.Close();

                        if (res == 1)
                        {
                            MessageBox.Show("Delete Data Suksess ...");
                            Lap3Mng_Load(null, null); // Refresh data
                        }
                        else
                        {
                            MessageBox.Show("Gagal Delete data, ID Booking tidak ditemukan.");
                        }
                    }
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
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                {
                    koneksi.Close(); // Pastikan koneksi ditutup
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 frmMain = new Form3();
            frmMain.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Periksa apakah txtNama atau txttlp berisi data
                if (!string.IsNullOrEmpty(txtname.Text) || !string.IsNullOrEmpty(txttelp.Text))
                {
                    // Query untuk mencari berdasarkan nama atau nomor telepon menggunakan parameter
                    query = "SELECT * FROM lapangan3 WHERE nama = @nama OR no_tlp = @no_tlp";

                    ds.Clear();
                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    perintah.Parameters.AddWithValue("@nama", txtname.Text);
                    perintah.Parameters.AddWithValue("@no_tlp", txttelp.Text);

                    adapter = new MySqlDataAdapter(perintah);
                    adapter.Fill(ds); // Isi dataset tanpa perlu ExecuteNonQuery()

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        // Mengisi DataGridView dan TextBox
                        dataGridView2.DataSource = ds.Tables[0];

                        // Mengambil baris pertama hasil pencarian
                        DataRow kolom = ds.Tables[0].Rows[0];
                        txtname.Text = kolom["nama"].ToString();
                        txttelp.Text = kolom["no_tlp"].ToString();
                        txtstatus.Text = kolom["status"].ToString();

                        txtname.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Data Tidak Ada!");
                        Lap3Mng_Load(null, null);
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
    }
}
