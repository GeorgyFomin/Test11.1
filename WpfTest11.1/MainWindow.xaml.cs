using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brushes = System.Windows.Media.Brushes;
using Image = System.Windows.Controls.Image;

namespace WpfTest11._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        /// <summary>
        /// Хранят иконки узлов. Берем из ресурсов.
        /// </summary>
        static readonly BitmapImage[] images =
            {
            new BitmapImage(new Uri("pack://application:,,,/images/org.png")),
            new BitmapImage(new Uri("pack://application:,,,/images/wDep.png")),
            new BitmapImage(new Uri("pack://application:,,,/images/mDep.png")),
            new BitmapImage(new Uri("pack://application:,,,/images/manager.png")),
            new BitmapImage(new Uri("pack://application:,,,/images/worker.png"))
        };
        /// <summary>
        /// Хранят контекстные меню.
        /// </summary>
        readonly ContextMenu
            orgMenu = new ContextMenu(), depMenu = new ContextMenu(),
            workerMenu = new ContextMenu(), managerMenu = new ContextMenu(),
            sortMenu = new ContextMenu();
        /// <summary>
        /// Хранят 
        /// </summary>
        readonly TextBox
            orgNameBox = new TextBox() { ToolTip = "Имя организации" }, depNameBox = new TextBox() { ToolTip = "Имя отдела" },
            managerNameBox = new TextBox() { ToolTip = "Имя менеджера" }, workerNameBox = new TextBox() { ToolTip = "Имя работника" },
            salaryBox = new TextBox() { ToolTip = "Зарплата работника" };
        readonly MenuItem
            // Org menu items
            orgEditItem = new MenuItem() { Header = "Edit" }, orgAddItem = new MenuItem() { Header = "Add" },
            orgAddDepItem = new MenuItem() { Header = "Отдел" }, orgAddMItem = new MenuItem() { Header = "Менеджера" },
            orgAddWItem = new MenuItem() { Header = "Работника" },
            // Dep menu items
            depRemoveItem = new MenuItem() { Header = "Remove" }, depEditItem = new MenuItem() { Header = "Edit" }, depAddItem = new MenuItem() { Header = "Add" },
            depAddDepItem = new MenuItem() { Header = "Отдел" }, depAddMItem = new MenuItem() { Header = "Менеджера" },
            depAddWItem = new MenuItem() { Header = "Работника" },
            // Manager menu items
            mRemoveItem = new MenuItem() { Header = "Remove" }, mEditItem = new MenuItem() { Header = "Edit" },
            // Worker menu items
            wRemoveItem = new MenuItem() { Header = "Remove" }, wEditItem = new MenuItem() { Header = "Edit" },
            sortItem = new MenuItem() { Header = "Sort by" }, sortByIdItem = new MenuItem() { Header = "ID" },
            sortByNameItem = new MenuItem() { Header = "Name" }, sortBySalaryItem = new MenuItem() { Header = "Salary" };
        /// <summary>
        /// Хранит ссылку на организацию.
        /// </summary>
        Org org;
        /// <summary>
        /// Хранит ссылку на редактируемый через контекстное меню объект типа Org, Dep, Manager или Worker. 
        /// </summary>
        object selectedNodeTag;
        /// <summary>
        ///  Хранит целевой отдел при перемещении.
        /// </summary>
        Dep targetDep;
        /// <summary>
        /// Текущий узел, перемещаемый во время drag N drop.
        /// </summary>
        TreeViewItem draggedNode;
        /// <summary>
        /// Хранит текущий порядок сортировки имен отделов.
        /// </summary>
        ListSortDirection curDepListSortDirection;
        #endregion
        /// <summary>
        /// Инициализирует поля окна.
        /// </summary>
        public MainWindow()
        {
            // Отказываемся от сообщений, касающихся связи с источником данных, за исключением критических.
            //System.Diagnostics.PresentationTraceSources.DataBindingSource.Switch.Level = System.Diagnostics.SourceLevels.Critical;
            InitializeComponent();
            // Собираем контекстное меню.
            SetContextMenus();
            // Устанавливаем обработчики окна и дерева.
            Loaded += InitHandler;
            orgView.MouseDoubleClick += InitHandler;
            // Устанавливаем обработчик сортировки отделов.
            depListView.MouseDoubleClick += DepListView_MouseDoubleClick;
            // Устанавливаем обработчики кликов по спискам с тем, чтобы снять выделение.
            depListView.MouseDown += UnselectClick;
            mListView.MouseDown += UnselectClick;
            wListView.MouseDown += UnselectClick;
            // Прикрепляем контекстное меню сортировки.
            mListView.ContextMenu = wListView.ContextMenu = sortMenu;
        }
        #region Methods
        /// <summary>
        /// Инициализирует контекстные меню организации, отделов, менеджеров и работников.
        /// Подключает элементы меню (команды) и их обработчики.
        /// </summary>
        void SetContextMenus()
        {
            // Заполняем меню организации. Добавляем обработчики.
            orgMenu.Opened += OrgMenu_Opened;
            orgEditItem.Items.Add(orgNameBox);
            orgMenu.Items.Add(orgEditItem);
            orgNameBox.KeyDown += OrgNameBox_KeyDown;
            orgAddItem.Items.Add(orgAddDepItem);
            orgAddDepItem.Click += OrgAddDepItem_Click;
            orgAddItem.Items.Add(orgAddMItem);
            orgAddMItem.Click += OrgAddMItem_Click;
            orgAddItem.Items.Add(orgAddWItem);
            orgAddWItem.Click += OrgAddWItem_Click;
            orgMenu.Items.Add(orgAddItem);
            // Заполняем меню отдела.Добавляем обработчики.
            depMenu.Opened += DepMenu_Opened;
            depMenu.Items.Add(depRemoveItem);
            depRemoveItem.Click += DepRemoveItem_Click;
            depEditItem.Items.Add(depNameBox);
            depNameBox.KeyDown += DepNameBox_KeyDown;
            depMenu.Items.Add(depEditItem);
            depAddItem.Items.Add(depAddDepItem);
            depAddDepItem.Click += DepAddDepItem_Click;
            depAddItem.Items.Add(depAddMItem);
            depAddMItem.Click += DepAddMItem_Click;
            depAddItem.Items.Add(depAddWItem);
            depAddWItem.Click += DepAddWItem_Click;
            depMenu.Items.Add(depAddItem);
            // Заполняем меню менеджера.Добавляем обработчики.
            managerMenu.Opened += ManagerMenu_Opened;
            managerMenu.Items.Add(mRemoveItem);
            mRemoveItem.Click += MRemoveItem_Click;
            mEditItem.Items.Add(managerNameBox);
            managerNameBox.KeyDown += ManagerNameBox_KeyDown;
            managerMenu.Items.Add(mEditItem);
            // Заполняем меню работника.Добавляем обработчики.
            workerMenu.Opened += WorkerMenu_Opened;
            workerMenu.Items.Add(wRemoveItem);
            wRemoveItem.Click += WRemoveItem_Click;
            wEditItem.Items.Add(workerNameBox);
            workerNameBox.KeyDown += WorkerNameBox_KeyDown;
            wEditItem.Items.Add(salaryBox);
            salaryBox.KeyDown += SalaryBox_KeyDown;
            workerMenu.Items.Add(wEditItem);
            // Заполняем меню сортировки.
            sortMenu.Items.Add(sortItem);
            sortItem.Items.Add(sortByIdItem);
            sortItem.Items.Add(sortByNameItem);
            sortItem.Items.Add(sortBySalaryItem);
            // Добавляем обработчик к вызовам команд меню сортировки.
            sortByIdItem.Click += SortItems_Click;
            sortByNameItem.Click += SortItems_Click;
            sortBySalaryItem.Click += SortItems_Click;
        }
        /// <summary>
        /// Обновляет состав организации случайным набором данных и населяет элементы дерева этими данными.
        /// </summary>
        void InitOrgView()
        {
            // Организация.
            // Создаем организацию со случайными отделами.
            // Имя организации orgName.
            org = RandomOrg.GetRandomOrg();
            // Заселяем дерево данными об организации.
            ResetOrgView();
        }
        /// <summary>
        /// Обновляет дерево и заселяет его данными об организации.
        /// </summary>
        void ResetOrgView()
        {
            // Определяем заголовок окна.
            Title = "Организация " + org.Name;
            // Очищаем элементы orgView. 
            orgView.Items.Clear();
            // Создаем узел.
            TreeViewItem node = new TreeViewItem()
            {
                AllowDrop = true, // Открываем доступ к операции drag and drop.
                ContextMenu = orgMenu,// Подключаем контекстное меню.
                Tag = org,// Ссылку на объект организации помещаем в Tag узла.
                // Формируем Tip узла организации.
                ToolTip = new ToolTip()
                {
                    Content = org.Info(),
                    Style = (Style)TryFindResource("toolTipStyle")
                }
            };
            // Определяем внешний вид узла.
            Image image = new Image() { Source = images[0], Width = 20, Height = 20, Tag = node, AllowDrop = true };
            node.Header = new TextBlock()
            {
                Foreground = Brushes.Yellow,
                Inlines = { image, org.Name }
            };
            // Обновляем заголовок окна.
            Title = org.Name;
            // Устанавливаем связь со списками, которые должны отображаться в ListView.
            depListView.ItemsSource = org.Deps;
            mListView.ItemsSource = org.Managers;
            wListView.ItemsSource = org.Workers;
            // Подключаем обработчик передачи данных в списки ListView.
            image.MouseDown += Image_MouseDown;
            // Добавляем организацию к дереву.
            orgView.Items.Add(node);
            // Подключаем обработчик Drop.
            image.Drop += Image_Drop;
            // Добавляем к организации менеджеров, работников и отделы.
            Add(orgView.Items, org.Managers);
            Add(orgView.Items, org.Workers);
            Add(node.Items, org.Deps);
        }
        /// <summary>
        /// Добавляет к коллекции элементов коллекцию объектов типа менеджер, работник или отдел.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="items">Коллекция дерева.</param>
        /// <param name="list">Список объектов.</param>
        void Add<T>(ItemCollection items, List<T> list)
        {
            foreach (T item in list)
            {
                // Размеры иконки зависят от типа узла.
                int Side()
                { return item is Dep && (item as Dep).Deps.Count > 0 ? 18 : item is Dep ? 16 : 14; }
                // Создаем узел.
                TreeViewItem node = new TreeViewItem()
                {
                    // Определяем доступ в операции Drag and Drop.
                    AllowDrop = item is Dep,
                    // Подключаем контекстное меню.
                    ContextMenu =
                        item is Manager ? managerMenu :
                        item is Worker ? workerMenu :
                        depMenu,
                    Tag = item,// Ссылку на объект помещаем в Tag узла.
                    // Формируем Tip узла
                    ToolTip = new ToolTip()
                    {
                        Content = item is Manager ? "\tManager\n" + (item as Manager).Info() :
                        item is Worker ? "\tWorker\n" + (item as Worker).Info() :
                        (item as Dep).Info(),
                        Style = (Style)TryFindResource("toolTipStyle")
                    }
                };
                // Определяем внешний вид узла.
                Image image = new Image()
                {
                    Source = images[item is Manager ? 3 : item is Worker ? 4 : (item as Dep).Deps.Count > 0 ? 1 : 2],
                    Tag = node,
                    Width = Side(),
                    Height = Side(),
                    AllowDrop = item is Dep
                };
                // Задаем загаловок.
                node.Header = new TextBlock()
                {
                    Foreground = Brushes.Yellow,
                    Inlines = { image, (item as Named).Name }
                };
                // Добавляем обработчик перемещения мышки.
                image.MouseMove += Image_MouseMove;
                // Добавляем узел в исходную коллекцию.
                items.Add(node);
                // Добавляем к узлу, связанному с отделом, обработчик события Drop и узлы входящих в отдел коллекций.
                if (item is Dep)
                {
                    // Подключаем обработчик передачи данных в списки ListView.
                    image.MouseDown += Image_MouseDown;
                    // Текущий отдел.
                    Dep dep = item as Dep;
                    // Подключаем обработчик Drop.
                    image.Drop += Image_Drop;
                    // Добавляем к узлу менеджеров, работников и отделы.
                    Add(node.Items, dep.Managers);
                    Add(node.Items, dep.Workers);
                    Add(node.Items, dep.Deps);
                    if (selectedNodeTag != null && selectedNodeTag is Dep && (selectedNodeTag as Dep).Guid == dep.Guid)
                        // Раскроем узел.
                        node.ExpandSubtree();
                    if (targetDep != null && targetDep.Guid == dep.Guid)
                        // Раскроем узел.
                        node.ExpandSubtree();
                }
            }
        }
        #region Handlers
        /// <summary>
        /// Вызывается при загрузке формы и при двойном клике по дереву.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InitHandler(object sender, EventArgs e)
        {
            InitOrgView();
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dep dep = ((e.Source as Image).Tag as TreeViewItem).Tag as Dep;
            // Обновляем заголовок окна.
            Title = dep.Name;
            // Устанавливаем связь со списками, которые должны отображаться в ListView.
            depListView.ItemsSource = dep.Deps;
            mListView.ItemsSource = dep.Managers;
            wListView.ItemsSource = dep.Workers;
        }
        private void UnselectClick(object sender, MouseButtonEventArgs e)
        {
            // Снимаем выделение элементов, если оно было.
            (sender as ListView).UnselectAll();
        }
        #region Sorting
        private void DepListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Меняем порядок сортировки на противоположный.
            curDepListSortDirection = (ListSortDirection)(((int)curDepListSortDirection + 1) % 2);
            // Очищаем список сортировки.
            depListView.Items.SortDescriptions.Clear();
            // Сортируем список отделов по имени.
            depListView.Items.SortDescriptions.Add(new SortDescription("Name", curDepListSortDirection));
        }
        private void SortItems_Click(object sender, RoutedEventArgs e)
        {
            // Фиксируем выбранный элемент меню.
            MenuItem clickedItem = sender as MenuItem;
            // Определяем имя свойства, по которому следует сортировать список.
            string propertyName = clickedItem.Header as string;
            // Определяем список, на котором открыто контекстное меню.
            ListView list = ((clickedItem.Parent as MenuItem).Parent as ContextMenu).PlacementTarget as ListView;
            // Очищаем список сортировок.
            list.Items.SortDescriptions.Clear();
            // Добавляем в список сортировок свойство, выбранное из меню.
            list.Items.SortDescriptions.Add(new SortDescription(propertyName, ListSortDirection.Ascending));
        }
        #endregion
        #region Context Menu Opened
        /// <summary>
        /// Определяет тэг выбранного узла дерева. В нем сохраняется ссылка на редактируемый объект из числа Dep, Manager, Worker.
        /// </summary>
        /// <param name="e">Информация о событии.</param>
        /// <returns>Возвращает найденный тэг как объект заданного типа T.</returns>
        /// <remarks>Метод имеет побочный эффект, возвращая тэг как объект класса object в поле selectedNodeTag. 
        /// Используется при открывании контекстного меню над TreeViewNode.</remarks>
        T GetSelectedTag<T>(RoutedEventArgs e) => (T)(selectedNodeTag = ((e.OriginalSource as ContextMenu).PlacementTarget as TreeViewItem).Tag);
        /// <summary>
        /// Определяет тэг меню, в котором хранится ссылка на объект организации и засылает в TextBox имя организации.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrgMenu_Opened(object sender, RoutedEventArgs e) => orgNameBox.Text = GetSelectedTag<Dep>(e).Name;
        private void DepMenu_Opened(object sender, RoutedEventArgs e) => depNameBox.Text = GetSelectedTag<Dep>(e).Name;
        private void ManagerMenu_Opened(object sender, RoutedEventArgs e) => managerNameBox.Text = GetSelectedTag<Manager>(e).Name;
        private void WorkerMenu_Opened(object sender, RoutedEventArgs e)
        {
            workerNameBox.Text = GetSelectedTag<Worker>(e).Name;
            salaryBox.Text = (selectedNodeTag as Worker).Salary.ToString();
        }
        #endregion
        #region Add
        /// <summary>
        /// Добавляет в организацию пустой элемент типа отдел, менеджер или работник.
        /// </summary>
        /// <typeparam name="T">Тип добавляемого элемента.</typeparam>
        void Add<T>() where T : class, new()
        {
            // Определяем ссылку на текущий отдел.
            Dep dep = selectedNodeTag as Dep;
            // Добавляем пустой элемент (отдел, менеджер, работник) к текущему отделу.
            org.Add(new T(), dep);
            // Обновляем изображение.
            ResetOrgView();
        }
        private void OrgAddDepItem_Click(object sender, RoutedEventArgs e) => Add<Dep>();
        private void OrgAddMItem_Click(object sender, RoutedEventArgs e) => Add<Manager>();
        private void OrgAddWItem_Click(object sender, RoutedEventArgs e) => Add<Worker>();
        private void DepAddDepItem_Click(object sender, RoutedEventArgs e) => Add<Dep>();
        private void DepAddMItem_Click(object sender, RoutedEventArgs e) => Add<Manager>();
        private void DepAddWItem_Click(object sender, RoutedEventArgs e) => Add<Worker>();
        #endregion
        #region Remove
        /// <summary>
        /// Удавляет из организации элемент типа отдел, менеджер или работник.
        /// </summary>
        /// <typeparam name="T">Тип удаляемого элемента.</typeparam>
        void Remove<T>() where T : class
        {
            // Определяем ссылку на текущий элемент.
            T item = selectedNodeTag as T;
            // Определяем имя выбранного элемента.
            string name = item is Dep ? (item as Dep).Name : item is Manager ? (item as Manager).Name : (item as Worker).Name;
            // Уточняем выбранное решение.
            if (MessageBox.Show
                    ($"Вы действительно хотите удалить {name}?", " Внимание!",
                    MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.Yes
                    ) == MessageBoxResult.Yes
                )
            // Удаляем элемент из организации.
            { org.Remove(item); ResetOrgView(); }
        }
        private void DepRemoveItem_Click(object sender, RoutedEventArgs e) => Remove<Dep>();
        private void MRemoveItem_Click(object sender, RoutedEventArgs e) => Remove<Manager>();
        private void WRemoveItem_Click(object sender, RoutedEventArgs e) => Remove<Worker>();
        #endregion
        #region Edit
        private void SalaryBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                WorkerSalaryEdit();
        }
        private void WorkerNameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                WorkerNameEdit();
        }
        private void ManagerNameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ManagerNameEdit();
        }
        private void DepNameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                DepNameEdit();
        }
        private void OrgNameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                OrgNameEdit();
        }
        #endregion
        #region Editors
        void NameEdit<T>(string name) where T : Named
        {
            if (!string.IsNullOrEmpty(name.Trim()))
            {
                (selectedNodeTag as T).Name = name;
                ResetOrgView();
            }
        }
        void OrgNameEdit() => NameEdit<Org>(orgNameBox.Text);
        void DepNameEdit() => NameEdit<Dep>(depNameBox.Text);
        void ManagerNameEdit() => NameEdit<Manager>(managerNameBox.Text);
        void WorkerNameEdit() => NameEdit<Worker>(workerNameBox.Text);
        void WorkerSalaryEdit()
        {
            if (int.TryParse(salaryBox.Text, out int salary) && salary >= 0)
            {
                Worker worker = selectedNodeTag as Worker;
                worker.Salary = salary;
                org.ResetManagers(worker);
                ResetOrgView();
            }
        }
        #endregion
        #region Drag and Drop
        /// <summary>
        /// Перемещает объект (отдел, менеджера, работника) в целевой отдел.
        /// </summary>
        /// <param name="draggedTag">Тэг узла, несущий ссылку на перемещаемый объект.</param>
        /// <param name="targetDep">Целевой отдел.</param>
        void Move(object draggedTag, Dep targetDep)
        {
            if (draggedTag is Worker)
                org.Move(draggedTag as Worker, targetDep);
            if (draggedTag is Manager)
                org.Move(draggedTag as Manager, targetDep);
            if (draggedTag is Dep)
                org.Move(draggedTag as Dep, targetDep);
            // Перестраиваем дерево.
            ResetOrgView();
        }
        /// <summary>
        /// Определяет степень родства узлов дерева.
        /// </summary>
        /// <param name="node1">Первый узел.</param>
        /// <param name="node2">Второй узел.</param>
        /// <returns>
        /// true, если второй узел является дочерним по отношению к первому узлу в каком-либо поколении, и false - в противном случае.
        /// </returns>
        private bool ContainsNode(TreeViewItem node1, TreeViewItem node2)
        {
            // У второго узла нет родителя
            if (node2 == null || node2.Parent == null) return false;
            // Проверяем, является ли 1-ый узел родителем второго
            if (node2.Parent.Equals(node1)) return true;
            // Проверяем все то-же самое для родителя второго узла 
            return ContainsNode(node1, node2.Parent as TreeViewItem);
        }
        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            // Если не нажата левая кнопка мышки, покидаем обработчик.
            if (e.LeftButton != MouseButtonState.Pressed)
                return;
            // Определяем перемещаемый узел.
            draggedNode = e.Source is Image ? (e.Source as Image).Tag as TreeViewItem : null;  //GetMoveDropNode(e.Source);
            // Если узел найден и он не корневой.
            if (draggedNode != null && draggedNode.Parent != null)
                // Выполняем перемещение. Событие Node_Drop наступает при выполнении метода DoDragDrop.
                DragDrop.DoDragDrop(draggedNode, draggedNode.Tag, DragDropEffects.Move);
        }
        private void Image_Drop(object sender, DragEventArgs e)
        {
            // Принимающий, или целевой узел. На него проводится перемещение.
            TreeViewItem targetNode = e.Source is Image ? (e.Source as Image).Tag as TreeViewItem : null;
            if (
                // Целевой узел отсутствует.
                targetNode == null ||
                // Либо
                // Тэг узла targetNode не имеет тип Dep (отдел).
                !(targetNode.Tag is Dep) ||
                // Либо
                // Узел targetNode совпадает с переносимым узлом draggedNode.
                draggedNode.Equals(targetNode) ||
                // Либо
                // Узел targetNode является дочерним переносимого узла draggedNode в каком-либо поколении.
                ContainsNode(draggedNode, targetNode) ||
                // Либо
                // Узел targetNode является прямым родителем переносимого узла draggedNode.
                targetNode == draggedNode.Parent
                )
            {
                // Перенос запрещаем.
                e.Effects = DragDropEffects.None;
                // Покидаем обработчик.
                return;
            }
            // Узел является целевым для перемещения.
            e.Effects = e.AllowedEffects;
            // Перемещаем данные об объекте (отдел, менеджер, работник) в отдел узла назначения.
            Move(draggedNode.Tag, targetDep = targetNode.Tag as Dep);
        }
        #endregion
        #endregion
        #endregion
    }
}
