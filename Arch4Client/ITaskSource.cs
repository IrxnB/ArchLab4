using Arch4Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch4Client
{
    internal interface ITaskSource
    {
        List<TaskEntity> ReadAll();

        void Create(TaskEntity? task);

        void Update(TaskEntity? task);

        void Delete(long? id);
    }
}
