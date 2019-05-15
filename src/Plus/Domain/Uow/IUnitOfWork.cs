using System;
using System.Collections.Generic;
using System.Text;

namespace Plus.Domain.Uow
{
    public interface IUnitOfWork : IActiveUnitOfWork, IUnitOfWorkCompleteHandle
    {

    }
}