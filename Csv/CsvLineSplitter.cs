using System.Collections.Generic;
using System.Text.RegularExpressions;

#if NETCOREAPP3_1 || NETSTANDARD2_1
using MemoryText = System.ReadOnlyMemory<char>;
using SpanText = System.ReadOnlySpan<char>;
#else
using System; // NOTE: Used for Tuple
using MemoryText = System.String;
using SpanText = System.String;
#endif

namespace Csv
{
    /// <summary>
    /// Splits a single line (multiline handling is done independently) into multiple parts
    /// </summary>
    internal sealed class CsvLineSplitter
    {
#if NETCOREAPP3_1 || NETSTANDARD2_1
        private static readonly Dictionary<(char Separator, bool AllowSingleQuoteToEncloseFieldValues), CsvLineSplitter> splitterCache = new Dictionary<(char, bool), CsvLineSplitter>();
#else
        private static readonly Dictionary<Tuple<char, bool>, CsvLineSplitter> splitterCache = new Dictionary<Tuple<char, bool>, CsvLineSplitter>();
#endif

        private static readonly object syncRoot = new object();

        private readonly char separator;
        private readonly Regex splitter;

        private CsvLineSplitter(char separator, Regex splitter)
        {
            this.separator = separator;
            this.splitter = splitter;
        }

        public static CsvLineSplitter Get(CsvOptions options)
        {
            CsvLineSplitter? splitter;
            lock (syncRoot)
            {
#if NETCOREAPP3_1 || NETSTANDARD2_1
                var key = (options.Separator, options.AllowSingleQuoteToEncloseFieldValues);
#else
                var key = Tuple.Create(options.Separator, options.AllowSingleQuoteToEncloseFieldValues);
#endif
                if (!splitterCache.TryGetValue(key, out splitter))
                    splitterCache[key] = splitter = Create(options);
            }

            return splitter;
        }

        private static CsvLineSplitter Create(CsvOptions options)
        {
            const string patternEscape = @"(?>(?(IQ)(?(ESC).(?<-ESC>)|\\(?<ESC>))|(?!))|(?(IQ)\k<QUOTE>(?<-IQ>)|(?<=^|{0})(?<QUOTE>[{1}])(?<IQ>))|(?(IQ).|[^{0}]))+|^(?={0})|(?<={0})(?={0})|(?<={0})$";
            const string patternNoEscape = @"(?>(?(IQ)\k<QUOTE>(?<-IQ>)|(?<=^|{0})(?<QUOTE>[{1}])(?<IQ>))|(?(IQ).|[^{0}]))+|^(?={0})|(?<={0})(?={0})|(?<={0})$";
            var separator = Regex.Escape(options.Separator.ToString());
            var quoteChars = options.AllowSingleQuoteToEncloseFieldValues ? "\"'" : "\"";
            // Since netstandard1.0 doesn't include RegexOptions.Compiled, we include it by value (in case the target platform supports it)
            const RegexOptions regexOptions = RegexOptions.Singleline | ((RegexOptions/*.Compiled*/)8);
            if (options.AllowBackSlashToEscapeQuote)
                return new CsvLineSplitter(options.Separator, new Regex(string.Format(patternEscape, separator, quoteChars), regexOptions));
            return new CsvLineSplitter(options.Separator, new Regex(string.Format(patternNoEscape, separator, quoteChars), regexOptions));
        }

        public static bool IsUnterminatedQuotedValue(SpanText value, CsvOptions options)
        {
            if (value.Length == 0)
                return false;

            char quoteChar;
            if (value[0] == '"')
            {
                quoteChar = '"';
            }
            else if (options.AllowSingleQuoteToEncloseFieldValues && value[0] == '\'')
            {
                quoteChar = '\'';
            }
            else
            {
                return false;
            }

            var regex = options.AllowBackSlashToEscapeQuote ? $@"\\?{quoteChar}+$" : $@"{quoteChar}+$";
#if NETCOREAPP3_1 || NETSTANDARD2_1
            var trailingQuotes = StringHelpers.RegexMatch(value[1..], regex);
#else
            var trailingQuotes = StringHelpers.RegexMatch(value.Substring(1), regex);
#endif
            // if the first trailing quote is escaped, ignore it
            if (options.AllowBackSlashToEscapeQuote && trailingQuotes.StartsWith("\\"))
            {
#if NETCOREAPP3_1 || NETSTANDARD2_1
                trailingQuotes = trailingQuotes[2..];
#else
                trailingQuotes = trailingQuotes.Substring(2);
#endif
            }
            // the value is properly terminated if there are an odd number of unescaped quotes at the end
            return trailingQuotes.Length % 2 == 0;
        }

        public IList<MemoryText> Split(MemoryText line, CsvOptions options)
        {
            var matches = splitter.Matches(line.AsString());
            var values = new List<MemoryText>(matches.Count);
            var p = -1;
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < matches.Count; i++)
            {
#if NETCOREAPP3_1 || NETSTANDARD2_1
                var value = line.Slice(matches[i].Index, matches[i].Length);
#else
                var value = matches[i].Value;
#endif
                if (p >= 0 && IsUnterminatedQuotedValue(values[p].AsSpan(), options))
                {
                    values[p] = StringHelpers.Concat(values[p], separator.ToString(), value);
                }
                else
                {
                    values.Add(value);
                    p++;
                }
            }
            return values;
        }
    }
}