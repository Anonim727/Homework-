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
            //Расписание на понедельник
            StudyDay mondaySchedule = new StudyDay("Понедельник");
            mondaySchedule.ChangeSchedule(0, new Lesson("Геометрия", "9:00", 8));
            mondaySchedule.ChangeSchedule(1, new Lesson("История", "10:00", 5));
            mondaySchedule.ChangeSchedule(2, new Lesson("Физика", "11:00", 9));

            // Сохраняем расписание в Word
            SaveScheduleToWord(mondaySchedule, "monday_schedule.docx");
        }
        // Интерфейс для сложности предмета
        public interface IComplexity
        {
            int Complexity { get; set; }
        }
        public class Lesson : IComplexity
        {
            public string Subject { get; set; }
            public string Time { get; set; }
            public int Complexity { get; set; }

            public Lesson(string subject, string time, int complexity)
            {
                Subject = subject; 
                Time = time;
                Complexity = complexity;
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

            // Метод для создания и изменения  расписания
            public void ChangeSchedule(int lessonIndex, Lesson newLesson)
            {
                if (lessonIndex >= 0 && lessonIndex < Lessons.Length)
                {
                    // Проверка на последовательность сложных предметов
                    if (lessonIndex > 0 && Lessons[lessonIndex - 1] != null && Lessons[lessonIndex - 1].Complexity >= 9 && newLesson.Complexity >= 9)
                    {
                        Console.WriteLine("Нельзя ставить два сложных предмета подряд!");
                        return;
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

