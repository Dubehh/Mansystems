using Assets.Scripts.App.Game;
using Assets.Scripts.App.Tracking.Table;

/// <summary>
///     Singleton class used to ensure single-initialization of instances.
/// </summary>
public class AppData {
    private static AppData _instance;

    private AppData() {
        Game = new GameManager();
        Registry = new DataTableRegistry();
    }

    public GameManager Game { get; private set; }
    public DataTableRegistry Registry { get; private set; }
    public MannyAttribute MannyAttribute { get; set; }

    /// <summary>
    ///     Returns the instance of the AppData class
    /// </summary>
    /// <returns>AppData instance</returns>
    public static AppData Instance() {
        return _instance ?? (_instance = new AppData());
    }
}