using AquaManager.Presentation.Controls;

namespace AquaManager.Presentation.Forms;
/// <summary>
/// Форма для выбора сохранения
/// </summary>
public partial class SelectSaveForm : Form
{
    /// <summary>
    /// Название выбранного сохранения
    /// Пустое значение, если пользователь закрыл форму
    /// </summary>
    public string SelectedSave { get; private set; }

    private readonly List<string> _saves;
    private readonly string _defaultSave;
    private readonly Action<string> _onDelete;

    public SelectSaveForm(List<string> saves, string defaultSave, Action<string> onDelete)
    {
        _saves = saves ?? throw new ArgumentNullException(nameof(saves));
        _defaultSave = defaultSave;
        _onDelete = onDelete ?? throw new ArgumentNullException(nameof(onDelete));
        SelectedSave = string.Empty;

        InitializeComponent();
        RenderList();
    }

    #region Построение списка сохранений
    private void RenderList()
    {
        listPanel.Controls.Clear();

        int y = 6;

        foreach (string name in _saves)
        {
            var row = new SelectSaveControl(name, SelectAndClose, DeleteSave);
            row.Location = new Point(row.Location.X, y);
            listPanel.Controls.Add(row);
            y += 44;
        }

        if (_saves.Count == 0)
        {
            var lblEmpty = new Label
            {
                Text = "Нет доступных сохранений",
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 100),
                Size = new Size(393, 40),
            };
            listPanel.Controls.Add(lblEmpty);
        }
    }

    #endregion

    #region Методы для обработки выбора и удаления сохранений
    private void SelectAndClose(string name)
    {
        SelectedSave = name;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void DeleteSave(string name)
    {
        var result = MessageBox.Show(
                $"Удалить сохранение «{name}»?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

        if (result != DialogResult.Yes) return;

        _onDelete(name);
        _saves.Remove(name);
        RenderList();
    }

    #endregion

    #region Обработка нажатий на клавиши клавиатуры
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Escape)
        {
            SelectedSave = string.Empty;
            DialogResult = DialogResult.Cancel;
            Close();
            return true;
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }
    #endregion
}