using ChuanGoing.Storage.Dapper.Commands;
using ChuanGoing.Storage.Dapper.Filters;
using Dapper;
using System.Collections;
using System.Collections.Generic;

namespace ChuanGoing.Storage.MySql
{
    public static class MySqlCommandExtension
    {
        public static SqlCommand ToSqlCommand(this Filter filter, int index = 0)
        {
            var filterStr = string.Empty;
            var param = new DynamicParameters();
            if (filter is Equal fEqual)
            {
                if (fEqual.Value != null)
                {
                    filterStr = $"`{fEqual.Field}` {(!(fEqual.Value is string) && fEqual.Value is IEnumerable ? "IN" : "=")} @{fEqual.Field}_{index}";
                    param.Add($"{fEqual.Field}_{index}", fEqual.Value);
                }
            }
            else if (filter is Fuzzy fFuzzyMatch)
            {
                if (fFuzzyMatch.Value != null)
                {
                    switch (fFuzzyMatch.Type)
                    {
                        case FuzzyType.Left:
                            filterStr = $"`{fFuzzyMatch.Field}` LIKE @{fFuzzyMatch.Field}_{index}";
                            param.Add($"{fFuzzyMatch.Field}_{index}", $"%{fFuzzyMatch.Value}");
                            break;
                        case FuzzyType.Right:
                            filterStr = $"`{fFuzzyMatch.Field}` LIKE @{fFuzzyMatch.Field}_{index}";
                            param.Add($"{fFuzzyMatch.Field}_{index}", $"{fFuzzyMatch.Value}%");
                            break;
                        case FuzzyType.All:
                            filterStr = $"`{fFuzzyMatch.Field}` LIKE @{fFuzzyMatch.Field}_{index}";
                            param.Add($"{fFuzzyMatch.Field}_{index}", $"%{fFuzzyMatch.Value}%");
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (filter is Interval fInterval)
            {
                if (fInterval.Start != null && fInterval.End != null)
                {
                    var startName = $"{filter.Field}_{index}_Start";
                    var endName = $"{filter.Field}_{index}_End";
                    filterStr = $"(`{filter.Field}` BETWEEN @{startName} AND @{endName})";
                    param.Add(startName, fInterval.Start);
                    param.Add(endName, fInterval.End);
                }
                else if (fInterval.Start != null)
                {
                    filterStr = $"`{filter.Field}` > @{filter.Field}_{index}";
                    param.Add($"{filter.Field}_{index}", fInterval.Start);
                }
                else if (fInterval.End != null)
                {
                    filterStr = $"`{filter.Field}` < @{filter.Field}_{index}";
                    param.Add($"{filter.Field}_{index}", fInterval.End);
                }
            }
            else if (filter is FullText fFullText)
            {
                var wordStrs = new List<string>();
                foreach (var word in fFullText.Words)
                {
                    string modifier = string.Empty;
                    switch (word.Modifier)
                    {
                        case WordModifier.Essential:
                            modifier = "+";
                            break;
                        case WordModifier.Without:
                            modifier = "-";
                            break;
                        case WordModifier.Default:
                        default:
                            break;
                    }
                    if (!string.IsNullOrWhiteSpace(word.Value))
                    {
                        wordStrs.Add($"{modifier}{word.Value}");
                    }
                }
                if (wordStrs.Count > 0)
                {
                    filterStr = $"MATCH(`{fFullText.Field}`) AGAINST (@{fFullText.Field}_{index} IN BOOLEAN MODE)";
                    param.Add($"{fFullText.Field}_{index}", string.Join(" ", wordStrs));
                }
            }
            if (!string.IsNullOrEmpty(filterStr))
            {
                return new SqlCommand(filterStr, param);
            }
            else
            {
                return null;
            }
        }
    }
}
