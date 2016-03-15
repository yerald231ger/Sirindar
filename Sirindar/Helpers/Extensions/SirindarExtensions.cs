using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using Sirindar.Core;
using Sirindar.Entity;

namespace Sirindar.Helpers.Extensions
{
    public static class SirindarExtensions
    {
        public static List<TSource> WhereIsActive<TSource>(this IEnumerable<TSource> tSource, Func<TSource, bool> predicate) where TSource : TableDbConventions
        {
            return tSource.Where(ts => ts.EsActivo).Where(predicate).ToList();
        }        

        public static List<TSource> WhereIsActive<TSource>(this IEnumerable<TSource> tSource) where TSource : TableDbConventions
        {
            return tSource.Where(ts => ts.EsActivo).ToList();
        }

        public static TSource FirstIsActive<TSource>(this IEnumerable<TSource> tSource, Func<TSource, bool> predicate) where TSource : TableDbConventions
        {
            return tSource.Where(ts => ts.EsActivo).First(predicate);
        }

        public static TSource FirstOrDefaultIsActive<TSource>(this IEnumerable<TSource> tSource, Func<TSource, bool> predicate) where TSource : TableDbConventions
        {
            return tSource.Where(ts => ts.EsActivo).FirstOrDefault(predicate);
        }
       
        public static IQueryable<T> IncludeIsActive<T, TProperty>(this IQueryable<T> tSource, Expression<Func<T, TProperty>> path) where T : TableDbConventions
        {
            return tSource.Where(ts => ts.EsActivo);
        }

        public static IEnumerable<TResult> SelectIsActive<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector) where TSource : TableDbConventions
        {
            return source.Where(s => s.EsActivo).Select(selector);
        }

        public static IEnumerable<TSource> SkipWhileIsActive<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) where TSource : TableDbConventions 
        {
            return source.Where(s => s.EsActivo).SkipWhile(predicate);
        }

        public static TEntity Read<TEntity>(this TEntity entity, int? id) where TEntity : TableDbConventions
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    var type = typeof(TEntity);
                    var tblName = type.Name.StrcutTableConvention();
                    var tblIdName = type.GetProperties().First(p => p.Name.Contains("Id")).Name;
                    var query = String.Format("Select * from {0} where {1} = {2}", tblName, tblIdName, id);
                    entity = (TEntity)db.Database.SqlQuery<TEntity>(query).FirstOrDefault<TEntity>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return entity;
        }

        public static TEntity Read<TEntity>(this TEntity entity) where TEntity : TableDbConventions
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    var type = typeof(TEntity);
                    var tblName = type.Name.StrcutTableConvention();
                    var tblIdName = type.GetProperties().First(p => p.Name.Contains("Id")).Name;
                    var tblIdValue = (int)type.GetProperty(tblIdName).GetValue(entity);
                    var query = String.Format("Select * from {0} where {1} = {2}", tblName, tblIdName, tblIdValue);
                    entity = (TEntity)db.Database.SqlQuery<TEntity>(query).FirstOrDefault<TEntity>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return entity;
        }

        public static bool Create<TEntity>(this TEntity entity) where TEntity : TableDbConventions
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    db.Entry(entity).State = EntityState.Added;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Update<TEntity>(this TEntity entity) where TEntity : TableDbConventions
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Delete<TEntity>(this TEntity entity) where TEntity : TableDbConventions
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    var type = typeof(TEntity);
                    var tblName = type.Name.StrcutTableConvention();
                    var tblIdName = type.GetProperties().First(p => p.Name.Contains("Id")).Name;
                    var tblIdValue = (int)type.GetProperty(tblIdName).GetValue(entity);
                    var query = String.Format("update {0} set EsActivo = 0 where {1} = {2}", tblName, tblIdName, tblIdValue);
                    db.Database.ExecuteSqlCommand(query);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static bool Delete<TEntity>(this TEntity entity, int? id) where TEntity : TableDbConventions
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    var type = typeof(TEntity);
                    var tblName = type.Name.StrcutTableConvention();
                    var tblIdName = type.GetProperties().First(p => p.Name.Contains("Id")).Name;
                    var query = String.Format("update {0} set EsActivo = 0 where {1} = {2}", tblName, tblIdName, id);
                    db.Database.ExecuteSqlCommand(query);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        
        private static string StrcutTableConvention(this string name)
        {
            var parts = new List<string>();
            parts.Add("Tbl");
            System.Threading.Tasks.Task.Run<bool>(() => true);
            for (int i = 1; i < name.Length - 1; i++)
            {
                int ci = name[i];
                if (ci >= 65 && ci <= 90)
                {
                    parts.Add(name.Substring(0, i));
                    parts[1] += AgregaPlural(parts[1].Last());
                    parts.Add(name.Substring(i, name.Length - i));
                    parts[2] += AgregaPlural(parts[2].Last());
                    return parts[0] + parts[1] + parts[2];
                }
            }
            var t = name.Substring(0, name.Length);
            parts.Add(t);
            parts[1] += AgregaPlural(parts[1].Last());
            return parts[0] + parts[1];
        }

        private static string AgregaPlural(char c)
        {
            if ((c == 'a') || (c == 'e') || (c == 'i') || (c == 'o') || (c == 'u'))
            {
                return "s";
            }
            else if (c != 's')
                return "es";
            else
                return "";
        }
    }

}