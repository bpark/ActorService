using System.Collections.Generic;

namespace ActorService.Controllers
{
    public class ResultListDto<T>
    {
        public int TotalCount { get; }
        public IReadOnlyList<T> Items{ get; }

        public ResultListDto(int totalCount, IReadOnlyList<T> items)
        {
            TotalCount = totalCount;
            Items = items;
        }
    }
}