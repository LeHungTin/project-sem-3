using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ProjectQT.Web.Areas.Admin.Models
{
    public class Reflection
    {
        public List<Type> GetController(string namespaces)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type) && type.Namespace.Contains(namespaces))
                .OrderBy(x => x.Name);
            return types.ToList();
                

        }
    }
}