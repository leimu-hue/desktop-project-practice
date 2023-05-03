using System;

namespace IntelligentControl.attributes
{
    /// <summary>
    /// 数据库相关的属性特征
    /// </summary>
    internal class TableAttribute : Attribute
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string? TableName { get; set; }

    }
}
