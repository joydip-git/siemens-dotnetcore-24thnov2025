using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.DotNetCore.PmsApp.ServiceManager
{
    public class Mapper
    {
        public static TTarget Map<TSource, TTarget>(TSource sourceObject) where TSource : class where TTarget : class, new()
        {
            //TTarget? target = (TTarget?)Activator.CreateInstance(typeof(TTarget));
            TTarget target = new TTarget();

            Type sourceType = sourceObject.GetType();
            Type targetType = target.GetType();

            PropertyInfo[] sourceProperties = sourceType.GetProperties();
            PropertyInfo[] targetProperties = targetType.GetProperties();

            foreach (var sourceProp in sourceProperties)
            {
                foreach (var targetProp in targetProperties)
                {
                    if (sourceProp.PropertyType == targetProp.PropertyType && sourceProp.Name == targetProp.Name)
                    {
                        targetProp.SetValue(target, sourceProp.GetValue(sourceObject));
                        break;
                    }
                }
            }

            return target;
        }
    }
}
