using System;
using System.Linq;
using Assets.Scripts.Dashboard;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Manny))]
public class DashboardController : MonoBehaviour {

    private DashboardAnimationHandler _dashboardAnimationHandler;
    private DashboardBackground _current;
    private UIStatIndicator _indicator;

    [SerializeField] public DashboardBackground BackgroundDead;
    [SerializeField] public DashboardBackground[] Backgrounds;
    [SerializeField] public RawImage Dialog;
    [SerializeField] public Slider ExperienceIndicator;
    [SerializeField] public Text LevelIndicator;
    [SerializeField] public Manny Manny;

    // Use this for initialization
    private void Start() {
        _indicator = new UIStatIndicator(ExperienceIndicator, Attribute.Experience, Manny);
        _dashboardAnimationHandler = new DashboardAnimationHandler();
        SetExperienceGoal();
        UpdateIndicators();
    }

    private void OnEnable() {
        InvalidateBackground();
    }

    private void Update() {
        UpdateIndicators();
        _dashboardAnimationHandler.ScanInput();
    }

    /// <summary>
    ///     Updates the experience indicator and level if the player reaches a new level
    ///     and contains the functionality for the dialog to vanish on touch
    /// </summary>
    private void UpdateIndicators() {
        _indicator.Update();
        if (ExperienceIndicator.value >= ExperienceIndicator.maxValue) {
            Manny.Attribute.IncrementAttribute(Attribute.Level, 1);
            SetExperienceGoal();
        }
        if (Input.touchCount > 0 && Dialog.gameObject.activeSelf)
            Dialog.gameObject.SetActive(false);
    }

    /// <summary>
    ///     Gets the needed experience for the next level and passes it to the slider
    /// </summary>
    private void SetExperienceGoal() {
        var level = (int)Manny.Attribute.GetAttribute(Attribute.Level);
        ExperienceIndicator.minValue = Manny.Leveling.GetRequiredExperience(level);
        LevelIndicator.text = "Level " + level;
        _indicator.SetMax((int)Manny.Leveling.GetRequiredExperience(level + 1));
    }

    /// <summary>
    ///     Displays a dialog on the screen with a message
    /// </summary>
    /// <param name="message">The message to be displayed in the dialog</param>
    public void DisplayDialog(string message) {
        Dialog.gameObject.SetActive(true);
        Dialog.GetComponentInChildren<Text>().text = message;
    }

    /// <summary>
    ///     Invalidates the background with its manny design
    /// </summary>
    public void InvalidateBackground() {
        var now = DateTime.Now.Hour;
        if(_current != null) {
            _current.Background.SetActive(false);
            _current.Manny.SetActive(false);
        }
        _current = Manny.HasDied() ? BackgroundDead : Backgrounds.FirstOrDefault(x => x.Time.Min <= now && x.Time.Max >= now);
        _current = _current ?? Backgrounds[0];
        _current.Background.SetActive(true);
        _current.Manny.SetActive(true);
        if (_dashboardAnimationHandler == null) return;
        _dashboardAnimationHandler.SetAnimator(_current.Manny);
    }

    public DashboardBackground GetCurrentBackground() {
        return _current;
    }
}