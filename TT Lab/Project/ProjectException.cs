using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Project
{

    /// <summary>
    /// Occurs when a project didn't open correctly
    /// </summary>
    [Serializable]
    public class ProjectException : Exception
    {
        public ProjectException() { }
        public ProjectException(string message) : base(message) { }
        public ProjectException(string message, Exception inner) : base(message, inner) { }
        protected ProjectException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
