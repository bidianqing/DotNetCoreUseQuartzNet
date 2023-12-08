using Quartz;
using Quartz.Impl;

namespace DotNetCoreUseQuartzNet
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            await RunProgram();

            Console.ReadKey();
        }

        private static async Task RunProgram()
        {
            try
            {
                IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();

                await scheduler.Start();

                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("hellojob", "default")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                        .StartNow()
                        .UsingJobData("phone", "120")
                        .WithSimpleSchedule(x => x
                            .WithIntervalInSeconds(10) //每隔10秒执行一次
                            .RepeatForever())
                        .Build();
                await scheduler.ScheduleJob(job, trigger);

                ITrigger trigger2 = TriggerBuilder.Create()
                        .StartNow()
                        .ForJob("hellojob", "default")
                        .UsingJobData("phone", "110")
                        .WithSimpleSchedule(x => x
                            .WithIntervalInSeconds(3)
                            .RepeatForever())
                        .Build();
                // 添加一个指定的Job到scheduler，并关联一个触发器
                await scheduler.ScheduleJob(trigger2);
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }
    }
}
