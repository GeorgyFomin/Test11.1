using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Named
    {
        /// <summary>
        /// Устанавливает и возвращает имя сотрудника.
        /// </summary>
        public string Name { get; set; }
        protected Named() => Name = "Noname";
        /// <summary>
        /// Перекрывает унаследованный метод текстового представления объекта.
        /// </summary>
        /// <returns>Идентификатор сотрудника.</returns>
        public override string ToString() => Name;

    }
}
