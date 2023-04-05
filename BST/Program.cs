using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace Zadanie
{

    class Program
    {
        class Node // Wezel
        {
            public int key;

            public Node left;
            public Node right;

            public Node(int d)
            {
                key = d;
            }
        }
        public class BST
        {

            Node root;

            BST() 
            { 
                root = null; 
            }

            BST(int V)
            {
                root = new Node(V);
            }
            void search(int key)
            {
                searchRec(root, key);
            }

            Node searchRec(Node node, int key)
            {
                if (root == null || root.key == key)
                {
                    return root;
                }
                else if (root.key < key)
                {
                    return searchRec(root.left, key);
                }
                else 
                {
                    return searchRec(root.left, key);
                }   
            }

            void insert(int key)
            {
                root = insertRec(root, key);
            }

            Node insertRec(Node root, int key)
            {
                if (root == null)
                {
                    return (new Node(key));
                }

                if (key < root.key)
                {
                    root.left = insertRec(root.left, key);
                }
                else if (key > root.key)
                {
                    root.right = insertRec(root.right, key);
                }

                return root;
            }

            void delete(int key)
            {
                root = deleteRec(root, key);
            }

            Node deleteRec(Node root, int key)
            {
                if (root == null)
                {
                    return root;
                }

                if (key < root.key)
                {
                    root.left = deleteRec(root.left, key);
                }
                else if (key > root.key)
                {
                    root.right = deleteRec(root.right, key);
                }
                else
                {
                    if (root.left == null)
                    {
                        return root.right;
                    }
                    else if (root.right == null)
                    {
                        return root.left;
                    }

                    root.key = minValue(root.right);

                    root.right = deleteRec(root.right, root.key);

                }

                return root;
            }

            int minValue(Node root)
            {
                int min = root.key;

                while (root.left != null)
                {
                    min = root.left.key;
                    root = root.left;
                }

                return min;
            }

            void KLP(Node root, StreamWriter writer)
            {
                if (root != null)
                {
                    writer.Write(root.key + " ");
                    Console.Write(root.key + " ");
                    KLP(root.left, writer);
                    KLP(root.right, writer);
                }
            }

            public static void Main(string[] args)
            {

                BST tree = new BST();

                FileStream inFile = new FileStream("InTest.txt", FileMode.Open, FileAccess.Read);
                FileStream outFile = new FileStream("OutTest.txt", FileMode.Truncate, FileAccess.Write);
                using var reader = new StreamReader(inFile);
                using var writer = new StreamWriter(outFile);

                Random rand = new Random();
                int liczba;

                string line;
                string[] splitLine;
                int n;

                line = reader.ReadLine();
                int[] numbers = line.Split(' ').Select(int.Parse).ToArray();
                n = numbers.Length;

                int tryb = 1; // 1 insert z pliku, 2 insert loowych liczb
                if (tryb == 1)
                {
                    for (int i = 0; i < n; i++)
                    {
                        //Console.WriteLine(numbers[i]);
                        tree.insert(numbers[i]);
                    }
                }
                else if (tryb == 2)
                {
                    for (int i = 0; i < 60; i++)
                    {
                        liczba = rand.Next(100, 200); // przedział liczb

                        tree.insert(liczba);
                    }
                }

                
                writer.Write("KLP: ");
                Console.Write("KLP: ");
                tree.KLP(tree.root, writer);
                writer.WriteLine();
                Console.WriteLine();

                int help = 0, insert, search, delete;
                while (help != 9)
                {
                    Console.WriteLine("Co chcesz zrobić");
                    Console.WriteLine("1. search");
                    Console.WriteLine("2. insert");
                    Console.WriteLine("3. delete");
                    Console.WriteLine("4. KLP (do pliku)");
                    Console.WriteLine("9. end");
                    help = Int32.Parse(Console.ReadLine());

                    if (help == 1)
                    {
                        Console.WriteLine("Co chcesz znalezc: ");
                        search = Int32.Parse(Console.ReadLine());

                        tree.search(search);

                    }
                    else if (help == 2)
                    {
                        Console.WriteLine("Co chcesz dodac: ");
                        insert = Int32.Parse(Console.ReadLine());

                        tree.insert(insert);

                    }
                    else if (help == 3)
                    {
                        Console.WriteLine("Co chcesz usunac: ");
                        delete = Int32.Parse(Console.ReadLine());

                        tree.delete(delete);

                    }
                    else if (help == 4)
                    {
                        Console.Write("Nowe KLP: ");
                        tree.KLP(tree.root, writer);
                        Console.WriteLine();

                    }

                }

            }

        }
    }
}

