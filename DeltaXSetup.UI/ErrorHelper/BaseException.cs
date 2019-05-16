using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;

namespace DeltaXSetup.ErrorHelper
{
    /// <summary>  
    /// Api Exception  
    /// </summary>  
    [Serializable]
    [DataContract]
    public class BaseException : Exception, IExceptions
    {
        #region Public Serializable properties.  
        [DataMember]  
        public int ErrorCode { get; set; }  
        [DataMember]  
        public string ErrorDescription { get; set; }  
        [DataMember]  
        public HttpStatusCode HttpStatus { get; set; }  
          
        string reasonPhrase = "BaseException";  
  
        [DataMember]  
        public string ReasonPhrase  
        {  
            get { return this.reasonPhrase; }  
  
            set { this.reasonPhrase = value; }  
        }  
        #endregion

    }
}