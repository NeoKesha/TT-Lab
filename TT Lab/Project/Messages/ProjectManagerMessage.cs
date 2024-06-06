using System;
using System.Runtime.CompilerServices;

namespace TT_Lab.Project.Messages
{
    public class ProjectManagerMessage
    {
        public ProjectManagerMessage([CallerMemberName] string? propName = null)
        {
            PropertyName = propName ?? string.Empty;
        }

        public String PropertyName { get; set; }
    }
}
