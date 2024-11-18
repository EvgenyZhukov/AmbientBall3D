using UnityEngine;

public class Puzzle1 : MonoBehaviour
{

    public ButtonPuzzle buttonPuzzle1;
    public ButtonPuzzle buttonPuzzle2;
    public ButtonPuzzle buttonPuzzle3;
    public ButtonPuzzle buttonPuzzle4;
    public ButtonPuzzle buttonPuzzle5;
    public int[] puzzle = new int[5] { 0, 0, 0, 0, 0 };
    public int press = 0;
    private bool button1 = false;
    private bool button2 = false;
    private bool button3 = false;
    private bool button4 = false;
    private bool button5 = false;
    float speed = 2f;               
    public float timer = 0f;
    public bool pressed = false;
    public AudioSource buttonSound;
    bool soundReady = true;

    void Update()
    {
        #region Механика учета нажатий кнопок
        // Наполняет массив значениями определяющими нажата ли кнопка и считает количество нажатых кнопок
        if (buttonPuzzle1.activated == true && button1 == false)
        {
            puzzle[0] = 1;
            press++;
            button1 = true;
        }

        if (buttonPuzzle2.activated == true && button2 == false)
        {
            puzzle[1] = 1;
            press++;
            button2 = true;
        }

        if (buttonPuzzle3.activated == true && button3 == false)
        {
            puzzle[2] = 1;
            press++;
            button3 = true;
        }

        if (buttonPuzzle4.activated == true && button4 == false)
        {
            puzzle[3] = 1;
            press++;
            button4 = true;
        }

        if (buttonPuzzle5.activated == true && button5 == false)
        {
            puzzle[4] = 1;
            press++;
            button5 = true;
        }
        #endregion

        #region Механика проверки порядка нажатия кнопок
        if (press == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                if (puzzle[i] != 1)
                {
                    Invoke("Cancel", 0f);
                }
            }
        }

        if (press == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                if (puzzle[i] != 1)
                {
                    Invoke("Cancel", 0f);
                }
            }

        }

        if (press == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                if (puzzle[i] != 1)
                {
                    Invoke("Cancel", 0f);
                }
            }

        }

        if (press == 5)
        {
            for (int i = 0; i < 5; i++)
            {
                if (puzzle[i] != 1)
                {
                    Invoke("Cancel", 0f);
                }
            }
        }
        #endregion

        #region Завершение головоломки
        if (buttonPuzzle1.activated == true && buttonPuzzle2.activated == true && buttonPuzzle3.activated == true && buttonPuzzle4.activated == true && buttonPuzzle5.activated == true && pressed == false)
        {
            transform.position -= new Vector3(0.0f, speed * Time.fixedDeltaTime, 0.0f);
            timer += 1f * Time.fixedDeltaTime;

            if (soundReady)
            {
                buttonSound.Play();
                soundReady = false;
            }
        }
        if (timer >= 3.5)
        {
            pressed = true;
        }
        #endregion
    }

    /// <summary>
    /// Откатывает головоломку
    /// </summary>
    void Cancel()
    {
        puzzle[0] = 0;
        puzzle[1] = 0;
        puzzle[2] = 0;
        puzzle[3] = 0;
        puzzle[4] = 0;
        press = 0;
        buttonPuzzle1.activated = false;
        buttonPuzzle2.activated = false;
        buttonPuzzle3.activated = false;
        buttonPuzzle4.activated = false;
        buttonPuzzle5.activated = false;
        button1 = false;
        button2 = false;
        button3 = false;
        button4 = false;
        button5 = false;
    }
}
