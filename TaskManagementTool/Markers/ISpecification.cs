using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementTool.Core.Markers
{
    public interface ISpecification<T>
    {
        /// <summary>
        /// Number of records within the page
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Non-Zero based index of the page
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// Expression to be executed to fetch the data
        /// </summary>
        /// <returns></returns>
        public abstract Expression<Func<T, bool>> ToExpression();
        public bool ValidateSpec(T entity);
        public ISpecification<T> And (ISpecification<T> specification);
        public ISpecification<T> Or(ISpecification<T> specification);
    }
}
