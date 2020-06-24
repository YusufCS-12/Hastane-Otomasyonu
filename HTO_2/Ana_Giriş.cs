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
    public partial class Ana_Giriş : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Asif Amiri\Desktop\Hastana_Takip_Son.mdf;Integrated Security=True;Connect Timeout=30");
        public Ana_Giriş()
        {
            InitializeComponent();
        }
        public static string Gnd_Yntc_Ad = "";
        private void Yntc_Grs_Btn_Click(object sender, EventArgs e)
        {

            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("select * from Yonetici where Yonetici_Adi_Soyadi=@1 and Yonetici_Sifre=@2",con);
            cmd.Parameters.AddWithValue("@1",textBox1.Text);
            cmd.Parameters.AddWithValue("@2", textBox2.Text);
            SqlDataReader dr = cmd.ExecuteReader();


            if(dr.Read())
            {
                Gnd_Yntc_Ad = dr.GetString(2);
                Yönetici_Modülü ynt_frm = new Yönetici_Modülü();
                ynt_frm.Show();
                dr.Close();


            }
            else
            {
                MessageBox.Show("Yanlış Yönetici Adi Ve Şifre");
                dr.Close();
            }



        }
        public static string Gnd_Skrtr_Ad = "";
        private void Hstn_Grs_Btn_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("select * from Sekreter where Sekreter_Adi_Soyadi=@1 and Sekreter_Sifre=@2", con);
            cmd.Parameters.AddWithValue("@1", textBox3.Text);
            cmd.Parameters.AddWithValue("@2", textBox4.Text);
            SqlDataReader dr1 = cmd.ExecuteReader();


            if (dr1.Read())
            {
                Gnd_Skrtr_Ad = dr1.GetString(1);
                Hasta_Kabül Hst_Kbl_frm = new Hasta_Kabül();

                Hst_Kbl_frm.Show();

                dr1.Close();

            }
            else
            {
                MessageBox.Show("Yanlış Sekreter Adi Ve Şifre");
                dr1.Close();
            }

        }

        private void Ana_Giriş_Load(object sender, EventArgs e)
        {

        }
    }
}
