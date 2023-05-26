using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Patterns.Query
{
    public interface IQueryHandler<TQuery, TCallBack> where TQuery : IQuery where TCallBack : IQueryCallback<object>
    {
        void Handle(TQuery query, TCallBack callBack);
    }
}
