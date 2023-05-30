using System.Collections;

namespace EnterpriseDT.Util;

internal class StringSplitter
{
    public static string[] Split(string str)
    {
        ArrayList arrayList = new ArrayList(str.Split(null));
        for (int num = arrayList.Count - 1; num >= 0; num--)
        {
            if (((string)arrayList[num]).Trim().Length == 0)
            {
                arrayList.RemoveAt(num);
            }
        }
        return (string[])arrayList.ToArray(typeof(string));
    }
}
