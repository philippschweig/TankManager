using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFSresources.App.Update
{
    public interface IUpdate
    {
        bool IsUpdateable();
        void Update(bool silent = false);
    }
}
