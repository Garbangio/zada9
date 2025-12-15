using System;
using System.IO;
class Program
{
    static void Main()
    {
        if (!File.Exists("input.txt"))
        {
            using (StreamWriter writer = new StreamWriter("input.txt"))
            {
                writer.WriteLine("<HTML><body><H1>Hello</H1></body></HTML>");
                writer.WriteLine("<P><b>Привет</b></P>");
                writer.WriteLine("<div><H1>Test</H1></div>");
            }
            Console.WriteLine("Файл input.txt \n");
        }
        string[] lines = File.ReadAllLines("input.txt");
        MyArrayList<string> tags = new MyArrayList<string>();
        for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
        {
            string line = lines[lineIndex];
            string tag = "";
            bool insideTag = false;
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '<')
                {
                    insideTag = true;
                    tag = "<";
                }
                else if (c == '>' && insideTag == true)
                {
                    tag = tag + ">";
                    tags.Add(tag);
                    insideTag = false;
                    tag = "";
                }
                else if (insideTag == true)
                {
                    tag = tag + c;
                }
            }
        }
        MyArrayList<string> unique = new MyArrayList<string>();
        for (int i = 0; i < tags.Size(); i++)
        {
            string current = tags.Get(i);
            bool alreadyExists = false;
            for (int j = 0; j < unique.Size(); j++)
            {
                string existing = unique.Get(j);
                if (SameTag(current, existing) == true)
                {
                    alreadyExists = true;
                }
            }
            if (alreadyExists == false)
            {
                unique.Add(current);
            }
        }
        Console.WriteLine("Уникальные теги:");
        for (int i = 0; i < unique.Size(); i++)
        {
            Console.WriteLine(unique.Get(i));
        }
        using (StreamWriter writer = new StreamWriter("output.txt"))
        {
            for (int i = 0; i < unique.Size(); i++)
            {
                writer.WriteLine(unique.Get(i));
            }
        }
        Console.WriteLine("\n  V файл output.txt");
        Console.ReadKey();
    }
    static bool SameTag(string t1, string t2)
    {
        string s1 = "";
        string s2 = "";
        for (int i = 0; i < t1.Length; i++)
        {
            char c = t1[i];
            if (c != '/')
            {
                if (c >= 'A' && c <= 'Z')
                {
                    c = (char)(c + 32);
                }
                s1 = s1 + c;
            }
        }
        for (int j = 0; j < t2.Length; j++)
        {
            char c = t2[j];
            if (c != '/')
            {
                if (c >= 'A' && c <= 'Z')
                {
                    c = (char)(c + 32);
                }
                s2 = s2 + c;
            }
        }
        if (s1 == s2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
class MyArrayList<T>
{
    private T[] arr;
    private int count;
    public MyArrayList()
    {
        arr = new T[4];
        count = 0;
    }
    public void Add(T element)
    {
        if (count == arr.Length)
        {
            T[] newArr = new T[arr.Length * 2];
            for (int i = 0; i < arr.Length; i++)
            {
                newArr[i] = arr[i];
            }
            arr = newArr;
        }
        arr[count] = element;
        count = count + 1;
    }
    public T Get(int index)
    {
        if (index >= 0 && index < count)
        {
            return arr[index];
        }
        else
        {
            return default(T);
        }
    }
    public int Size()
    {
        return count;
    }
    public bool Contains(T o)
    {
        for (int i = 0; i < count; i++)
        {
            if (Equals(arr[i], o))
            {
                return true;
            }
        }
        return false;
    }
}