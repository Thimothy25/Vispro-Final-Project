using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FinalPeoject
{
    public partial class BHistory : Form
    {
        private MySqlConnection koneksi;
        private string alamat;

        public BHistory()
        {
            alamat = "server=localhost; database=db_playpad; username=root; password=;";
            koneksi = new MySqlConnection(alamat);

            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home form = new Home();
            form.Show();
            this.Hide();
        }

        private void BHistory_Load(object sender, EventArgs e)
        {
            LoadBookingData();
        }

        private void FillDataTable(string tableName, DataTable combinedTable)
        {
            string query = $"SELECT * FROM {tableName}";
            using (MySqlCommand perintah = new MySqlCommand(query, koneksi))
            {
                koneksi.Open();
                using (MySqlDataReader reader = perintah.ExecuteReader())
                {
                    // Jika tabel gabungan belum memiliki kolom, tambahkan kolom dari tabel sementara
                    if (combinedTable.Columns.Count == 0)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            combinedTable.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                        }
                    }

                    while (reader.Read())
                    {
                        DataRow row = combinedTable.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (!reader.IsDBNull(i))
                            {
                                // Cek apakah kolom adalah tanggal
                                if (reader.GetFieldType(i) == typeof(DateTime))
                                {
                                    row[i] = reader.GetDateTime(i);
                                }
                                else
                                {
                                    row[i] = reader.GetValue(i);
                                }
                            }
                            else
                            {
                                row[i] = DBNull.Value; // Menyimpan nilai NULL sebagai DBNull
                            }
                        }
                        combinedTable.Rows.Add(row);
                    }
                }
                koneksi.Close(); // Pastikan koneksi ditutup di sini
            }
        }

        private void LoadBookingData()
        {
            try
            {
                // DataTable untuk setiap lapangan
                DataTable lapangan1Table = new DataTable();
                lapangan1Table.TableName = "Lapangan1Bookings";
                FillDataTable("lapangan1", lapangan1Table);
                dataGridView1.DataSource = lapangan1Table;

                DataTable lapangan2Table = new DataTable();
                lapangan2Table.TableName = "Lapangan2Bookings";
                FillDataTable("lapangan2", lapangan2Table);
                dataGridView2.DataSource = lapangan2Table;

                DataTable lapangan3Table = new DataTable();
                lapangan3Table.TableName = "Lapangan3Bookings";
                FillDataTable("lapangan3", lapangan3Table);
                dataGridView3.DataSource = lapangan3Table;

                // Mengatur lebar dan header kolom
                SetDataGridViewColumns(dataGridView1);
                SetDataGridViewColumns(dataGridView2);
                SetDataGridViewColumns(dataGridView3);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void SetDataGridViewColumns(DataGridView dataGridView)
        {
            dataGridView.Columns[0].Width = 100;
            dataGridView.Columns[0].HeaderText = "ID Booking";
            dataGridView.Columns[1].Width = 150;
            dataGridView.Columns[1].HeaderText = "Nama";
            dataGridView.Columns[2].Width = 120;
            dataGridView.Columns[2].HeaderText = "No Telp";
            dataGridView.Columns[3].Width = 120;
            dataGridView.Columns[3].HeaderText = "Tanggal";
            dataGridView.Columns[4].Width = 120;
            dataGridView.Columns[4].HeaderText = "Mulai";
            dataGridView.Columns[5].Width = 120;
            dataGridView.Columns[5].HeaderText = "Selesai";
            dataGridView.Columns[6].Width = 120;
            dataGridView.Columns[6].HeaderText = "Status";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home form = new Home();
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormBookingCR formBookingCR = new FormBookingCR();
            formBookingCR.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Logika untuk klik pada picture box (jika ada)
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Logika untuk klik pada konten sel (jika ada)
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
