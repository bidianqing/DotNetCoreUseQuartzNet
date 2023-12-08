using Quartz;

namespace DotNetCoreUseQuartzNet
{
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            context.MergedJobDataMap.TryGetString("phone", out string value);

            await Console.Out.WriteLineAsync($"Hello {value}");
        }
    }
}
