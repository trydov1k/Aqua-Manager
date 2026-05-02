using AquaManager.Presentation.Enums;

namespace AquaManager.Forms
{
    /// <summary>
    /// Игровое меню (открывается по кнопке «Меню» или по Escape).
    /// Предоставляет действия: продолжить, сохранить, загрузить, новая игра, выйти.
    /// </summary>
    public class GameMenuForm : Form
    {
        public MenuAction SelectedAction { get; private set; } = MenuAction.None;

        private Button _btnContinue;
        private Button _btnSave;
        private Button _btnLoad;
        private Button _btnTutorial;
        private Button _btnNewGame;
        private Button _btnExit;
        private Label _lblTitle;
        private Label _lblVersion;

        public GameMenuForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Меню";
            Size = new Size(360, 555);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(15, 30, 60);
            Font = new Font("Segoe UI", 11f);

            // Заголовок
            _lblTitle = new Label
            {
                Text = "🐠  Aqua Manager",
                Font = new Font("Segoe UI", 18f, FontStyle.Bold),
                ForeColor = Color.DeepSkyBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 30),
                Size = new Size(360, 50),
            };
            Controls.Add(_lblTitle);

            // Линия-разделитель
            var sep = new Panel
            {
                BackColor = Color.FromArgb(0, 100, 180),
                Location = new Point(40, 88),
                Size = new Size(280, 2),
            };
            Controls.Add(sep);

            // Кнопки меню
            int btnX = 60, btnW = 240, btnH = 46;
            int[] btnY = { 110, 170, 225, 280, 335, 405 };

            _btnContinue = CreateMenuButton("▶  Продолжить игру", Color.FromArgb(0, 130, 200), btnX, btnY[0], btnW, btnH);
            _btnSave = CreateMenuButton("💾  Сохранить игру", Color.FromArgb(0, 110, 60), btnX, btnY[1], btnW, btnH);
            _btnLoad = CreateMenuButton("📂  Загрузить игру", Color.FromArgb(0, 80, 150), btnX, btnY[2], btnW, btnH);

            _btnTutorial = CreateMenuButton("📖  Начать обучение", Color.FromArgb(0, 140, 160), btnX, btnY[3], btnW, btnH);

            _btnNewGame = CreateMenuButton("🆕  Новая игра", Color.FromArgb(100, 60, 0), btnX, btnY[4], btnW, btnH);
            _btnExit = CreateMenuButton("🚪  Выйти из игры", Color.FromArgb(140, 20, 20), btnX, btnY[5], btnW, btnH);

            _btnContinue.Click += (s, e) => { SelectedAction = MenuAction.Continue; Close(); };
            _btnSave.Click += BtnSave_Click;
            _btnLoad.Click += BtnLoad_Click;
            _btnTutorial.Click += BtnTutorial_Click;
            _btnNewGame.Click += BtnNewGame_Click;
            _btnExit.Click += BtnExit_Click;

            Controls.AddRange(new Control[]
            {
                _btnContinue, _btnSave, _btnLoad, _btnTutorial, _btnNewGame, _btnExit
            });

            // Версия / подпись
            _lblVersion = new Label
            {
                Text = "Aqua Manager v1.0",
                Font = new Font("Segoe UI", 8f),
                ForeColor = Color.DimGray,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 460),
                Size = new Size(360, 20),
            };
            Controls.Add(_lblVersion);
        }

        private static Button CreateMenuButton(string text, Color backColor, int x, int y, int w, int h)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, h),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 11f),
                Padding = new Padding(8, 0, 0, 0),
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(backColor.R + 40, backColor.G + 40, backColor.B + 40);
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(
                Math.Min(backColor.R + 30, 255),
                Math.Min(backColor.G + 30, 255),
                Math.Min(backColor.B + 30, 255));
            return btn;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SelectedAction = MenuAction.Save;
            MessageBox.Show("Игра сохранена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            SelectedAction = MenuAction.Load;
            MessageBox.Show("Игра загружена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void BtnTutorial_Click(object sender, EventArgs e)
        {
            SelectedAction = MenuAction.Tutorial;
            Close();
        }

        private void BtnNewGame_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Начать новую игру?\nВесь текущий прогресс будет потерян!",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                SelectedAction = MenuAction.NewGame;
                Close();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Выйти из игры?\nНесохранённый прогресс будет потерян.",
                "Выход",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SelectedAction = MenuAction.Exit;
                Close();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                SelectedAction = MenuAction.Continue;
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}