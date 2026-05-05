namespace AquaManager.Presentation.Controls;

public partial class SelectSaveControl : UserControl
{
    private Action<string> _selectAndClose;
    private Action<string> _deleteSave;
    private string _saveName;
    public SelectSaveControl(string name, Action<string> selectAndClose, Action<string> deleteSave)
    {
        _selectAndClose = selectAndClose;
        _deleteSave = deleteSave;
        _saveName = name;

        InitializeComponent();
        lblName.Text = name;
    }

    #region События (навелся мышкой)
    private void SelectSaveControl_MouseEnter(object sender, EventArgs e)
    {
        BackColor = Color.FromArgb(45, 80, 145);
    }

    private void SelectSaveControl_MouseLeave(object sender, EventArgs e)
    {
        BackColor = Color.FromArgb(30, 58, 110);
    }
    #endregion

    #region Обработка нажатий
    private void SelectSaveControl_Click(object sender, EventArgs e)
        => _selectAndClose(_saveName);

    private void lblIcon_Click(object sender, EventArgs e)
        => _selectAndClose(_saveName);

    private void lblName_Click(object sender, EventArgs e)
         => _selectAndClose(_saveName);

    private void btnDelete_Click(object sender, EventArgs e)
        => _deleteSave(_saveName);

    private void btnDelete_MouseEnter(object sender, EventArgs e)
        => BackColor = Color.FromArgb(30, 58, 110);

    #endregion
}
