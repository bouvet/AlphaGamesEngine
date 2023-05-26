using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Patterns.Query
{
    public interface IQueryCallback<TResponse>
    {
        void OnSuccess(TResponse response);
        void OnFailure();
    }
}
