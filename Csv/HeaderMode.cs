namespace Csv
{
    /// <summary>
    /// Indicates the presence (default) or absence of a header row.
    /// </summary>
    public enum HeaderMode
    {
        /// <summary>
        /// Indicates that the CSV file has a header row. (Default)
        /// </summary>
        HeaderPresent,

        /// <summary>
        /// Indicates that the CSV file does not have a header row.
        /// </summary>
        HeaderAbsent
    }
}