using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Var

    [Header("-InputFields")]
    public TMP_InputField nameInpFld;
    public TMP_InputField emailInpFld;

    [Header("-Text")]
    public TMP_Text nameTxt;
    public TMP_Text emailTxt;

    [Header("-Buttons")]
    public Button saveBtn;
    public Button updateBtn;
    public Button retriveBtn;
    public Button deleteBtn;

    [Header("-Script")]
    [SerializeField] DatabaseManager databaseManager;

    #endregion

    #region Unity_Events

    private void Reset()
    {
        var mainPanelOfInpfld = gameObject.transform.Find("EditPanel");

        ///input fields
        nameInpFld = mainPanelOfInpfld.transform.Find("InputFieldsPanel/NamePanel/InputField (TMP)").GetComponent<TMP_InputField>();
        emailInpFld = mainPanelOfInpfld.transform.Find("InputFieldsPanel/EmailPanel/InputField (TMP)").GetComponent<TMP_InputField>();

        ///text
        nameTxt = mainPanelOfInpfld.transform.Find("GetDataPanel/ForShowName/NameShow").GetComponent<TMP_Text>();
        emailTxt = mainPanelOfInpfld.transform.Find("GetDataPanel/ForShowEmail/EmailShow").GetComponent<TMP_Text>();

        ///buttons
        saveBtn = mainPanelOfInpfld.transform.Find("ButtonsPanel/Save").GetComponent<Button>();
        updateBtn = mainPanelOfInpfld.transform.Find("ButtonsPanel/Update").GetComponent<Button>();
        retriveBtn = mainPanelOfInpfld.transform.Find("ButtonsPanel/Reterive").GetComponent<Button>();
        deleteBtn = mainPanelOfInpfld.transform.Find("ButtonsPanel/Delete").GetComponent<Button>();

        ///script
        databaseManager = FindObjectOfType<DatabaseManager>(true);
    }

    private void Start()
    {
        ///input fields
        nameInpFld.onValueChanged.AddListener(NameInpfldListnr);
        emailInpFld.onValueChanged.AddListener(EmailInpfldListnr);

        ///buttons
        saveBtn.onClick.AddListener(SaveBtnLisner);
        updateBtn.onClick.AddListener(UpdateBtnLisner);
        retriveBtn.onClick.AddListener(RetriveBtnLisner);
        deleteBtn.onClick.AddListener(DeleteBtnListner);
    }

    #endregion

    #region ListnerMethod

    ///---------input field listner--------- 
    void NameInpfldListnr(string value)
    {
        nameInpFld.text = value;
    }

    void EmailInpfldListnr(string value)
    {
        emailInpFld.text = value;
    }

    ///---------buttons listner---------
    void SaveBtnLisner()
    {
        databaseManager.CreateUserDataInfo();

        Debug.Log("Data save in firebase");
    }

    void UpdateBtnLisner()
    {
        databaseManager.UpdateUserDataInfo();

        Debug.Log("Update Data in firebase");
    }

    void RetriveBtnLisner()
    {
        databaseManager.RetriveUserDataInfo();

        Debug.Log("Data Get from firebase");
    }

    void DeleteBtnListner()
    {
        databaseManager.DeleteUserDataInfo();

        Debug.Log("Delete Data from firebase");
    }

    #endregion
}
