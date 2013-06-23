// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
	/// <summary>
	/// Represents a function that returns the sum of all items from a set of items.
	/// </summary>
    public class SumFunction : EnumerableSelectorAggregateFunction
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SumFunction"/> class.
		/// </summary>
		public SumFunction()
		{
		}

        /// <summary>
        /// Gets the the Min method name.
        /// </summary>
        /// <value><c>Min</c>.</value>
        protected internal override string AggregateMethodName
        {
            get
            {
                return "Sum";
            }
        }
	}
}
