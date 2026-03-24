using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apideneme
{
    public partial class Form1 : Form
    {
        // Servisimizi Form seviyesinde tanımlıyoruz
        private GeminiService _gemini;

        // Seçilen dosyanın teknik bilgilerini hafızada tutuyoruz
        private string base64Dosya = "";
        private string mimeType = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // DİKKAT: Kendi API anahtarınızı buraya yazın!
            string apiKey = "BURAYA_KENDI_API_ANAHTARINIZI_YAZIN";

            // YENİ ROL: Asistanımız huysuz hocadan kurtuldu!
            string rol = "Sen her şeyi bilen, çok yardımsever, kibar ve zeki bir yapay zeka asistanısın. Kullanıcıların tüm sorularına detaylı ve açıklayıcı cevaplar verirsin. Görselleri ve dosyaları analiz edebilirsin. Sadece Türkçe konuş.";

            // Servisimizi başlatıyoruz (API Key ve Rolü vererek)
            _gemini = new GeminiService(apiKey, rol);

            txtCevap.Text = "Sistem Hazır! Sorunuzu veya görselinizi bekliyorum...";
        }

        // --- YENİ HATAYI ÇÖZEN EKSİK METOT ---
        private void txtSoru_TextChanged(object sender, EventArgs e)
        {
            // İçi boş kalabilir, sadece tasarım hatasını susturmak için buraya koyduk.
        }

        // --- YENİ EKLENEN KISIM: GÖRSEL/DOSYA SEÇME BUTONU ---
        private void btnDosyaSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                // Kullanıcının seçeceği dosya türlerini kısıtlıyoruz (Resim ve PDF/TXT gibi temel dosyalar)
                ofd.Filter = "Görsel Dosyaları|*.jpg;*.jpeg;*.png;*.webp|Dokümanlar|*.pdf;*.txt|Tüm Dosyalar|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Temiz bir başlangıç: Önceki teknik bilgileri sil
                    base64Dosya = "";
                    mimeType = "";
                    pictureBox1.Image = null; // Önizlemeyi temizle

                    try
                    {
                        // 1. Dosya Görsel ise, PictureBox'ta önizlemesini göster
                        string uzanti = Path.GetExtension(ofd.FileName).ToLower();
                        if (uzanti == ".jpg" || uzanti == ".jpeg" || uzanti == ".png" || uzanti == ".webp")
                        {
                            pictureBox1.Image = Image.FromFile(ofd.FileName);
                        }

                        // 2. Dosyanın içeriğini Base64 formatına (metne) çevir (Google bu formatı bekler)
                        byte[] dosyaBytes = File.ReadAllBytes(ofd.FileName);
                        base64Dosya = Convert.ToBase64String(dosyaBytes);

                        // 3. Dosyanın formatını (MimeType) belirle
                        mimeType = uzanti == ".png" ? "image/png" :
                                   uzanti == ".webp" ? "image/webp" :
                                   uzanti == ".pdf" ? "application/pdf" :
                                   uzanti == ".txt" ? "text/plain" : "image/jpeg"; // Varsayılan JPG

                        MessageBox.Show("Dosya başarıyla yüklendi! Şimdi sorunuzu sorabilirsiniz.", "Dosya Hazır", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Dosya okunurken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Eğer soru kısmı boşsa hiçbir şey yapma
            if (string.IsNullOrWhiteSpace(txtSoru.Text)) return;

            string soru = txtSoru.Text;

            // UI/UX Dokunuşları: Kullanıcıya işlemin başladığını hissettir
            btnGonder.Enabled = false;
            btnGonder.Text = "Düşünüyor...";
            Cursor = Cursors.WaitCursor; // Fare imlecini kum saatine çevir

            // Ekranda önceki cevabın altına yeni soruyu ekle (Chatbot görünümü)
            txtCevap.AppendText(Environment.NewLine + Environment.NewLine + "Sen: " + soru);
            txtSoru.Clear();

            try
            {
                // Spagetti kod yok! Servisimize hem metni hem de varsa dosyanın teknik bilgilerini gönderiyoruz
                string cevap = await _gemini.SoruSorAsync(soru, base64Dosya, mimeType);

                txtCevap.AppendText(Environment.NewLine + "Asistan: " + cevap);

                // İşlem başarılı olduysa seçili dosyayı temizle (Sohbet geçmişine eklendi zaten)
                base64Dosya = "";
                mimeType = "";
                pictureBox1.Image = null; // Önizlemeyi temizle
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // İşlem bitince arayüzü eski haline getir
                btnGonder.Enabled = true;
                btnGonder.Text = "Sor";
                Cursor = Cursors.Default;
            }
        }
    }
}