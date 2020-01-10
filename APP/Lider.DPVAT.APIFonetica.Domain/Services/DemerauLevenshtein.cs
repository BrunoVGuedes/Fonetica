using System;
using System.Collections.Generic;
using System.Text;

namespace Lider.DPVAT.APIFonetica.Domain.Services
{
    public class DemerauLevenshtein
    {
        public static int ComputeDistance(string source, string target)
        {
            if (string.IsNullOrEmpty(source))
            {
                if (string.IsNullOrEmpty(target))
                {
                    return 0;
                }
                else
                {
                    return target.Length;
                }
            }
            else if (string.IsNullOrEmpty(target))
            {
                return source.Length;
            }

            var score = new int[source.Length + 2, target.Length + 2];
            var inf = source.Length + target.Length;

            score[0, 0] = inf;

            for (var i = 0; i <= source.Length; i++)
            {
                score[i + 1, 1] = i; score[i + 1, 0] = inf;
            }

            for (var j = 0; j <= target.Length; j++)
            {
                score[1, j + 1] = j; score[0, j + 1] = inf;
            }

            var sd = new SortedDictionary<char, int>();

            foreach (var letter in (source + target))
            {
                if (!sd.ContainsKey(letter))
                    sd.Add(letter, 0);
            }

            for (var i = 1; i <= source.Length; i++)
            {
                var db = 0;

                for (var j = 1; j <= target.Length; j++)
                {
                    var i1 = sd[target[j - 1]];
                    var j1 = db;

                    if (source[i - 1] == target[j - 1])
                    {
                        score[i + 1, j + 1] = score[i, j];
                        db = j;
                    }
                    else
                    {
                        score[i + 1, j + 1] = Math.Min(score[i, j], Math.Min(score[i + 1, j], score[i, j + 1])) + 1;
                    }

                    score[i + 1, j + 1] = Math.Min(score[i + 1, j + 1], score[i1, j1] + (i - i1 - 1) + 1 + (j - j1 - 1));
                }

                sd[source[i - 1]] = i;
            }

            return score[source.Length + 1, target.Length + 1];
        }
    }
}
