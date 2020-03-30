using System;

namespace delegates_pluralsight
{

    class Program
    {
        public static void Main(string[] args)
        {
            //WorkPerformedHandler del1 = new WorkPerformedHandler(WorkPerformed1);
            //WorkPerformedHandler del2 = new WorkPerformedHandler(WorkPerformed2);
            //WorkPerformedHandler del3 = new WorkPerformedHandler(WorkPerformed3);


            //invoke delegate to pass the data to workperformed1 for us
            //note how intellisense knows to ask for WorkPerformed1 parameters
            //del1(5, WorkType.Golf);
            //invoke delgate to pass the data to workperformed2 for us
            //note how intellisense knows to ask for WorkPerformed1 parameters
            //del2(8, WorkType.GenerateReports);


            //This is where the power of delegates come in to play
            //DoWork(del2);

            //Mulicast delegate
            //del1 += del2 + del3; //del1, del2, del3 gets called sequentially in that roder
            //one point of reference to invoke mulitiple subscribers
            //del1(10, WorkType.GenerateReports);

            var worker = new Worker();
            //Defining and attaching event handlers OPTION 1
            //worker.WorkPerformed += new EventHandler<WorkPerformedEventArgs>(worker_WorkPerformed);
            //worker.WorkCompleted += new EventHandler(worker_WorkCompleted);

            //OR DO

            //Defining and attaching event handlers by Delegate Inference OPTION 2
            worker.WorkPerformed += worker_WorkPerformed;
            worker.WorkCompleted += worker_WorkCompleted;
            //test -= to remove(doesn't make sense here tho)
            //worker.WorkCompleted -= worker_WorkCompleted;


            //OR DO

            //Defining and attaching event handlers by Anomymous Methods
            //worker.WorkPerformed += delegate (object sender, WorkPerformedEventArgs e)
            //{
            //    Console.WriteLine("Hours Worked: " + e.Hours + " " + e.WorkType);
            //};
            //worker.WorkCompleted += delegate (object sender, EventArgs e)
            //{
            //    Console.WriteLine("Worker is done");
            //};

            worker.DoWork(8, WorkType.GoToMeetings);

            Console.Read();
        }

        //This method is a callback
        private static void worker_WorkCompleted(object sender, EventArgs e)
        {
            Console.WriteLine("Worker is done");
        }

        //This method is the callback
        public static void worker_WorkPerformed(object sender, WorkPerformedEventArgs e)
        {
            Console.WriteLine("Hours Worked: " + e.Hours + " " + e.WorkType);
        }

        //public static void DoWork(WorkPerformedHandler del)
        //{
        //    del(5, WorkType.GoToMeetings);
        //}
        //public static void WorkPerformed1(int hours, WorkType workType)
        //{
        //    Console.WriteLine("WorkedPerformed1 called " + hours.ToString() + " " + workType.ToString());
        //}

        //public static void WorkPerformed2(int hours, WorkType workType)
        //{
        //    Console.WriteLine("WorkedPerformed2 called " + hours.ToString() + " " + workType.ToString());
        //}
        //public static void WorkPerformed3(int hours, WorkType workType)
        //{
        //    Console.WriteLine("WorkedPerformed3 called " + hours.ToString() + " " + workType.ToString());
        //}
    }

 
    public enum WorkType
    {
        GoToMeetings,
        Golf,
        GenerateReports
    }
}
