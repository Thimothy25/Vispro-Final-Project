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
        private string connectionString = "server=localhost;user id=root;password=;database=db_playpad";

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
            AdjustDataGridViewSize();
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
            string query = @"
                SELECT *, 'Lapangan 1' AS Sumber FROM lapangan1
                UNION ALL
                SELECT *, 'Lapangan 2' AS Sumber FROM lapangan2
                UNION ALL
                SELECT *, 'Lapangan 3' AS Sumber FROM lapangan3;
            ";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    // Mengisi data ke dalam DataTable
                    adapter.Fill(dataTable);

                    // Menampilkan data di DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void SetDataGridViewColumns(DataGridView dataGridView)
        {
            dataGridView.Columns[0].Width = 20;
            dataGridView.Columns[0].HeaderText = "ID Booking";
            dataGridView.Columns[1].Width = 50;
            dataGridView.Columns[1].HeaderText = "Nama";
            dataGridView.Columns[2].Width = 70;
            dataGridView.Columns[2].HeaderText = "No Telp";
            dataGridView.Columns[3].Width = 70;
            dataGridView.Columns[3].HeaderText = "Tanggal";
            dataGridView.Columns[4].Width = 30;
            dataGridView.Columns[4].HeaderText = "Mulai";
            dataGridView.Columns[5].Width = 30;
            dataGridView.Columns[5].HeaderText = "Selesai";
            dataGridView.Columns[6].Width = 50;
            dataGridView.Columns[6].HeaderText = "Status";
            dataGridView.Columns[7].Width = 60;
            dataGridView.Columns[7].HeaderText = "Biaya";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home form = new Home();
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FORMHISTORY formBookingCR = new FORMHISTORY();
            formBookingCR.Show();
        }
      

        private void AdjustDataGridViewSize()
        {
            // Mengatur DataGridView untuk memenuhi lebar yang tersedia tanpa scroll horizontal
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Menyesuaikan tinggi setiap baris secara otomatis
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Mengatur ukuran kontrol DataGridView
            dataGridView1.Width = 627;
            dataGridView1.Height = 483;

            // Mengatur lebar minimum kolom tertentu (opsional)
            dataGridView1.Columns[0].MinimumWidth = 50; // ID Booking
            dataGridView1.Columns[1].MinimumWidth = 100; // Nama
            dataGridView1.Columns[2].MinimumWidth = 100; // No Telp
            dataGridView1.Columns[3].MinimumWidth = 100; // Tanggal
            dataGridView1.Columns[4].MinimumWidth = 60; // Mulai
            dataGridView1.Columns[5].MinimumWidth = 60; // Selesai
            dataGridView1.Columns[6].MinimumWidth = 80; // Status
            dataGridView1.Columns[7].MinimumWidth = 70; // Lapangan
            dataGridView1.Columns[8].MinimumWidth = 50;
           


            // Atur ulang header agar terlihat rapi
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
