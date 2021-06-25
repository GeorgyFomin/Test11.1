using System;
using System.IO;

namespace ClassLibrary
{
    /// <summary>
    /// Описывает сотрудника организации.
    /// </summary>
    public class Employee : Named
    {
        /// <summary>
        /// Хранит заголовок для текстового представления сотрудника.
        /// </summary>
        public static readonly string header = string.Format("№\t\tName\tSalary");
        #region Properties
        /// <summary>
        /// Устанавливает и возвращает размер оклада сотрудника.
        /// </summary>
        public int Salary { get; set; }
        /// <summary>
        /// Устанавливает и возвращает уникальный номер.
        /// </summary>
        public uint ID { get; set; }
        #endregion
        /// <summary>
        /// Инициализирует поле ID.
        /// </summary>
        protected Employee() : base() => ID = (uint)GetHashCode();
        #region Printing
        /// <summary>
        /// Печатает заголовок полей сотрудника.
        /// </summary>
        /// <param name="tw">Райтер.</param>
        public static void PrintHeader(TextWriter tw) => tw.WriteLine(header);
        /// <summary>
        /// Печатает поля сотрудника.
        /// </summary>
        /// <param name="tw">Райтер.</param>
        public void PrintFields(TextWriter tw) => tw.WriteLine(Info(false));
        #endregion
        /// <summary>
        /// Готовит текстовое представление информации об объекте.
        /// </summary>
        /// <param name="w">Определяет выбор формата - true для вывода в окно (по умолчанию) и false для вывода на консоль.</param>
        /// <returns>Возвращает текстовое представление информации об объекте.</returns>
        public string Info(bool w = true) => w ? string.Format(header + $"\n{ID,-16}\t{Name}\t{Salary}") : string.Format($"{ID,-16}{Name,-8}{Salary,-32}");
    }
}
