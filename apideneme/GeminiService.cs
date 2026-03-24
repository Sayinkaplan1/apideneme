using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace apideneme
{
    public class GeminiService
    {
        private readonly string _apiKey;
        private readonly HttpClient _client;

        // Sohbet geçmişini tutacağımız liste (Hafıza)
        private List<object> _sohbetGecmisi;

        // Yapay zekanın rolü
        private readonly string _sistemMesaji;

        public GeminiService(string apiKey, string sistemMesaji = "")
        {
            _apiKey = apiKey;
            _client = new HttpClient();
            _sohbetGecmisi = new List<object>();
            _sistemMesaji = sistemMesaji;
        }

        // --- GÜNCELLENEN METOT: HEM METİN HEM DE GÖRSEL/DOSYA DESTEĞİ ---
        public async Task<string> SoruSorAsync(string kullaniciSorusu, string base64Dosya = "", string mimeType = "")
        {
            // İsteği oluşturacak parça listesi (prompt + opsiyonel dosya)
            var parcaListesi = new List<object>();

            // 1. Metin parçasını ekle
            parcaListesi.Add(new { text = kullaniciSorusu });

            // 2. Eğer kullanıcı dosya SEÇTİYSE, Google'ın beklediği teknik formata (inline_data) çevirip ekle
            if (!string.IsNullOrEmpty(base64Dosya) && !string.IsNullOrEmpty(mimeType))
            {
                parcaListesi.Add(new { inline_data = new { mime_type = mimeType, data = base64Dosya } });
            }

            // 3. Kullanıcının tüm bu girdilerini (metin + dosya) hafızaya ekle
            _sohbetGecmisi.Add(new { role = "user", parts = parcaListesi });

            string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}";

            // 4. Google'a gönderilecek paketi hazırla (Rol + Hafıza)
            var requestBody = new
            {
                // Yapay zekaya kim olduğunu söylüyoruz (System Prompt)
                system_instruction = new { parts = new { text = _sistemMesaji } },
                // Tüm sohbet geçmişini (son soru ve dosya dahil) gönderiyoruz
                contents = _sohbetGecmisi
            };

            string jsonBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // 5. İsteği at
            HttpResponseMessage response = await _client.PostAsync(url, content);
            string responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                dynamic json = JsonConvert.DeserializeObject(responseString);
                string cevap = json.candidates[0].content.parts[0].text;

                // 6. Gemini'ın verdiği cevabı da hafızaya ekle (Sohbet kopmasın)
                // Gemini sadece metin cevabı verdiği için model rolünde sadece text parçasını yolluyoruz.
                _sohbetGecmisi.Add(new { role = "model", parts = new[] { new { text = cevap } } });

                return cevap;
            }
            else
            {
                // Hata olursa son soruyu hafızadan sil ki sistem tıkanmasın
                _sohbetGecmisi.RemoveAt(_sohbetGecmisi.Count - 1);
                throw new Exception("API Hatası: " + responseString);
            }
        }
    }
}