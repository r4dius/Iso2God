using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace EnterpriseDT.Util;

internal class PathUtil
{
    private const char SEPARATOR_CHAR = '/';

    private const string SAMEDIR_STRING = ".";

    private const string UPDIR_STRING = "..";

    public static char SeparatorChar => '/';

    public static string Separator => '/'.ToString();

    public static string Fix(string path)
    {
        return Implode(Fix(Explode(path)));
    }

    public static StringCollection Fix(StringCollection path)
    {
        if (path.Count == 0)
        {
            return path;
        }
        StringCollection stringCollection = new StringCollection();
        for (int i = 0; i < path.Count; i++)
        {
            string text = path[i];
            if (text == Separator)
            {
                if (i == 0 || (stringCollection.Count > 0 && stringCollection[stringCollection.Count - 1] != Separator))
                {
                    stringCollection.Add(text);
                }
            }
            else if (text == "..")
            {
                if (stringCollection.Count == 1)
                {
                    throw new ArgumentException("Cannot change up a directory from the root");
                }
                if (stringCollection.Count >= 2 && stringCollection[stringCollection.Count - 2] != "..")
                {
                    stringCollection.RemoveAt(stringCollection.Count - 1);
                    stringCollection.RemoveAt(stringCollection.Count - 1);
                }
                else
                {
                    stringCollection.Add(text);
                }
            }
            else if (text != "" && text != ".")
            {
                stringCollection.Add(text);
            }
        }
        while (stringCollection.Count > 1 && stringCollection[stringCollection.Count - 1] == Separator)
        {
            stringCollection.RemoveAt(stringCollection.Count - 1);
        }
        if (stringCollection.Count == 0)
        {
            stringCollection.Add(".");
        }
        return stringCollection;
    }

    public static bool IsAbsolute(string path)
    {
        return path?.StartsWith("/") ?? false;
    }

    public static string GetFileName(string path)
    {
        if (path.IndexOf('/'.ToString()) >= 0)
        {
            return path.Substring(path.LastIndexOf('/') + 1);
        }
        return path;
    }

    public static string GetFolderPath(string path)
    {
        if (path.IndexOf('/'.ToString()) >= 0)
        {
            return path.Substring(0, path.LastIndexOf('/'));
        }
        return path;
    }

    public static string Combine(string pathLeft, string pathRight)
    {
        if (pathLeft == null)
        {
            pathLeft = "/";
        }
        switch (pathRight)
        {
            case null:
            case "":
            case ".":
                return pathLeft;
            default:
                if (pathRight.StartsWith(Separator))
                {
                    throw new ArgumentException("Second argument cannot be absolute", "pathRight");
                }
                if (pathLeft.EndsWith(Separator))
                {
                    pathLeft = pathLeft.Substring(0, pathLeft.Length - 1);
                }
                return Fix(pathLeft + Separator + pathRight);
        }
    }

    public static string Combine(string path1, string path2, params string[] pathN)
    {
        string text = Combine(path1, path2);
        foreach (string pathRight in pathN)
        {
            text = Combine(text, pathRight);
        }
        return text;
    }

    public static StringCollection Explode(string path)
    {
        StringCollection stringCollection = new StringCollection();
        int num = 0;
        while (true)
        {
            int num2 = path.IndexOf(SeparatorChar, num);
            if (num2 < 0)
            {
                break;
            }
            if (num == num2)
            {
                stringCollection.Add(Separator);
                num = num2 + 1;
            }
            else
            {
                stringCollection.Add(path.Substring(num, num2 - num));
                num = num2;
            }
        }
        if (num < path.Length)
        {
            stringCollection.Add(path.Substring(num));
        }
        return stringCollection;
    }

    public static string Implode(IEnumerable pathElements, int start, int length)
    {
        StringBuilder stringBuilder = new StringBuilder();
        int num = 0;
        foreach (string pathElement in pathElements)
        {
            if (num >= start && (length < 0 || num < start + length))
            {
                stringBuilder.Append(pathElement);
            }
            num++;
        }
        return stringBuilder.ToString();
    }

    public static string Implode(IEnumerable pathElements, int start)
    {
        return Implode(pathElements, start, -1);
    }

    public static string Implode(IEnumerable pathElements)
    {
        return Implode(pathElements, 0, -1);
    }
}
