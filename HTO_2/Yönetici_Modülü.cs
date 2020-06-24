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
    public partial class Yönetici_Modülü : Form
    { SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Asif Amiri\Desktop\Hastana_Takip_Son.mdf;Integrated Security=True;Connect Timeout=30");
        public Yönetici_Modülü()
        {
            InitializeComponent();
        }

        private void Klinik_Getir()
       {
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adptr = new SqlDataAdapter("select * from Klinik", con);
            adptr.Fill(dt);
            Klinik_Datagridview.DataSource = dt;
            con.Close();

       }
        private void Tahlil_Getir()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adptr = new SqlDataAdapter("select * from Tahlil_Tanim", con);
            adptr.Fill(dt);
            Tahlil_Datagridview.DataSource = dt;
            con.Close();


        }
        private void Klinik_Getir_Combobox( ComboBox cmbx)
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

        private void Hastalik_Getir_Combobox(ComboBox cmbx)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            DataTable dt = new DataTable();
            SqlDataAdapter adptr = new SqlDataAdapter("select * from Hastalik order by Hastalik_Id ASC", con);
            adptr.Fill(dt);
            cmbx.ValueMember = "Hastalik_Id";
            cmbx.DisplayMember = "Hastalik_Adi";
            cmbx.DataSource = dt;

        }


        private void Doktor_Getir ()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adptr = new SqlDataAdapter("select * from Doktor", con);
            adptr.Fill(dt);
           Doktor_Datagridview.DataSource = dt;
            con.Close();


        }

        private void Hastalik_Getir()
        {

            if (con.State == ConnectionState.Closed)
                con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adptr = new SqlDataAdapter("select * from Hastalik", con);
            adptr.Fill(dt);
            Hastalik_Datagridview.DataSource = dt;
            con.Close();

        }
        private void Yönetici_Modülü_Load(object sender, EventArgs e)
        {
            label1.Text ="Hoşgeldin: "+" "+ Ana_Giriş.Gnd_Yntc_Ad;
            Hastalik_Panel.Visible = false;
            İlac_Panel.Visible = false;
            Doktor_Panel.Visible = false;
            Klinik_Panel.Visible = false;
            Tahlil_Panel.Visible = false;

        }

        private void Kln_Btn_Click(object sender, EventArgs e)
        {
            Hastalik_Panel.Visible = false;
            İlac_Panel.Visible = false;
            Doktor_Panel.Visible = false;
            Klinik_Panel.Visible = true;
            Tahlil_Panel.Visible = false;
            Klinik_Getir();

        }

        private void Dktr_Btn_Click(object sender, EventArgs e)
        {
            Hastalik_Panel.Visible = false;
            İlac_Panel.Visible = false;
            Doktor_Panel.Visible = true;
            Klinik_Panel.Visible = false;
            Tahlil_Panel.Visible = false;
            Doktor_Getir();
            Klinik_Getir_Combobox(comboBox1);
        }

        private void Ilc_Btn_Click(object sender, EventArgs e)
        {
            Hastalik_Panel.Visible = false;
            İlac_Panel.Visible = true;
            Doktor_Panel.Visible = false;
            Klinik_Panel.Visible = false;
            Tahlil_Panel.Visible = false;
            Hastalik_Getir_Combobox(comboBox2);
            İlac_Getir();
        }

        private void Hstlk_Btn_Click(object sender, EventArgs e)
        {
            Hastalik_Panel.Visible =true;
            İlac_Panel.Visible = false;
            Doktor_Panel.Visible = false;
            Klinik_Panel.Visible = false;
            Tahlil_Panel.Visible = false;
            Hastalik_Getir();
          
        }

        private void Thll_Btn_Click(object sender, EventArgs e)
        {
            Hastalik_Panel.Visible = false;
            İlac_Panel.Visible = false;
            Doktor_Panel.Visible = false;
            Klinik_Panel.Visible = false;
            Tahlil_Panel.Visible = true;
            Tahlil_Getir();
        }

        public static string Klnk_Id="";
        private void dataGridView_Klnk_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Klinik_Datagridview.SelectedRows[0];
            Klnk_Id = row.Cells[0].Value.ToString();
            textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text = row.Cells[2].Value.ToString();



        }
        private void İlac_Getir()
        {


            if (con.State == ConnectionState.Closed)
                con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adptr = new SqlDataAdapter("select * from İlac", con);
            adptr.Fill(dt);
            İlac_Datagridview.DataSource = dt;
            con.Close();


        }

        private void Klinik_Ekle_Button_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("insert into Klinik (Klinik_Id,Klinik_Adi,Klinik_Aciklama) values (@1,@2,@3)", con);
            cmd.Parameters.AddWithValue("@1", textBox1.Text);
            cmd.Parameters.AddWithValue("@2", textBox2.Text);
            cmd.Parameters.AddWithValue("@3", textBox3.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Başarılı");
            Klinik_Getir();
        }

        private void Klinik_Sil_Button_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("delete from Klinik where Klinik_Id=@1", con);
            cmd.Parameters.AddWithValue("@1", textBox1.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Silme Başarılı");
            Klinik_Getir();
        }

        private void Klinik_Güncelle_Button_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("update Klinik set  Klinik_Adi=@2,Klinik_Aciklama=@3 where Klinik_Id=@1  ", con);
            cmd.Parameters.AddWithValue("@1", textBox1.Text);
            cmd.Parameters.AddWithValue("@2", textBox2.Text);
            cmd.Parameters.AddWithValue("@3", textBox3.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Güncelleme Başarılı");
            Klinik_Getir();
        }


        private void Klinik_Datagridview_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Klinik_Datagridview.SelectedRows[0];
            textBox1.Text = row.Cells[0].Value.ToString();
            textBox2.Text = row.Cells[1].Value.ToString();
            textBox3.Text = row.Cells[2].Value.ToString();
        }

        private void Tahlil_Datagridview_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Tahlil_Datagridview.SelectedRows[0];
            textBox4.Text = row.Cells[0].Value.ToString();
            textBox5.Text = row.Cells[1].Value.ToString();
            textBox6.Text = row.Cells[2].Value.ToString();

        }

        private void Tahlill_Ekle_Button_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("insert into Tahlil_Tanim (Tahlil_Id,Tahlil_Adi,Tahlil_Aciklama) values (@1,@2,@3)", con);
            cmd.Parameters.AddWithValue("@1", textBox4.Text);
            cmd.Parameters.AddWithValue("@2", textBox5.Text);
            cmd.Parameters.AddWithValue("@3", textBox6.Text);


            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Başarılı");
            Tahlil_Getir();
        }

        private void Tahlil_Güncelle_Button_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("update  Tahlil_Tanim set Tahlil_Adi=@2,Tahlil_Aciklama=@3  where Tahlil_Id=@1 ", con);
            cmd.Parameters.AddWithValue("@1", textBox4.Text);
            cmd.Parameters.AddWithValue("@2", textBox5.Text);
            cmd.Parameters.AddWithValue("@3", textBox6.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Güncelleme Başarılı");
            Tahlil_Getir();
        }

        private void Tahlil_Sil_Button_Click(object sender, EventArgs e)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("delete from Tahlil_Tanim where Tahlil_Id=@1", con);
            cmd.Parameters.AddWithValue("@1", textBox4.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Silme Başarılı");
            Tahlil_Getir();
        }

        private void Dokto_Ekle_Button_Click(object sender, EventArgs e)
        {   if(con.State==ConnectionState.Closed)
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into doktor (Doktor_Tc,Doktor_Adi_Soyadi,Doktor_Telefon,Doktor_Adres,Doktor_Sifre,Klinik_Id) values (@1,@2,@3,@4,@5,@6)", con);
            cmd.Parameters.AddWithValue("@1",textBox7.Text);
            cmd.Parameters.AddWithValue("@2",textBox8.Text);
            cmd.Parameters.AddWithValue("@3",textBox9.Text);
            cmd.Parameters.AddWithValue("@4",textBox10.Text);
            cmd.Parameters.AddWithValue("@5",textBox11.Text);
            cmd.Parameters.AddWithValue("@6",comboBox1.SelectedValue);


            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Başarılı");
            Doktor_Getir();
        }

        private void Doktor_Sil_Button_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("delete from Doktor where Doktor_Tc=@1", con);
            cmd.Parameters.AddWithValue("@1", textBox7.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Silme Başarılı");
            Doktor_Getir();
        }

        private void Doktor_Güncelle_Button_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("update  Doktor set Doktor_Adi_Soyadi=@2,Doktor_Telefon=@3,Doktor_Adres=@4,Doktor_Sifre=@5,Klinik_Id=@6 where Doktor_Tc=@1 ", con);
            cmd.Parameters.AddWithValue("@1", textBox7.Text);
            cmd.Parameters.AddWithValue("@2", textBox8.Text);
            cmd.Parameters.AddWithValue("@3", textBox9.Text);
            cmd.Parameters.AddWithValue("@4", textBox10.Text);
            cmd.Parameters.AddWithValue("@5", textBox11.Text);
            cmd.Parameters.AddWithValue("@6", comboBox1.SelectedValue);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Güncelleme Başarılı");
            Doktor_Getir();
        }

        private void Doktor_Datagridview_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Doktor_Datagridview.SelectedRows[0];
            textBox7.Text = row.Cells[0].Value.ToString();
            textBox8.Text = row.Cells[1].Value.ToString();
            textBox9.Text = row.Cells[2].Value.ToString();
         
            textBox10.Text = row.Cells[3].Value.ToString();
            textBox11.Text = row.Cells[4].Value.ToString();
         
        }

        private void Hastalik_Datagridview_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Hastalik_Datagridview.SelectedRows[0];
            textBox12.Text = row.Cells[0].Value.ToString();
            textBox13.Text = row.Cells[1].Value.ToString();
            textBox14.Text = row.Cells[2].Value.ToString();
        }

        private void Hastalik_Güncelle_Button_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("update  Hastalik set Hastalik_Adi=@2,Hastalik_Aciklama=@3  where Hastalik_Id=@1 ", con);
            cmd.Parameters.AddWithValue("@1", textBox12.Text);
            cmd.Parameters.AddWithValue("@2", textBox13.Text);
            cmd.Parameters.AddWithValue("@3", textBox14.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Güncelleme Başarılı");
           Hastalik_Getir();
        }

        private void button11_Click(object sender, EventArgs e)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("delete from Hastalik where Hastalik_Id=@1", con);
            cmd.Parameters.AddWithValue("@1", textBox12.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Silme Başarılı");
            Hastalik_Getir();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("insert into Hastalik (Hastalik_Id,Hastalik_Adi,Hastalik_Aciklama) values (@1,@2,@3)", con);
            cmd.Parameters.AddWithValue("@1", textBox12.Text);
            cmd.Parameters.AddWithValue("@2", textBox13.Text);
            cmd.Parameters.AddWithValue("@3", textBox14.Text);


            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Başarılı");
            Hastalik_Getir();
        }

        private void İlac_EklButton_Click(object sender, EventArgs e)
        {
            if(con.State==ConnectionState.Closed)
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into İlac (Ilac_Id,Hastalik_Id,Ilac_Adi,Ilac_Aciklama,Ilac_Fiyati) values (@1,@2,@3,@4,@5)", con);
            cmd.Parameters.AddWithValue("@1", textBox15.Text);
            cmd.Parameters.AddWithValue("@2", comboBox2.SelectedValue);
            cmd.Parameters.AddWithValue("@3", textBox16.Text);
            cmd.Parameters.AddWithValue("@4", textBox17.Text);
            cmd.Parameters.AddWithValue("@5", textBox18.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Başarılı");
            İlac_Getir();
        }

        private void İlac_Sil_Button_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("delete from İlac where Ilac_Id=@1", con);
            cmd.Parameters.AddWithValue("@1", textBox15.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Silme Başarılı");
            İlac_Getir();

        }

        private void İlac_Güncelle_Button_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("update  İlac set Hastalik_Id=@2,Ilac_Adi=@3,Ilac_Aciklama=@4,Ilac_Fiyati=@5 where Ilac_Id=@1", con);
            cmd.Parameters.AddWithValue("@1", textBox15.Text);
            cmd.Parameters.AddWithValue("@2", comboBox2.SelectedValue);
            cmd.Parameters.AddWithValue("@3", textBox16.Text);
            cmd.Parameters.AddWithValue("@4", textBox17.Text);
            cmd.Parameters.AddWithValue("@5", textBox18.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Başarılı");
            İlac_Getir();

        }

        private void İlac_Datagridview_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = İlac_Datagridview.SelectedRows[0];
            textBox15.Text = row.Cells[0].Value.ToString();
            textBox16.Text = row.Cells[2].Value.ToString();
            textBox17.Text = row.Cells[3].Value.ToString();
            textBox18.Text = row.Cells[4].Value.ToString();
           
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
