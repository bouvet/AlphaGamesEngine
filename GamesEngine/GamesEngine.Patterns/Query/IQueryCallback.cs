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

    public class QueryCallback<TResponse> : IQueryCallback<TResponse>
    {
        public delegate void OnSuccessDelegate(TResponse response);
        public delegate void OnFailureDelegate();

        private OnSuccessDelegate OnSuccessCallback { get; }
        private OnFailureDelegate OnFailureCallback { get; }

        public QueryCallback(OnSuccessDelegate onSuccessCallback, OnFailureDelegate onFailureCallback)
        {
            OnSuccessCallback = onSuccessCallback;
            OnFailureCallback = onFailureCallback;
        }

        public void OnSuccess(TResponse response)
        {
            OnSuccessCallback(response);
        }

        public void OnFailure()
        {
            OnFailureCallback();
        }
    }
}
