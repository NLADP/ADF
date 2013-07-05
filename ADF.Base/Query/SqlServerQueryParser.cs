using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Adf.Base.Data;
using Adf.Core.Data;
using Adf.Core.Domain;
using Adf.Core.Query;
using Adf.Core.Extensions;

namespace Adf.Base.Query
{
    public class SqlServerQueryParser : IQueryParser
    {
        /// <summary>
        /// 0 = From
        /// 1 = Where
        /// 2 = Select
        /// 3 = Top
        /// 4 = Distinct
        /// 5 = Join
        /// 6 = Order By
        /// 7 = Group By
        /// </summary>
        private const string _select = "SELECT {4} {3} {2} FROM {0} {5} {1} {6} {7}";
        private const string _delete = "DELETE {0} FROM {0} {5} {1}";
        private const string _count = "SELECT COUNT(*) FROM {0} {5} {1}";
        
        public DataSourceType Type { get { return DataSourceType.SqlServer;  } }

        #region Select

        private static string ParseStatement(IAdfQuery query)
        {
            var result = string.Empty;
 
            if (query.QueryType == QueryType.Free) result = string.Empty;
            if (query.QueryType == QueryType.StoredProcedure) result = string.Empty;
            if (query.QueryType == QueryType.Delete) result = _delete;
            if (query.QueryType == QueryType.Count) result = _count;
            if (query.QueryType == QueryType.Select) result = _select;

            return result;
        }

        private static string ParseSelect(IAdfQuery query)
        {
            return query.Selects.Count == 0 ? query.Tables[0].EscapeFunction() + ".*" : string.Join(", ", query.Selects.Select(e => e.FullName()));
        }

        #endregion Select

        #region From

        private static string ParseTables(IEnumerable<ITable> tables)
        {
            return string.Join(", ", tables.Select(table => table.FullName()));
        }

        private static string ParseJoins(IEnumerable<IJoin> joins)
        {
            return String.Join(" ", joins.Select(j => string.Format(CultureInfo.InvariantCulture,
                                                                    "{0} JOIN {1} ON {2} = {3}",
                                                                    j.Type.Name,
                                                                    j.JoinColumn.Table.FullName(),
                                                                    j.JoinColumn.FullName(),
                                                                    j.SourceColumn.FullName())));
        }

        #endregion From

        #region Where

        private string ParseWheres(IList<IWhere> wheres)
        {
            const string _where = "WHERE {0}";
            const string condition = "{3} {4}{0} {1} {2}{5}";
            const string conditionfirst = "{4}{0} {1} {2}{5} ";

            ParseParameters(wheres);

            wheres = wheres.Where(where => where.Parameter.Type != ParameterType.QueryParameter).ToList();  // query parameters should not be included in where

            var first = wheres.FirstOrDefault();

            if (first == null) return string.Empty;
            
            var result = ParseWhere(conditionfirst, first);

            result += string.Join(" ", wheres.Skip(1).Select(where => ParseWhere(condition, where)));

            return string.Format(CultureInfo.InvariantCulture, _where, result);
        }

        private string ParseWhere(string format, IWhere where)
        {
            string parameter;

            if (where.Parameter.Type == ParameterType.Query)
            {
                parameter = string.Format(CultureInfo.InvariantCulture, "({0})", Parse(where.Parameter.Value as IAdfQuery));   // parse inner query
            }
            else if (where.Parameter.Type == ParameterType.ValueList)
            {
                var names = String.Join(",",
                                        ((IEnumerable) where.Parameter.Value).Cast<object>().Select((v, i) => string.Format("@{0}_v{1}", where.Parameter.Name, i)));

                // when list is empty this clause should not return results
                if (string.IsNullOrEmpty(names)) return string.Format(CultureInfo.InvariantCulture, format, 1, "=", 0, where.Predicate, "", "");    

                parameter = string.Format(CultureInfo.InvariantCulture, "({0})", names);
            }
            else
            {
                parameter =  (where.Parameter.Type == ParameterType.Column)
                                ? ((IColumn) where.Parameter.Value).FullName()
                                : string.Format(CultureInfo.InvariantCulture, "@{0}", where.Parameter.Name);

                if (where.Operator == OperatorType.IsNotEqualOrIsNull)
                {
                    // Surround this clause with brackets
                    where.OpenBracket++;
                    where.CloseBracket++;
                    // Add the check on NULL so that also records that are NULL are returned
                    parameter += string.Format(CultureInfo.InvariantCulture, " OR {0} IS NULL", where.Column.FullName());
                }
            }

            // NULL and NOT NULL are rendered different and dont use a parameter
            if (where.Operator == OperatorType.IsNull || where.Operator == OperatorType.IsNotNull)
            {
                parameter = null;
            }

            return string.Format(CultureInfo.InvariantCulture,
                                 format,
                                 where.Column.FullName(),
                                 where.Operator.Value,
                                 parameter,
                                 where.Predicate,
                                 "".PadLeft(where.OpenBracket, '('),
                                 "".PadLeft(where.CloseBracket, ')'));
        }

