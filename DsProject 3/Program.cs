using System;

namespace DsProject3
{
    class HeapInt
    {
        private readonly int[] array;
        private int elemanSayisi;
        private readonly int size;

        public HeapInt(int maxsize)
        {
            size = maxsize;
            array = new int[size];
            elemanSayisi = 0;
        }

        public int getItems()
        {
            return elemanSayisi;
        }

        public bool isEmpty()
        {
            return elemanSayisi == 0;
        }

        public bool insert(int sayi)
        {
            if (elemanSayisi == size)
            {
                return false;
            }

            for (int i = 0; i < elemanSayisi; i++)
            {
                if (array[i] == sayi)
                {
                    return false;
                }
            }

            array[elemanSayisi] = sayi;
            yukarıDüzenleme(elemanSayisi++);
            return true;
        }

        public int remove()
        {
            int sayı = array[0];
            array[0] = array[--elemanSayisi];

            aşağıDüzenleme(0);

            return sayı;
        }

        public void yukarıDüzenleme(int bakılanIndex)
        {
            int bottom = array[bakılanIndex];
            int parent = (bakılanIndex - 1) / 2;
            while (bakılanIndex > 0 && bottom > array[parent])
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
            int kök = array[bakılanIndex];
            while (bakılanIndex < elemanSayisi / 2)
            {
                int leftChild = bakılanIndex * 2 + 1;
                int rightChild = leftChild + 1;
                if (rightChild > elemanSayisi && array[rightChild] > array[leftChild])
                {
                    largerChild = rightChild;
                }
                else
                {
                    largerChild = leftChild;
                }

                if (kök >= largerChild)
                {
                    break;
                }

                array[bakılanIndex] = array[largerChild];
                bakılanIndex = largerChild;
            }

            array[elemanSayisi] = 0;
            if (elemanSayisi != 0) array[bakılanIndex] = kök;
        }

        public bool change(int index, int yeniSayı)
        {
            if (index >= size || index < 0 || index > elemanSayisi)
            {
                return false;
            }

            int öncekiSayı = array[index];
            array[index] = yeniSayı;
            if (yeniSayı < öncekiSayı)
            {
                aşağıDüzenleme(index);
            }
            else
            {
                yukarıDüzenleme(index);
            }

            return true;
        }

        public void ekranaYaz(int c)
        {
            Console.Write("Heap TamSayı Dizisi:\n[ ");
            for (int i = 0; i < size; i++)
            {
                if (array[i] != 0)
                {
                    Console.Write(array[i] + ", ");
                }
                else
                {
                    Console.Write("**, ");
                }
            }

            Console.Write("]\nHeap Binary Ağacı:\n");
            Console.Write(new string('-', 130) + "\n");
            int boşluk = 64;
            int sütün = 0;
            int satırdakiElemanSayısı = 1;
            int toplamYazılanEleman = 0;
            while (elemanSayisi > 0)
            {
                if (sütün == 0)
                {
                    Console.Write(new string(' ', boşluk));
                }

                if (c == 0)
                {
                    Console.Write(array[toplamYazılanEleman]);
                }
                else
                {
                    Console.Write($"{array[toplamYazılanEleman]} ({toplamYazılanEleman})");
                }

                if (++toplamYazılanEleman == elemanSayisi)
                {
                    break;
                }

                if (++sütün == satırdakiElemanSayısı)
                {
                    sütün = 0;
                    satırdakiElemanSayısı *= 2;
                    boşluk /= 2;
                    Console.WriteLine();
                }
                else
                {
                    Console.Write(new string(' ', (boşluk * 2) - 2));
                }
            }

            Console.Write("\n" + new string('-', 130) + "\n");
        }
    }

    internal class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            HeapInt heapI = new HeapInt(20);
            int z = 0;
            while (z == 0)
            {
                Console.WriteLine(
                    "Tam Sayılardan Oluşan Heap İşlemleri İçin (1)\nRastgele sayılardan oluşan Heap görüntülemek için(2)\nÇıkış (3)");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        int a = 0;
                        while (a == 0)
                        {
                            Console.WriteLine(
                                "Sayı girişi(1)\nSilme (2)\nGörüntüleme(3)\nSayı Değiştirme(4)\nGeri dönüş(5)");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    Console.WriteLine("Sayiyi Girin: ");
                                    heapI.insert(Int32.Parse(Console.ReadLine()));
                                    Console.Clear();
                                    Console.WriteLine("Yeni Hali:");
                                    heapI.ekranaYaz(0);
                                    break;
                                case "2":
                                    if (!heapI.isEmpty())
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Silinen Sayı: " + heapI.remove());

                                        heapI.ekranaYaz(0);
                                        break;
                                    }

                                    Console.Clear();
                                    Console.WriteLine("Heap boş!");
                                    break;
                                case "3":
                                    Console.Clear();
                                    heapI.ekranaYaz(0);
                                    break;
                                case "4":
                                    if (!heapI.isEmpty())
                                    {
                                        Console.Clear();
                                        heapI.ekranaYaz(1);
                                        Console.Write("Değiştirmek istediğiniz sayının indexini yazın: ");
                                        int index = Int32.Parse(Console.ReadLine());
                                        if (index < heapI.getItems())
                                        {
                                            Console.Write("\nYeni değerini yazın: ");
                                            int yeniSayı = Int32.Parse(Console.ReadLine());
                                            heapI.change(index, yeniSayı);
                                            Console.Clear();
                                            Console.WriteLine("Yeni Hali:");
                                            heapI.ekranaYaz(0);
                                            break;
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Girilen indexte bir sayı yok!");
                                            break;
                                        }
                                    }

                                    Console.Clear();
                                    Console.WriteLine("Heap Boş!");
                                    break;
                                default:
                                    Console.Clear();
                                    a = 1;
                                    break;
                            }
                        }

                        break;
                    case "2":
                        Console.Clear();
                        rastgeleTamsayıHeap();
                        break;
                    case "3":
                        z = 1;
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }

        static void rastgeleTamsayıHeap()
        {
            int size = random.Next(20, 30);
            HeapInt heap = new HeapInt(size);
            int elemanSayisi = random.Next(10, 20);
            Console.WriteLine("Rastgale Heap (Boyut 20 - 30 , Eleman Sayisi 10 - 20 , Sayılar 10 - 100 )");


            for (int i = 0; i < elemanSayisi; i++)
            {
                int a = random.Next(10, 100);
                heap.insert(a);
            }

            heap.ekranaYaz(0);
        }
    }
}