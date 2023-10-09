
using VRC.Udon;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using KitKat.UdonBenchmark.Runtime.Abstract;

namespace KitKat.UdonBenchmark.Runtime.Examples
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ExampleBench : AbstractBenchmark
    {
        public override void BenchA()
        {
            _stopwatchBenchA.Start();

            // Stuff to benchmark.

            _stopwatchBenchA.Stop();
        }
        public override void BenchB()
        {
            _stopwatchBenchB.Start();

            // Stuff to benchmark.

            _stopwatchBenchB.Stop();
        }
    }
}