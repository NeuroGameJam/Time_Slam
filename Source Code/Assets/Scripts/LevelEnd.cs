using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour 
{
    public TextMesh[] estimate = new TextMesh[5];
    public TextMesh[] real = new TextMesh[5];
    public TextMesh[] score = new TextMesh[5];

    private void Start()
    {
        Game game = LoadSaveManager.atualGame;

        for (int i = 0; i < game.levels.Count; i++)
        {
            if (i >= 5)
                return;

            estimate[i].text = game.levels[i].levelParams.timeGuess.ToString();
            real[i].text = game.levels[i].levelParams.timePlayed.ToString();
            score[i].text = game.levels[i].point.Count.ToString();

        }
    }

}

[System.Serializable]
public class Tabela
{
}