        private static IEnumerable<IWhere> GetAndConfigureInnerWheres(IList<IWhere> wheres, int paramExtension = 0)
        {
            if (wheres == null || wheres.Count == 0) return new List<IWhere>();

            var allWheres = new List<IWhere>();
            var subQueries = wheres.Where(w => w.Parameter.Type == ParameterType.Query)
                                   .Select(w => (IAdfQuery) w.Parameter.Value).ToList();
            var innerparams = wheres.Where(w => w.Parameter.Type == ParameterType.Query)
                                    .SelectMany(w => ((IAdfQuery)w.Parameter.Value).Wheres).ToList();

            foreach(var query in subQueries)
            {
                query.RenameFunctionParameters(++paramExtension);
                query.RenameJoinFunctionParameters(paramExtension);
            }

            if (innerparams.Count > 0)
            {
                allWheres.AddRange(innerparams);
                allWheres.AddRange(GetAndConfigureInnerWheres(innerparams, ++paramExtension));
            }

            return allWheres;
        }

        private static void ParseParameters(IList<IWhere> wheres)
        {
            var index = 1;
            var list = new List<string>();

            var innerparams = GetAndConfigureInnerWheres(wheres);

            // NULL and NOT NULL are rendered different and dont use a parameter
            var allWheres = wheres.Concat(innerparams).Where(w => w.Operator != OperatorType.IsNull && w.Operator != OperatorType.IsNotNull);

            foreach (var where in allWheres)
            {
                if (where.Parameter.Name.IsNullOrEmpty())
                {
                    where.Parameter.Name = where.Column.ColumnName;
                }

                if (list.Contains(where.Parameter.Name))
                {
                    where.Parameter.Name += index++;
                }

                list.Add(where.Parameter.Name);
                
                ApplyLike(where);
            }
        }

        private static void ApplyLike(IWhere where)
        {
            var text = where.Parameter.Value as string;

            if (string.IsNullOrEmpty(text) || text.Contains("%")) return;

            if (where.Operator == OperatorType.Like) where.Parameter.Value = string.Format(CultureInfo.InvariantCulture, "%{0}%", text);
            else if (where.Operator == OperatorType.LikeRight) where.Parameter.Value = string.Format(CultureInfo.InvariantCulture, "%{0}", text);
            else if (where.Operator == OperatorType.LikeLeft) where.Parameter.Value = string.Format(CultureInfo.InvariantCulture, "{0}%", text);
        }

        #endregion Where

        private static string ParseOrderBy(List<IOrderBy> orderBys)
        {
            if (orderBys.Count == 0) return string.Empty;

            return "ORDER BY " + String.Join(", ", orderBys.Select(o => string.Format(CultureInfo.InvariantCulture, "{0} {1}", o.Column.FullName(), o.SortOrder == SortOrder.Descending ? "DESC" : "ASC")));
        }

        private static string ParseGroupBy(ICollection<IExpression> groupBys)
        {
            if (groupBys.Count == 0) return string.Empty;

            return "GROUP BY " + string.Join(", ", groupBys.Select(e => e.FullName()));
        }

        public string Parse(IAdfQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return query.QueryType == QueryType.Insert
                       ? ParseInsert(query)
                       : query.QueryType == QueryType.Update
                             ? ParseUpdate(query)
                             : ParseQuery(query);
        }

        private string ParseInsert(IAdfQuery query)
        {
            var from = ParseTables(query.Tables);

            var wheres = query.Wheres.Where(w => w.Parameter.Type == ParameterType.QueryParameter).ToList();

            var columns = string.Join(", ", wheres.Select(w => w.Column.EncloseInBrackets()));

            ParseWheres(query.Wheres);      // give the parameters a name

            var parameters = string.Join(", ", wheres.Select(w => w.Parameter.Value == null ? "NULL" : "@" + w.Parameter.Name));

            var result = string.Format("INSERT INTO {0} ({1}) VALUES({2})", from, columns, parameters);
#if DEBUG
            Debug.WriteLine(DebugQueryString(query, result));
#endif
            return result;
        }

