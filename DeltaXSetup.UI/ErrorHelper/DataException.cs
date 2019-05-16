using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Net;

namespace DeltaXSetup.ErrorHelper
{
    /// <summary>  
    ///  Data Exception  
    /// </summary>  
    [Serializable]
    [DataContract]
    public class DataException: Exception, IExceptions
    {
        #region Public Serializable properties.
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string ErrorDescription { get; set; }
        [DataMember]
        public HttpStatusCode HttpStatus { get; set; }

        string reasonPhrase = "DataException";

        [DataMember]
        public string ReasonPhrase
        {
            get { return this.reasonPhrase; }

            set { this.reasonPhrase = value; }
        }

        #endregion

        #region Public Constructor.
        /// <summary>  
        /// Public constructor for Data Exception  
        /// </summary>  
        /// <param name="errorCode"></param>  
        /// <param name="errorDescription"></param>  
        /// <param name="httpStatus"></param>  
        public DataException(int errorCode, string errorDescription, HttpStatusCode httpStatus)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
            HttpStatus = httpStatus;
        }
        #endregion 
    }
}