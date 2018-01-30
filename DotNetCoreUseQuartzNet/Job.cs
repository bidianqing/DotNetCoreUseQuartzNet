using Quartz;
using System;
using System.Threading.Tasks;

namespace DotNetCoreUseQuartzNet
{
    public class Job : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Hello World!");
        }
    }
}
