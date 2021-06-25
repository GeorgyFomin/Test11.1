using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Описывает отдел организации.
    /// </summary>
    public class Dep : Named
    {
        #region Fields
        /// <summary>
        /// Хранит список работников отдела.
        /// </summary>
        List<Worker> workers = new List<Worker>();
        /// <summary>
        /// Хранит список подчиненных отделов.
        /// </summary>
        List<Dep> deps = new List<Dep>();
        /// <summary>
        /// Хранит список менеджеров.
        /// </summary>
        List<Manager> managers = new List<Manager>();
        #endregion
        #region Properties
        /// <summary>
        /// Возвращает уникальный идентификатор отдела.
        /// </summary>
        public Guid Guid { get { return guid; } }
        /// <summary>
        /// Хранит уникальный идентификатор отдела.
        /// </summary>
        Guid guid = Guid.NewGuid();
        /// <summary>
        /// Устанавливает и возвращает ссылку на менеджеров отдела.
        /// </summary>
        public List<Manager> Managers
        {
            get => managers;
            set
            {
                managers = value ?? new List<Manager>();
                // Указываем всем менеджерам их зарплату.
                foreach (Manager manager in managers)
                {
                    manager.Salary = Manager.GetSalary(this);
                }
            }
        }
        /// <summary>
        /// Устанавливает и возвращает подчиненные отделы.
        /// </summary>
        public List<Dep> Deps { get => deps; set { deps = value ?? new List<Dep>(); } }
        /// <summary>
        /// Устанавливает и возвращает ссылку на список сотрудников, входящих в состав отдела.
        /// </summary>
        public List<Worker> Workers { get => workers; set { workers = value ?? new List<Worker>(); } }
        /// <summary>
        /// Возвращает информацию об отделе.
        /// </summary>
        /// <param name="w">
        /// Определяет выбор формата - для вывода в качестве подсказки в окно (true по умолчанию), или для вывода на консоль.
        /// </param>
        public string Info(bool w = true)
        {
            // Оформляем заголовок отдела.
            string str = (this is Org ? "Организация " : "Отдел ") + Name;
            // Добавляем информацию об оделах.
            str += Deps.Count == 0 ? "\nПодчиненных отделов нет." : $"\nЧисло подчиненных отделов {Deps.Count}";
            // Добавляем информацию о менеджерах.
            if (Managers.Count == 0)
                str += "\nМенеджеры отсутствуют.";
            else
            if (!w)
            {
                str += "\n\tМенеджеры\n" + Employee.header;
                foreach (Manager manager in Managers)
                    str += "\n" + manager.Info(w);
            }
            else
                str += $"\nЧисло менеджеров {Managers.Count}";
            // Добавляем информацию о работниках.
            if (Workers.Count == 0)
                str += "\nРаботников нет.";
            else
            if (!w)
            {
                str += "\n\tРаботники\n" + Employee.header;
                foreach (Worker worker in Workers)
                    str += "\n" + worker.Info(w);
            }
            else
                str += $"\nЧисло работников {Workers.Count}";
            return string.Format(str);
        }
        #endregion
        #region Ctrs
        /// <summary>
        /// Инициализирует поля отдела.
        /// </summary>
        public Dep() : base() { }
        /// <summary>
        /// Инициализирует поля отдела.
        /// </summary>
        /// <param name="name">Название отдела.</param>
        /// <param name="managers">Список руководителей отдела.</param>
        /// <param name="deps">Список подчиненных отделов.</param>
        /// <param name="workers">Список работников.</param>
        public Dep(string name = null, List<Manager> managers = null, List<Dep> deps = null, List<Worker> workers = null)
        {
            Name = name ?? "Noname";// Присваиваем имя.
            Deps = deps;// Сохраняем подчиненные отделы.
            Workers = workers;// Сохраняем работников.
            // Сохраняем список менеджеров.
            Managers = managers;
        }
        #endregion
        /// <summary>
        /// Печатает сведения об отделе.
        /// </summary>
        /// <param name="tw">Райтер.</param>
        public void Print(TextWriter tw)
        {
            // Печатаем информацию об отделе.
            tw.WriteLine(Info(false));
            // Печатаем сведения об отделах.
            foreach (Dep dep in Deps)
            {
                dep.Print(tw);
            }
        }
        #region Methods
        /// <summary>
        /// Обновляет менеджеров отдела.
        /// </summary>
        /// <param name="dep">Отдел.</param>
        static public void ResetManagers(Dep dep) => dep.Managers = dep.Managers;
        /// <summary>
        /// Восстанавливает менеджеров по заданному работнику.
        /// </summary>
        /// <param name="worker">Работник.</param>
        /// <param name="path">Маршрут, используемый для рекурсии.</param>
        protected void ResetManagers(Worker worker, List<Dep> path = null)
        {
            // При первом входе инициализируем список маршрута.
            if (path == null)
                path = new List<Dep>();
            // Добавляем в список маршрута вызывающий объект.
            path.Add(this);

            // Ищем среди подчиненных работников.
            foreach (Worker item in Workers)
                if (worker.ID == item.ID)
                {
                    // Если работник найден.
                    // Обновляем менеджеров во всех отделах, стоящих выше по иерархии.
                    for (int i = path.Count - 1; i > -1; i--)
                        ResetManagers(path[i]);
                    return;
                }
            // Обращаемся к подчиненным отделам.
            foreach (Dep curDep in Deps)
                // Возвращаемся к поиску в коллекии работников текущего отдела. 
                curDep.ResetManagers(worker, path);
            // Если поиск по текущей ветви не дал результата,
            // то последняя ссылка в маршруте стирается, возвращая маршрут в предыдущее состояние.
            path.RemoveAt(path.Count - 1);
        }
        /// <summary>
        /// Удаляет отдел из списка подчиненных отделов.
        /// </summary>
        /// <param name="dep">Отдел, который следует удалить из списка подчиненных отделов.</param>
        /// <param name="path">Список отделов, образующих маршрут к удаленному объекту. Используется только для рекурсии.</param>
        /// <returns>true, если отдел удален успешно; false в противном случае.</returns>
        protected bool RemoveDep(Dep dep, List<Dep> path = null)
        {
            bool result = false;
            // При первом входе инициализируем список маршрута.
            if (path == null)
                path = new List<Dep>();
            // Добавляем в список маршрута вызывающий объект.
            path.Add(this);
            // Проходим по всем подчиненным отделам, если таковые есть.
            foreach (Dep item in Deps)
                // Если это какой-либо из подчиненных отделов.
                if (item.Guid == dep.Guid)
                {
                    // Он удаляется из списка отделов.
                    result = Deps.Remove(item);
                    // Обновляем менеджеров во всех отделах, стоящих выше по иерархии.
                    for (int i = path.Count - 1; i > -1; i--)
                        ResetManagers(path[i]);
                    // Поиск прекращается.
                    break;
                }
                else
                {
                    // Если текущий подчиненный отдел не относится к искомым, то поиск возобновляется среди его подчиненных отделов.
                    result = item.RemoveDep(dep, path);
                }
            // Если поиск по текущей ветви не дал результата, то последняя ссылка в маршруте стирается.
            path.RemoveAt(path.Count - 1);
            // Возвращаем результат.
            return result;
        }
        /// <summary>
        /// Удаляет сотрудника (менеджера или работника) из отдела.
        /// </summary>
        /// <typeparam name="T">Тип сотрудника.</typeparam>
        /// <param name="employee">Ссылка на сотрудника.</param>
        /// <param name="path">Список маршрута отделов.</param>
        /// <returns>true при успешном удалении, false в противном случае.</returns>
        protected bool Remove<T>(T employee, List<Dep> path = null)
        {
            // Хранит результат операции по удалению - успешная или нет.
            bool result = false;
            // При первом входе инициализируем список маршрута.
            if (path == null)
                path = new List<Dep>();
            // Добавляем в список маршрута вызывающий объект.
            path.Add(this);
            // Пытаемся удалить из списков менеджеров или работников.
            bool Remove()
            {
                switch (typeof(T).Name)
                {
                    case "Manager":
                        foreach (Manager manager in Managers)
                            if (manager.ID == (employee as Manager).ID && (result = Managers.Remove(manager)))
                            {
                                // Обновляем менеджеров во всех отделах, стоящих выше по иерархии.
                                for (int i = path.Count - 1; i > -1; i--)
                                    ResetManagers(path[i]);
                                // Поиск прекращается.
                                break;
                            }
                        break;
                    case "Worker":
                        foreach (Worker worker in Workers)
                            if (worker.ID == (employee as Worker).ID && (result = Workers.Remove(worker)))
                            {
                                // Обновляем менеджеров во всех отделах, стоящих выше по иерархии.
                                for (int i = path.Count - 1; i > -1; i--)
                                    ResetManagers(path[i]);
                                // Поиск прекращается.
                                break;
                            }
                        break;
                }
                return result;
            }
            // Пытаемся удалить из списков отдела методом Remove.
            // При неудаче проходим по всей коллекции подчиненных отделов.
            if (!Remove())
            {
                foreach (Dep dep in Deps)
                    if (result = dep.Remove(employee, path))
                        return result;
                // Если поиск по текущей ветви не дал результата,
                // то последняя ссылка в маршруте стирается, возвращая маршрут в предыдущее состояние.
                path.RemoveAt(path.Count - 1);
            }
            // Возвращаем результат.
            return result;
        }
        /// <summary>
        /// Добавляет отдел, менеджера или работника в одну из коллекций.
        /// </summary>
        /// <param name="item">Добавляемый отдел или сотрудник.</param>
        /// <param name="dep">Отдел, к которому добавляется.</param>
        /// <param name="path">Маршрут - список обновляемых отделов.</param>
        /// <returns></returns>
        protected void Add<T>(T item, Dep dep, List<Dep> path = null)
        {
            // Добавляет к определенного типа коллекции отдела новый элемент.
            void AddItem(Dep curDep)
            {
                switch (typeof(T).Name)
                {
                    case "Dep":
                        // Добавляем отдел в коллекцию подчиненных отделов.
                        curDep.Deps.Add(item as Dep); break;
                    case "Manager":
                        // Добавляем менеджера в коллекцию менеджеров.
                        curDep.Managers.Add(item as Manager); break;
                    case "Worker":
                        // Добавляем работника в коллекцию работников.
                        curDep.Workers.Add(item as Worker); break;
                }
                // Обновляем менеджеров вызываемого отдела.
                ResetManagers(curDep);
            }
            // При первом входе инициализируем список маршрута.
            if (path == null)
                path = new List<Dep>();
            // Добавляем в список маршрута вызывающий объект.
            path.Add(this);
            // Если добавляющий отдел совпадает с вызываемым.
            if (Guid == dep.Guid)
                AddItem(this);
            else
            {
                // Ищем среди подчиненных отделов тот который редактируется.
                foreach (Dep curDep in Deps)
                {
                    // Если добавляющий отдел совпадает с текущим.
                    if (curDep.Guid == dep.Guid)
                    {
                        AddItem(curDep);
                        // Обновляем менеджеров во всех отделах, стоящих выше по иерархии.
                        for (int i = path.Count - 1; i > -1; i--)
                            ResetManagers(path[i]);
                        // Поиск прекращается.
                        break;
                    }
                    else
                        // Если добавляющий отдел не является текущим, то текущий вызывает AddDep.
                        curDep.Add(item, dep, path);
                }
            }
            // Если поиск по текущей ветви не дал результата,
            // то последняя ссылка в маршруте стирается, возвращая маршрут в предыдущее состояние.
            path.RemoveAt(path.Count - 1);
        }
        /// <summary>
        /// Вычисляет маршрут к отделу из текущего отдела.
        /// </summary>
        /// <param name="dep">Отдел, к которому вычисляется маршрут.</param>
        /// <param name="isEnd">Флаг завершения поиска.</param>
        /// <param name="path">Исходный маршрут, если он задан.</param>
        /// <returns>Маршрут к отделу</returns>
        /// <remarks>
        /// Маршрут это строка, состоящая из индексов подчиненных отделов, разделенных точками.
        /// Например, маршрут ".3.12.1" означает, что заданный отдел является 
        /// первым в списке двенадцатого отдела в списке третьего отдела в списке вызываемого отдела, если исходный маршрут не задан.</remarks>
        protected string GetPath(Dep dep, out bool isEnd, StringBuilder path = null)
        {
            // Инициализируем маршрут, если он отсутствует.
            if (path == null)
                path = new StringBuilder();
            // Инициализацируем флаг завершения поиска.
            isEnd = false;
            // Ищем отдел с совпадающим Guid.
            for (int index = 0; index < Deps.Count; index++)
            {
                // Достраиваем маршрут в ходе поиска.
                path.Append("." + index);
                // Обновляем флаг завершения поиска.
                isEnd = Deps[index].Guid == dep.Guid;
                // Если поиск не завершен, продолжаем его в подчиненных отделах.
                if (!isEnd)
                    Deps[index].GetPath(dep, out isEnd, path);
                // Если поиск завершен, возвращаем результат.
                if (isEnd)
                    return path.ToString();
            }
            // Убираем сегмент маршрута, приведшего в тупик.
            int periodLastIndex = path.ToString().LastIndexOf(".");
            if (periodLastIndex > -1)
                path.Remove(periodLastIndex, path.Length - periodLastIndex);
            // Возвращаем маршрут.
            return path.ToString();
        }
        /// <summary>
        /// Вычисляет маршрут к сотруднику из текущего отдела.
        /// </summary>
        /// <typeparam name="T">Тип объекта сотрудника - менеджер или работник.</typeparam>
        /// <param name="item">Объект сотрудника.</param>
        /// <param name="isEnd">Флаг завершения поиска.</param>
        /// <param name="path">Текущий маршрут.</param>
        /// <returns>
        /// Маршрут - это строка, состоящая из индексов подчиненных отделов, разделенных точками.
        /// Например, маршрут ".3.12.1.name" означает, что сотрудник находится в отделе, который является 
        /// первым в списке двенадцатого отдела в списке третьего отдела в списке вызываемого отдела, если исходный маршрут не задан.
        /// </returns>
        internal string GetPath<T>(T item, out bool isEnd, StringBuilder path = null) where T : Employee
        {
            if (path == null)
                // Инициализируем маршрут, если он отсутствует.
                path = new StringBuilder();
            isEnd = false;
            // Проверяет наличие сотрудника в списке отдела dep.
            bool Test(Dep dep)
            {
                // Инициализируем результат проверки.
                bool result = false;
                // Проводим тест.
                if (item is Worker)
                {
                    foreach (Worker worker in dep.Workers)
                        if (result = item.ID == worker.ID)
                            break;
                }
                else
                    foreach (Manager manager in dep.Managers)
                        if (result = item.ID == manager.ID)
                            break;
                // Возвращаем результат проверки.
                return result;
            }
            // Проверяем наличие сотрудника в текущем отделе.
            if (!Test(this))
            {
                // Если сотрудника нет в текущем отделе, просматриваем все подчиненные отделы.
                for (int index = 0; index < Deps.Count; index++)
                {
                    // Достраиваем маршрут.
                    path.Append("." + index);
                    // Обновляем флаг завершения поиска.
                    isEnd = Test(Deps[index]);
                    // Если поиск не завершен, продолжаем его в подчиненных отделах.
                    if (!isEnd)
                        Deps[index].GetPath(item, out isEnd, path);
                    // Если поиск завершен, возвращаем результат.
                    if (isEnd)
                        return path.ToString() + "." + item.ID;
                }
            }
            // Убираем тупиковый сегмент маршрута.
            int lastIndexOfPoint = path.ToString().LastIndexOf(".");
            if (lastIndexOfPoint > -1)
                path.Remove(lastIndexOfPoint, path.Length - lastIndexOfPoint);
            // Возвращаем маршрут.
            return path.ToString() + "." + item.ID;
        }
        #endregion
    }
}
