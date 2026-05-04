using AquaManager.Forms;
using AquaManager.Presentation.Enums;

namespace AquaManager.Presentation.Forms;

public partial class NameInputForm : Form
{
    public string EnteredName { get; private set; } = string.Empty;

    private readonly string _defaultName;
    private readonly string _emojiText;

    private readonly string _buyMessage;
    private readonly string _giveNameMessage;

    private readonly int MaxNameLenght = 15;

    public NameInputForm(NameInputFormType entityType, string defaultName, string emoji = "🐠")
    {
        _buyMessage = entityType == NameInputFormType.Fish ? "рыбку"
            : entityType == NameInputFormType.Aquarium ? "аквариум"
            : "сохранение";
        _giveNameMessage = entityType == NameInputFormType.Fish ? "рыбке"
            : entityType == NameInputFormType.Aquarium ? "аквариуму"
            : "сохранению";
        _defaultName = defaultName;
        _emojiText = emoji;

        InitializeComponent();

        Text = $"Назовите {_buyMessage}";
        lblIcon.Text = _emojiText;
        lblPrompt.Text = $"Дайте имя {_giveNameMessage}:";
        txtName.MaxLength = MaxNameLenght;
        lblHint.Text = $"Макс. {MaxNameLenght} символов. Оставьте пустым для имени по умолчанию.";
    }

    #region События (выделение текстового поля при заходе в него)
    private void txtName_Enter(object sender, EventArgs e)
    {
        txtName.BackColor = Color.FromArgb(50, 90, 170);
    }

    private void txtName_Leave(object sender, EventArgs e)
    {
        txtName.BackColor = Color.FromArgb(40, 70, 130);
    }

    #endregion

    #region Обработка нажатий на кнопки
    private void btnOk_Click(object sender, EventArgs e)
    {
        string trimmed = txtName.Text.Trim();
        EnteredName = string.IsNullOrEmpty(trimmed) ? _defaultName : trimmed;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnSkip_Click(object sender, EventArgs e)
    {
        EnteredName = _defaultName;
        DialogResult = DialogResult.Cancel;
        Close();
    }

    #endregion

    #region Обработка нажатий на клавиши клавиатуры
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Escape)
        {
            DialogResult = DialogResult.Cancel;
            Close();
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }
    #endregion
}
