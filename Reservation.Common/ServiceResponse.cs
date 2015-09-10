using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Common
{
    
    /// <summary>
    /// Generic response class for all the reservices
    /// </summary>
    /// <typeparam name="T">The type of which the result is desired for a successful operation</typeparam>
    public class ServiceResponse<T>
    {
        /// <summary>
        /// This property tells whether Operation was successful or not
        /// </summary>
        public virtual bool OperationSuccess { get; set; }


        /// <summary>
        /// Service return message
        /// </summary>
        public virtual string ServiceMessage { get; set; }

        /// <summary>
        /// return the error message
        /// </summary>
        public virtual string ErrorMessage { get; set; }

        /// <summary>
        /// If the OperationSuccess is false, then this property returns the list of validation error messages
        /// </summary>
        public virtual List<string> ValidationMessages { get; set; }

        /// <summary>
        /// If the OperationSuccess is true, then this property returns the result of the operation i.e. the desired return value
        /// </summary>
        public T Result { get; set; }

    }
}
