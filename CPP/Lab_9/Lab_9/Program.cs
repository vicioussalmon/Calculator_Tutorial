﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Collections;

namespace BookStore
{
    //Lab_6
    class DataLogicException : Exception
    {
        public DataLogicException(string message) : base(message) { }
        public DataLogicException(string message, Exception innerException) : base(message, innerException) { }
    }
    //Lab_7
    interface IName<T> where T : IComparable
    {
        string Name { get; }
        decimal GetPrice();
    }

    public class Product : IName<string>, IComparable<Product>
    {

        public string Name { get; protected set; }
        public decimal Price { get; protected set; }
        public Product()
        {
            Name = "Unknown";
            Price = -1;
        }

        public Product(string names, decimal prices)
        {
            Name = names;
            Price = prices;
        }

        public override string ToString()
        {
            return "Name:" + Name + "\n" + "Price:" + Price;
        }

        public int CompareTo(Product obj) { return Name.CompareTo(obj); }

        decimal IName<string>.GetPrice() { return Price; }

    }
    //Class Book
    public class Book : Product, IName<string>, IComparable<Book>
    {
        public string BookName { get; private set; }
        public string AuthorName { get; private set; }

        public Book() : base()
        {
            BookName = "Unknown";
            AuthorName = "Unknown";
        }

        public Book(string name, decimal price, string bookName, string authorName) : base(name, price)
        {
            BookName = bookName;
            AuthorName = authorName;
        }
        public override string ToString()
        {
            return base.ToString() + "\nBook:" + BookName + "\nAuthor:" + AuthorName;
        }
        public int CompareTo(Book obj) { return Name.CompareTo(obj); }
    }
    public class SingleBook : Book, IName<string>, IComparable<SingleBook>
    {

        public SingleBook() : base() { }
        public SingleBook(string name, decimal price, string bookName, string authorName) : base(name, price, bookName, authorName) { }
        public override string ToString()
        {
            return base.ToString() + "\nQuantity of books: 1";

        }
        public int CompareTo(SingleBook obj) { return Name.CompareTo(obj); }
    }
    public class BookSeries : Book, IName<string>, IComparable<BookSeries>
    {

        public BookSeries() : base() { }
        public BookSeries(string name, decimal price, string bookName, string authorName) : base(name, price, bookName, authorName) { }
        public override string ToString()
        {
            return base.ToString() + "\nThis book is one of the serie of books";
        }
        public int CompareTo(BookSeries obj) { return Name.CompareTo(obj); }
    }
    //Class Bookmarks set
    public class BookmarksSet : Product, IName<string>, IComparable<BookmarksSet>
    {
        public string Colour { get; private set; }
        public float QuantityInSet { get; private set; }

        public BookmarksSet() : base()
        {
            Colour = "Unknown";
            QuantityInSet = 0;
        }

        public BookmarksSet(string name, decimal price, string colour, float quantity) : base(name, price)
        {
            Colour = colour;
            QuantityInSet = quantity;
        }

        public override string ToString()
        {
            return base.ToString() + "\nColour:" + Colour + "\nQuantity:" + QuantityInSet;
        }
        public int CompareTo(BookmarksSet obj) { return Name.CompareTo(obj); }
    }
    public class TranslucentBookmarks : BookmarksSet, IName<string>, IComparable<TranslucentBookmarks>
    {
        TranslucentBookmarks() : base() { }
        public TranslucentBookmarks(string name, decimal price, string colour, float quantity) : base(name, price, colour, quantity) { }
        public override string ToString()
        {
            return base.ToString() + "\nThis set is translucent";
        }
        public int CompareTo(TranslucentBookmarks obj) { return Name.CompareTo(obj); }
    }
    public class OpaqueBookmarks : BookmarksSet, IName<string>, IComparable<OpaqueBookmarks>
    {
        OpaqueBookmarks() : base() { }
        public OpaqueBookmarks(string name, decimal price, string colour, float quantity) : base(name, price, colour, quantity) { }
        public override string ToString()
        {
            return base.ToString() + "\nThis set is opaque";
        }
        public int CompareTo(OpaqueBookmarks obj) { return Name.CompareTo(obj); }
    }
    //Class Posters
    public class Posters : Product, IName<string>, IComparable<Posters>
    {
        public char Size { get; private set; }

        public Posters() : base()
        {
            Size = ' ';
        }

        public Posters(string name, decimal price, char size) : base(name, price)
        {
            Size = size;
        }

