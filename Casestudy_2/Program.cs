using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casestudy_2
{
    internal class Program
    {
        //Assign path to variable fname
        static string fname = @"D:\Narender\RPA\.Net\Practice\CaseStudy\CaseStudy\text files\text.txt";
        static string header_fname = @"D:\Narender\RPA\.Net\Practice\CaseStudy\CaseStudy\text files\Header.txt";

        static void Main(string[] args)
        {

            //Task Application for ToDo list
            Console.WriteLine("Please Enter User Name: ");
            String User_name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Password: ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            int Password = Convert.ToInt32(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            if (User_name == "Nani" && Password==123)
            {
                ToDoApp();
            }
            else
            {
                GuestLogin();

            }

            

        }
        //Task Application for ToDo list
        static void ToDoApp()
        {
            //Creating a task app to maintain task detail like task name status and priority

            //check if the text file exists else create new file
            if (File.Exists(header_fname))
            {
                //Console.WriteLine("header File found...");
            }
            else
            {
                //Console.WriteLine("header File not found...");

                using (StreamWriter sw = File.CreateText(header_fname))
                {

                    sw.WriteLine("Hello Welcome to ToDo App!\n");
                    sw.WriteLine("Sr.No" + "\t" + "Task" + "\t\t\t" + "Created_Date" + "\t\t\t" + "Status" + "\t" + "Priority" + "\t" + "Mod_date");

                }
                //Console.WriteLine("header File created...");
            }

            if (File.Exists(fname))
            {
                //Console.WriteLine("tasks File found...");
            }

            else
            {
                //Console.WriteLine("tasks File not found...");

                using (StreamWriter swf = File.CreateText(fname))
                {                  
                }
                //Console.WriteLine("task File created...");
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
                            Readfile();
                            break;
                        case 2:
                            //using try catch block as user may input text instead of a number
                            try
                            {
                                //Adding new task
                                Console.Clear();
                                Readfile();
                                Addtask();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Please check your input or \nErrormessage: " + ex.Message);
                            }

                            break;

                        case 3:

                            //get the kine number to be marked as done
                            Console.Clear();
                            Readfile();
                            Console.WriteLine("please enter Sr.No of task to be marked as done\n");


                            try
                            {
                                //Update the status as done
                                Updatestatus();

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Please check your input or \nErrormessage: " + ex.Message);
                            }

                            break;

                        case 4:

                            //Get the task number to be deleted
                            Console.Clear();
                            Console.WriteLine("please enter Sr.No of task to be deleted\n");
                            Readfile();
                            try
                            {
                                //Deletetask a task
                                Deletetask();

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Please check your input or \nErrormessage: " + ex.Message);
                            }
                            break;

                        case 0:
                            //Exit application
                            Console.WriteLine("\nExiting the application Thank you!\n");
                            Continue = 0;
                            break;

                        default:
                            Console.WriteLine("Please enter valid Corresponding operation number");
                            break;

                    }

                }
                catch (Exception ex)
                { Console.WriteLine("Please check your input or \nErrormessage: " + ex.Message); }
            }
        }

        static void GuestLogin()
        {
            Console.Clear();
            Console.WriteLine("Please enter valid Credentials or Choose from below \n1: Login as Guest user \n2: Exit Application");
            int guest_In = Convert.ToInt32(Console.ReadLine());
            if (guest_In == 1)
            {
                Console.WriteLine("Choose from below \n1: View_ToDo list \n2: Exit Application");
                int view_tasks = Convert.ToInt32(Console.ReadLine());
                if (view_tasks == 1)
                {
                    Console.WriteLine();
                    Readfile();
                }

            }
            else
            {
                Console.WriteLine("Exiting the application");
            }
        }

        //Menu options to choose the operation
        static int Mainmenu()
        {

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

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

        //1. Display the text file information

        static void Readfile()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.White;

            //Display the header and todo text file information
            string header_filtxt = File.ReadAllText(header_fname);
            Console.WriteLine(header_filtxt);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.White;

            string task_filtxt = File.ReadAllText(fname);
            Console.WriteLine(task_filtxt);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;


        }

        //2. Get the task details from user store and append to text file

        static void Addtask()
        {
            //get searial number from user
            Console.WriteLine("please enter Serial No of task\n");
            int SNo = Convert.ToInt32(Console.ReadLine());

            //Get task description from
            Console.WriteLine("please enter name of the task\n");
            string Description = Console.ReadLine();

            //storing current date to date variable
            DateTime date = DateTime.Now;

            //Get work status
            String status = "WIP";

            //get Priority
            String priority = "Low";

            Console.WriteLine("please enter priority" +
                "\n1. High" +
                "\n2. Medium" +
                "\n3. Low");
            int Priority = Convert.ToInt32(Console.ReadLine());

            if (Priority<=3 && Priority>0)
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
            else { Console.WriteLine("please choose a valid number "); }

            //modified date
            String Mod_date = "Modifed_date";

            //write to text file
            File.AppendAllText(fname, SNo + "\t" + Description + "\t\t" + date + "\t\t" + status + "\t" + priority + "\t\t" + Mod_date + "\n");

            //Reading File
            Console.WriteLine();
            Console.WriteLine("\nPrinting Updated ToDo list: \n");

            //Display the header and todo text file information
            Readfile();
        }
        
        //3. Update task status
        static void Updatestatus()
        {
            //Console.Clear();
            int taskNo = Convert.ToInt32(Console.ReadLine());
            string[] filarr = File.ReadAllLines(fname);

            //check if the number entered is less than or equla to number of total tasks
            if (filarr.Length >= taskNo && taskNo > 0)
            {
                for (int i = 0; i < filarr.Length; i++)
                {
                    //index starts with zero so subtracted by 1
                    if (i == taskNo-1)
                    {
                        //changing the status of the task to Done
                        filarr[i] = filarr[i].Replace("WIP", "Done");
                        String date = DateTime.Now.ToString();
                        filarr[i] = filarr[i].Replace("Modifed_date", date);                        
                        
                    }

                }

                //save to text file
                File.WriteAllLines(fname, filarr);
                Console.WriteLine("After updating the status of task: " + taskNo + "\n");

                //Display the header and todo text file information
                Readfile();
            }
            else
            {
                Console.WriteLine("Please try again with a valid Sr.No of task");
            }
        }

        //4. Dete a task
        static void Deletetask()
        {
            //Console.Clear();
            int taskNo = Convert.ToInt32(Console.ReadLine());
            string[] filarr = File.ReadAllLines(fname);


            //check if the number entered is less than or equal to number of total tasks
            if (filarr.Length >= taskNo && taskNo > 0)
            {
                //addind all files to a list
                List<string> list = new List<string>(filarr);
                //Removing the task
                list.RemoveAt(taskNo-1);
                //converting back to array again
                filarr = list.ToArray();

                File.WriteAllLines(fname, filarr);
                Console.WriteLine("Updated task list After deleting the task: " + taskNo +"\n");

                //Display the header and todo text file information
                Readfile();
            }
            else
            {
                Console.WriteLine("Please try again with a valid Sr.No of task");
            }
        }

    }


}
