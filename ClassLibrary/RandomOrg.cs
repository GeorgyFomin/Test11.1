using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ClassLibrary
{
    /// <summary>
    /// Обеспечивает методы случайной выборки.
    /// </summary>
    public static class RandomOrg
    {
        /// <summary>
        /// Хранит ссылку на генератор случайных чисел.
        /// </summary>
        static public readonly Random random = new Random();
        /// <summary>
        /// Хранит флаг, указывающий на отсутствие в отделе менеджеров.
        /// </summary>
        public static bool managersOff;
        /// <summary>
        /// Хранит флаг, указывающий на отсутствие в отделе работников.
        /// </summary>
        public static bool workersOff;
        /// <summary>
        /// Генерирует случайную организацию.
        /// </summary>
        /// <returns>Случайную организацию.</returns>
        public static Org GetRandomOrg()
        {
            string orgName = GetRandomString(random.Next(2, 7), random);
            return new Org(orgName,
                GetRandomManagers(random.Next(1, 4), random),
                GetRandomDeps(orgName, random.Next(1, 3), random),
                GetRandomWorkers(random.Next(1, 4), random));
        }
        /// <summary>
        /// Ищет отдел, не состоящий из отделов.
        /// </summary>
        /// <param name="dep">Пробный отдел.</param>
        /// <returns>Отдел, в котором есть только сотрудники.</returns>
        public static Dep GetSealedDep(Dep dep) => dep.Deps.Count > 0 ? GetSealedDep(dep.Deps[0]) : dep;
        /// <summary>
        /// Создает список случайных отделов.
        /// </summary>
        /// <param name="rootPath">Имя корневого отдела.</param>
        /// <param name="v">Число отделов.</param>
        /// <param name="random">Генератор случайных чисел.</param>
        /// <returns></returns>
        public static List<Dep> GetRandomDeps(string rootPath, int v, Random random)
        {
            // Возвращает текущий отдел.
            Dep CurDep(int index)
            {
                if (string.IsNullOrEmpty(rootPath))
                {
                    rootPath = "Noname";
                }
                // Имя отдела определяем по корневому имени, модифицируя его случайным именем.
                string path = rootPath + "." + index.ToString();// GetRandomString(random.Next(2, 6), random);
                // Создаем текущий отдел.
                return
                    new Dep(
                        path,
                        managersOff ? null : GetRandomManagers(random.Next(1, 4), random),
                        random.NextDouble() < .5 ? null : GetRandomDeps(path, random.Next(1, 3), random),
                        workers: workersOff ? null : GetRandomWorkers(random.Next(2, 6), random));
            }
            // Возвращаем список отделов.
            return Enumerable.Range(0, v).Select(CurDep).ToList();
        }
        /// <summary>
        /// Создает случайный список менеджеров.
        /// </summary>
        /// <param name="v">Количество менеджеров.</param>
        /// <param name="random">Генератор случайных чисел.</param>
        /// <returns>Список созданных менеджеров.</returns>
        public static List<Manager> GetRandomManagers(int v, Random random)
            => Enumerable.Range(0, v).Select(index => new Manager("M" + index.ToString()/* GetRandomString(random.Next(2, 6), random)*/)).ToList();
        /// <summary>
        /// Создает случайный список работников.
        /// </summary>
        /// <param name="v">Количество работников.</param>
        /// <param name="random">Генератор случайных чисел.</param>
        /// <returns>Список созданных работников.</returns>
        public static List<Worker> GetRandomWorkers(int v, Random random)
            => Enumerable.Range(0, v).Select(index => new Worker("W" + index.ToString()/*GetRandomString(random.Next(2, 6), random)*/,
                random.Next(1_000, 10_000))).ToList();
        /// <summary>
        /// Генерирует случайную строку из латинских букв нижнего регистра..
        /// </summary>
        /// <param name="length">Длина строки.</param>
        /// <param name="random">Генератор случайных чисел.</param>
        /// <returns></returns>
        public static string GetRandomString(int length, Random random)
            => new string(Enumerable.Range(0, length).Select(x => (char)random.Next('a', 'z' + 1)).ToArray());
    }
}
