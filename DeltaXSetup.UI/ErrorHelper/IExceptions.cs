using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace DeltaXSetup.ErrorHelper
{
    /// <summary>  
    /// IExceptions Interface  
    /// </summary>
    public interface IExceptions
    {
        /// <summary>  
        /// ErrorCode  
        /// </summary>  
        int ErrorCode { get; set; }
        /// <summary>  
        /// ErrorDescription  
        /// </summary>  
        string ErrorDescription { get; set; }
        /// <summary>  
        /// HttpStatus  
        /// </summary>  
        HttpStatusCode HttpStatus { get; set; }
        /// <summary>  
        /// ReasonPhrase  
        /// </summary>  
        string ReasonPhrase { get; set; }
    }
}