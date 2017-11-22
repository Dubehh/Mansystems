using System.Collections.Generic;

public class PrizeController {

    private int[] _prizes;
    private int _currentPrizeIndex;
    public int CurrentPrize { get; set; }

    public int StaticPrize { get; set; }

    public PrizeController() {
        _prizes = new int[] {
            0,
            2, 5, 10, 20, 25,
            50, 100, 125, 250, 300,
            400, 550, 750, 850, 1000
        };

        CurrentPrize = _prizes[0];
    }

    /// <summary>
    /// Increases the current prize
    /// </summary>
    public void IncreasePrize() {
        _currentPrizeIndex += 1;
        CurrentPrize = _prizes[_currentPrizeIndex];

        if(_currentPrizeIndex % 5 == 0)
            StaticPrize = CurrentPrize;
    }
}
