using System;
using System.Collections.Generic;
using System.Text;

namespace delegates_pluralsight
{
    //public delegate void WorkPerformedHandler(int hours, WorkType workType);
    //change to more .net standard
    //public delegate void WorkPerformedHandler(object sender, WorkPerformedEventArgs e);

    public class Worker
    {
        public event EventHandler<WorkPerformedEventArgs> WorkPerformed;
        //public event WorkPerformedHandler WorkPerformed;
        public event EventHandler WorkCompleted;  //EventHander is built in

        public void DoWork(int hours, WorkType workType)
        {
            for (int i = 0; i < hours; i++)
            {
                //Raise event for WorkPerformed
                System.Threading.Thread.Sleep(1000); //for testing only...to watch in console window
                onWorkedPerformed(i + 1, workType);
            }
            //Raise event for WorkCompleted
            onWorkedCompleted();
        }
        protected virtual void onWorkedPerformed(int hours, WorkType workType)
        {
            //option 1
            //if (WorkPerformed != null)
            //{
            //    WorkPerformed(hours, workType);
            //}

            // OR
            //option 2 (his preferred option)
            //var del = WorkPerformed as WorkPerformedHandler;
            var del = WorkPerformed as EventHandler<WorkPerformedEventArgs>;
            if (del != null)
            {
                //del(hours, workType);
                del(this, new WorkPerformedEventArgs(hours, workType));
            }


        }
        protected virtual void onWorkedCompleted()
        {
            //option 1
            //if (WorkCompleted != null)
            //{
            //    WorkCompleted(this, EventArgs.Empty);
            //}

            // OR
            //option 2 (his preferred option)
            var del = WorkCompleted as EventHandler;
            if (del != null)
            {
                del(this, EventArgs.Empty);
            }


        }

    }
}
