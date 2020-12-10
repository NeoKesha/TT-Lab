﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Project;

namespace TT_Lab.Command
{
    public class SaveProjectCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Boolean CanExecute(Object parameter)
        {
            return ProjectManagerSingleton.PM.ProjectOpened;
        }

        public void Execute(Object parameter = null)
        {
            throw new NotImplementedException();
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}