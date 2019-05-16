using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolver
{
    public interface IRegisterComponent
    {
        /// <summary>  
        /// Register type method  
        /// </summary>  
        /// <typeparam name="TFrom"></typeparam>  
        /// <typeparam name="TTo"></typeparam>  
        /// <param name="withInterception"></param>  
        void RegisterType<TFrom, TTo>(bool withInterception = false) where TTo : TFrom;
        /// <summary>  
        /// Register type with container controlled life time manager  
        /// </summary>  
        /// <typeparam name="TFrom"></typeparam>  
        /// <typeparam name="TTo"></typeparam>  
        /// <param name="withInterception"></param>  
        void RegisterTypeWithControlledLifeTime<TFrom, TTo>(bool withInterception = false) where TTo : TFrom;
    }

    /// <summary>  
    /// Register underlying types with unity.  
    /// </summary>  
    public interface IComponent
    {
        void SetUp(IRegisterComponent registerComponent);
    } 
}
