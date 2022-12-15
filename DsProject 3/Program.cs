using System;
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
        public MilliPark leftchild;
        public MilliPark rightchild;

        public MilliPark(string milliParkAdı, string bulunduğuŞehirler, string yüzÖlçümü, string ilanTarihi, string[] paragrafCümleleri)
        {
            this.paragrafBilgi = new List<string>();
            this.milliParkAdı = milliParkAdı;
            this.bulunduğuŞehirler = bulunduğuŞehirler;
            this.ilanTarihi = ilanTarihi;
            this.yüzÖlçümü = yüzÖlçümü;
            this.paragrafBilgi.AddRange(paragrafCümleleri);
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

    class kelimeNode
    {
        public string kelime;
        public int count;
        public kelimeNode leftchild;
        public kelimeNode rightchild;

        public kelimeNode(string kelime)
        {
            this.kelime = kelime;
            count = 0;
        }

        public void ekranaYaz()
        {
            Console.Write("{0,-20} Bulunma Sayisi: {1}\n", kelime, count);
        }
    }

    class MilliParkAğaç
    {
        private MilliPark root;

        public MilliParkAğaç()
        {
            root = null;
        }

        public MilliPark köküAl()
        {
            return root;
        }

        public void MilliParkEkle(MilliPark millipark)
        {
            if (root == null)
            {
                root = millipark;
            }
            else
            {
                MilliPark current = root;
                MilliPark parent;
                while (true)
                {
                    parent = current;
                    if (string.Compare(millipark.milliParkAdı, current.milliParkAdı) < 0)
                    {
                        current = current.leftchild;
                        if (current == null)
                        {
                            parent.leftchild = millipark;
                            return;
                        }
                    }
                    else
                    {
                        current = current.rightchild;
                        if (current == null)
                        {
                            parent.rightchild = millipark;
                            return;
                        }
                    }
                }
            }
        }

        public void İlkÜçHarfleBulma()
        {
            if (root == null)
            {
                Console.WriteLine("Ağaç Boş!");
                return;
            }

            Console.Write("Aradığınız Parkın İlk Üç Harfini veya daha fazlasını Giriniz: ");
            string arananHarfler = Console.ReadLine();

            MilliPark current = root;
            string ilkÜçHarf = current.milliParkAdı.Substring(0, Math.Min(current.milliParkAdı.Length, arananHarfler.Length));
            while (!ilkÜçHarf.Equals(arananHarfler))
            {
                ilkÜçHarf = current.milliParkAdı.Substring(0, Math.Min(current.milliParkAdı.Length, arananHarfler.Length));
                if (string.Compare(arananHarfler, ilkÜçHarf) < 0)
                {
                    current = current.leftchild;
                }
                else if (string.Compare(arananHarfler, ilkÜçHarf) > 0)
                {
                    current = current.rightchild;
                }

                if (current == null)
                {
                    Console.WriteLine("Bulunamadı!");
                    return;
                }
            }

            Console.Write("Aranan Milli Park: {0}\nBulunduğu İller: {1}\n\n", current.milliParkAdı, current.bulunduğuŞehirler);
        }

        public int DerinlikBul(MilliPark node)
        {
            if (node == null) return 0;
            int ldepth = DerinlikBul(node.leftchild);
            int rdepth = DerinlikBul(node.rightchild);
            if (ldepth > rdepth)
            {
                return ldepth + 1;
            }
            else
            {
                return rdepth + 1;
            }
        }

        public int düğümSayısıBul(MilliPark node)
        {
            if (node == null) return 0;
            int lDüğüm = düğümSayısıBul(node.leftchild);
            int rDüğüm = düğümSayısıBul(node.rightchild);
            return 1 + lDüğüm + rDüğüm;
        }


        public void AğaçBilgileriniYaz(int i)
        {
            switch (i)
            {
                case 1:
                    preorder(root);
                    break;
                case 2:
                    inorder(root);
                    break;
                case 3:
                    postorder(root);
                    break;
            }

            int düğümSayısı = düğümSayısıBul(köküAl());
            Console.Write("\n\nAğacın Derinliği: {0}\nAğacın Düğüm Sayısı: {1}\n", DerinlikBul(root), düğümSayısı);
            double derinlik = Math.Log(düğümSayısı + 1, 2) - 1;
            Console.WriteLine("Ağaç Dengeli Olsaydı Derinliği: {0}", Convert.ToInt32(derinlik));
        }

        public void ParagrafBilgileriniInorderAl(MilliPark node, List<List<string>> Liste)
        {
            if (node != null)
            {
                ParagrafBilgileriniInorderAl(node.leftchild, Liste);
                Liste.Add(node.paragrafBilgi);
                ParagrafBilgileriniInorderAl(node.rightchild, Liste);
            }
        }

        public void inorder(MilliPark node)
        {
            if (node != null)
            {
                inorder(node.leftchild);
                node.ekranaYaz();
                inorder(node.rightchild);
            }
        }

        public void preorder(MilliPark node)
        {
            if (node != null)
            {
                node.ekranaYaz();

                inorder(node.leftchild);
                inorder(node.rightchild);
            }
        }

        public void postorder(MilliPark node)
        {
            if (node != null)
            {
                inorder(node.leftchild);
                inorder(node.rightchild);
                node.ekranaYaz();
            }
        }
    }

    class KelimeAğacı
    {
        private kelimeNode root;

        public KelimeAğacı()
        {
            root = null;
        }

        public kelimeNode getRoot()
        {
            return root;
        }

        public void KelimeEkle(kelimeNode eklenicekNode)
        {
            if (root == null)
            {
                root = eklenicekNode;
            }
            else
            {
                kelimeNode current = root;
                kelimeNode parent;
                while (true)
                {
                    parent = current;
                    if (string.Compare(eklenicekNode.kelime, current.kelime) < 0)
                    {
                        current = current.leftchild;
                        if (current == null)
                        {
                            parent.leftchild = eklenicekNode;
                            eklenicekNode.count++;
                            return;
                        }
                    }
                    else if (string.Compare(eklenicekNode.kelime, current.kelime) > 0)
                    {
                        current = current.rightchild;
                        if (current == null)
                        {
                            parent.rightchild = eklenicekNode;
                            eklenicekNode.count++;

                            return;
                        }
                    }
                    else
                    {
                        current.count++;
                        return;
                    }
                }
            }
        }

        public void kelimeleriInorderYaz(kelimeNode node)
        {
            if (node != null)
            {
                kelimeleriInorderYaz(node.leftchild);
                node.ekranaYaz();

                kelimeleriInorderYaz(node.rightchild);
            }
        }
    }

    internal class Program
    {
        public static Random random = new Random();


        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.GetEncoding("iso-8859-9");

            MilliParkAğaç MilliParkAğaç = new MilliParkAğaç();
            List<MilliPark> MilliParkListesi = MilliParkDosyasıOku();
            foreach (MilliPark park in MilliParkListesi)
            {
                MilliParkAğaç.MilliParkEkle(park);
            }

            KelimeAğacı kelimeAğaç = kelimeAğacıOluştur(MilliParkAğaç);
            int z = 0;
            while (z == 0)
            {
                Console.WriteLine("Milli Park Ağacının bilgilerini yazdırmak için: Preorder(1)  Inorder(2) Postorder(3)\nİlk üç harfi verilen parkın şehirlerini yazmak için(4)\nKelime Ağacını Inorder yazdırmak için(5)\nÇıkış için (6)");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        MilliParkAğaç.AğaçBilgileriniYaz(1);
                        break;

                    case "2":
                        Console.Clear();

                        MilliParkAğaç.AğaçBilgileriniYaz(2);
                        break;
                    case "3":
                        Console.Clear();

                        MilliParkAğaç.AğaçBilgileriniYaz(3);
                        break;
                    case "4":
                        Console.Clear();

                        MilliParkAğaç.İlkÜçHarfleBulma();
                        break;
                    case "5":
                        Console.Clear();
                        kelimeAğaç.kelimeleriInorderYaz(kelimeAğaç.getRoot());
                        break;
                    case "6":
                        Console.Clear();

                        z = 1;
                        break;
                    default:
                        break;
                }
            }
        }

        static KelimeAğacı kelimeAğacıOluştur(MilliParkAğaç milliParkAğaç)
        {
            KelimeAğacı kelimeAğacı = new KelimeAğacı();
            List<List<string>> paragrafListesi = new List<List<string>>();
            milliParkAğaç.ParagrafBilgileriniInorderAl(milliParkAğaç.köküAl(), paragrafListesi);
            string[] stringDizi;
            for (int i = 0; i < milliParkAğaç.düğümSayısıBul(milliParkAğaç.köküAl()); i++)
            {
                foreach (string cümle in paragrafListesi[i])
                {
                    stringDizi = cümle.Split(' ');
                    foreach (string kelime in stringDizi)
                    {
                        kelimeAğacı.KelimeEkle(new kelimeNode(kelime));
                    }
                }
            }

            return kelimeAğacı;
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
    }
}