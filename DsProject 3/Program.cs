using System;
using System.Collections.Generic;
using System.Globalization;

namespace DsProject3
{
    class HeapMilliPark
    {
        private readonly MilliPark[] array;
        private int elemanSayisi;
        private readonly int size;

        public HeapMilliPark(int maxsize)
        {
            size = maxsize;
            array = new MilliPark[size];
            elemanSayisi = 0;
        }

        public bool isEmpty()
        {
            return elemanSayisi == 0;
        }

        public bool insert(MilliPark park)
        {
            if (elemanSayisi == size)
            {
                return false;
            }

            array[elemanSayisi] = park;
            yukarıDüzenleme(elemanSayisi++);
            return true;
        }

        public MilliPark remove()
        {
            MilliPark temp = array[0];
            array[0] = array[--elemanSayisi];

            aşağıDüzenleme(0);

            return temp;
        }

        public void yukarıDüzenleme(int bakılanIndex)
        {
            MilliPark bottom = array[bakılanIndex];
            int parent = (bakılanIndex - 1) / 2;
            while (bakılanIndex > 0 && bottom.yüzÖlçümü > array[parent].yüzÖlçümü)
            {
                array[bakılanIndex] = array[parent];
                bakılanIndex = parent;
                parent = (parent - 1) / 2;
            }

            array[bakılanIndex] = bottom;
        }

        public void aşağıDüzenleme(int bakılanIndex)
        {
            int largerChild;
            MilliPark kök = array[bakılanIndex];
            while (bakılanIndex < elemanSayisi / 2)
            {
                int leftChild = bakılanIndex * 2 + 1;
                int rightChild = leftChild + 1;
                if (rightChild > elemanSayisi && array[rightChild].yüzÖlçümü > array[leftChild].yüzÖlçümü)
                {
                    largerChild = rightChild;
                }
                else
                {
                    largerChild = leftChild;
                }

                if (kök.yüzÖlçümü >= array[largerChild].yüzÖlçümü)
                {
                    break;
                }

                array[bakılanIndex] = array[largerChild];
                bakılanIndex = largerChild;
            }

            array[elemanSayisi] = null;
            if (elemanSayisi != 0) array[bakılanIndex] = kök;
        }
    }

    class MilliPark
    {
        public string milliParkAdı;
        public string bulunduğuŞehirler;
        public string ilanTarihi;
        public int yüzÖlçümü;
        public List<string> paragrafBilgi;


        public MilliPark(string milliParkAdı, string bulunduğuŞehirler, int yüzÖlçümü, string ilanTarihi, string[] paragrafBilgi)
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
            Console.Write("Milli Park Adı: {0,-50}\nBulunduğu Şehirler: {1,-30}\nİlan Tarihi: {2}\nYüzölçümü: {3:n0} Hektar\n", milliParkAdı, bulunduğuŞehirler, ilanTarihi, yüzÖlçümü);
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
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            List<MilliPark> liste = MilliParkDosyasıOku();
            HeapMilliPark heap = new HeapMilliPark(liste.Count);
            foreach (MilliPark park in liste)
            {
                heap.insert(park);
            }

            Console.WriteLine("İlk Üç Hektarlı Milli Park:\n");
            for (int i = 0; i < 3; i++)
            {
                MilliPark temp = heap.remove();
                temp.ekranaYaz();
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
                milliParkListesi.Add(new MilliPark(dataDizisi[i][0], dataDizisi[i][1], Int32.Parse(dataDizisi[i][2], NumberStyles.AllowThousands, CultureInfo.InvariantCulture), dataDizisi[i][3], cümleler));
            }


            return milliParkListesi;
        }
    }
}