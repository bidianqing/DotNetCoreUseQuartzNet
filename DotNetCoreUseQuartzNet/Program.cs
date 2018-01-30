using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace DotNetCoreUseQuartzNet
{
    class Program
    {
        static void Main(string[] args)
        {
            RunProgram().GetAwaiter().GetResult();

            Console.ReadKey();
        }

        private static async Task RunProgram()
        {
            try
            {
                IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();

                await scheduler.Start();

                IJobDetail job = JobBuilder.Create<Job>()
                    .WithIdentity("job1", "group1")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                        .WithIdentity("trigger1", "group1")
                        .StartNow()
                        .WithSimpleSchedule(x => x
                            .WithIntervalInSeconds(10) //每隔10秒执行一次
                            .RepeatForever())
                        .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }
    }
}
