using Labb2_LINQ.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_LINQ.Models
{
    public class Metoder
    {

        public static void ObjectsAdded()
        {
            var context = new SkolaDBContext();

            //Classes

            Klass sut20 = new Klass() { KlassNamn = "SUT20" };
            Klass sut21 = new Klass() { KlassNamn = "SUT21" };
            Klass sut22 = new Klass() { KlassNamn = "SUT22" };

            //context.Klasser.Add(sut20);
            //context.Klasser.Add(sut21);
            //context.Klasser.Add(sut22);

            //Teachers
            Lärare anas = new Lärare() { FirstName = "Anas", LastName = "Qlok" };
            Lärare tobias = new Lärare() { FirstName = "Tobias", LastName = "Svensson" };
            Lärare reidar = new Lärare() { FirstName = "Reidar", LastName = "Gustavsson" };

            //context.Lärare.Add(anas);
            //context.Lärare.Add(tobias);
            //context.Lärare.Add(reidar);


            //Elever
            Elev kenny = new Elev { FirstName = "kenny", LastName = "Lindblom", Klasser = sut20 };
            Elev benny = new Elev { FirstName = "benny", LastName = "Kalrsson", Klasser = sut20 };
            Elev lenny = new Elev { FirstName = "lenny", LastName = "Roue", Klasser = sut21 };
            Elev selly = new Elev { FirstName = "selly", LastName = "Melly", Klasser = sut21 };

            //context.Add(kenny);
            //context.Add(benny);
            //context.Add(lenny);
            //context.Add(selly);

            //Courses
            Kurs Prog1 = new Kurs { KursNamn = "Programmering 1", Lärarna = anas, Klasser = sut20 };
            Kurs Agil = new Kurs { KursNamn = "Agila metoder", Lärarna = tobias, Klasser = sut20 };
            Kurs matte = new Kurs { KursNamn = "Matematik", Lärarna = reidar, Klasser = sut21 };
            Kurs svenska = new Kurs { KursNamn = "Svenska", Lärarna = tobias, Klasser = sut20 };
            Kurs Kemi = new Kurs { KursNamn = "Matematik", Lärarna = reidar, Klasser = sut21 };
            Kurs Prgo2 = new Kurs { KursNamn = "Programering 2", Lärarna = anas, Klasser = sut20 };


            //context.Add(Prgo2);
            //context.Add(Prog1);
            //context.Add(Agil);
            //context.Add(matte);
            //context.Add(svenska);
            //context.Add(Kemi);

            //context.SaveChanges();


        }


        public static void StartProgram()
        {
            SkolaDBContext Context = new SkolaDBContext();
            string[] menuItems = { "Alla lärare som undervisar i Matematik", "Hämta alla elever med sina lärare", "Sök efter kursnamn", "Ändra namn på en kurs", "Uppdatera lärare" };
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Välkommen till Labben om LINQ");
                Console.WriteLine("-----------------------------");

                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (selectedIndex == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(menuItems[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(menuItems[i]);
                    }
                }

                ConsoleKeyInfo cki = Console.ReadKey(true);
                switch (cki.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex--;
                        if (selectedIndex < 0)
                        {
                            selectedIndex = menuItems.Length - 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex++;
                        if (selectedIndex == menuItems.Length)
                        {
                            selectedIndex = 0;
                        }
                        break;
                    case ConsoleKey.Enter:
                        switch (menuItems[selectedIndex])
                        {
                            case "Alla lärare som undervisar i Matematik":
                                AllMathTeachers(Context);
                                
                                Console.WriteLine("Du valda: " + menuItems[selectedIndex]);
                                Console.ReadKey();
                                
                                break;
                            case "Hämta alla elever med sina lärare":
                                AllStudentWithTeacher(Context);
                                Console.WriteLine("Du valde: " + menuItems[selectedIndex]);
                                Console.ReadKey();
                                break;
                            case "Sök efter kursnamn":
                                ContainsCourse(Context);
                                Console.WriteLine("Du valde: " + menuItems[selectedIndex]);
                                Console.ReadKey();
                                break;
                            case "Ändra namn på en kurs":
                                ChangeCourseName(Context);
                                Console.WriteLine("Du valde: " + menuItems[selectedIndex]);
                                Console.ReadKey();
                                break;
                            case "Uppdatera lärare":
                                ChangerTeacher(Context);
                                Console.WriteLine("Du valde: " + menuItems[selectedIndex]);
                                Console.ReadKey();
                                break;
                        }
                        break;
                }
            }
        }



         static void AllMathTeachers(SkolaDBContext Context)
        {
           

            Console.Clear();
            Console.WriteLine("Nu hämtar vi alla Matte läare");
            var mathTeacher = (from e in Context.Kurser
                              join t in Context.Lärare
                              on e.Lärarna.Id equals t.Id
                              where e.KursNamn == "Matematik"
                              select new
                              {
                                  KursNamn = e.KursNamn,
                                  LärarFnamn = t.FirstName,
                                  LärareEnanmn = t.LastName

                              }).Distinct();
            
            foreach(var item in mathTeacher)
            {
                Console.WriteLine($"{item.LärarFnamn} {item.LärareEnanmn} är lärare i {item.KursNamn}");
            }

            
        }

        static void AllStudentWithTeacher(SkolaDBContext Context)
        {
            Console.Clear();
            Console.WriteLine("----Nu hämtar vi alla elver med sina lärare");

            var studentTeacherGet = (from a in Context.Elever
                                     join b in Context.Klasser on a.Klasser.Id equals b.Id
                                     join e in Context.Kurser on b.Id equals e.Klasser.Id
                                     join d in Context.Lärare on e.Lärarna.Id equals d.Id
                                     orderby a.FirstName ascending
                                     select new
                                     {
                                         elevFnamn = a.FirstName,
                                         elevEnamn = a.LastName,
                                         lärareförnamn = d.FirstName,
                                         lärareEnamn = d.LastName,
                                         kurs = e.KursNamn
                                     }).Distinct();

            
            foreach(var item in studentTeacherGet)
            {
                Console.WriteLine($"Elever : {item.elevFnamn} {item.elevEnamn}");

                Console.WriteLine($"Lärare : {item.lärareförnamn} {item.lärareEnamn}");

                Console.WriteLine($"Kurs : {item.kurs}");
                Console.WriteLine("-------------------------------------------------");

                Thread.Sleep(1000);
            }

        }


        static void ContainsCourse(SkolaDBContext Context)
        {
            Console.Clear();

            Console.WriteLine("Vilken kurs vill du söka efter? ");
            string searchCourse = Console.ReadLine();

            if (Context.Kurser.Select(s => s.KursNamn).ToList().Contains(searchCourse))
            {
                Console.WriteLine($" Du sökte efter {searchCourse} , denna kurs finns i databasen");
            }

            else
            {
                Console.WriteLine($" Du sökte efter {searchCourse} , finns tyvärr inte i databasen för tillfället");
            }

        }

        static void ChangeCourseName(SkolaDBContext Context)
        {
            Console.Clear();
            Console.WriteLine("----Ändra namn på en kurs-----");

            var ListCourses = Context.Kurser.Select(s => s.KursNamn).ToList();
            foreach(string course in ListCourses)
            {
                Console.WriteLine(course);
            }
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Vilken kurs vill du byta namn på? ");

            string CourseNow = Console.ReadLine();

            if(Context.Kurser.Select(c=> c.KursNamn).ToList().Contains(CourseNow))
            {
                Console.WriteLine("Vad vill du att kursen ska heta ?");
                

                string CourseUpdate = Console.ReadLine();
                var newNameCourse = Context.Kurser.Where(c => c.KursNamn == CourseNow).FirstOrDefault();

                newNameCourse.KursNamn = CourseUpdate;
                Context.SaveChanges();
            }

            else
            {
                Console.WriteLine("Det finns ingen kurs med det namnet i databasen för tillfället");
            }
                



        }

        static void ChangerTeacher(SkolaDBContext Context)
        {
            Console.Clear();
            Console.WriteLine("---------Byta lärare----------");

            
            if(Context.Kurser.Select(s=> s.Lärarna.FirstName).ToList().Contains("Anas"))
            {
                var changeTeacherFrom = Context.Lärare.Where(s => s.FirstName == "Reidar" && s.LastName == "Gustavsson").FirstOrDefault();

                if (changeTeacherFrom != null)
                {

                    changeTeacherFrom.FirstName = "Anas";
                    changeTeacherFrom.LastName = "Qlok";

                    var changeTeacherTo = Context.Lärare.Where(s => s.FirstName == "Anas" && s.LastName == "Qlok").FirstOrDefault();

                    changeTeacherTo.FirstName = "Reidar";
                    changeTeacherTo.LastName = "Gustavsson";


                    string UppdateText = "Uppdaterar.....";
                    for (int i = 0; i < UppdateText.Length; i++)
                    {
                        Thread.Sleep(200);
                        Console.Write(UppdateText[i]);
                    }

                    

                    Console.WriteLine("Anas och Reidar bytat lektioner");

                    Context.SaveChanges();
                    Console.ReadKey();
                }
            }

        }



    }
}


 
