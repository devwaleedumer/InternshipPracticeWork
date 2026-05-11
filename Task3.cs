using System;
using System.Collections;
using System.Collections.Generic;

namespace Task3
{
    class Program
    {


        static void Main()
        {
            List<string> tasksList = [];
            bool isRunning = true;
            do
            {
                Console.WriteLine("\n*****Task Tracker*****\n");
                Console.WriteLine("\n Press 1 to add task " + "\n Press 2 to view tasks" + "\n Press 3 to remove tasks" + "\n Press 4 to exit");
                int input;

                if (int.TryParse(Console.ReadLine(), out input))
                {
                    switch (input)
                    {
                        case 1:
                            AddTask(tasksList);
                            break;
                        case 2:
                            ViewTasks(tasksList);
                            break;
                        case 3:
                            RemoveTask(tasksList);
                            break;
                        case 4:
                            isRunning = false;
                            break;
                        default:
                            Console.WriteLine("Please Enter Valid No.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Only Digits are alowed");
                }    

            }
            while (isRunning);



         
            static void AddTask(List<string> tasks)
            {
                string? task;
                do
                {
                    Console.WriteLine("Enter a task to add:");
                    task = Console.ReadLine();

                    if (string.IsNullOrEmpty(task))
                        Console.WriteLine("Task cannot be empty. Please try again.\n");

                } while (string.IsNullOrEmpty(task));

                tasks.Add(task);
                Console.WriteLine("Task added successfully.\n");
            }

            static void ViewTasks(List<string> tasks)
            {
                if (tasks.Count == 0)
                {
                    Console.WriteLine("No tasks yet.\n");
                    return;
                }

                for (int i = 0; i < tasks.Count; i++)
                    Console.WriteLine($"{i + 1}. {tasks[i]}");
            }

            static void RemoveTask(List<string> tasks)
            {
                if (tasks.Count == 0)
                {
                    Console.WriteLine("No tasks to remove.\n");
                    return;
                }
                int index;
                do
                {
                    Console.WriteLine("Enter the task number to remove:");
                    string? input = Console.ReadLine();
                    if (!int.TryParse(input, out index) || index < 1 || index > tasks.Count)
                        Console.WriteLine("Invalid task number. Please try again.\n");
                    else
                        break;
                } while (true);
                tasks.RemoveAt(index - 1);
                Console.WriteLine("Task removed successfully.\n");
            }
        }
    }
}


//  Why is it better to use a List instead of an Array (string[]) for this specific project ?
//  For todo  list tasks are added and removed constantly so sized is unknown, list is to go datastructure  
//  Because tasks are to be added and removed and we dont know how many tasks will be added 
//  Array has fixed size and List has dynamic size, In array we have to keep pointer to track 
//  Array size to add new items, while List grows automatically
//  Deleting an element in an array require shifting of lements while
//  List does it automatically for us



