using System;
using SortingLibrary;

namespace SortingLibrary
{
    public interface sortable
    {
        int[] Sort(int[] numbers, bool ascending);
    }

    public class Sorting : sortable
    {
        public int[] Sort(int[] numbers, bool ascending)
        {
            Console.WriteLine("Choose sorting method: 1. Bubble Sort 2. Insertion Sort 3. Selection Sort 4. Merge Sort");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    return BubbleSort(numbers, ascending);
                case 2:
                    return InsertionSort(numbers, ascending);
                case 3:
                    return SelectionSort(numbers, ascending);
                case 4:
                    return MergeSort(numbers, ascending);
                default:
                    throw new ArgumentException("Invalid sorting method selected.");
            }
        }

        private int[] BubbleSort(int[] numbers, bool ascending)
        {
            int n = numbers.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if ((ascending && numbers[j] > numbers[j + 1]) || (!ascending && numbers[j] < numbers[j + 1]))
                    {
                        // Swap
                        int temp = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = temp;
                    }
                }
            }
            return numbers;
        }

        private int[] InsertionSort(int[] numbers, bool ascending)
        {
            int n = numbers.Length;
            for (int i = 1; i < n; i++)
            {
                int key = numbers[i];
                int j = i - 1;

                while (j >= 0 && ((ascending && numbers[j] > key) || (!ascending && numbers[j] < key)))
                {
                    numbers[j + 1] = numbers[j];
                    j--;
                }
                numbers[j + 1] = key;
            }
            return numbers;
        }

        private int[] SelectionSort(int[] numbers, bool ascending)
        {
            int n = numbers.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int index = i;
                for (int j = i + 1; j < n; j++)
                {
                    if ((ascending && numbers[j] < numbers[index]) || (!ascending && numbers[j] > numbers[index]))
                    {
                        index = j;
                    }
                }
                // Swap
                int temp = numbers[index];
                numbers[index] = numbers[i];
                numbers[i] = temp;
            }
            return numbers;
        }

        private int[] MergeSort(int[] numbers, bool ascending)
        {
            if (numbers.Length <= 1)
                return numbers;

            int mid = numbers.Length / 2;
            int[] left = MergeSort(numbers[..mid], ascending);
            int[] right = MergeSort(numbers[mid..], ascending);
            return Merge(left, right, ascending);
        }

        private int[] Merge(int[] left, int[] right, bool ascending)
        {
            int[] result = new int[left.Length + right.Length];
            int i = 0, j = 0, k = 0;

            while (i < left.Length && j < right.Length)
            {
                if ((ascending && left[i] <= right[j]) || (!ascending && left[i] >= right[j]))
                {
                    result[k++] = left[i++];
                }
                else
                {
                    result[k++] = right[j++];
                }
            }

            while (i < left.Length)
                result[k++] = left[i++];

            while (j < right.Length)
                result[k++] = right[j++];

            return result;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            int[] numbers = new int[10];
            Console.WriteLine("Enter 10 different numbers:");

            for (int i = 0; i < 10; i++)
            {
                numbers[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("Sort in ascending order? (yes/no)");
            bool ascending = Console.ReadLine().ToLower() == "yes";

            sortable sortingStrategy = new Sorting();
            int[] sortedNumbers = sortingStrategy.Sort(numbers, ascending);

            Console.WriteLine("Sorted Numbers: " + string.Join(", ", sortedNumbers));
        }
    }
}