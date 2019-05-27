using System;

namespace Plus.Domain.Uow
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IActiveUnitOfWork, IUnitOfWorkCompleteHandle, IDisposable
    {
        string Id { get; }

        IUnitOfWork Outer { get; set; }

        void Begin(UnitOfWorkOptions options);
    }
}