        public override string ToString()
        {

            return base.ToString() + "\nSize:" + Size;
        }
        public int CompareTo(Posters obj) { return Name.CompareTo(obj); }
    }
    public class Coloured : Posters, IName<string>, IComparable<Coloured>
    {
        public Coloured() : base() { }
        public Coloured(string name, decimal price, char size) : base(name, price, size) { }
        public override string ToString()
        {
            return base.ToString() + "\nColoured: Yes";
        }
        public int CompareTo(Coloured obj) { return Name.CompareTo(obj); }
    }
    public class UnColoured : Posters, IName<string>, IComparable<UnColoured>
    {
        public UnColoured() : base() { }
        public UnColoured(string name, decimal price, char size) : base(name, price, size) { }
        public override string ToString()
        {
            return base.ToString() + "\nColoured: No";
        }
        public int CompareTo(UnColoured obj) { return Name.CompareTo(obj); }
    }

    class ArrayContainer<T> : IEnumerable<T> where T : IName<string>
    {
        private T[] items;
        private int position = -1;

        public ArrayContainer(int size)
        {
            items = new T[size];
        }

        public void Add(T item)
        {
            if (position == items.Length - 1)
            {
                throw new Exception("The container is full.");
            }

            items[++position] = item;
        }

        public T this[int index]
        {
            get { return items[index]; }
            set { items[index] = value; }
        }

        public int Count { get { return position + 1; } }

        public IEnumerator<T> GetEnumerator()
        {
            return new ArrayContainerIterator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class ArrayContainerIterator<T> : IEnumerator<T> where T : IName<string>
    {
        private ArrayContainer<T> container;
        private int position = -1;

        public ArrayContainerIterator(ArrayContainer<T> container)
        {
            this.container = container;
        }

        public T Current { get { return container[position]; } }

        object IEnumerator.Current { get { return Current; } }

        public bool MoveNext()
        {
            return ++position < container.Count;
        }

        public void Reset() { position = -1; }

        public void Dispose() { }
    }

    //Linked List Container
    class LinkedListContainer<T>: IEnumerable<T> where T : IName<string>
    {
        private class Node
        {
            public T item;
            public Node next;
            public Node prev;

            public Node(T item)
            {
                this.item = item;
                next = null;
                prev = null;
            }
        }

        private Node head;
        private Node tail;
        private int count;

        public LinkedListContainer() { head = null; tail = null; count = 0; }
        public IEnumerator<T> GetEnumerator() { return new LinkedListContainerEnumerator(this); }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        private class LinkedListContainerEnumerator : IEnumerator<T>
        {
            private LinkedListContainer<T> list;
            private Node current;

            public LinkedListContainerEnumerator(LinkedListContainer<T> list)
            {
                this.list = list;
                current = null;
            }

            public T Current { get { return current.item; } }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (current == null)
                    current = list.head;
                else
                    current = current.next;

                return current != null;
            }

            public void Reset() { current = null; }
            public void Dispose() { }
        }
        public void AddProduct(T item)
        {
            Node newNode = new Node(item);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.next = newNode;
                newNode.prev = tail;
                tail = newNode;
            }

            count++;
        }

        public LinkedListContainer<T> GetByName(string name)
        {
            LinkedListContainer<T> result = new LinkedListContainer<T>();
            Node current = head;

            while (current != null)
            {
                if (string.Equals(current.item.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    result.AddProduct(current.item);
                }

                current = current.next;
            }

            return result;
        }

        public LinkedListContainer<T> GetByPrice(decimal price)
        {
            LinkedListContainer<T> result = new LinkedListContainer<T>();
            Node current = head;

            while (current != null)
            {
                if (current.item.GetPrice() == price)
                {
                    result.AddProduct(current.item);
                }

                current = current.next;
            }

            return result;
        }

        public T GetByIndex(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();


            Node current = head;
            for (int i = 0; i < index; i++)
                current = current.next;

            return current.item;
        }

        public void SortProductsByName()
        {
            Node current = head;
            while (current != null)
            {
                Node minNode = current;
                Node nextNode = current.next;
                while (nextNode != null)
                {
                    if (string.Compare(nextNode.item.Name, minNode.item.Name) < 0)
                    {
                        minNode = nextNode;
                    }
                    nextNode = nextNode.next;
                }

                if (minNode != current)
                {
                    T temp = current.item;
                    current.item = minNode.item;
                    minNode.item = temp;
                }

                current = current.next;
            }
        }

        public int Count { get { return count; } }

        public void Clear() { head = null; tail = null; count = 0; }

        public void Remove(T item)
        {
            Node current = head;

            while (current != null)
            {
                if (current.item.Equals(item))
                {
                    if (current.prev != null)
                    {
                        current.prev.next = current.next;
                    }
                    else
                    {
                        head = current.next;
                    }

                    if (current.next != null)
                    {
                        current.next.prev = current.prev;
                    }
                    else
                    {
                        tail = current.prev;
                    }

                    count--;
                    break;
                }

                current = current.next;
            }
        }

        public override string ToString()
        {
            string result = "Choosed Products:" + "\n";
            result += "-------------------" + "\n";
            Node current = head;

            for (int i = 0; i < count; i++)
            {
                result += " ----- Item " + i + "---- " + "\n" + current.item;

                current = current.next;
            }
            result += "\n" + "-------------------";
            return result;
        }

        public int Length() { return count; }
    }
    class Program
    {

