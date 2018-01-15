using System.Linq;
using Assets.Scripts.App;
using Assets.Scripts.App.Data_Management.Handshakes;
using Assets.Scripts.App.Data_Management.Table;
using Assets.Scripts.Minigames.Finder.Profile;
using Assets.Scripts.Minigames.Finder.Profile_management;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Minigames.Finder.Profile_setup {
    public class AddInformation : MonoBehaviour {
        [SerializeField] public GameObject FormContent;
        [SerializeField] public GameObject Warning;

        /// <summary>
        ///     Event for the final button of the profile setup:
        ///     - Inserts the user's information into the database
        ///     - Creates a table for liked profiles
        ///     - Opens the finder screen and closes the profile setup screen
        /// </summary>
        public void Finish() {
            var profileSetup = GetComponentInParent<ProfileSetup>();
            var finderProfile = new DataTable(FinderController.ProfileTable);
            finderProfile.AddProperty(new DataProperty("Name", DataProperty.DataPropertyType.VARCHAR));
            finderProfile.AddProperty(new DataProperty("Age", DataProperty.DataPropertyType.VARCHAR));
            finderProfile.AddProperty(new DataProperty("City", DataProperty.DataPropertyType.VARCHAR));
            finderProfile.AddProperty(new DataProperty("PhoneNumber", DataProperty.DataPropertyType.VARCHAR));
            finderProfile.AddProperty(new DataProperty("FavMovie", DataProperty.DataPropertyType.VARCHAR));
            finderProfile.AddProperty(new DataProperty("FavMusic", DataProperty.DataPropertyType.VARCHAR));
            finderProfile.AddProperty(new DataProperty("FavFood", DataProperty.DataPropertyType.VARCHAR));
            finderProfile.AddProperty(new DataProperty("FavSport", DataProperty.DataPropertyType.VARCHAR));
            finderProfile.AddProperty(new DataProperty("FavGame", DataProperty.DataPropertyType.VARCHAR));
            finderProfile.AddProperty(new DataProperty("FavVacation", DataProperty.DataPropertyType.VARCHAR));
            AppData.Instance().Registry.Register(finderProfile);

            var fields = FormContent.GetComponentsInChildren<Text>().Where(x => x.tag == "InputName");

            var handshake = new InformationProtocol(Protocol.Data)
                .SetHandler("finderProfileUpdate", InformationProtocol.HandlerType.Update)
                .AddParameter("action", "insert")
                .AddParameter("name", PlayerPrefs.GetString("name"))
                .AddParameter("uid", PlayerPrefs.GetString("uid"));

            var parameters = DataParams.Build();

            foreach (var field in fields) {
                var key = field.name;
                var value = field.GetComponentInChildren<InputField>().text;

                if (string.IsNullOrEmpty(value)) {
                    Warning.SetActive(true);
                    return;
                }

                parameters.Append(key, value);
                handshake.AddParameter(key, value);
            }

            finderProfile.Insert(parameters);
            handshake.Send();

            var likeTable = new DataTable(FinderController.LikeTable);
            likeTable.AddProperty(new DataProperty("ProfileID", DataProperty.DataPropertyType.VARCHAR));
            AppData.Instance().Registry.Register(likeTable);

            profileSetup.gameObject.SetActive(false);
            profileSetup.FinderController.gameObject.SetActive(true);
        }
    }
}