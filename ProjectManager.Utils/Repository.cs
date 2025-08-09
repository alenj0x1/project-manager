using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Utils
{
    public class Repository<T>
    {
        private readonly List<T> _data = [];

        public T Add(T value)
        {
			try
			{
                _data.Add(value);
				return value;
			}
			catch (Exception)
			{

				throw;
			}
        }

        public IQueryable<T> Queryable()
        {
            try
            {
                return _data.AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public T Update(T value)
        {
            try
            {
                Remove(value);
                Add(value);

                return value;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Remove(T value)
        {
            try
            {
                _data.Remove(value);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
