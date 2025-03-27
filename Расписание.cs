using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    class Program
    {
        static void Main(string[] args)
        { 
        
        
        
        }
    public class Lesson
        {
            public string Subject { get; set; }
            public string Time { get; set; }

            public Lesson(string subject, string time)
            {
                Subject = subject;
                Time = time;
            }
        }

        
        public class StudyDay
        {
            public string DayOfWeek { get; set; }
            public Lesson[] Lessons { get; set; }
            
            public StudyDay(string dayOfWeek)
            {
                DayOfWeek = dayOfWeek;
                Lessons = new Lesson[8]; 
            }

            // Метод для изменения расписания
            public void ChangeSchedule(int lessonIndex, Lesson newLesson)
            {
                if (lessonIndex >= 0 && lessonIndex < Lessons.Length)
                {
                    Lessons[lessonIndex] = newLesson;
                }
                else
                {
                    Console.WriteLine("Ошибка: Неправильный индекс урока.");
                }
            }

            // Метод для отображения расписания
            public void WriteSchedule()
            {
                Console.WriteLine($"Расписание на {DayOfWeek}:");
                for (int i = 0; i < Lessons.Length; i++)
                {
                    if (Lessons[i] != null)
                    {
                        Console.WriteLine($"Урок {i + 1}: {Lessons[i].Subject} в {Lessons[i].Time}");
                    }
                    else
                    {
                        Console.WriteLine($"Урок {i + 1}: пусто");
                    }
                }
            }
        }
    }
    }

