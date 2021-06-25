using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;


namespace Test11._1
{
    class OrgTest
    {
        /// <summary>
        /// Создает случайную организацию. Сортирует сотрудников. 
        /// Проводит сериализацию и десериализацию организации, отделов и отдельных сотрудников.
        /// </summary>
        static void Main()
        {
            Random random = new Random();// Генератор случайных чисел.
            Console.OutputEncoding = Encoding.UTF8;// Кодировка вывода.
            Org org;// Организация.
            string orgName;// Имя организации.
            // Создаем пустую организацию.
            org = new Org();
            // Распечатываем ее.
            org.Print(Console.Out);

            // В органиизации есть только менеджеры.
            orgName = "Managers_only";
            Console.WriteLine($"Нажмите любую клавишу для работы с организацией {orgName}.");
            Console.ReadKey(true);
            RandomOrg.managersOff = false;
            RandomOrg.workersOff = true;
            org = new Org(name: orgName, managers: RandomOrg.GetRandomManagers(random.Next(1, 4), random));
            org.Print(Console.Out);

            // В органиизации есть только работники.
            orgName = "Workers_only";
            Console.WriteLine($"Нажмите любую клавишу для работы с организацией {orgName}.");
            Console.ReadKey(true);
            RandomOrg.managersOff = true;
            RandomOrg.workersOff = false;
            org = new Org(name: orgName, workers: RandomOrg.GetRandomWorkers(random.Next(1, 4), random));
            org.Print(Console.Out);

            // В органиизации есть только отделы.
            orgName = "Deps_only";
            Console.WriteLine($"Нажмите любую клавишу для работы с организацией {orgName}.");
            Console.ReadKey(true);
            RandomOrg.managersOff = true;
            RandomOrg.workersOff = true;
            org = new Org(name: orgName, deps: RandomOrg.GetRandomDeps(orgName, random.Next(1, 3), random));
            org.Print(Console.Out);

            // В органиизации есть только работники.
            orgName = "Workers_and_Managers_only";
            Console.WriteLine($"Нажмите любую клавишу для работы с организацией {orgName}.");
            Console.ReadKey(true);
            RandomOrg.managersOff = true;
            RandomOrg.workersOff = false;
            org = new Org(name: orgName,
                managers: RandomOrg.GetRandomManagers(random.Next(1, 4), random),
                workers: RandomOrg.GetRandomWorkers(random.Next(1, 4), random));
            org.Print(Console.Out);

            // В органиизации нет менеджеров.
            orgName = "Workers_and_Deps_only";
            Console.WriteLine($"Нажмите любую клавишу для работы с организацией {orgName}.");
            Console.ReadKey(true);
            RandomOrg.managersOff = true;
            RandomOrg.workersOff = false;
            org = new Org(name: orgName,
                deps: RandomOrg.GetRandomDeps(orgName, random.Next(1, 3), random),
                workers: RandomOrg.GetRandomWorkers(random.Next(1, 4), random));
            org.Print(Console.Out);
            // Случайные организации со всеми полями.
            // Задаем имя, менеджеров, работников и, возможно, отделы.
            Console.WriteLine("Нажмите любую клавишу для работы со случайной организацией.");
            Console.ReadKey(true);
            do
            {
                // Организация.
                // Создаем организацию со случайным именем, случайными менеджерами, случайными отделами (которых может и не быть) и случайными работниками.
                RandomOrg.workersOff = false;
                RandomOrg.managersOff = false;
                org = RandomOrg.GetRandomOrg();
                // Сохраняем имя организации.
                orgName = org.Name;
                // Печатаем данные об организации.
                org.Print(Console.Out);
                #region Serialization & Deserialization Test
                Console.WriteLine("Сериализация и десериализация.");
                Console.WriteLine("В формате XML (Y/N)?");
                bool mode = Console.ReadKey(true).KeyChar == 'Y';
                string ext = mode ? ".xml" : ".json";// Расширение файла.
                Console.WriteLine("\t\t\tВ формате " + (mode ? "XML." : "Json."));
                #region Org Test
                // Организация
                Console.WriteLine("\t\tОрганизация в целом.");
                string path = orgName + ext;// Маршрут к файлу.
                XmlJsonStudio.Serialize(path, org, mode);// Сериализуем организацию.
                Console.WriteLine("Сериализация организации проведена.");
                Org reOrg = XmlJsonStudio.Deserialize<Org>(path, mode);// Восстановленная организация.
                Console.WriteLine("Восстановленная организация.");
                // Печатаем восстановленную организацию.
                reOrg.Print(Console.Out);
                #endregion
                if (org.Deps.Count > 0)
                {
                    Console.WriteLine("Нажмите любую клавишу для работы со случайным отделом.");
                    Console.ReadKey(true);
                    #region Dep Test
                    // Отдел
                    //Выбираем случайный отдел.
                    int depNmb = random.Next(org.Deps.Count);
                    Dep dep = org.Deps[depNmb];
                    Console.WriteLine($"\nОтдел {dep.Name}.");
                    path = dep.Name + ext;
                    XmlJsonStudio.Serialize(path, dep, mode);
                    Console.WriteLine($"Сериализация отдела {dep.Name} проведена. Нажмите любую клавишу для продолжения.");
                    Dep reDep = XmlJsonStudio.Deserialize<Dep>(path, mode);// Восстановленный отдел.
                    Console.WriteLine("Восстановленнй отдел.");
                    reDep.Print(Console.Out);
                    #endregion
                    Console.WriteLine("Нажмите любую клавишу для работы со случайным менеджером.");
                    Console.ReadKey(true);
                    #region Manager Test
                    // Отдельный сотрудник.
                    // Выбираем случайного менеджера.
                    int managerNmb = random.Next(dep.Managers.Count);// Номер менеджера
                    Manager manager = dep.Managers[managerNmb];
                    Console.WriteLine($"\nМенеджер с ID = {manager.ID} по имени {manager.Name} из отдела {dep.Name}");
                    // Маршрут к файлу.
                    path = dep.Name + "." + manager.Name + ext;
                    // Сериализуем его.
                    XmlJsonStudio.Serialize(path, manager, mode);
                    Console.WriteLine($"Сериализация менеджера {manager.ID} по имени {manager.Name} из отдела {dep.Name} проведена.");
                    // Воссоздаем менеджера из файла XML или Json.
                    Manager reManager = XmlJsonStudio.Deserialize<Manager>(path, mode);// Восстановленный менеджер.
                    Console.WriteLine(
                        $"Данные о менеджере {reManager.ID} по имени {reManager.Name} из отдела {dep.Name} после сериализации и восстановления в " + (mode ? "xml" : "json") + "-формате.");
                    // Печатаем на экране.
                    Employee.PrintHeader(Console.Out);
                    reManager.PrintFields(Console.Out);
                    #endregion
                    Console.WriteLine("Нажмите любую клавишу для работы со случайным работником.");
                    Console.ReadKey(true);
                    #region Worker Test
                    // Отдельный работник.
                    // Используем первый отдел, в котором есть только работники.
                    dep = RandomOrg.GetSealedDep(org.Deps[0]);
                    // Выбираем случайного работника.
                    int workerNmb = random.Next(dep.Workers.Count);// Номер работника.
                    Worker worker = dep.Workers[workerNmb];
                    Console.WriteLine($"\nРаботник с ID = {worker.ID} по имени {worker.Name} из отдела {dep.Name}");
                    // Маршрут к файлу.
                    path = dep.Name + "." + worker.Name + ext;
                    // Сериализуем его.
                    XmlJsonStudio.Serialize(path, worker, mode);
                    Console.WriteLine($"Сериализация работника {worker.ID} по имени {worker.Name} из отдела {dep.Name} проведена.");
                    // Воссоздаем работника из файла XML или Json.
                    Worker reWorker = XmlJsonStudio.Deserialize<Worker>(path, mode);// Восстановленный работник.
                    Console.WriteLine(
                        $"Данные о работнике {reWorker.ID} по имени {reWorker.Name} из отдела {dep.Name} после сериализации и восстановления в " + (mode ? "xml" : "json") + "-формате.");
                    // Печатаем на экране.
                    Employee.PrintHeader(Console.Out);
                    reWorker.PrintFields(Console.Out);
                    #endregion
                }
                #endregion
                Console.WriteLine("\nДля новой выборки нажмите любую клавишу. Для завершения нажмите esc.");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
