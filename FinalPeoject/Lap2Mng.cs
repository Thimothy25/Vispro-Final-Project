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
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

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
            txtIDB.Visible = false;
            for (int hour = 8; hour <= 21; hour++)
            {
                CBmulai.Items.Add($"{hour}:00");
                CBselesai.Items.Add($"{hour}:00");
            }
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
                if (!string.IsNullOrEmpty(txtname.Text) || !string.IsNullOrEmpty(txttelp.Text))
                {
                    // Query untuk mencari berdasarkan nama atau nomor telepon menggunakan parameter
                    query = "SELECT * FROM lapangan2 WHERE nama = @nama OR no_tlp = @no_tlp";

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
                        dataGridView1.DataSource = ds.Tables[0];

                        // Mengambil baris pertama hasil pencarian
                        DataRow kolom = ds.Tables[0].Rows[0];
                        txtIDB.Text = kolom["id_booking"].ToString();  // Mengisi txtIDB dengan id_booking
                        txtname.Text = kolom["nama"].ToString();
                        txttelp.Text = kolom["no_tlp"].ToString();
                        txtstatus.Text = kolom["status"].ToString();

                        txtname.Enabled = true;
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
                if (txtname.Text != "" && txttelp.Text != "" && txttanggal.Text != "" && CBmulai.Text != "" && CBselesai.Text != "")
                {
                    int jamMulai = int.Parse(CBmulai.Text.Split(':')[0]);
                    int jamSelesai = int.Parse(CBselesai.Text.Split(':')[0]);

                    // Hitung durasi dalam jam
                    int durasi = jamSelesai - jamMulai;

                    if (durasi > 0)
                    {
                        // Cek apakah ada booking dengan tanggal dan waktu yang sama
                        string checkBookingQuery = string.Format("SELECT COUNT(*) FROM lapangan2 WHERE tanggal = '{0}' " +
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
                            string updateQuery = string.Format("UPDATE lapangan2 SET nama = '{0}', tanggal = '{1}', jam_mulai = '{2}', jam_selesai = '{3}', biaya = {4}, no_tlp = {5} WHERE id_booking = '{6}';",
                                                                txtname.Text, txttanggal.Text, CBmulai.Text, CBselesai.Text, biaya, txttelp.Text, txtIDB.Text);

                            koneksi.Open();
                            perintah = new MySqlCommand(updateQuery, koneksi);
                            int res = perintah.ExecuteNonQuery();
                            koneksi.Close();

                            if (res == 1)
                            {
                                MessageBox.Show("Update Data Sukses ...");
                                Lap2Mng_Load(null, null);
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtname.Text != "")
                {
                    if (MessageBox.Show("Anda Yakin Menghapus Data Ini ??", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        query = string.Format("Delete from lapangan2 where id_booking = '{0}'", txtIDB.Text);
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
                    txtname.Enabled = true;
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
            BForm2 frmMain = new BForm2();
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
