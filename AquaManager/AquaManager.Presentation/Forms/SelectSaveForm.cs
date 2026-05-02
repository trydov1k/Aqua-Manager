namespace AquaManager.Forms
{
    /// <summary>
    /// Форма выбора сохранения для загрузки.
    /// </summary>
    public class SelectSaveForm : Form
    {
        // ── публичное поле результата ─────────────────────────────────────
        public string SelectedSave { get; private set; }

        // ── зависимости ──────────────────────────────────────────────────
        private readonly List<string> _saves;
        private readonly string _defaultSave;
        private readonly Action<string> _onDelete; // метод удаления из внешнего сервиса

        // ── контролы ─────────────────────────────────────────────────────
        private Panel _listPanel;       // прокручиваемая панель со строками
        private Button _btnCancel;
        private Label _lblTitle;

        public SelectSaveForm(List<string> saves, string defaultSave, Action<string> onDelete)
        {
            _saves = saves ?? throw new ArgumentNullException(nameof(saves));
            _defaultSave = defaultSave ?? string.Empty;
            _onDelete = onDelete ?? throw new ArgumentNullException(nameof(onDelete));
            SelectedSave = string.Empty;

            InitializeComponent();
            RenderList();
        }

        private void InitializeComponent()
        {
            Text = "Загрузка сохранения";
            Size = new Size(460, 450);
            MinimumSize = new Size(460, 300);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(15, 30, 60);
            Font = new Font("Segoe UI", 10f);

            _lblTitle = new Label
            {
                Text = "Выберите сохранение:",
                ForeColor = Color.LightCyan,
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                Location = new Point(20, 16),
                Size = new Size(400, 26),
            };
            Controls.Add(_lblTitle);

            // Прокручиваемая панель со списком
            _listPanel = new Panel
            {
                Location = new Point(20, 50),
                Size = new Size(410, 300),
                AutoScroll = true,
                BackColor = Color.FromArgb(22, 44, 88),
            };
            Controls.Add(_listPanel);

            _btnCancel = new Button
            {
                Text = "Отмена",
                Location = new Point(340, 362),
                Size = new Size(96, 34),
                BackColor = Color.FromArgb(60, 60, 90),
                ForeColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel,
            };
            _btnCancel.FlatAppearance.BorderColor = Color.Gray;
            Controls.Add(_btnCancel);

            CancelButton = _btnCancel;
        }

        // ── построение списка ─────────────────────────────────────────────
        private void RenderList()
        {
            _listPanel.Controls.Clear();

            int y = 6;
            const int rowH = 44;
            const int panelW = 393; // ширина с учётом скроллбара

            foreach (string name in _saves)
            {
                string captureName = name; // захват для лямбд
                bool isDefault = name == _defaultSave;

                // ── строка-контейнер ──────────────────────────────────────
                var row = new Panel
                {
                    Location = new Point(0, y),
                    Size = new Size(panelW, rowH - 4),
                    BackColor = isDefault
                        ? Color.FromArgb(0, 70, 130)
                        : Color.FromArgb(30, 58, 110),
                    Cursor = Cursors.Hand,
                    Tag = captureName,
                };

                // Подсветка при наведении
                row.MouseEnter += (s, e) =>
                {
                    if (row.BackColor != Color.FromArgb(0, 100, 180))
                        row.BackColor = isDefault
                            ? Color.FromArgb(0, 90, 160)
                            : Color.FromArgb(45, 80, 145);
                };
                row.MouseLeave += (s, e) =>
                {
                    row.BackColor = isDefault
                        ? Color.FromArgb(0, 70, 130)
                        : Color.FromArgb(30, 58, 110);
                };

                // Клик на строке — выбор сохранения
                row.Click += (s, e) => SelectAndClose(captureName);

                // ── иконка ────────────────────────────────────────────────
                var lblIcon = new Label
                {
                    Text = isDefault ? "🔄" : "💾",
                    Font = new Font("Segoe UI Emoji", 14f),
                    ForeColor = Color.White,
                    Location = new Point(6, 6),
                    Size = new Size(34, 28),
                    TextAlign = ContentAlignment.MiddleCenter,
                };
                lblIcon.Click += (s, e) => SelectAndClose(captureName);
                row.Controls.Add(lblIcon);

                // ── название ──────────────────────────────────────────────
                var lblName = new Label
                {
                    Text = name + (isDefault ? "  ★" : ""),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", isDefault ? 10f : 9.5f,
                        isDefault ? FontStyle.Bold : FontStyle.Regular),
                    Location = new Point(44, 0),
                    Size = new Size(isDefault ? 310 : 270, rowH - 4),
                    TextAlign = ContentAlignment.MiddleLeft,
                };
                lblName.Click += (s, e) => SelectAndClose(captureName);
                row.Controls.Add(lblName);

                // ── кнопка удаления (только для НЕ-дефолтных) ────────────
                if (!isDefault)
                {
                    var btnDelete = new Button
                    {
                        Text = "🗑",
                        Font = new Font("Segoe UI Emoji", 11f),
                        Location = new Point(panelW - 46, 6),
                        Size = new Size(36, 28),
                        BackColor = Color.FromArgb(130, 20, 20),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Cursor = Cursors.Default,
                        TabStop = false,
                    };
                    btnDelete.FlatAppearance.BorderColor = Color.DarkRed;
                    btnDelete.FlatAppearance.MouseOverBackColor = Color.FromArgb(180, 30, 30);
                    btnDelete.Click += (s, e) => DeleteSave(captureName);

                    // Чтобы клик по кнопке не всплывал на row
                    btnDelete.MouseEnter += (s, e) => row.BackColor = Color.FromArgb(30, 58, 110);
                    row.Controls.Add(btnDelete);
                }

                _listPanel.Controls.Add(row);
                y += rowH;
            }

            // Пустой список
            if (_saves.Count == 0)
            {
                var lblEmpty = new Label
                {
                    Text = "Нет доступных сохранений",
                    ForeColor = Color.Gray,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(0, 100),
                    Size = new Size(panelW, 40),
                };
                _listPanel.Controls.Add(lblEmpty);
            }
        }

        // ── выбор и закрытие ─────────────────────────────────────────────
        private void SelectAndClose(string name)
        {
            SelectedSave = name;
            DialogResult = DialogResult.OK;
            Close();
        }

        // ── удаление ─────────────────────────────────────────────────────
        private void DeleteSave(string name)
        {
            var result = MessageBox.Show(
                $"Удалить сохранение «{name}»?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            _onDelete(name);        // вызываем переданный метод удаления
            _saves.Remove(name);
            RenderList();           // перестраиваем список
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                SelectedSave = _defaultSave;
                DialogResult = DialogResult.Cancel;
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}