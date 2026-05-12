using InventoryManagementSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task3
{
    class Task3
    {


        static void Main()
        {
            List<string> tasksList = [];
            bool isRunning = true;
            Console.WriteLine("\n*****Task Tracker*****\n");
            do
            {
                Console.WriteLine("\n Press 1 to add task " + "\n Press 2 to view tasks" + "\n Press 3 to remove tasks" + "\n Press 4 to exit");
                int choice = ReadChoice();
                switch (choice)
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
                        Console.WriteLine("Please Enter Valid No.\n");
                        break;
                }
            }
            while (isRunning);




            static void AddTask(List<string> tasks)
            {
                string? task;
                do
                {
                    Console.Write("Enter a task to add: ");
                    task = Console.ReadLine();

                    if (string.IsNullOrEmpty(task))
                        Console.WriteLine("Task cannot be empty. Please try again.\n");

                } while (string.IsNullOrEmpty(task));

                tasks.Add(task);
                Console.WriteLine("Task added successfully.\n");
            }

            static void ViewTasks(List<string> tasks)
            {
                Console.WriteLine("\nYour Tasks:");
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

        private static int ReadChoice()
        {
            if (int.TryParse(Console.ReadLine(), out int input))
            {
                return input;
            }
            else
            {
                Console.WriteLine("Only Digits are alowed");
                return -1;
            }
        }
    }
}

//Self - Study & Research
//Before you start coding, research these concepts:

//Arrays vs. Lists: What is the main difference between an Array and a List in C#?
//A) Arrays have a fixed size, while Lists can grow and shrink dynamically.
//B) Arrays can only store primitive types, while Lists can store any type of object.
//C) Arrays are part of the System.Collections namespace, while Lists are part of the System.Collections.Generic namespace.
//D) Arrays are faster than Lists for all operations, while Lists are slower.

//Methods: How do you pass parameters into a method? What does the static keyword mean here?
//A) You can pass parameters into a method by including them in the method's parentheses.
//The static keyword means that the method belongs to the class itself rather than an instance of the class,
//allowing you to call it without creating an object of the class.


//Zero-based Indexing: Lists start at 0. How do you show the user "Task #1" while the code identifies it as index 0?
//A) You can display the task number to the user by adding 1 to the index when showing it.
//For example, if you have a task at index 0, you would display it as "Task #1" by using (index + 1) in your output.


//Research Question (Include in your PR)
//Why is it better to use a List instead of an Array (string[]) for this specific project ?
//A) Using a List is better for this project because it allows for dynamic resizing,
//which means you can easily add or remove tasks without worrying about the underlying data structure.
//With an Array, you would need to create a new array and copy the existing tasks every time you want to add or remove a task,
//which can be inefficient and cumbersome. Lists also provide built-in methods
//for adding, removing, and managing items, making it more convenient for this type of application.
//