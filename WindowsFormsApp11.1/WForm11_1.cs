using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp11._1.Properties;

namespace WindowsFormsApp11._1
{
    public partial class WForm11_1 : Form
    {
        /// <summary>
        /// Хранит коллекцию картинок, отображающих организацию, отдел с отделами, отдел с работниками, менеджеров и работников.
        /// </summary>
        static readonly Image[] images = { Resources.org, Resources.wDep, Resources.mDep, Resources.manager, Resources.worker };
        /// <summary>
        /// Хранит ссылку на организацию.
        /// </summary>
        Org org;
        /// <summary>
        /// Хранит ссылку на редактируемый через контекстное меню объект типа Dep, Manager или Worker. 
        /// </summary>
        object selectedNodeTag;
        /// <summary>
        /// Инициализирует поля окна.
        /// </summary>
        public WForm11_1()
        {
            InitializeComponent();
            // Создаем коллекцию картинок.
            orgView.ImageList = new ImageList();
            // Населяем коллекцию картинками.
            orgView.ImageList.Images.AddRange(images);
            // Добавляем обработчик клика к узлам организации.
            orgView.NodeMouseClick += OrgView_NodeMouseClick;
        }
        /// <summary>
        /// Инициализирует состав организации случайным набором данных и населяет элементы дерева этими данными.
        /// </summary>
        void InitOrgView()
        {
            // Организация.
            // Создаем организацию со случайным составом.
            org = RandomOrg.GetRandomOrg();
            // Восстанавливаем дерево организации.
            ResetOrgView();
        }
        /// <summary>
        /// Восстанавливает дерево организации.
        /// </summary>
        void ResetOrgView()
        {
            // Определяем заголовок окна.
            Text = "Организация " + org.Name;
            // Блокируем изображение структуры treeView.
            orgView.BeginUpdate();
            // Список узлов очищаем
            orgView.Nodes.Clear();
            // Добавляем к элементам orgView организацию.
            TreeNode node = orgView.Nodes.Add(org.ToString());
            // Запоминаем ссылку на организацию в тэге узла.
            node.Tag = org;
            // Добавляем контекстное меню, Tip, картинки к узлу организации.
            (node.ContextMenuStrip, node.ToolTipText, node.ImageIndex, node.SelectedImageIndex) = (orgMenu, org.Info(), 0, 0);
            // Добавляем узлы с сотрудниками и отделениями к узлу организации.
            Add(node, org.Managers);
            Add(node, org.Workers);
            Add(node, org.Deps);
            // Активируем изображение дерева.
            orgView.EndUpdate();
            // Раскроем дерево.
            orgView.ExpandAll();
        }
        /// <summary>
        /// Добавляет в коллекцию узла дерева коллекцию объектов типа отдел, менеджер и работник.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="treeNode">Узел, в чью коллекцию добавляются объекты.</param>
        /// <param name="items">Список объектов.</param>
        void Add<T>(TreeNode treeNode, List<T> items) where T : class
        {
            foreach (T item in items)
            {
                // Создаем новый узел, добавляя его к корневому узлу.
                TreeNode node = treeNode.Nodes.Add(item.ToString());
                // Запоминаем ссылку на объект.
                node.Tag = item;
                switch (typeof(T).Name)
                {
                    case "Manager":
                        // Подключаем меню. Указываем Tip этому узлу. Указываем картинку менеджера.
                        (node.ContextMenuStrip, node.ToolTipText, node.ImageIndex, node.SelectedImageIndex) =
                            (managerMenu, "\tManager\n" + (item as Manager).Info(), 3, 3);
                        break;
                    case "Worker":
                        // Подключаем меню. Указываем Tip этому узлу. Указываем картинку менеджера.
                        (node.ContextMenuStrip, node.ToolTipText, node.ImageIndex, node.SelectedImageIndex) =
                            (workerMenu, "\tWorker\n" + (item as Worker).Info(), 4, 4);
                        break;
                    case "Dep":
                        // Текущий отдел.
                        Dep dep = item as Dep;
                        // Индекс иконки отдела.
                        int imgIndex = dep.Deps.Count > 0 ? 1 : 2;
                        // Подключаем меню. Указываем Tip этому узлу. Указываем картинку менеджера.
                        (node.ContextMenuStrip, node.ToolTipText, node.ImageIndex, node.SelectedImageIndex) = (depMenu, dep.Info(), imgIndex, imgIndex);
                        // Добавляем к колекциям узла менеджеров, работников и отделы.
                        Add(node, dep.Managers);
                        Add(node, dep.Workers);
                        Add(node, dep.Deps);
                        break;
                }
            }
        }
        #region Editors
        void OrgNameEdit()
        {
            if (!string.IsNullOrEmpty(orgNameBox.Text.Trim()))
            {
                (selectedNodeTag as Org).Name = orgNameBox.Text;
                ResetOrgView();
            }
            orgMenu.Hide();
        }
        void DepNameEdit()
        {
            if (!string.IsNullOrEmpty(depNameBox.Text.Trim()))
            {
                (selectedNodeTag as Dep).Name = depNameBox.Text;
                ResetOrgView();
            }
            depMenu.Hide();
        }
        void ManagerNameEdit()
        {
            (selectedNodeTag as Manager).Name = string.IsNullOrEmpty(managerNameBox.Text.Trim()) ? (selectedNodeTag as Manager).Name : managerNameBox.Text;
            ResetOrgView();
            managerMenu.Hide();
        }
        void WorkerNameEdit()
        {
            if (!string.IsNullOrEmpty(workerNameBox.Text.Trim()))
            {
                (selectedNodeTag as Worker).Name = workerNameBox.Text;
                ResetOrgView();
            }
            workerMenu.Hide();
        }
        void WorkerSalaryEdit()
        {
            if (int.TryParse(workerSalaryBox.Text, out int salary) && salary >= 0)
            {
                Worker worker = selectedNodeTag as Worker;
                worker.Salary = salary;
                org.ResetManagers(worker);
                ResetOrgView();
            }
            workerMenu.Hide();
        }
        #endregion
        #region Handlers
        /// <summary>
        /// Инициализирует состав организации при загрузке формы и при двойном клике по дереву.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitOrgHandler(object sender, EventArgs e)
        {
            // Инициализируем организацию.
            InitOrgView();
        }
        private void OrgView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            // Сохраняем ссылку на Tag выбранного узла дерева, который содержит ссылку на редактируемый объект.
            selectedNodeTag = e.Node.Tag;
        }
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
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1
                    ) == DialogResult.Yes
                )
            // Удаляем элемент из организации.
            { org.Remove(item); ResetOrgView(); }
        }
        private void RemoveDepClick(object sender, EventArgs e) => Remove<Dep>();
        private void RemoveManagerClick(object sender, EventArgs e) => Remove<Manager>();
        private void RemoveWorkerClick(object sender, EventArgs e) => Remove<Worker>();
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
        private void AddDepClick(object sender, EventArgs e) => Add<Dep>();
        private void AddManagerClick(object sender, EventArgs e) => Add<Manager>();
        private void AddWorkerClick(object sender, EventArgs e) => Add<Worker>();
        #endregion
        #region Opening Menu Handlers
        private void OrgMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e) => orgNameBox.Text = (selectedNodeTag as Org).Name;
        private void DepMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e) => depNameBox.Text = (selectedNodeTag as Dep).Name;
        private void ManagerMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e) => managerNameBox.Text = (selectedNodeTag as Manager).Name;
        private void WorkerMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            workerNameBox.Text = (selectedNodeTag as Worker).Name;
            workerSalaryBox.Text = (selectedNodeTag as Worker).Salary.ToString();
        }
        #endregion
        #region KeyPress Text Boxes Handlers
        private void OrgNameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                OrgNameEdit();
        }
        private void DepNameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                DepNameEdit();
        }
        private void ManagerNameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                ManagerNameEdit();
        }
        private void WorkerNameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                WorkerNameEdit();
        }
        private void WorkerSalaryBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                WorkerSalaryEdit();
        }
        #endregion
        #region Drag and Drop Handlers
        /// <summary>
        /// Включается при нажатии левой кнопки над узлом дерева 
        /// перед началом переноса узла в том случае, 
        /// если узел не является корневым
        /// </summary>
        /// <param name="sender">Ссылка на дерево</param>
        /// <param name="e">Объект, содержащий информацию о переносимом узле</param>
        private void OrgView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Если нажата левая кнопка мышки, 
            // и узел дерева является не корневым (сеть)
            if (e.Button == MouseButtons.Left && (e.Item as TreeNode).Parent != null)
                // События DragOver и DragDrop наступают при выполнении метода DoDragDrop
                DoDragDrop(e.Item, DragDropEffects.Move);
        }
        /// <summary>
        /// Включается при проносе над узлом дерева.
        /// </summary>
        /// <param name="sender">Ссылка на дерево</param>
        /// <param name="e">
        /// Объект, содержащий информацию о событии, целевом эффекте и положении курсора
        /// </param>
        private void OrgView_DragOver(object sender, DragEventArgs e)
        {
            // Определяются координаты точки назначения.
            Point targetPoint = orgView.PointToClient(new Point(e.X, e.Y));
            if (orgView.GetNodeAt(targetPoint) == null)
                return;
            // Выбирается и выделяется узел в месте положения курсора.
            TreeNode overNode = orgView.SelectedNode = orgView.GetNodeAt(targetPoint);
            // Определяется переносимый узел дерева.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            // Условие того, что узел может быть целевым для переноса
            bool isDropAllowed =
                // Over-узел не является ни менеджером ни работником
                (overNode.Tag is Dep || overNode.Tag is Org)
                // over-узел не совпадает с переносимым узлом
                && !draggedNode.Equals(overNode)
                // over-узел не является дочерним переносимого узла в каком-либо поколении
                && !ContainsNode(draggedNode, overNode)
                // over-узел не является прямым родителем переносимого узла
                && overNode != draggedNode.Parent;
            // Установка эффекта
            e.Effect = isDropAllowed ? e.AllowedEffect : DragDropEffects.None;
        }
        /// <summary>
        /// Определяет степень родства узлов дерева
        /// </summary>
        /// <param name="node1">первый узел</param>
        /// <param name="node2">второй узел</param>
        /// <returns>
        /// true, если второй узел является дочерним по отношению к первому узлу в каком-либо поколении, 
        /// и false - в противном случае 
        /// </returns>
        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            // У второго узла нет родителя
            if (node2.Parent == null) return false;
            // Проверяем, является ли 1-ый узел родителем второго
            if (node2.Parent.Equals(node1)) return true;
            // Проверяем все то-же самое для родителя второго узла 
            return ContainsNode(node1, node2.Parent);
        }
        /// <summary>
        /// Включается при опускании на узел дерева.
        /// </summary>
        /// <param name="sender">Ссылка на дерево</param>
        /// <param name="e">
        /// Объект, содержащий информацию о событии, целевом эффекте и положении курсора
        /// </param>
        private void OrgView_DragDrop(object sender, DragEventArgs e)
        {
            // Определяются координаты точки назначения.
            Point targetPoint = orgView.PointToClient(new Point(e.X, e.Y));
            // Определяется узел дерева в точке назначения.
            TreeNode targetNode = orgView.GetNodeAt(targetPoint);
            if (targetNode == null) return;
            // Определяется переносимый узел дерева.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            // Перестройка организации.
            Dep targetDep = targetNode.Tag as Dep;
            // Перемещаем данные в зависимости от типа объекта, содержащегося в узле.
            if (draggedNode.Tag is Worker)
                org.Move(draggedNode.Tag as Worker, targetDep);
            if (draggedNode.Tag is Manager)
                org.Move(draggedNode.Tag as Manager, targetDep);
            if (draggedNode.Tag is Dep)
                org.Move(draggedNode.Tag as Dep, targetDep);
            // Перестройка дерева.
            ResetOrgView();
        }
        #endregion
        #endregion
    }
}
