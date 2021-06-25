using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Описывает функционирование организации.
    /// </summary>
    /// <remarks>Планируется создать методы поиска, добавления и удаления отделов и сотрудников.</remarks>
    public class Org : Dep
    {
        #region Ctrs
        public Org() : base() { }
        public Org(string name = null, List<Manager> managers = null, List<Dep> deps = null, List<Worker> workers = null) : base(name, managers, deps, workers) { }
        #endregion
        /// <summary>
        /// Удаляет отдел, менеджера или работника из организации.
        /// </summary>
        /// <typeparam name="T">Тип удаляемого объекта - Dep, Manager или Worker.</typeparam>
        /// <param name="item">Удаляемый объект.</param>
        /// <returns>true, если удаление прошло успешно; false - в противном случае.</returns>
        public bool Remove<T>(T item) => typeof(T).Name == "Dep" ? RemoveDep(item as Dep) : base.Remove(item);
        /// <summary>
        /// Добавляет отдел, менеджера или работника в организацию.
        /// </summary>
        /// <typeparam name="T">Тип добавляемого объекта - Dep, Manager или Worker.</typeparam>
        /// <param name="item">Добавляемый объект.</param>
        /// <param name="dep">Отдел, в список которого добавляется объект.</param>
        public void Add<T>(T item, Dep dep) => base.Add(item, dep);
        /// <summary>
        /// Перемещает отдел, менеджера или работника.
        /// </summary>
        /// <typeparam name="T">Тип перемещаемого объекта - Dep, Manager или Worker.</typeparam>
        /// <param name="item">Перемещаемый объект.</param>
        /// <param name="dep">Отдел, в список которого переносится объект.</param>
        public void Move<T>(T item, Dep dep) { if (Remove(item)) Add(item, dep); }
        /// <summary>
        /// Восстанавливает менеджеров по заданному работнику до верха иерархии организации.
        /// </summary>
        /// <param name="worker">Заданный работник.</param>
        /// <remarks>Используется при смене зарплаты работника.</remarks>
        public void ResetManagers(Worker worker) => base.ResetManagers(worker);        // Вызываем унаследованный метод.
        /// <summary>
        /// Вычисляет маршрут к отделу, к менеджеру или к работнику.
        /// </summary>
        /// <param name="item">Объект, к которому надо вычислить маршрут.</param>
        /// <returns>Строку маршрута с указанием номеров всех отделов, разделенных точкой. Маршрут включает имя работника или менеджера.</returns>
        public string GetPath<T>(T item) =>
            "0" + (item is Dep ? GetPath(item as Dep, out _) : item is Worker ? GetPath(item as Worker, out _) : GetPath(item as Manager, out _));

    }
}
