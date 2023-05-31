using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Patterns.Query
{
    public interface ICommandCallback<TResponse>
    {
        void OnSuccess(TResponse response);
        void OnFailure();
    }

    public class CommandCallback<TResponse> : ICommandCallback<TResponse>
    {
        public delegate void OnSuccessDelegate(TResponse response);
        public delegate void OnFailureDelegate();

        private OnSuccessDelegate OnSuccessCallback { get; }
        private OnFailureDelegate OnFailureCallback { get; }

        public CommandCallback(OnSuccessDelegate onSuccessCallback, OnFailureDelegate onFailureCallback)
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
