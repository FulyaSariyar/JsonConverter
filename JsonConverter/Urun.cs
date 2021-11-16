using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonConverter
{
    public class Urun
    {
        public string UrunKategori { get; set; }
        public string UrunAd { get; set; }
        public string Fiyat { get; set; }
        // public string Id { get; set; }
        public byte[] Fotograf { get; set; }

        public override string ToString() => $"{UrunAd}-{Fiyat}";
    }
    public enum Menuler:byte
    {
        Balıklar,
        FastFood,
        Salatalar,
        Çorbalar,
        Kahvaltı,
        Mezeler,
        Tatlılar,
        Yemekler,
        İçecekler
    }
}
