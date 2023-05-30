namespace EnterpriseDT.Net.Ftp;

public class DirectoryEmptyStrings : ServerStrings
{
	public static string NO_FILES = "NO FILES";

	public static string NO_SUCH_FILE_OR_DIR = "NO SUCH FILE OR DIRECTORY";

	public static string EMPTY_DIR = "EMPTY";

	public static string NO_DATA_SETS_FOUND = "NO DATA SETS FOUND";

	public DirectoryEmptyStrings()
	{
		Add(NO_FILES);
		Add(NO_SUCH_FILE_OR_DIR);
		Add(EMPTY_DIR);
		Add(NO_DATA_SETS_FOUND);
	}
}