        private string ParseUpdate(IAdfQuery query)
        {
            var from = ParseTables(query.Tables);

            var where = ParseWheres(query.Wheres);

            var sets = string.Join(", ",
                                   query.Wheres
                                       .Where(w => w.Parameter.Type == ParameterType.QueryParameter)
                                       .Select(
                                           w => string.Format("{0} = {1}", w.Column.EncloseInBrackets(),
                                                         w.Parameter.Value == null ? "NULL" : "@" + w.Parameter.Name)));

            var result = string.Format("UPDATE {0} SET {1} {2}", from, sets, where);
#if DEBUG
            Debug.WriteLine(DebugQueryString(query, result));
#endif
            return result;
        }

        private string ParseQuery(IAdfQuery query)
        {
            var statement = ParseStatement(query);
            var distinct = (query.Distinct == null) ? string.Empty : "DISTINCT";
            var top = (query.Top == null) ? string.Empty : string.Format(CultureInfo.InvariantCulture, "TOP ({0})", query.Top);

            var select = ParseSelect(query);

            var from = ParseTables(query.Tables);

            var joins = ParseJoins(query.Joins);

            var where = ParseWheres(query.Wheres);

            var orderby = ParseOrderBy(query.OrderBys);

            var groupby = ParseGroupBy(query.GroupBys);

            var result = string.Format(CultureInfo.InvariantCulture, statement, from, where, select, top, distinct, joins, orderby, groupby);
#if DEBUG
            Debug.WriteLine(DebugQueryString(query, result));
#endif            
            return result;
        }

        public string DebugQueryString(IAdfQuery query, string result = null)
        {
            var querystring = new StringBuilder(result ?? Parse(query));

            var wheres = query.Wheres.OrderByDescending(w => w.Parameter.Name);

            foreach (var where in wheres)
            {
                if (@where.Parameter.Name.IsNullOrEmpty()) continue;

                var parValue = @where.Parameter.Value;
                string value = parValue == null ? "null"
                                   : (parValue is IEnumerable && !(parValue is string) ? string.Join(",", ((IEnumerable)parValue).Cast<object>().Select(o => o.ToString()))
                                   : parValue is DateTime ? string.Format("'{0}'", ((DateTime)parValue).ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss"))
                                   : string.Format("'{0}'", parValue));
                querystring.Replace("@" + @where.Parameter.Name, value);
            }
            return querystring.ToString();
        }
    }

    public static class SqlDescriberExtensions
    {
        public static string FullName(this IColumn column)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}.[{1}]", column.Table.EscapeFunction(), column.ColumnName);
        }

        public static string EncloseInBrackets(this IColumn column)
        {
            return string.Format(CultureInfo.InvariantCulture, "[{0}]", column.ColumnName);
        }

        public static string FullName(this IExpression expression)
        {
            var column = expression.Column.FullName();

            if (expression.Type == ExpressionType.Column) return column;

            if (expression.Type == ExpressionType.Table)
                return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", expression.Column.Table.EscapeFunction(), expression.Column.ColumnName);

            return string.Format(CultureInfo.InvariantCulture, "{0}({1}){2}", expression.Type, column, expression.Alias);
        }

        public static string FullName(this ITable table)
        {
            // don't put [] around if it's a function
            return table.Name.IndexOf("(") > 0 ? table.Name : string.Format(CultureInfo.InvariantCulture, "[{0}]", table.Name);
        }

        public static string EscapeFunction(this ITable table)
        {
            var indexOf = table.Name.IndexOf('(');                   // check if this is a function

            var name = indexOf == -1 ? table.Name : table.Name.Substring(0, indexOf); // remove () part

            return string.Format(CultureInfo.InvariantCulture, "[{0}]", name);
        }

        public static IEnumerable<string> GetParameters(this ITable table)
        {
            var indexOf = table.Name.IndexOf('(');

            if(indexOf == -1) return new List<string>();

            return table.Name.Substring(indexOf + 1, table.Name.IndexOf(')') - indexOf - 1).Split(',').Select(s => s.Trim());
        }
    }
}
