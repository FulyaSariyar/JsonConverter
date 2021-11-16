using Newtonsoft.Json;
using System.Drawing.Imaging;

namespace JsonConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ListeyiDoldur()
        {
            lstUrunler.Items.Clear();
            foreach (Urun urun in UrunContext.Urunler)
            {
                lstUrunler.Items.Add(urun);
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Urun yeniUrun = new Urun()
            {
                UrunAd = txtUrunAd.Text,
                Fiyat = txtFiyat.Text+$" TL ",
                UrunKategori=cmbKategori.SelectedItem.ToString(),
               // Id = txtId.Text
            };
            if (pbResim.Image != null)
            {
                MemoryStream resimStream = new MemoryStream();
                pbResim.Image.Save(resimStream, ImageFormat.Jpeg);

                yeniUrun.Fotograf = resimStream.ToArray();
            }
            UrunContext.Urunler.Add(yeniUrun);
            ListeyiDoldur();
            UrunContext.Save();

        }

        private void pbResim_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "Bir fotoðraf seçiniz";
            dialog.Filter = "Resim Dosyalarý | *.jpeg; *.jpg; *.png; *.jfif";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                pbResim.ImageLocation = dialog.FileName;
            }
        }

        private void lstUrunler_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void jSONDýþarýAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Dýþarý aktar";
            dialog.Filter = "JSON Format | *.json";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FileStream fileStream = new FileStream(dialog.FileName, FileMode.OpenOrCreate);
                StreamWriter writer = new StreamWriter(fileStream);
                writer.Write(JsonConvert.SerializeObject(UrunContext.Urunler, Formatting.Indented));
                writer.Close();
                writer.Dispose();
                MessageBox.Show($"{UrunContext.Urunler.Count} adet kiþi dýþarý aktarýldý.");
            }
        }

        private void jSONÝçeriAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"/Menuler/{cmbKategori.Text}.json";
            //OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Multiselect = false;
            //dialog.Title = "Bir JSON dosyasý seçiniz";
            //dialog.Filter = "JSON | *.json";
            //dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //StreamReader reader = new StreamReader(path);
            //string dosyaIcerigi = reader.ReadToEnd();
            //UrunContext.Urunler = JsonConvert.DeserializeObject<List<Urun>>(dosyaIcerigi);
            //MessageBox.Show($"{UrunContext.Urunler.Count} adet ürün içeri aktarýldý");
            //ListeyiDoldur();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "Bir JSON dosyasý seçiniz";
            dialog.Filter = "JSON | *.json";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FileStream fileStream = new FileStream(dialog.FileName, FileMode.Open);
                StreamReader reader = new StreamReader(fileStream);
                string dosyaIcerigi = reader.ReadToEnd();
                UrunContext.Urunler = JsonConvert.DeserializeObject<List<Urun>>(dosyaIcerigi);
                MessageBox.Show($"{UrunContext.Urunler.Count} adet kiþi içeri aktarýldý");
                ListeyiDoldur();
            }

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbKategori.DataSource = Enum.GetNames(typeof(Menuler));
        }
    }
}