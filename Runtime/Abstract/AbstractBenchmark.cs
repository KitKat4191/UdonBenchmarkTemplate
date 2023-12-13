
using UdonSharp;
using UnityEngine;

namespace KitKat.UdonBenchmark.Runtime.Abstract
{
    public abstract class AbstractBenchmark : UdonSharpBehaviour
    {
        #region Serialized Fields

        [SerializeField] protected int iterations;

        [Space()]
        [SerializeField] protected string BENCH_A_NAME = "A";
        [SerializeField] protected string BENCH_B_NAME = "B";

        #endregion Serialized Fields

        protected System.Diagnostics.Stopwatch _stopwatchBenchB = new System.Diagnostics.Stopwatch();
        protected System.Diagnostics.Stopwatch _stopwatchBenchA = new System.Diagnostics.Stopwatch();

        [ContextMenu("Run Benchmark")]
        public virtual void RunFromEditor()
        {
            SendCustomEvent(nameof(RunBenchmark));
        }

        public virtual void RunBenchmark()
        {
            _printWarning("--------[ STARTING NEW BENCH ]--------");

            _stopwatchBenchA.Reset();
            _stopwatchBenchB.Reset();

            for (int i = 0; i < iterations; i++) 
            {
                BenchA();
                BenchB();
            }

            LogResults();
        }

        public virtual void BenchA()
        {
            _stopwatchBenchA.Start();

            // Stuff to benchmark.

            _stopwatchBenchA.Stop();
        }
        public virtual void BenchB()
        {
            _stopwatchBenchB.Start();

            // Stuff to benchmark.

            _stopwatchBenchB.Stop();
        }

        public virtual void LogResults()
        {
            double elapsedMs_A = _stopwatchBenchA.Elapsed.TotalMilliseconds;
            double elapsedMs_B = _stopwatchBenchB.Elapsed.TotalMilliseconds;

            _print($"{BENCH_A_NAME} completed in {elapsedMs_A}ms");
            _print($"{BENCH_B_NAME} completed in {elapsedMs_B}ms");

            bool A_WasFastest = elapsedMs_A < elapsedMs_B;
            double timesFaster = A_WasFastest ?
                elapsedMs_B / elapsedMs_A :
                elapsedMs_A / elapsedMs_B;
            
            _printSuccess($"{(A_WasFastest ? BENCH_A_NAME : BENCH_B_NAME)} completed {timesFaster.ToString("F2")} times faster than {(A_WasFastest ? BENCH_B_NAME : BENCH_A_NAME)}!");
        }

        #region Logging

        protected const string LOG_IDENTIFIER = "[<color=purple>Benchmarking</color>]: ";
        protected virtual void _print(string message)
        {
            Debug.Log($"{LOG_IDENTIFIER}<color=lightblue>{message}</color>", this);
        }

        protected virtual void _printSuccess(string message)
        {
            Debug.Log($"{LOG_IDENTIFIER}<color=lime>{message}</color>", this);
        }

        protected virtual void _printWarning(string message)
        {
            Debug.LogWarning($"{LOG_IDENTIFIER}<color=orange>{message}</color>", this);
        }

        protected virtual void _printError(string message)
        {
            Debug.LogError($"{LOG_IDENTIFIER}<color=red>{message}</color>", this);
        }
        
        #endregion
    }
}