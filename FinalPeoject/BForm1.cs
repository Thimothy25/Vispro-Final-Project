using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FinalPeoject
{
    public partial class BForm : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;
        private DataSet ds = new DataSet();
        private string alamat, query;

        public BForm()
        {
            alamat = "server=localhost; database=db_playpad; username=root; password=;";
            koneksi = new MySqlConnection(alamat);


            InitializeComponent();
            for (int hour = 8; hour <= 21; hour++)
            {
                CBmulai.Items.Add($"{hour}:00");
                CBselesai.Items.Add($"{hour}:00");
            }
        }


        private void BForm_Load(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                query = string.Format("select *  from lapangan1");
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            BCourt frmMain = new BCourt();
            frmMain.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void BForm_Load_1(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                query = string.Format("select *  from lapangan1");
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
                dataGridView1.Columns[3].Width = 90;
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lap1Manage  frmMain = new Lap1Manage();
            frmMain.Show();
            this.Hide();
        }

        private void CBmulai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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
                        // Cek apakah sudah ada booking dengan nomor telepon yang sama
                        string checkPhoneQuery = string.Format("SELECT COUNT(*) FROM lapangan1 WHERE no_tlp = '{0}'", txttelp.Text);

                        koneksi.Open();
                        perintah = new MySqlCommand(checkPhoneQuery, koneksi);
                        int phoneCount = Convert.ToInt32(perintah.ExecuteScalar());
                        koneksi.Close();

                        if (phoneCount > 0)
                        {
                            // Jika nomor telepon sudah terdaftar
                            MessageBox.Show("Nomor telepon sudah terdaftar. Silakan gunakan nomor lain.");
                        }
                        else
                        {
                            // Cek apakah ada booking dengan tanggal dan waktu yang sama
                            string checkBookingQuery = string.Format("SELECT COUNT(*) FROM lapangan1 WHERE tanggal = '{0}' " +
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

                                // Insert booking baru ke database
                                string insertQuery = string.Format("INSERT INTO lapangan1 (nama, no_tlp, tanggal, jam_mulai, jam_selesai, biaya) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', {5});",
                                                                      txtname.Text, txttelp.Text, txttanggal.Text, CBmulai.Text, CBselesai.Text, biaya);

                                koneksi.Open();
                                perintah = new MySqlCommand(insertQuery, koneksi);
                                int res = perintah.ExecuteNonQuery();
                                koneksi.Close();

                                if (res == 1)
                                {
                                    MessageBox.Show("Insert Data Sukses ...");
                                    BForm_Load(null, null);
                                }
                                else
                                {
                                    MessageBox.Show("Gagal Insert Data...");
                                }
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
    }
}
