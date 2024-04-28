namespace DsProject3
{
    internal class Program
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
                    Console.Write($"{array[i]}\n");
                }

                Console.WriteLine("]");
            }

            public int elemanSayısı()
            {
                return items;
            }
        }

        static void Main(string[] args)
        {
            Random random = new Random();
            Console.WriteLine("Kaç adet sayı?");
            int size = Convert.ToInt32(Console.ReadLine());
            QuickSorting quicksort = new QuickSorting(size);
            List<int> list = new List<int>();
            for (int i = 0; i < size; i++)
            {
                int sayi = random.Next(0, 1000000);
                quicksort.insert(sayi);
            }

            quicksort.quicksort();
            quicksort.display();


            Console.ReadLine();
        }
    }
}