using System;

namespace DsProject3
{
    class QuickSorting
    {
        protected readonly int[] array;
        protected readonly int size;
        protected int items;
        public int karşılaştırma;
        public int değiştirme;

        public QuickSorting(int maxsize)
        {
            size = maxsize;
            array = new int[size];
            items = 0;
            karşılaştırma = 0;
            değiştirme = 0;
        }

        public void insert(int sayi)
        {
            if (items != size)
            {
                array[items++] = sayi;
            }
        }

        public void quicksort()
        {
            recquicksort(0, items - 1);
        }

        public void manualSort(int left, int right)
        {
            int size = right - left + 1;
            if (size <= 1)
                return;
            if (size == 2)
            {
                if (array[left] > array[right])
                    swap(left, right);
                return;
            }
            else
            {
                if (array[left] > array[right - 1])
                    swap(left, right - 1);
                if (array[left] > array[right])
                    swap(left, right);
                if (array[right - 1] > array[right])
                    swap(right - 1, right);
            }
        }

        public int medianOf3(int left, int right)
        {
            int center = (left + right) / 2;
            if (array[left] > array[center])
                swap(left, center);
            if (array[left] > array[right])
                swap(left, right);
            if (array[center] > array[right])
                swap(center, right);
            swap(center, right - 1);
            return array[right - 1];
        }

        public void recquicksort(int left, int right)
        {
            int size = right - left + 1;
            if (size <= 3)
                manualSort(left, right);
            else
            {
                int median = medianOf3(left, right);
                int partition = partitionIt(left, right, median);
                recquicksort(left, partition - 1);
                recquicksort(partition + 1, right);
            }
        }

        public int partitionIt(int left, int right, int pivot)
        {
            int leftptr = left;
            int rightptr = right - 1;
            while (true)
            {
                while (array[++leftptr] < pivot) ;
                while (array[--rightptr] > pivot) ;
                if (leftptr >= rightptr)
                {
                    break;
                }
                else
                {
                    değiştirme++;

                    swap(leftptr, rightptr);
                }
            }

            karşılaştırma += (rightptr + leftptr);
            swap(leftptr, right - 1);
            değiştirme++;
            return leftptr;
        }

        public void swap(int a, int b)
        {
            int temp = array[a];
            array[a] = array[b];
            array[b] = temp;
        }

        public void display()
        {
            Console.Write("Array: [ ");
            for (int i = 0; i < items; i++)
            {
                Console.Write($"{array[i]}, ");
            }

            Console.WriteLine("]");
        }

        public int elemanSayısı()
        {
            return items;
        }
    }

    class SelectionSorting : QuickSorting
    {
        public SelectionSorting(int maxsize) : base(maxsize)
        {
        }

        public void selectionSort()
        {
            for (int dış = 0; dış < items - 1; dış++)
            {
                int min = dış;
                for (int iç = dış + 1; iç < items; iç++)
                {
                    if (array[iç] < array[min])
                    {
                        min = iç;
                    }

                    karşılaştırma++;
                }

                if (min != dış) değiştirme++;
                swap(min, dış);
            }
        }
    }