        static void Main(string[] args)
        {
            LinkedListContainer<Product> ChoosedProdukts = new LinkedListContainer<Product>();
            while (true)
            {
                Console.WriteLine("Welcome to the Book Store!");
                Console.WriteLine("Please, choose an operation!");
                Console.WriteLine("1 -- Buy Books");
                Console.WriteLine("2 -- Buy Set of Bookmarks");
                Console.WriteLine("3 -- Buy Posters");
                Console.WriteLine("4 -- View Choosed Products");
                Console.WriteLine("5 -- Delete a Choosed Product");
                Console.WriteLine("6 -- Sort produkts by name");
                Console.WriteLine("7 -- Search a product");
                Console.WriteLine("0 -- Exit");

            MCH: int select = -1;
                try
                {
                    select = int.Parse(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Uncorrect choice. Please try one more time. (・・ )?");
                    goto MCH;
                }

                switch (select)
                {
                    case 1:
                        BuyBook(ChoosedProdukts);
                        Console.WriteLine("\n\n");
                        break;
                    case 2:
                        BuyBookmarksSet(ChoosedProdukts);
                        Console.WriteLine("\n\n");
                        break;
                    case 3:
                        BuyPosters(ChoosedProdukts);
                        Console.WriteLine("\n\n");
                        break;
                    case 4:
                        Console.Write(ChoosedProdukts);
                        Console.WriteLine("\n\n");
                        break;
                    case 5:
                        RemoveChoosedProdukts(ChoosedProdukts);
                        Console.WriteLine("\n\n");
                        break;

                    case 6:
                        ChoosedProdukts.SortProductsByName();
                        break;
                    case 7:
                        SearchBy(ChoosedProdukts);
                        Console.WriteLine("\n\n");
                        break;
                    case 0:
                        Console.WriteLine("Thank you for shopping with us!");
                        return;
                    default:
                        Console.WriteLine("Uncorrect choice. Please try one more time. (・・ )?");
                        break;
                }
            }
            Console.WriteLine("Thank you for shopping with us!");
            Console.ReadLine();

        }
        static void BuyBook(LinkedListContainer<Product> ChoosedProdukts)
        {
            Console.WriteLine("You`re choosing a book.");
            Console.WriteLine("Please enter the kind of book and genre (f.e. Book.Detective or Manga.Fantasy or Comics.Adventures):");
            string genre = Console.ReadLine();

            Console.WriteLine("Please enter the price of the book:");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Please enter the name of the book:");
            string bookName = Console.ReadLine();

            Console.WriteLine("Please enter the name of the author:");
            string authorName = Console.ReadLine();

            Console.WriteLine("Please, choose type of book!");
            Console.WriteLine("1 -- Single Book");
            Console.WriteLine("2 -- Book Series");
        BOOKTYPE: int select;
            try
            {
                select = int.Parse(Console.ReadLine());
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Oops.. Fail! Unknown product!(・・ )?");
                goto BOOKTYPE;
            }
            if (select == 1)
            {
                SingleBook book = new SingleBook(genre, price, bookName, authorName);
                Console.WriteLine("\n" + book + "\n");
                ChoosedProdukts.AddProduct(book);
            }
            else if (select == 2)
            {
                BookSeries book = new BookSeries(genre, price, bookName, authorName);
                Console.WriteLine("\n" + book + "\n");
                ChoosedProdukts.AddProduct(book);
            }
            else Console.WriteLine("Oops.. Fail! Unknown product!(・・ )?");
        }
        static void BuyBookmarksSet(LinkedListContainer<Product> ChoosedProdukts)
        {
            Console.WriteLine("You`re choosing a set of bookmarks.");
            Console.WriteLine("Please enter the name of the set (f.e. Spring):");
            string name = Console.ReadLine();

            Console.WriteLine("Please enter the price of the set:");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Please enter the colour of the set (f.e. Green):");
            string colour = Console.ReadLine();


            Console.WriteLine("Please enter the quantity of the bookmarks in set:");
            float quantity = float.Parse(Console.ReadLine());

            Console.WriteLine("Select type of bookmarks set!");
            Console.WriteLine("1 -- Translucent Bookmarks");
            Console.WriteLine("2 -- Opaque Bookmarks");
        BOOKMARKSTYPE: int select;
            try
            {
                select = int.Parse(Console.ReadLine());
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Oops.. Fail! Unknown product!(・・ )?");
                goto BOOKMARKSTYPE;
            }
            if (select == 1)
            {
                TranslucentBookmarks bookmarks = new TranslucentBookmarks(name, price, colour, quantity);
                Console.WriteLine("\n" + bookmarks + "\n");
                ChoosedProdukts.AddProduct(bookmarks);
            }
            else if (select == 2)
            {
                OpaqueBookmarks bookmarks = new OpaqueBookmarks(name, price, colour, quantity);
                Console.WriteLine("\n" + bookmarks + "\n");
                ChoosedProdukts.AddProduct(bookmarks);
            }
            else Console.WriteLine("Oops.. Fail! Unknown product!(・・ )?");
        }
        static void BuyPosters(LinkedListContainer<Product> ChoosedProdukts)
        {
            Console.WriteLine("You`re choosing posters.");
            Console.WriteLine("Please enter the name of poster`s fandom (f.e. Avengers):");
            string name = Console.ReadLine();

            Console.WriteLine("Please enter the price of the poster:");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Please enter the size of the corn bucket (S - A4 M - A3 L - A2 XL - A1 XXL - A0):");
            char size = char.Parse(Console.ReadLine());

            Console.WriteLine("Should your poster be cloured? ( Y or N )");
        POSTERSTYPE: char choice;
            try
            {
                choice = char.Parse(Console.ReadLine());
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Oops.. Fail! Unknown product!(・・ )?");
                goto POSTERSTYPE;
            }
            if (choice == 'Y')
            {
                Coloured poster = new Coloured(name, price, size);
                Console.WriteLine("\n" + poster + "\n");
                ChoosedProdukts.AddProduct(poster);
            }
            else if (choice == 'N')
            {
                UnColoured poster = new UnColoured(name, price, size);
                Console.WriteLine("\n" + poster + "\n");
                ChoosedProdukts.AddProduct(poster);
            }
            else Console.WriteLine("Oops.. Fail! Unknown product!(・・ )?");

        }
        static void RemoveChoosedProdukts(LinkedListContainer<Product> ChoosedProdukts)
        {
            Console.WriteLine("Enter the index of removing produkt:");
            int productToDelete;
            int.TryParse(Console.ReadLine(), out productToDelete);

            try
            {
                ChoosedProdukts.Remove(ChoosedProdukts.GetByIndex(productToDelete - 1));
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

        }
        static void SearchBy(LinkedListContainer<Product> ChoosedProdukts)
        {
            Console.WriteLine("Please, choose type of searching!");
            Console.WriteLine("1 -- Search by index");
            Console.WriteLine("2 -- Search by price");
            Console.WriteLine("3 -- Search by name");
            Console.WriteLine("0 -- Back");
        DCH: int choice = -1;
            try
            {
                choice = int.Parse(Console.ReadLine());
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Oops.. Fail! Unknown product!(・・ )?");
                goto DCH;
            }
            switch (choice)
            {
                case 1:
                    Console.Write("Input your index -- ");
                    int index = int.Parse(Console.ReadLine());
                    Console.WriteLine("\n" + ChoosedProdukts.GetByIndex(index - 1) + "\n");
                    break;

                case 2:
                    Console.Write("Input your price -- ");
                    decimal price = decimal.Parse(Console.ReadLine());
                    Console.WriteLine("\n" + ChoosedProdukts.GetByPrice(price) + "\n");
                    break;

                case 3:
                    Console.Write("Input your name -- ");
                    string name = Console.ReadLine();
                    Console.WriteLine("\n" + ChoosedProdukts.GetByName(name) + "\n");
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("\n\t\tOops.. Fail! Unknown product!(・・ )?\n");
                    break;
            }

        }
    }


}