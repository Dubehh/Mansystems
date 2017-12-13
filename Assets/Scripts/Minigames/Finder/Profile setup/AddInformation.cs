using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Assets.Scripts.App.Data_Management.Handshakes;
using Assets.Scripts.App.Tracking.Table;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class AddInformation : MonoBehaviour {

    [SerializeField] private GameObject _formContent;

    public void Finish() {
        var profileSetup = GetComponentInParent<ProfileSetup>();
        var fields = _formContent.GetComponentsInChildren<Text>().Where(x => x.tag == "InputName");

        var handshake = new InformationProtocol(Protocol.Insert)
            .AddParameter("targetTable", "module_finder")
            .AddParameter("uid", PlayerPrefs.GetString("uid"));
        DataParams parameters = DataParams.Build();

        foreach (var field in fields) {
            var key = field.name;
            var value = field.GetComponentInChildren<InputField>().text;

            parameters.Append(key, value);
            handshake.AddParameter(key, value);
        }

        profileSetup.FinderProfile.Insert(parameters);
        handshake.Send();

        var likeTable = new DataTable("FinderLikes");
        likeTable.AddProperty(new DataProperty("ProfileID", DataProperty.DataPropertyType.VARCHAR));
        AppData.Instance().Registry.Register(likeTable);

        profileSetup.gameObject.SetActive(false);
        profileSetup.FinderController.gameObject.SetActive(true);
    }
}
