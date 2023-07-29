using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Code.Main
{
    public class StartGameDelay : IDisposable
    {
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        public bool IsStartGame { get; private set; }

        public StartGameDelay(float delayBeforeStart)
        {
            DelayBeforeStartGame(delayBeforeStart);
        }

        private async void DelayBeforeStartGame(float delayBeforeStart)
        {
            IsStartGame = false;

            await UniTask.Delay(TimeSpan.FromSeconds(delayBeforeStart), cancellationToken: tokenSource.Token);

            IsStartGame = true;
        }

        public void Dispose()
        {
            tokenSource?.Dispose();
        }
    }
}