using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Utils
{
    public static class Parser
    {
        public static Guid? ToGuid(string value)
        {
			try
			{
				return Guid.Parse(value);
			}
			catch (Exception)
			{
				return null;
			}
        }
    }
}
