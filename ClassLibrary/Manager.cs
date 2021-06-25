namespace ClassLibrary
{
    /// <summary>
    /// Описывает сотрудника-менеджера.
    /// </summary>
    public class Manager : Employee
    {
        /// <summary>
        /// Хранит минимальную зарплату менеджера.
        /// </summary>
        const int minManagerSalary = 1300;
        /// <summary>
        /// Устанавливает зарплату менеджеру.
        /// </summary>
        /// <param name="dep"></param>
        /// <returns>Зарплата менеджера.</returns>
        public static int GetSalary(Dep dep)
        {
            // Подсчитываем зарплату менеджера.
            int salary = 0;
            // Суммируем зарплаты подчиненных сотрудников.
            foreach (Worker worker in dep.Workers)
                salary += worker.Salary;
            // Вычисляем 15% суммарной зарплаты сотрудников.
            salary = (int)(.15 * salary);
            // Суммируем зарплаты подчиненных менеджеров.
            foreach (Dep curDep in dep.Deps)
                foreach (Manager manager in curDep.Managers)
                    salary += manager.Salary;
            // Возвращаем значение minManagerSalary, если сумма оказалась меньше и если в отделе есть не только менеджеры.
            return salary < minManagerSalary && (dep.Deps.Count > 0 || dep.Workers.Count > 0) ? minManagerSalary : salary;
        }
        /// <summary>
        /// Инициализирует поля менеджера значениями по умолчанию.
        /// </summary>
        public Manager() : base() { }
        /// <summary>
        /// Инициализирует поля менеджера.
        /// </summary>
        /// <param name="name">Имя менеджера.</param>
        public Manager(string name = null) : this() => Name = string.IsNullOrEmpty(name) ? "Noname" : name;
    }
}
