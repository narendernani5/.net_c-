using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casestudy_3
{
    internal class Program
    {
        //Assign path to variable fname
        static string fname = @"C:\Users\Public\demonet\text.txt";
        static string header_fname = @"C:\Users\Public\demonet\Header.txt";

        static void Main(string[] args)
        {

            //Task Application for ToDo list
            ToDoApp();

        }

        static void ToDoApp()
        {
            //Creating a task app to maintain task detail like task name status and priority

            //check if the text file exists else create new file
            if (File.Exists(header_fname))
            {
                Console.WriteLine("header File found...");
            }
            else
            {
                Console.WriteLine("header File not found...");

                using (StreamWriter sw = File.CreateText(header_fname))
                {

                    sw.WriteLine("Hello Welcome to ToDo List");
                    sw.WriteLine("Sr. No" + "\t" + "Task" + "\t\t\t" + "Date" + "\t\t\t\t" + "Status" + "\t\t\t\t" + "Status");

                }
                Console.WriteLine("File created...");

                if (File.Exists(fname))
                {
                    Console.WriteLine("tasks File found...");
                }

                else
                {
                    Console.WriteLine("tasks File not found...");

                    using (StreamWriter swf = File.CreateText(fname))
                    {

                        swf.WriteLine();

                    }
                    Console.WriteLine("File created...");
                }
            }

            

            int Continue = -1;
            //while loop to continue giving options till user chooses exit option
            while (Continue != 0)
            {
                try
                {
                    //Display operations
                    Continue = Mainmenu();
                    //Used swith to perform multiple conditional operations beased on input
                    switch (Continue)
                    {
                        case 1:
                            //Display the text file information
                            readfile();
                            break;
                        case 2:
                            //using try catch block as user may input text instead of a number
                            try
                            {
                                //Adding new task
                                Addtask();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Please enter a valid Number\nErrormessage: " + ex.Message);
                            }

                            break;

                        case 3:

                            //get the kine number to be marked as done
                            Console.WriteLine("please enter Sr.No of task to be marked as done\n");
                            Console.WriteLine("\n" + File.ReadAllText(header_fname));
                            Console.WriteLine("\n" + File.ReadAllText(fname));

                            try
                            {
                                //Update the status as done
                                Updatestatus();

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Please enter a valid Number\nErrormessage: " + ex.Message);
                            }

                            break;

                        case 4:

                            //Get the task number to be deleted
                            Console.WriteLine("please enter Sr.No of task to be deleted\n");
                            Console.WriteLine("\n" + File.ReadAllText(header_fname));
                            Console.WriteLine("\n" + File.ReadAllText(fname));

                            try
                            {
                                //Deletetask a task
                                Deletetask();

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Please enter a valid Number\nErrormessage: " + ex.Message);
                            }
                            break;

                        case 0:
                            //Exit application
                            Console.WriteLine("Exiting the application Thank you!");
                            Continue = 0;
                            break;

                        default:
                            Console.WriteLine("Please enter valid Corresponding operation number");
                            break;

                    }

                }
                catch (Exception ex)
                { Console.WriteLine("Please Enter corresponding Number: " + ex.Message); }
            }
        }

        static int Mainmenu()
        {
            //Display operations
            Console.WriteLine("\nPlease choose the operation to perform" +
                " \n 1: Print ToDo list " +
                " \n 2: Add New Task" +
                " \n 3: Mark a task as Done" +
                " \n 4: Delete a task" +
                " \n 0: Exit \n");

            int Input = Convert.ToInt32(Console.ReadLine());
            return Input;
        }


        static void readfile()
        {
            //Display the text file information

            //Display the header and todo text file information
            string header_filtxt = File.ReadAllText(header_fname);
            Console.WriteLine(header_filtxt);

            string task_filtxt = File.ReadAllText(fname);
            Console.WriteLine(task_filtxt);


        }

        static void Addtask()
        {
            //Get the task details from user store and append to text file
            //get searial number from user
            Console.WriteLine("please enter Serial No of task\n");
            int SNo = Convert.ToInt32(Console.ReadLine());

            //Get task description from
            Console.WriteLine("please enter name of the task\n");
            string task1 = Console.ReadLine();

            //storing current date to date variable
            DateTime date = DateTime.UtcNow;

            //Get work status
            String status = "WIP";

            //get Priority
            String priority = "Low";

            Console.WriteLine("please enter priority" +
                "\n1. High" +
                "\n2. Medium" +
                "\n3. Low");
            int Priority = Convert.ToInt32(Console.ReadLine());

            if (Priority <= 3 && Priority > 0)
            {
                if (Priority == 1)
                {
                    priority = "High";
                }
                else if (Priority == 2)
                {
                    priority = "Medium";
                }
                else
                {
                    priority = "Low";
                }
            }
            else { Console.WriteLine("please shoose a valid number "); }


            //write to text file
            File.AppendAllText(fname, "\n" + SNo + "\t" + task1 + "\t\t" + date + "\t\t" + status + "\t\t" + priority);

            //Reading File
            Console.WriteLine();
            Console.WriteLine("\nPrinting Updated ToDo list");

            //Display the header and todo text file information
            Console.WriteLine(File.ReadAllText(header_fname));
            Console.WriteLine(File.ReadAllText(fname));
        }

        static void Updatestatus()
        {
            int taskNo = Convert.ToInt32(Console.ReadLine());
            string[] filarr = File.ReadAllLines(fname);

            //check if the number entered is less than or equla to number of total tasks
            if (filarr.Length >= taskNo && taskNo > 0)
            {
                for (int i = 0; i < filarr.Length; i++)
                {
                    //index starts with zero so subtracted by 1
                    if (i == taskNo - 1)
                    {
                        //changing the status of the task to Done
                        filarr[i] = filarr[i].Replace("WIP", "Done");
                    }

                }

                //save to text file
                File.WriteAllLines(fname, filarr);
                Console.WriteLine("After updating the status of task: " + taskNo + "\n");

                //Display the header and todo text file information
                Console.WriteLine(File.ReadAllText(header_fname));
                Console.WriteLine(File.ReadAllText(fname));
            }
            else
            {
                Console.WriteLine("Please try again with a valid Sr.No of task");
            }
        }

        static void Deletetask()
        {
            int taskNo = Convert.ToInt32(Console.ReadLine());
            string[] filarr = File.ReadAllLines(fname);


            //check if the number entered is less than or equal to number of total tasks
            if (filarr.Length >= taskNo && taskNo > 0)
            {
                //addind all files to a list
                List<string> list = new List<string>(filarr);
                //Removing the task
                list.RemoveAt(taskNo - 1);
                //converting back to array again
                filarr = list.ToArray();

                File.WriteAllLines(fname, filarr);
                Console.WriteLine("Updated task list After deleting the task: " + taskNo + "\n");
                //Display the header and todo text file information
                Console.WriteLine(File.ReadAllText(header_fname));
                Console.WriteLine(File.ReadAllText(fname));
            }
            else
            {
                Console.WriteLine("Please try again with a valid Sr.No of task");
            }
        }

    }


}
