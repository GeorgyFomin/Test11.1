namespace ClassLibrary
{
    /// <summary>
    /// Описывает сотрудника-рабочего или интерна.
    /// </summary>
    public class Worker : Employee
    {
        /// <summary>
        /// Инициализирует значения полей по умолчанию.
        /// </summary>
        public Worker() : base() { }
        /// <summary>
        /// Инициализирует поля работника.
        /// </summary>
        /// <param name="name">Имя сотрудника.</param>
        /// <param name="salary">Зарплата сотрудника.</param>
        public Worker(string name = null, int salary = 0) : this() => (Name, Salary) = (string.IsNullOrEmpty(name) ? "Noname" : name, salary);
    }
}
