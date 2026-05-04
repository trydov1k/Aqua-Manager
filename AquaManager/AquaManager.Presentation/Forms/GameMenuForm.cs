using AquaManager.Presentation.Enums;

namespace AquaManager.Presentation.Forms;

public partial class GameMenuForm : Form
{
    public MenuAction SelectedAction { get; private set; } = MenuAction.None;

    public GameMenuForm()
    {
        InitializeComponent();
    }

    #region Обработка нажатий кнопок
    private void btnContinue_Click(object sender, EventArgs e)
    {
        SelectedAction = MenuAction.Continue;
        Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        SelectedAction = MenuAction.Save;
        Close();
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
        SelectedAction = MenuAction.Load;
        Close();
    }

    private void btnTutorial_Click(object sender, EventArgs e)
    {
        SelectedAction = MenuAction.Tutorial;
        Close();
    }

    private void btnNewGame_Click(object sender, EventArgs e)
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

    private void btnExit_Click(object sender, EventArgs e)
    {
        SelectedAction = MenuAction.Exit;
        Close();
    }
    #endregion

    #region Обработка нажатий на клавиши клавиатуры
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

    #endregion
}
