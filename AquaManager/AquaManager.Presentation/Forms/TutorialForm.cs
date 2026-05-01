namespace AquaManager.Forms
{
    /// <summary>
    /// Форма обучения — показывает пошаговые подсказки новому игроку.
    /// Вызывается при первом запуске (NewGame) из GameEngine.
    /// </summary>
    public class TutorialForm : Form
    {
        // ── данные шагов обучения ────────────────────────────────────────
        private readonly (string Title, string Text, string Emoji)[] _steps =
        {
            ("Добро пожаловать в Aqua Manager!",
             "Вы — владелец аквариумного бизнеса.\n\n" +
             "Ваша цель: покупать рыбок, ухаживать за ними и зарабатывать монеты.\n\n" +
             "Нажмите «Далее», чтобы узнать, как играть.",
             "🐠"),

            ("Ваш аквариум",
             "В центре экрана вы видите аквариум.\n\n" +
             "Рыбки плавают внутри — они живые и нуждаются в уходе!\n\n" +
             "Чем дороже рыбки — тем больше пассивный доход.",
             "🪣"),

            ("Кормление рыбок",
             "Рыбки постепенно голодают. Следите за индикатором «Голод».\n\n" +
             "• Кнопка «Покормить всех» — кормит сразу весь аквариум.\n" +
             "• Кнопка «Режим кормдления», а после клик на рыбку — кормит только одну рыбку.\n\n" +
             "Если рыбка умрёт с голоду — вы потеряете доход!",
             "🍖"),

            ("Чистота воды",
             "Вода в аквариуме загрязняется со временем.\n\n" +
             "Следите за индикатором «Загрязнение».\n" +
             "При сильном загрязнении вода темнеет и рыбки начинают голодать в два раза сильнее.\n\n" +
             "• Кнопка «Сменить воду» — очищает аквариум.",
             "💧"),

            ("Магазин",
             "В магазине можно купить:\n\n" +
             "• Новых рыбок разных видов (разная цена и доход)\n" +
             "• Новые аквариумы — чтобы расширить бизнес\n\n" +
             "При покупке вы можете дать имя рыбке или аквариуму!",
             "🏪"),

            ("Сохранение и меню",
             "Используйте кнопку «Меню» (или клавишу Escape) для:\n\n" +
             "• Сохранения игры (можно иметь несколько слотов)\n" +
             "• Загрузки сохранения\n" +
             "• Начала новой игры\n\n" +
             "Игра также автоматически сохраняется каждые 5 минут.",
             "💾"),

            ("Готово!",
             "Теперь вы знаете всё необходимое.\n\n" +
             "Удачи в управлении аквариумным бизнесом!\n\n" +
             "Подсказка: нажмите F1 в любой момент, чтобы открыть это обучение снова.",
             "🎉"),
        };

        private int _currentStep = 0;

        // ── контролы ─────────────────────────────────────────────────────
        private Label _lblEmoji;
        private Label _lblTitle;
        private Label _lblText;
        private Label _lblStepCounter;
        private Button _btnPrev;
        private Button _btnNext;
        private Button _btnSkip;
        private Panel _pnlContent;

        public TutorialForm()
        {
            InitializeComponent();
            ShowStep(0);
        }

        private void InitializeComponent()
        {
            Text = "Обучение";
            Size = new Size(560, 460);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(20, 40, 80);
            Font = new Font("Segoe UI", 10f);

            // ── Панель контента ──────────────────────────────────────────
            _pnlContent = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(520, 340),
                BackColor = Color.FromArgb(30, 60, 120),
            };
            Controls.Add(_pnlContent);

            _lblEmoji = new Label
            {
                Font = new Font("Segoe UI Emoji", 36f),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                Size = new Size(520, 70),
                Location = new Point(0, 15),
            };
            _pnlContent.Controls.Add(_lblEmoji);

            _lblTitle = new Label
            {
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.LightCyan,
                Size = new Size(500, 36),
                Location = new Point(10, 95),
            };
            _pnlContent.Controls.Add(_lblTitle);

            _lblText = new Label
            {
                Font = new Font("Segoe UI", 10f),
                TextAlign = ContentAlignment.TopLeft,
                ForeColor = Color.White,
                Size = new Size(490, 160),
                Location = new Point(15, 140),
            };
            _pnlContent.Controls.Add(_lblText);

            // ── Счётчик шагов ────────────────────────────────────────────
            _lblStepCounter = new Label
            {
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.LightGray,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(520, 22),
                Location = new Point(0, 310),
            };
            _pnlContent.Controls.Add(_lblStepCounter);

            // ── Кнопки ───────────────────────────────────────────────────
            _btnSkip = new Button
            {
                Text = "Пропустить",
                Location = new Point(20, 380),
                Size = new Size(120, 36),
                BackColor = Color.FromArgb(60, 60, 100),
                ForeColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat,
            };
            _btnSkip.FlatAppearance.BorderColor = Color.Gray;
            _btnSkip.Click += (s, e) => Close();
            Controls.Add(_btnSkip);

            _btnPrev = new Button
            {
                Text = "◄ Назад",
                Location = new Point(300, 380),
                Size = new Size(100, 36),
                BackColor = Color.FromArgb(0, 80, 160),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
            };
            _btnPrev.FlatAppearance.BorderColor = Color.CornflowerBlue;
            _btnPrev.Click += BtnPrev_Click;
            Controls.Add(_btnPrev);

            _btnNext = new Button
            {
                Text = "Далее ►",
                Location = new Point(415, 380),
                Size = new Size(120, 36),
                BackColor = Color.FromArgb(0, 140, 200),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
            };
            _btnNext.FlatAppearance.BorderColor = Color.DeepSkyBlue;
            _btnNext.Click += BtnNext_Click;
            Controls.Add(_btnNext);
        }

        private void ShowStep(int index)
        {
            _currentStep = Math.Clamp(index, 0, _steps.Length - 1);
            var (title, text, emoji) = _steps[_currentStep];

            _lblEmoji.Text = emoji;
            _lblTitle.Text = title;
            _lblText.Text = text;
            _lblStepCounter.Text = $"Шаг {_currentStep + 1} из {_steps.Length}";

            _btnPrev.Enabled = _currentStep > 0;
            _btnNext.Text = _currentStep == _steps.Length - 1 ? "✔ Начать игру" : "Далее ►";
        }

        private void BtnPrev_Click(object sender, EventArgs e) => ShowStep(_currentStep - 1);

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (_currentStep == _steps.Length - 1)
                Close();
            else
                ShowStep(_currentStep + 1);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) { Close(); return true; }
            if (keyData == Keys.Right || keyData == Keys.Enter) { BtnNext_Click(null, null); return true; }
            if (keyData == Keys.Left) { BtnPrev_Click(null, null); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}