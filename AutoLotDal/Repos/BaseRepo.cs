using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using AutoLotDal.EF;
using AutoLotDal.Models.Base;

namespace AutoLotDal.Repos
{
    public class BaseRepo<T> : IDisposable, IRepo<T> where T:EntityBase, new()
    {
        private readonly DbSet<T> _table;
        private readonly AutoLotEntities _db;
        public  AutoLotEntities Context { get; }

        public BaseRepo()
        {
            _db=new AutoLotEntities();
            _table = _db.Set<T>();
            Context = _db;
        }

        internal int SaveChanges()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                //parallelism error
                Console.WriteLine(e);
                throw;
            }
            catch (DbUpdateException e)
            {
                //error of update. Check inner exception.
                Console.WriteLine(e);
                throw;
            }
            catch (CommitFailedException e)
            {
                //error of transaction
                Console.WriteLine(e);
                throw;
            }
            catch (Exception e)
            {
                //other exception
                Console.WriteLine(e);
                throw;
            }
        }
        public void Dispose()
        {
            _db?.Dispose();
        }

        public int Add(T entity)
        {
            _table.Add(entity);
            return SaveChanges();
        }

        public int AddRange(IList<T> entities)
        {
            _table.AddRange(entities);
            return SaveChanges();
        }

        public int Save(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }

        public int Delete(int id, byte[] timeStanp)
        {
            _db.Entry(new T() {Id = id, Timestamp = timeStanp}).State = EntityState.Deleted;
            return SaveChanges();
        }
        
        public int Delete(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();        }

        public T GetOne(int? id) => _table.Find(id);
        
        public virtual List<T> GetAll() => _table.ToList();

        public List<T> ExecuteQuery(string sql) => _table.SqlQuery(sql).ToList();

        public List<T> ExecuteQuery(string sql, object[] sqlParametersObjects) =>
            _table.SqlQuery(sql, sqlParametersObjects).ToList();
        
    }
}