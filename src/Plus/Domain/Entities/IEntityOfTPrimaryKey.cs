namespace Plus.Domain.Entities
{
    /// <summary>
    /// 为基本实体类型定义接口，系统中的所有实体都必须实现此接口
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        TPrimaryKey Id { get; set; }

        /// <summary>
        /// True => transient
        /// </summary>
        /// <returns></returns>
        bool IsTransient();
    }
}