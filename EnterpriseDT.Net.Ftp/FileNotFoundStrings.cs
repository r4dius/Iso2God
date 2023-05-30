namespace EnterpriseDT.Net.Ftp;

public class FileNotFoundStrings : ServerStrings
{
    public const string NO_SUCH_FILE = "NO SUCH FILE";

    public const string CANNOT_FIND_THE_FILE = "CANNOT FIND THE FILE";

    public const string FAILED_TO_OPEN_FILE = "FAILED TO OPEN FILE";

    public const string COULD_NOT_GET_FILE = "COULD NOT GET FILE";

    public const string DOES_NOT_EXIST = "DOES NOT EXIST";

    public static string FILE_NOT_FOUND = "NOT FOUND";

    public FileNotFoundStrings()
    {
        Add(FILE_NOT_FOUND);
        Add("NO SUCH FILE");
        Add("CANNOT FIND THE FILE");
        Add("FAILED TO OPEN FILE");
        Add("COULD NOT GET FILE");
        Add("DOES NOT EXIST");
    }
}
