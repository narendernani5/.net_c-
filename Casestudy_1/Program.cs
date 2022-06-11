using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casestudy_1
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
            if (File.Exists(fname))
            {
                Console.WriteLine("File found...");
            }
            else
            {
                Console.WriteLine("File not found...");

                using (StreamWriter sw = File.CreateText(fname))
                {

                    sw.WriteLine("Hello Welcome to ToDo List");
                    sw.WriteLine("Sr. No" + "\t" + "Task" + "\t\t\t" + "Date" + "\t\t\t\t" + "Status\n");

                }
                Console.WriteLine("File created...");
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

        static void checkfile()
        {
            if (File.Exists(fname))
            {
                Console.WriteLine("File found...");
            }
            else
            {
                Console.WriteLine("File not found...");

                using (StreamWriter sw = File.CreateText(fname))
                {

                    sw.WriteLine("Hello Welcome to ToDo List");
                    sw.WriteLine("Sr. No" + "\t" + "Task" + "\t\t\t" + "Date" + "\t\t\t\t" + "Status");

                }
                Console.WriteLine("File created...");
            }
        }
        static void readfile()
        {
            //Display the text file information
            int number = 1;
            string item_header;
            string item;
            
            List<string> items_header = new List<string>();
            List<string> items = new List<string>();

            StreamReader in_header_file = new StreamReader(header_fname);
            StreamReader in_items_file = new StreamReader(fname);

            Console.WriteLine("start of while");
            
            while (items_header.Count >= number)
            {
                item_header = in_header_file.ReadLine();
                items_header.Add(item_header);
                ++number;
            }

            in_header_file.Close();
            in_items_file.Close();

            while (items_header.Count >= number)
            {
                item = in_header_file.ReadLine();
                items.Add(item);
                ++number;
            }
            Console.WriteLine("end of while2");

            StreamWriter out_header = new StreamWriter(header_fname);
            StreamWriter out_items = new StreamWriter(fname);

            out_header.Close();
            out_items.Close();

            Console.WriteLine("end of while3");

            File.ReadAllLines(header_fname);

            for (int i = 0; i < items_header.Count; i++)
                out_header.WriteLine(items_header[i]);

            for (int i = 0; i < items.Count; i++)
                out_items.WriteLine(items[i]);

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

            //making default status as wip
            String status = "WIP";

            //write to text file
            File.AppendAllText(fname, SNo + "\t" + task1 + "\t\t" + date + "\t\t" + status);

            //Reading File
            Console.WriteLine();
            Console.WriteLine("\nPrinting Updated ToDo list");

            //print text file
            Console.WriteLine("\n" + File.ReadAllText(fname));
        }

        static void Updatestatus()
        {
            int taskNo = Convert.ToInt32(Console.ReadLine());
            string[] filarr = File.ReadAllLines(fname);

            //check if the number entered is less than or equla to number of total tasks
            if (filarr.Length >= taskNo + 3 && taskNo > 0)
            {
                for (int i = 0; i < filarr.Length; i++)
                {
                    //There are two header lines and index starts with zero so added 2 and subtracted by 1
                    if (i == taskNo + 3 - 1)
                    {
                        //changing the status of the task to Done
                        filarr[i] = filarr[i].Replace("WIP", "Done");
                    }

                }

                //save to text file
                File.WriteAllLines(fname, filarr);
                Console.WriteLine("After updating the status of task: " + taskNo);
                Console.WriteLine("\n" + File.ReadAllText(fname));
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
            if (filarr.Length >= taskNo + 3 && taskNo > 0)
            {
                //addind all files to a list
                List<string> list = new List<string>(filarr);
                //Removing the task
                list.RemoveAt(taskNo + 3 - 1);
                //converting back to array again
                filarr = list.ToArray();

                File.WriteAllLines(fname, filarr);
                Console.WriteLine("\n" + File.ReadAllText(fname));
            }
            else
            {
                Console.WriteLine("Please try again with a valid Sr.No of task");
            }
        }

    }


}
