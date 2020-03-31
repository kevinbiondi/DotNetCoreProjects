using System;
using System.Collections.Generic;
using System.Linq;

namespace delegates_pluralsight
{
    public delegate int BizRulesDelegate(int x, int y);
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


            var custs = new List<Customer>
            {
                new Customer {City = "King George", FirstName = "Kevin", LastName = "Biondi", ID=1},
            new Customer { City = "King George", FirstName = "Connor", LastName = "Biondi", ID = 2 },
            new Customer { City = "Richmond", FirstName = "Jody", LastName = "DesRoches", ID = 3 },
            new Customer { City = "Pittsburgh", FirstName = "Ben", LastName = "Roethisberger", ID = 5 },
            new Customer { City = "King George", FirstName = "Sydney", LastName = "Biondi", ID = 500 }
            };

            var kgCustomers = custs.Where(c => c.City == "King George" && c.ID < 500)
                                    .OrderBy(c => c.FirstName);
            foreach (var cust in kgCustomers)
            {
                Console.WriteLine(cust.FirstName);
            }





            var data = new ProcessData();
            BizRulesDelegate addDel = (x, y) => x + y;
            BizRulesDelegate multiplyDel = (x, y) => x * y;
            //Note:  Process doesn't know until runtime which business rules to call: addDel or multiplyDel
            //There could be an if statement that determines whether addDel or mulitplyDel gets called
            //This is dynamic at runtime
            data.Process(2, 3, addDel);

            //The following does the same as above but you don;t need to declare BizRulesDelegate like above
            Func<int, int, int> funcAddDel = (x, y) => x + y;
            Func<int, int, int> funcMultDel = (x, y) => x * y;
            data.ProcessFunc(5, 2, funcAddDel);



            

            //Action<int, int> myAction = (x, y) => Console.WriteLine(x + y);
            Action<int, int> myMultiply = (x, y) => Console.WriteLine(x * y);
            data.ProcessAction(5, 6, myMultiply);

            var worker = new Worker();
            //Defining and attaching event handlers OPTION 1
            //worker.WorkPerformed += new EventHandler<WorkPerformedEventArgs>(worker_WorkPerformed);
            //worker.WorkCompleted += new EventHandler(worker_WorkCompleted);

            //OR DO

            //Defining and attaching event handlers by Delegate Inference OPTION 2
            //worker.WorkPerformed += worker_WorkPerformed;
            //worker.WorkCompleted += worker_WorkCompleted;
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


            //OR DO
            //As a lambda
            //worker.WorkPerformed += worker_WorkPerformed;
            //Note: commented out the callback method worker_WorkCompleted below since now here
            worker.WorkPerformed += (s, e) =>
            {
                Console.WriteLine("Hours Worked: " + e.Hours + " " + e.WorkType);
                Console.WriteLine("Demonstate a lambda doing an inline method in curly braces for work performed");
            };
            worker.WorkCompleted += (s, e) => Console.WriteLine("Worker is done");

            worker.DoWork(8, WorkType.GoToMeetings);

            Console.Read();
        }

        //This method is a callback
        //private static void worker_WorkCompleted(object sender, EventArgs e)
        //{
        //    Console.WriteLine("Worker is done");
        //}

        //This method is the callback
        //public static void worker_WorkPerformed(object sender, WorkPerformedEventArgs e)
        //{
        //    Console.WriteLine("Hours Worked: " + e.Hours + " " + e.WorkType);
        //}

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
