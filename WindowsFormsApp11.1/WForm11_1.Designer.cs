
namespace WindowsFormsApp11._1
{
    partial class WForm11_1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.orgView = new System.Windows.Forms.TreeView();
            this.depMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.depRemoveItem = new System.Windows.Forms.ToolStripMenuItem();
            this.depEditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.depNameBox = new System.Windows.Forms.ToolStripTextBox();
            this.depAddItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDepItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addManagerItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addWorkerItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mRemoveItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mEditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managerNameBox = new System.Windows.Forms.ToolStripTextBox();
            this.workerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.wRemoveItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wEditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workerNameBox = new System.Windows.Forms.ToolStripTextBox();
            this.workerSalaryBox = new System.Windows.Forms.ToolStripTextBox();
            this.orgMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.orgEditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orgNameBox = new System.Windows.Forms.ToolStripTextBox();
            this.orgAddItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orgAddDepItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orgAddManagerItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orgAddWorkerItem = new System.Windows.Forms.ToolStripMenuItem();
            this.depMenu.SuspendLayout();
            this.managerMenu.SuspendLayout();
            this.workerMenu.SuspendLayout();
            this.orgMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // orgView
            // 
            this.orgView.AllowDrop = true;
            this.orgView.BackColor = System.Drawing.Color.Navy;
            this.orgView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orgView.ForeColor = System.Drawing.Color.Yellow;
            this.orgView.Location = new System.Drawing.Point(0, 0);
            this.orgView.Name = "orgView";
            this.orgView.ShowNodeToolTips = true;
            this.orgView.Size = new System.Drawing.Size(583, 450);
            this.orgView.TabIndex = 0;
            this.orgView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.OrgView_ItemDrag);
            this.orgView.DragDrop += new System.Windows.Forms.DragEventHandler(this.OrgView_DragDrop);
            this.orgView.DragOver += new System.Windows.Forms.DragEventHandler(this.OrgView_DragOver);
            this.orgView.DoubleClick += new System.EventHandler(this.InitOrgHandler);
            // 
            // depMenu
            // 
            this.depMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.depRemoveItem,
            this.depEditItem,
            this.depAddItem});
            this.depMenu.Name = "stripMenu";
            this.depMenu.Size = new System.Drawing.Size(129, 70);
            this.depMenu.Opening += new System.ComponentModel.CancelEventHandler(this.DepMenu_Opening);
            // 
            // depRemoveItem
            // 
            this.depRemoveItem.Name = "depRemoveItem";
            this.depRemoveItem.Size = new System.Drawing.Size(128, 22);
            this.depRemoveItem.Text = "Удалить";
            this.depRemoveItem.Click += new System.EventHandler(this.RemoveDepClick);
            // 
            // depEditItem
            // 
            this.depEditItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.depNameBox});
            this.depEditItem.Name = "depEditItem";
            this.depEditItem.Size = new System.Drawing.Size(128, 22);
            this.depEditItem.Text = "Изменить";
            // 
            // depNameBox
            // 
            this.depNameBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.depNameBox.Name = "depNameBox";
            this.depNameBox.Size = new System.Drawing.Size(100, 23);
            this.depNameBox.ToolTipText = "Имя отдела";
            this.depNameBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DepNameBox_KeyPress);
            // 
            // depAddItem
            // 
            this.depAddItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDepItem,
            this.addManagerItem,
            this.addWorkerItem});
            this.depAddItem.Name = "depAddItem";
            this.depAddItem.Size = new System.Drawing.Size(128, 22);
            this.depAddItem.Text = "Добавить";
            // 
            // addDepItem
            // 
            this.addDepItem.Name = "addDepItem";
            this.addDepItem.Size = new System.Drawing.Size(138, 22);
            this.addDepItem.Text = "Отдел";
            this.addDepItem.Click += new System.EventHandler(this.AddDepClick);
            // 
            // addManagerItem
            // 
            this.addManagerItem.Name = "addManagerItem";
            this.addManagerItem.Size = new System.Drawing.Size(138, 22);
            this.addManagerItem.Text = "Менеджера";
            this.addManagerItem.Click += new System.EventHandler(this.AddManagerClick);
            // 
            // addWorkerItem
            // 
            this.addWorkerItem.Name = "addWorkerItem";
            this.addWorkerItem.Size = new System.Drawing.Size(138, 22);
            this.addWorkerItem.Text = "Работника";
            this.addWorkerItem.Click += new System.EventHandler(this.AddWorkerClick);
            // 
            // managerMenu
            // 
            this.managerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mRemoveItem,
            this.mEditItem});
            this.managerMenu.Name = "mStripMenu";
            this.managerMenu.Size = new System.Drawing.Size(129, 48);
            this.managerMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ManagerMenu_Opening);
            // 
            // mRemoveItem
            // 
            this.mRemoveItem.Name = "mRemoveItem";
            this.mRemoveItem.Size = new System.Drawing.Size(128, 22);
            this.mRemoveItem.Text = "Удалить";
            this.mRemoveItem.Click += new System.EventHandler(this.RemoveManagerClick);
            // 
            // mEditItem
            // 
            this.mEditItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.managerNameBox});
            this.mEditItem.Name = "mEditItem";
            this.mEditItem.Size = new System.Drawing.Size(128, 22);
            this.mEditItem.Text = "Изменить";
            // 
            // managerNameBox
            // 
            this.managerNameBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.managerNameBox.Name = "managerNameBox";
            this.managerNameBox.Size = new System.Drawing.Size(100, 23);
            this.managerNameBox.ToolTipText = "Имя менеджера";
            this.managerNameBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ManagerNameBox_KeyPress);
            // 
            // workerMenu
            // 
            this.workerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wRemoveItem,
            this.wEditItem});
            this.workerMenu.Name = "wStripMenu";
            this.workerMenu.Size = new System.Drawing.Size(181, 70);
            this.workerMenu.Opening += new System.ComponentModel.CancelEventHandler(this.WorkerMenu_Opening);
            // 
            // wRemoveItem
            // 
            this.wRemoveItem.Name = "wRemoveItem";
            this.wRemoveItem.Size = new System.Drawing.Size(180, 22);
            this.wRemoveItem.Text = "Удалить";
            this.wRemoveItem.Click += new System.EventHandler(this.RemoveWorkerClick);
            // 
            // wEditItem
            // 
            this.wEditItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.workerNameBox,
            this.workerSalaryBox});
            this.wEditItem.Name = "wEditItem";
            this.wEditItem.Size = new System.Drawing.Size(180, 22);
            this.wEditItem.Text = "Изменить";
            // 
            // workerNameBox
            // 
            this.workerNameBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.workerNameBox.Name = "workerNameBox";
            this.workerNameBox.Size = new System.Drawing.Size(100, 23);
            this.workerNameBox.ToolTipText = "Имя работника";
            this.workerNameBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WorkerNameBox_KeyPress);
            // 
            // workerSalaryBox
            // 
            this.workerSalaryBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.workerSalaryBox.Name = "workerSalaryBox";
            this.workerSalaryBox.Size = new System.Drawing.Size(100, 23);
            this.workerSalaryBox.ToolTipText = "Зарплата работника";
            this.workerSalaryBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WorkerSalaryBox_KeyPress);
            // 
            // orgMenu
            // 
            this.orgMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.orgEditItem,
            this.orgAddItem});
            this.orgMenu.Name = "orgStripMenu";
            this.orgMenu.Size = new System.Drawing.Size(129, 48);
            this.orgMenu.Opening += new System.ComponentModel.CancelEventHandler(this.OrgMenu_Opening);
            // 
            // orgEditItem
            // 
            this.orgEditItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.orgNameBox});
            this.orgEditItem.Name = "orgEditItem";
            this.orgEditItem.Size = new System.Drawing.Size(128, 22);
            this.orgEditItem.Text = "Изменить";
            // 
            // orgNameBox
            // 
            this.orgNameBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.orgNameBox.Name = "orgNameBox";
            this.orgNameBox.Size = new System.Drawing.Size(100, 23);
            this.orgNameBox.ToolTipText = "Имя организации";
            this.orgNameBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OrgNameBox_KeyPress);
            // 
            // orgAddItem
            // 
            this.orgAddItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.orgAddDepItem,
            this.orgAddManagerItem,
            this.orgAddWorkerItem});
            this.orgAddItem.Name = "orgAddItem";
            this.orgAddItem.Size = new System.Drawing.Size(128, 22);
            this.orgAddItem.Text = "Добавить";
            // 
            // orgAddDepItem
            // 
            this.orgAddDepItem.Name = "orgAddDepItem";
            this.orgAddDepItem.Size = new System.Drawing.Size(138, 22);
            this.orgAddDepItem.Text = "Отдел";
            this.orgAddDepItem.Click += new System.EventHandler(this.AddDepClick);
            // 
            // orgAddManagerItem
            // 
            this.orgAddManagerItem.Name = "orgAddManagerItem";
            this.orgAddManagerItem.Size = new System.Drawing.Size(138, 22);
            this.orgAddManagerItem.Text = "Менеджера";
            this.orgAddManagerItem.Click += new System.EventHandler(this.AddManagerClick);
            // 
            // orgAddWorkerItem
            // 
            this.orgAddWorkerItem.Name = "orgAddWorkerItem";
            this.orgAddWorkerItem.Size = new System.Drawing.Size(138, 22);
            this.orgAddWorkerItem.Text = "Работника";
            this.orgAddWorkerItem.Click += new System.EventHandler(this.AddWorkerClick);
            // 
            // WForm11_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 450);
            this.Controls.Add(this.orgView);
            this.Name = "WForm11_1";
            this.Text = "Org";
            this.Load += new System.EventHandler(this.InitOrgHandler);
            this.depMenu.ResumeLayout(false);
            this.managerMenu.ResumeLayout(false);
            this.workerMenu.ResumeLayout(false);
            this.orgMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView orgView;
        private System.Windows.Forms.ContextMenuStrip depMenu;
        private System.Windows.Forms.ContextMenuStrip managerMenu;
        private System.Windows.Forms.ContextMenuStrip workerMenu;
        private System.Windows.Forms.ToolStripMenuItem depRemoveItem;
        private System.Windows.Forms.ToolStripMenuItem depEditItem;
        private System.Windows.Forms.ToolStripTextBox depNameBox;
        private System.Windows.Forms.ToolStripMenuItem depAddItem;
        private System.Windows.Forms.ToolStripMenuItem addDepItem;
        private System.Windows.Forms.ToolStripMenuItem addManagerItem;
        private System.Windows.Forms.ToolStripMenuItem addWorkerItem;
        private System.Windows.Forms.ToolStripMenuItem mRemoveItem;
        private System.Windows.Forms.ToolStripMenuItem mEditItem;
        private System.Windows.Forms.ToolStripTextBox managerNameBox;
        private System.Windows.Forms.ToolStripMenuItem wRemoveItem;
        private System.Windows.Forms.ToolStripMenuItem wEditItem;
        private System.Windows.Forms.ToolStripTextBox workerNameBox;
        private System.Windows.Forms.ToolStripTextBox workerSalaryBox;
        private System.Windows.Forms.ContextMenuStrip orgMenu;
        private System.Windows.Forms.ToolStripMenuItem orgEditItem;
        private System.Windows.Forms.ToolStripTextBox orgNameBox;
        private System.Windows.Forms.ToolStripMenuItem orgAddItem;
        private System.Windows.Forms.ToolStripMenuItem orgAddDepItem;
        private System.Windows.Forms.ToolStripMenuItem orgAddManagerItem;
        private System.Windows.Forms.ToolStripMenuItem orgAddWorkerItem;
    }
}

