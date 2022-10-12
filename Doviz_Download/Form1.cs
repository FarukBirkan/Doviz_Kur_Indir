using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Doviz_Download
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

      
        private void button1_Click(object sender, EventArgs e)
        {

       
            try
            {
                XmlDocument xmlVerisi = new XmlDocument();

                DateTime dt1;
               
                dt1 = dateTimePicker1.Value.Date;
               
                try
                {
                   

                   if (dateTimePicker1.Value.DayOfWeek == DayOfWeek.Saturday || dateTimePicker1.Value.DayOfWeek == DayOfWeek.Sunday)
                    {

                        MessageBox.Show("Hafta Sonu Veri Yoktur ... ");
                    }
                    else
                    {
                        if (dateTimePicker1.Value.Day < 10 || dateTimePicker1.Value.Month < 10)
                        {
                            xmlVerisi.Load(@"https://www.tcmb.gov.tr/kurlar/" + dateTimePicker1.Value.Year + "" + dateTimePicker1.Value.Month.ToString().PadLeft(2, '0') + "/" + dateTimePicker1.Value.Day.ToString().PadLeft(2, '0') + "" + dateTimePicker1.Value.Month.ToString().PadLeft(2, '0') + "" + dateTimePicker1.Value.Year + ".xml");

                            decimal dolar = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "USD")).InnerText.Replace('.', ','));
                            decimal euro = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "EUR")).InnerText.Replace('.', ','));
                            decimal sterlin = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "GBP")).InnerText.Replace('.', ','));

                            lbldolar.Text = dolar.ToString();
                            lbleuro.Text = euro.ToString();
                            lblsterlin.Text = sterlin.ToString();

                            tablo.Rows.Add(dateTimePicker1.Value.ToShortDateString(), lbldolar.Text, lbleuro.Text, lblsterlin.Text);


                            dataGridView1.DataSource = tablo;
                        }
                        else
                        {
                            xmlVerisi.Load(@"https://www.tcmb.gov.tr/kurlar/" + dateTimePicker1.Value.Year + "" + dateTimePicker1.Value.Month + "/" + dateTimePicker1.Value.Day + "" + dateTimePicker1.Value.Month + "" + dateTimePicker1.Value.Year + ".xml");

                        }
                    }





                }
                catch (Exception)
                {

                    MessageBox.Show(xmlVerisi.ToString());
                }


            }
            catch (XmlException xml)
            {

                MessageBox.Show(xml.ToString());
            }
        }
        DataTable tablo = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            tablo.Columns.Add("Tarih", typeof(string));
           
            tablo.Columns.Add("DOLAR", typeof(string));
            tablo.Columns.Add("EURO", typeof(string));
            tablo.Columns.Add("STERLIN", typeof(string));

            dataGridView1.DataSource = tablo;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
