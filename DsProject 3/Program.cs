using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable MemberCanBePrivate.Local
// ReSharper disable ArrangeTypeMemberModifiers

namespace DsProject3
{
    class MilliPark
    {
        public string milliParkAdı;
        public string bulunduğuŞehirler;
        public string ilanTarihi;
        public string yüzÖlçümü;
        public List<string> paragrafBilgi;


        public MilliPark(string milliParkAdı, string bulunduğuŞehirler, string yüzÖlçümü, string ilanTarihi, string[] paragrafBilgi)
        {
            this.paragrafBilgi = new List<string>();
            this.milliParkAdı = milliParkAdı;
            this.bulunduğuŞehirler = bulunduğuŞehirler;
            this.ilanTarihi = ilanTarihi;
            this.yüzÖlçümü = yüzÖlçümü;
            this.paragrafBilgi.AddRange(paragrafBilgi);
        }

        public void ekranaYaz()
        {
            Console.Write("Milli Park Adı: {0,-50}\nBulunduğu Şehirler: {1,-30}\nİlan Tarihi: {2}\nYüzölçümü: {3} Hektar\n", milliParkAdı, bulunduğuŞehirler, ilanTarihi, yüzÖlçümü);
            Console.WriteLine("Bilgileri:");


            for (int i = 0; i < paragrafBilgi.Count; i++)
            {
                Console.Write("{0}.\n", paragrafBilgi[i]);
            }

            Console.WriteLine();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.GetEncoding("iso-8859-9");
            List<MilliPark> milliParkListe = MilliParkDosyasıOku();
            Hashtable hashtablosu = hashTablosuOluştur(milliParkListe);
            int z = 0;


            while (z == 0)
            {
                Console.WriteLine("Milli Parkların İlan Tarihlerini listelemek için 1 yazın. Çıkmak için -1 girin.\nBir milli parkın ilan tarihini değiştirmek için 2 girin\nMilli Park HashTable görüntülemek için 3 girin");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "-1":
                        z = 1;
                        break;
                    case "1":
                        Console.Clear();
                        foreach (MilliPark milliPark in milliParkListe)
                        {
                            Console.Write("{0,-50} İlan Tarihi: {1,15}\n", milliPark.milliParkAdı, milliPark.ilanTarihi);
                        }

                        break;
                    case "2":
                        Console.Clear();

                        Console.WriteLine("İlan tarihini değiştirmek istediğiniz parkın ismini girin: ");
                        string isim = Console.ReadLine();
                        MilliPark park = MilliParkıBul(hashtablosu, isim);
                        if (park != null)
                        {
                            İlanTarihiniDeğiştir(hashtablosu, park);
                        }
                        else
                        {
                            Console.WriteLine("Girdiğiniz isim ile uyuşan milli park yoktur.");
                        }

                        break;
                    case "3":
                        Console.Clear();
                        foreach (DictionaryEntry dictionaryEntry in hashtablosu)
                        {
                            MilliPark millipark = (MilliPark)dictionaryEntry.Value;
                            Console.Write($"{dictionaryEntry.Key}\n");
                            millipark.ekranaYaz();
                        }


                        Console.WriteLine();
                        break;
                    default:
                        break;
                }
            }
        }

        static List<MilliPark> MilliParkDosyasıOku()
        {
            List<MilliPark> milliParkListesi = new List<MilliPark>();
            List<string> mKopya = new List<string>();
            string[] milliParkTxt = System.IO.File.ReadAllLines(@"C:\Users\debim\Desktop\milliParklar.txt");
            string paragraftTxt = System.IO.File.ReadAllText(@"C:\Users\debim\Desktop\milliParkParagraflar.txt");
            string[][] dataDizisi = new string[48][];
            string[] paragrafDizisi = paragraftTxt.Split('$');
            string[] cümleler;
            for (int i = 0; i < 48; i++)
            {
                dataDizisi[i] = new string[4];
                dataDizisi[i] = milliParkTxt[i].Split(';');
                mKopya.Add(dataDizisi[i][0]);
            }

            mKopya.Sort();
            for (int i = 0; i < 48; i++)
            {
                cümleler = paragrafDizisi[mKopya.IndexOf(dataDizisi[i][0])].Split(new string[] { ". " }, StringSplitOptions.RemoveEmptyEntries);
                milliParkListesi.Add(new MilliPark(dataDizisi[i][0], dataDizisi[i][1], dataDizisi[i][2], dataDizisi[i][3], cümleler));
            }


            return milliParkListesi;
        }

        static Hashtable hashTablosuOluştur(List<MilliPark> liste)
        {
            Hashtable tablo = new Hashtable();
            for (int i = 0; i < liste.Count; i++)
            {
                tablo.Add(liste[i].milliParkAdı, liste[i]);
            }

            return tablo;
        }

        static MilliPark MilliParkıBul(Hashtable tablo, string word)
        {
            foreach (string milliParkAdı in tablo.Keys)
            {
                if (milliParkAdı.Contains(word))
                {
                    return (MilliPark)tablo[milliParkAdı];
                }
            }

            return null;
        }

        static void İlanTarihiniDeğiştir(Hashtable tablo, MilliPark park)
        {
            Console.Clear();
            Console.Write("İlan tarihini değiştirmek istediğiniz park bu mu? (E/H)\n\n");
            park.ekranaYaz();
            switch (Console.ReadLine().ToUpper())
            {
                case "E":
                    Console.Write("\nEski ilan tarihi: {0}\nYeni İlan Tarihini Girin (gün.ay.yıl): ", park.ilanTarihi);
                    park.ilanTarihi = Console.ReadLine();
                    Console.Clear();
                    Console.Write("Parkın Yeni Bilgileri:\n\n");
                    park.ekranaYaz();
                    break;
                case "H":
                    Console.Clear();
                    break;
                default:
                    Console.Clear();

                    break;
            }
        }
    }
}