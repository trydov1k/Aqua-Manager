namespace AquaManager.Forms
{
    /// <summary>
    /// Диалог для ввода имени при покупке рыбки или аквариума.
    /// Использование:
    ///   using var dlg = new NameInputForm("рыбку", "Клоун");
    ///   if (dlg.ShowDialog() == DialogResult.OK)
    ///       string name = dlg.EnteredName;
    /// </summary>
    public class NameInputForm : Form
    {
        // ── публичный результат ───────────────────────────────────────────
        public string EnteredName { get; private set; } = string.Empty;

        // ── контролы ─────────────────────────────────────────────────────
        private Label _lblPrompt;
        private Label _lblHint;
        private TextBox _txtName;
        private Button _btnOk;
        private Button _btnSkip;
        private Label _pbIcon;

        private readonly string _buyMessage;
        private readonly string _giveNameMessage;

        private readonly string _defaultName;  // предлагаемое имя по умолчанию
        private readonly string _emojiText;    // 🐠 или 🪣

        public NameInputForm(NameInputType entityType, string defaultName, string emoji = "🐠")
        {
            _buyMessage = entityType == NameInputType.Fish ? "рыбку" : entityType == NameInputType.Aquarium ? "аквариум" : "сохранение";
            _giveNameMessage = entityType == NameInputType.Fish ? "рыбке" : entityType == NameInputType.Aquarium ? "аквариуму" : "сохранению";
            _defaultName = defaultName;
            _emojiText = emoji;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = $"Назовите {_buyMessage}";
            Size = new Size(440, 280);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(20, 40, 80);
            Font = new Font("Segoe UI", 10f);

            // Эмодзи-иконка
            _pbIcon = new Label
            {
                Text = _emojiText,
                Font = new Font("Segoe UI Emoji", 32f),
                ForeColor = Color.White,
                Location = new Point(15, 15),
                TextAlign = ContentAlignment.MiddleCenter, 
                Size = new Size(80, 60),
            };
            Controls.Add(_pbIcon);

            // Заголовок
            _lblPrompt = new Label
            {
                Text = $"Дайте имя {_giveNameMessage}:",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.LightCyan,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 30),
                Size = new Size(400, 30),
            };
            Controls.Add(_lblPrompt);

            // Поле ввода
            _txtName = new TextBox
            {
                Text = _defaultName,
                Font = new Font("Segoe UI", 13f),
                Location = new Point(40, 100),
                Size = new Size(350, 36),
                MaxLength = 30,
                BackColor = Color.FromArgb(40, 70, 130),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
            };
            _txtName.SelectAll();
            Controls.Add(_txtName);

            // Подсказка
            _lblHint = new Label
            {
                Text = "Макс. 30 символов. Оставьте пустым для имени по умолчанию.",
                Font = new Font("Segoe UI", 8f),
                ForeColor = Color.LightGray,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 130),
                Size = new Size(400, 20),
            };
            Controls.Add(_lblHint);

            // Кнопка «Пропустить»
            _btnSkip = new Button
            {
                Text = "Пропустить",
                Location = new Point(40, 190),
                Size = new Size(120, 34),
                BackColor = Color.FromArgb(60, 60, 100),
                ForeColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel,
            };
            _btnSkip.FlatAppearance.BorderColor = Color.Gray;
            _btnSkip.Click += (s, e) =>
            {
                EnteredName = _defaultName; // при пропуске — дефолтное имя
                DialogResult = DialogResult.Cancel;
                Close();
            };
            Controls.Add(_btnSkip);

            // Кнопка «Подтвердить»
            _btnOk = new Button
            {
                Text = "✔ Подтвердить",
                Location = new Point(255, 190),
                Size = new Size(140, 34),
                BackColor = Color.FromArgb(0, 140, 200),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK,
            };
            _btnOk.FlatAppearance.BorderColor = Color.DeepSkyBlue;
            _btnOk.Click += BtnOk_Click;
            Controls.Add(_btnOk);

            AcceptButton = _btnOk;

            // Подсветка при фокусе на поле
            _txtName.Enter += (s, e) => _txtName.BackColor = Color.FromArgb(50, 90, 170);
            _txtName.Leave += (s, e) => _txtName.BackColor = Color.FromArgb(40, 70, 130);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            string trimmed = _txtName.Text.Trim();
            EnteredName = string.IsNullOrEmpty(trimmed) ? _defaultName : trimmed;
            DialogResult = DialogResult.OK;
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                EnteredName = _defaultName;
                DialogResult = DialogResult.Cancel;
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    public enum NameInputType
    {
        Fish,
        Aquarium,
        Save
    }
}