    internal class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            int a = 0;
            while (a == 0)
            {
                Console.WriteLine("Rastgele bir diziyi quicksort ile sıralamak için (1):\nSelection sort ile sıralamak için (2)\nİkisi ile aynı diziyi sıralayıp karşılaştırmak için (3)\nÇıkış için (4) ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        rastgeleQuickSort();
                        break;
                    case "2":
                        Console.Clear();
                        RastgeleSelectionSort();
                        break;
                    case "3":
                        Console.Clear();
                        karşılaştırma();
                        break;


                    case "4":
                        Console.Clear();
                        a = 1;
                        break;

                    default:

                        break;
                }
            }
        }

        static void rastgeleQuickSort()
        {
            int size = random.Next(20, 40);
            QuickSorting quicksort = new QuickSorting(size);
            int items = random.Next(20, size);
            for (int i = 0; i < items; i++)
            {
                quicksort.insert(random.Next(100));
            }

            Console.WriteLine("Sıralanmamış Hali:");
            quicksort.display();
            Console.WriteLine("\nQuicksort ile sıralandıktan sonra:\n ");
            quicksort.quicksort();
            int elemanSayısı = quicksort.elemanSayısı();
            Console.WriteLine($"Dizinin Boyutu: {elemanSayısı}  QuickSort Karşılaştırma (Ortalama) = O(N * log2N); En kötü Durum: O(N^2) , Değiştirme =  O(N/2 * log2N) 'den az");
            Console.WriteLine($"Hesaplanan Karşılaştırma: {Math.Round(Math.Log(elemanSayısı, 2) * elemanSayısı)} Değiştirme: {Math.Round(Math.Log(elemanSayısı, 2) * (elemanSayısı / 2))}");
            Console.WriteLine($"Algoritmada Ölçülen Karşılaştırma: {quicksort.karşılaştırma}, Değiştirme: {quicksort.değiştirme}");
            quicksort.display();
            Console.WriteLine();
        }

        static void RastgeleSelectionSort()
        {
            int size = random.Next(20, 40);
            SelectionSorting selectSort = new SelectionSorting(size);
            int items = random.Next(20, size);
            for (int i = 0; i < items; i++)
            {
                selectSort.insert(random.Next(100));
            }

            Console.WriteLine("Sıralanmamış Hali:");
            selectSort.display();
            int elemanSayısı = selectSort.elemanSayısı();
            selectSort.selectionSort();
            Console.WriteLine("\nSelection sort ile sıralama:");
            Console.WriteLine($"Dizinin Boyutu: {elemanSayısı}  Selection Karşılaştırma = O(N^2), Değiştirme = O(N) 'den az  ");
            Console.WriteLine($"Hesaplanan Karşılaştırma: {Math.Pow(elemanSayısı, 2)} Değiştirme: {elemanSayısı}");
            Console.WriteLine($"Algoritmada Ölçülen Karşılaştırma: {selectSort.karşılaştırma}, Değiştirme: {selectSort.değiştirme}");
            selectSort.display();
            Console.WriteLine();
        }

        static void karşılaştırma()
        {
            int size = random.Next(20, 40);
            QuickSorting quicksort = new QuickSorting(size);
            SelectionSorting selectsort = new SelectionSorting(size);
            int elemanSayisi = random.Next(20, 30);


            for (int i = 0; i < size; i++)
            {
                int sayi = random.Next(100);
                quicksort.insert(sayi);
                selectsort.insert(sayi);
            }


            Console.WriteLine("Dizinin sıralanmamış hali:");
            quicksort.display();
            Console.WriteLine("\nQuicksort ile sıralandıktan sonra: ");
            quicksort.quicksort();
            int elemanSayısı = quicksort.elemanSayısı();
            Console.WriteLine($"Dizinin Boyutu: {elemanSayısı}  QuickSort Karşılaştırma (Ortalama) = O(N * log2N); En kötü Durum: O(N^2) , Değiştirme =  O(N/2 * log2N) 'den az");
            Console.WriteLine($"Hesaplanan Karşılaştırma: {Math.Round(Math.Log(elemanSayısı, 2) * elemanSayısı)} Değiştirme: {Math.Round(Math.Log(elemanSayısı, 2) * (elemanSayısı / 2))}");
            Console.WriteLine($"Algoritmada Ölçülen Karşılaştırma: {quicksort.karşılaştırma}, Değiştirme: {quicksort.değiştirme}");
            quicksort.display();
            selectsort.selectionSort();
            Console.WriteLine("\nSelection sort ile sıralama:");
            Console.WriteLine($"Dizinin Boyutu: {elemanSayısı}  Selection Karşılaştırma = O(N^2), Değiştirme = O(N) 'den az  ");
            Console.WriteLine($"Hesaplanan Karşılaştırma: {Math.Pow(elemanSayısı, 2)} Değiştirme: {elemanSayısı}");
            Console.WriteLine($"Algoritmada Ölçülen Karşılaştırma: {selectsort.karşılaştırma}, Değiştirme: {selectsort.değiştirme}");
            selectsort.display();
            Console.WriteLine();
        }
    }
}