using System;
using System.Collections.Generic;

namespace Plus.Services.Dto
{
    [Serializable]
    public class PagedResultOutput<T> : PagedResultDto<T>, IOutputDto, IDto
    {
        public PagedResultOutput()
        {
        }

        public PagedResultOutput(int totalCount, IReadOnlyList<T> items)
            : base(totalCount, items)
        {
        }
    }
}