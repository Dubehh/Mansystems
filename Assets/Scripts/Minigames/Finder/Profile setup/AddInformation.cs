using System.Linq;
using Assets.Scripts.App.Data_Management.Handshakes;
using Assets.Scripts.App.Tracking.Table;
using UnityEngine;
using UnityEngine.UI;

public class AddInformation : MonoBehaviour {

    [SerializeField] public GameObject FormContent;

    /// <summary>
    /// Event for the final button of the profile setup:
    /// - Inserts the user's information into the database
    /// - Creates a table for liked profiles
    /// - Opens the finder screen and closes the profile setup screen
    /// </summary>
    public void Finish() {
        var profileSetup = GetComponentInParent<ProfileSetup>();
        var fields = FormContent.GetComponentsInChildren<Text>().Where(x => x.tag == "InputName");

        var handshake = new InformationProtocol(Protocol.Insert)
            .AddParameter("targetTable", "module_finder")
            .AddParameter("uid", PlayerPrefs.GetString("uid"));

        var parameters = DataParams.Build();

        foreach (var field in fields) {
            var key = field.name;
            var value = field.GetComponentInChildren<InputField>().text;

            parameters.Append(key, value);
            handshake.AddParameter(key, value);
        }

        profileSetup.FinderProfile.Insert(parameters);
        handshake.Send();

        var likeTable = new DataTable(FinderController.LikeTable);
        likeTable.AddProperty(new DataProperty("ProfileID", DataProperty.DataPropertyType.VARCHAR));
        AppData.Instance().Registry.Register(likeTable);

        profileSetup.gameObject.SetActive(false);
        profileSetup.FinderController.gameObject.SetActive(true);
    }
}
