using System.Collections.Generic;

namespace Assembler.LineSources
{
    public class CommentStrippingLineSource : ILineSource
    {
        private readonly ILineSource source;

        public CommentStrippingLineSource(ILineSource source)
        {
            this.source = source;
        }

        public IEnumerable<string> Lines()
        {
            foreach (var line in source.Lines())
            {
                if (IsCommentLine(line))
                {
                    var trimmed = RemoveTrailingComments(line);
                    if (!string.IsNullOrWhiteSpace(trimmed))
                    {
                        ProcessedLines += 1;
                        yield return trimmed;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the count of processed lines
        /// </summary>
        public int ProcessedLines { get; private set;  }


        /// <summary>
        ///     Returns true if this line is a comment line. Ie the first char is either '#' or ';'
        /// </summary>
        /// <param name="line">The line to test</param>
        /// <returns>true if whole line is a comment, false otherwise</returns>
        private static bool IsCommentLine(string line)
        {
            return !line.StartsWith("#") && !line.StartsWith(";");
        }

        /// <summary>
        ///     Removes any comments appended to the end of the line and trims the result
        /// </summary>
        /// <param name="line">The line to process</param>
        /// <returns>The line minus any trailing comment, trimmed.</returns>
        private static string RemoveTrailingComments(string line)
        {
            var res = RemoveCommentIfPresent(line, "#");
            res = RemoveCommentIfPresent(res, ";");
            return res.Trim();
        }

        /// <summary>
        ///     If the line contains the specified comment symbol, trim off the text to the right of (and including)
        ///     the comment character (trimmed).
        /// </summary>
        /// <param name="line">The line to process</param>
        /// <param name="commentCharacter">The comment character</param>
        /// <returns>The line trimmed. Any comments present are removed</returns>
        private static string RemoveCommentIfPresent(string line, string commentCharacter)
        {
            var index = line.IndexOf(commentCharacter);
            if (index >= 0) line = line.Substring(0, index);
            return line;
        }
    }
}