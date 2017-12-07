using Assets.Scripts.App.Game;
using System;
using Assets.Scripts.App.Data_Management.Handshakes;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FinderController : GameController {

    [SerializeField]
    public RawImage Match;

<<<<<<< HEAD
    public override void OnUnload() {
=======
    [SerializeField]
    private Text _name;

    [SerializeField]
    private Text _description;

    private FinderProfileController _finderProfileController;

    private void Start() {
        _finderProfileController = new FinderProfileController();

        new InformationProtocol(Protocol.Fetch).AddParameter("responseHandler", "finder").Send((request) => {
            // Make sure that the player doesn't see his own profile
            _finderProfileController.LoadProfiles(request);
            UpdateUI();
        });
>>>>>>> 8f7bed1101ca49fdc5bea148efb916f0c508e773
    }

    protected override void BeforeLoad() {
    }

    protected override void OnLoad() {
    }

    protected override void Update() {
        if (Input.touchCount == 1) {
            Debug.Log(Input.touches[0].position);
            Match.transform.Translate(0, 0, 0);           

        }
    }
}
