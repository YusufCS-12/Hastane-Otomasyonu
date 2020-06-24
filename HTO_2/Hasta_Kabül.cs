using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace HTO_2
{
    public partial class Hasta_Kabül : Form
    { SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Asif Amiri\Desktop\Hastana_Takip_Son.mdf;Integrated Security=True;Connect Timeout=30");
        public Hasta_Kabül()
        {
            InitializeComponent();
        }
        private void Klinik_Getir_Combobox(ComboBox cmbx)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            DataTable dt = new DataTable();
            SqlDataAdapter adptr = new SqlDataAdapter("select * from Klinik order by Klinik_Id ASC", con);
            adptr.Fill(dt);
           cmbx.ValueMember = "Klinik_Id";
           cmbx.DisplayMember = "Klinik_Adi";
            cmbx.DataSource = dt;

        }

        private void Hasta_Kabül_Load(object sender, EventArgs e)
        {
            label1.Text ="Hoşgeldin:  "+ "  "+ Ana_Giriş.Gnd_Skrtr_Ad;
            Klinik_Getir_Combobox(comboBox1);
            Klinik_Getir_Combobox(comboBox4);
        }

        private void randevu_getir(string tc)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            DataTable dt = new DataTable();
            SqlDataAdapter adprt = new SqlDataAdapter("select * from Randevu where Hasta_Tc='" + tc + "' ", con);
            adprt.Fill(dt);
          dataGridView6.DataSource = dt;
            con.Close();

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            SqlCommand cmd = new SqlCommand("Select * from Hasta where Hasta_Tc Like'" + textBox5.Text + "%'", con);
            SqlDataAdapter adptr = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adptr.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            if (comboBox1.SelectedIndex != -1)
            {
                DataTable dt = new DataTable();
                SqlDataAdapter adptr = new SqlDataAdapter("select * from Doktor where Klinik_Id= " + comboBox1.SelectedValue, con);
                adptr.Fill(dt);
                dataGridView3.DataSource = dt;

                con.Close();
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool durum = true;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (hastatc != "" && doktortc != "" && Convert.ToString(dateTimePicker1.Value) != "" && comboBox3.SelectedItem.ToString() != "")
            {
                SqlCommand cmd1 = new SqlCommand("select * from Randevu where Doktor_Tc=@1 and Tarih=@2 and Saat=@3 ", con);
                cmd1.Parameters.AddWithValue("@1", doktortc);
                cmd1.Parameters.AddWithValue("@2", dateTimePicker1.Value);
                cmd1.Parameters.AddWithValue("@3", comboBox3.SelectedItem.ToString());
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    durum = false;
                    dr.Close();
                }

                dr.Close();

                if (durum)
                {
                    SqlCommand cmd = new SqlCommand("insert into Randevu (Doktor_Tc,Hasta_Tc,Tarih,Saat) values (@1,@2,@3,@4)", con);
                    cmd.Parameters.AddWithValue("@1", doktortc);
                    cmd.Parameters.AddWithValue("@2", hastatc);
                    cmd.Parameters.AddWithValue("@3", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@4", comboBox3.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    randevu_getir(hastatc);
                }

                else
                {

                    MessageBox.Show("Secilen Tarih Ve Saat De Secilen Doktorun  Randevusu Var ");
                }

            }

            else
            {
                MessageBox.Show("Alanları Boş Geçemezsiniz ");


            }
        }
        public string doktortc = "";
        private void dataGridView3_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView3.SelectedRows[0];
            doktortc = row.Cells[0].Value.ToString();
        }
        public string hastatc = "";
        private void dataGridView2_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView2.SelectedRows[0];
            hastatc = row.Cells[0].Value.ToString();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            SqlCommand cmd = new SqlCommand("Select * from Hasta where Hasta_Tc Like'" + textBox10.Text + "%'", con);
            SqlDataAdapter adptr = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adptr.Fill(dt);
            dataGridView4.DataSource = dt;
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("insert into Hasta (Hasta_Tc,Hasta_Adi_Soyadi) values (@1,@2) ", con);
            cmd.Parameters.AddWithValue("@1", textBox6.Text);
            cmd.Parameters.AddWithValue("@2", textBox7.Text);
      
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayit Başarılı");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("update Hasta set  Hasta_Adi_Soyadi=@2  where Hasta_Tc=@1  ", con);
            cmd.Parameters.AddWithValue("@1", textBox6.Text);
            cmd.Parameters.AddWithValue("@2", textBox7.Text);
         
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Güncelleme Başarılı");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            SqlCommand cmd = new SqlCommand("Select * from Randevu where Hasta_Tc Like'" + textBox3.Text + "%'", con);
            SqlDataAdapter adptr = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adptr.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            textBox4.Text = row.Cells[0].Value.ToString();

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            if (comboBox1.SelectedIndex != -1)
            {
                DataTable dt = new DataTable();
                SqlDataAdapter adptr = new SqlDataAdapter("select * from Doktor where Klinik_Id= " + comboBox4.SelectedValue, con);
                adptr.Fill(dt);
                dataGridView5.DataSource = dt;

                con.Close();
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("update Randevu set  Doktor_Tc=@2,Hasta_Tc=@3,Tarih=@4,Saat=@5 where Randevu_Id=@1  ", con);
            cmd.Parameters.AddWithValue("@1", textBox4.Text);
            cmd.Parameters.AddWithValue("@2", doktortc1);
            cmd.Parameters.AddWithValue("@3", textBox3.Text);
            cmd.Parameters.AddWithValue("@4", dateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@5", comboBox2.SelectedItem.ToString());
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Güncelleme Başarılı");
           randevu_getir(textBox3.Text);
        }
        string doktortc1 = "";
        private void dataGridView5_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView5.SelectedRows[0];
            doktortc1 = row.Cells[0].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("delete from Randevu where Randevu_Id=@1", con);
            cmd.Parameters.AddWithValue("@1", textBox4.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Silme Basarılı");
            randevu_getir(textBox3.Text);
        }
    }
}
