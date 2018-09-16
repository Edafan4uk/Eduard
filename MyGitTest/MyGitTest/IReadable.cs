using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGitTest
{
    internal interface IReadable
    {
        List<IReadable> Read(string Path);
    }
}
