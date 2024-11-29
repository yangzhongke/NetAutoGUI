// A very simple wildcard match
// https://github.com/picrap/WildcardMatch

namespace WildcardMatch
{
    using System.Linq;

    /// <summary>
    /// Extensions to <see cref="System.String"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Tells if the given string matches the given wildcard.
        /// Two wildcards are allowed: '*' and '?'
        /// '*' matches 0 or more characters
        /// '?' matches any character
        /// </summary>
        /// <param name="wildcard">The wildcard.</param>
        /// <param name="s">The s.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns></returns>
        public static bool WildcardMatch(this string wildcard, string s, bool ignoreCase = false)
        {
            return WildcardMatch(wildcard, s, 0, 0, ignoreCase);
        }

        /// <summary>
        /// Internal matching algorithm.
        /// </summary>
        /// <param name="wildcard">The wildcard.</param>
        /// <param name="s">The s.</param>
        /// <param name="wildcardIndex">Index of the wildcard.</param>
        /// <param name="sIndex">Index of the s.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns></returns>
        private static bool WildcardMatch(this string wildcard, string s, int wildcardIndex, int sIndex, bool ignoreCase)
        {
            for (; ; )
            {
                // in the wildcard end, if we are at tested string end, then strings match
                if (wildcardIndex == wildcard.Length)
                    return sIndex == s.Length;

                var c = wildcard[wildcardIndex];
                switch (c)
                {
                    // always a match
                    case '?':
                        break;
                    case '*':
                        // if this is the last wildcard char, then we have a match, whatever the tested string is
                        if (wildcardIndex == wildcard.Length - 1)
                            return true;
                        // test if a match follows
                        return Enumerable.Range(sIndex, s.Length - sIndex).Any(i => WildcardMatch(wildcard, s, wildcardIndex + 1, i, ignoreCase));
                    default:
                        var cc = ignoreCase ? char.ToLower(c) : c;
                        if (s.Length == sIndex)
                        {
                            return false;
                        }
                        var sc = ignoreCase ? char.ToLower(s[sIndex]) : s[sIndex];
                        if (cc != sc)
                            return false;
                        break;
                }

                wildcardIndex++;
                sIndex++;
            }
        }
    }
}