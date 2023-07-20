using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using System.Xml.Serialization;
using System.Net.NetworkInformation;
namespace WindowsFormsApp1
{
    public partial class Rehber : Form
    {
        string temp = Path.Combine(Application.CommonAppDataPath, "data");
        public Rehber()
        {
            InitializeComponent();
            if (File.Exists(temp))
            {
                string jsondata = File.ReadAllText(temp);
                Kisis = JsonSerializer.Deserialize<List<Kisi>>(jsondata);
            }
            goster();
        }
        public List<Kisi> Kisis = new List<Kisi>()
    {
        new Kisi()
        {
           Ad="büşra ece",
            Soyad="sarıman",
            Telefon="05313300595",
            Mail="ecoelif@gmail.com",
        }
    };
        public void ekle(object sender, EventArgs e)
        {
            Form2 frm=new Form2() {Text="Kisi ekle",StartPosition=FormStartPosition.CenterParent,Kisi=new Kisi()};
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Kisis.Add(frm.Kisi);
                listeyeekle(frm.Kisi);
            }
        }
        public void duzenle(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            ListViewItem pItem = listView1.SelectedItems[0];
            Kisi secili = pItem.Tag as Kisi;
            Form2 frm = new Form2() {Text="Kisi duzenle",StartPosition=FormStartPosition.CenterParent ,Kisi=klon(secili) };
            if (frm.ShowDialog() == DialogResult.OK) 
            {
                secili = frm.Kisi;
                Kisiduzenle(pItem,secili);
            }
        }
        public void goster() 
        {
            listView1.Items.Clear();
            foreach (Kisi Kisi in Kisis) 
            {
                listeyeekle(Kisi);
            }
        }
        public void Kisiduzenle(ListViewItem pItem,Kisi Kisi) 
        {
            pItem.SubItems[0].Text = Kisi.Ad;
            pItem.SubItems[1].Text = Kisi.Soyad;
            pItem.SubItems[2].Text = Kisi.Telefon;
            pItem.SubItems[3].Text = Kisi.Mail;
            pItem.Tag=Kisi;
        }
        public void listeyeekle(Kisi Kisi) 
        {
            ListViewItem item= (new ListViewItem(new string[]
                {
                    Kisi.Ad,
                    Kisi.Soyad,
                    Kisi.Telefon,
                    Kisi.Mail,

                }
                ));
            item.Tag = Kisi;
            listView1.Items.Add(item);
        }
        Kisi klon(Kisi Kisi) 
        {
            return new Kisi()
            {
                id = Kisi.id,
                Ad = Kisi.Ad,
                Soyad = Kisi.Soyad,
                Telefon = Kisi.Telefon,
                Mail = Kisi.Mail,
            };
        }
        public void sil(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) 
            {
                return;
            }
            ListViewItem pItem = listView1.SelectedItems[0];
            Kisi secili = pItem.Tag as Kisi;
            var sonuc= MessageBox.Show("Seçili Kisiyı silisin mi ?","Silmeyi Onayla",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (sonuc == DialogResult.Yes) 
            { 
                Kisis.Remove(secili);
                listView1.Items.Remove(pItem); 
            }
        }
        public void kaydet(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog()
            {
                Filter = "Json Formatı|*.json|Xml Formatı|*.xml",
            };
            if (sf.ShowDialog() == DialogResult.OK) 
            {
                if (sf.FileName.EndsWith("json"))
                {
                    string data = JsonSerializer.Serialize(Kisis);
                    File.WriteAllText(sf.FileName, data);
                }
                else if (sf.FileName.EndsWith("xml")) 
                {
                    StreamWriter sw=new StreamWriter(sf.FileName);
                    XmlSerializer serializer=new XmlSerializer(typeof(List<Kisi>));
                    serializer.Serialize(sw, Kisis);
                    sw.Close();
                }
            }
        }
        public void ac(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog()
            {
                Filter = "Json,xml Formatları|*.json;*.xml",
            };
            if (of.ShowDialog() == DialogResult.OK) 
            {
                if (of.FileName.ToLower().EndsWith("json"))
                {
                    string jsondata=File.ReadAllText(of.FileName);
                    Kisis = JsonSerializer.Deserialize<List<Kisi>>(jsondata);
                }
               else if (of.FileName.ToLower().EndsWith("xml"))
               {
                    StreamReader sr=new StreamReader(of.FileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Kisi>));
                    Kisis= (List < Kisi > )serializer.Deserialize(sr);
                    sr.Close();
               }
                goster();
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            string data = JsonSerializer.Serialize(Kisis);
            File.WriteAllText(temp, data);
            base.OnClosing(e);
        }
        
        
         
    }
    [Serializable]
    public class Kisi 
    {
        public string id;
        [Browsable(false)]
        public string ID { get 
            {
                if (id == null)
                {
                    id = Guid.NewGuid().ToString();
                }
                return id;
            }
        set { id = value; }
        }
        [DisplayName("Ad")]
        public string Ad { get; set; }
        [DisplayName("Soyad")]
        public string Soyad { get; set; }
        [DisplayName("Telefon")]
        public string Telefon { get; set; }
        [DisplayName("Mail")]
        public string Mail { get; set; }
        
    }
    }
