using UnityEngine;

namespace PlayerPrefsSavingMethods
{
    public class SaveLoadData
    {
        #region �������� � ������� ����������� �������� (�������� ������ � ����)
        /// <summary>
        /// ����� ��������� ������ (����������)
        /// </summary>
        public static void ResetCoordinates()
        {
            PlayerPrefs.SetFloat("xPos", 0);
            PlayerPrefs.SetFloat("yPos", 0);
            PlayerPrefs.SetFloat("zPos", 0);
        }
        /// <summary>
        /// ���������� ��������� ������ (����������)
        /// </summary>
        /// <param name="x">���������� x</param>
        /// <param name="y">���������� y</param>
        /// <param name="z">���������� z</param>
        public static void SaveCoordinates(float x, float y, float z)
        {
            PlayerPrefs.SetFloat("xPos", x);
            PlayerPrefs.SetFloat("yPos", y);
            PlayerPrefs.SetFloat("zPos", z);
        }
        /// <summary>
        /// �������� ��������� ������ (����������)
        /// </summary>
        /// <param name="x">���������� x</param>
        /// <param name="y">���������� y</param>
        /// <param name="z">���������� z</param>
        public static void LoadCoordinates(out float x, out float y, out float z)
        {
            x = PlayerPrefs.GetFloat("xPos");
            y = PlayerPrefs.GetFloat("yPos");
            z = PlayerPrefs.GetFloat("zPos");
        }
        /// <summary>
        /// ����� ���������� ������ �� ������������� ��������
        /// </summary>
        public static void ResetLives()
        {
            PlayerPrefs.SetInt("lives", 2);
        }
        /// <summary>
        /// ���������� �������� ���������� ������
        /// </summary>
        /// <param name="lives">���������� ������</param>
        public static void SetLives(int lives)
        {
            PlayerPrefs.SetInt("lives", lives);
        }
        /// <summary>
        /// �������� �������� ���������� ������
        /// </summary>
        /// <returns>���������� ������</returns>
        public static int GetLives()
        {
            int lives = PlayerPrefs.GetInt("lives");
            return lives;
        }
        /// <summary>
        /// ���������, ��������� �� ����� �� ������ ����������� ������
        /// </summary>
        /// <param name="check"></param>
        public static void SetInProgress(bool check)
        {
            if (check)
            {
                PlayerPrefs.SetInt("inProgress", 1);
            }
            else
            {
                PlayerPrefs.SetInt("inProgress", 0);
            }
        }
        /// <summary>
        /// ���������, ��������� �� ����� �� ������ ����������� ������
        /// </summary>
        /// <returns></returns>
        public static bool GetInProgress()
        {
            int check = PlayerPrefs.GetInt("inProgress");
            bool mode;
            if (check == 1)
            {
                mode = true;
            }
            else
            {
                mode = false;
            }
            return mode;
        }
        /// <summary>
        /// ���������� �������� ��������� �����������
        /// </summary>
        /// <param name="lvl"></param>
        public static void SetLevelProgress(int lvl)
        {
            PlayerPrefs.SetInt("currentLvl", lvl);
        }
        /// <summary>
        /// �������� �������� ��������� �����������
        /// </summary>
        /// <returns></returns>
        public static int GetLevelProgress()
        {
            int lvl = PlayerPrefs.GetInt("currentLvl");
            return lvl;
        }
        /// <summary>
        /// ���������� ��������
        /// </summary>
        /// <param name="soundVolume"></param>
        /// <param name="musicVolume"></param>
        /// <param name="soundMuted"></param>
        /// <param name="musicMuted"></param>
        /// <param name="controlJoystick">true - ���������� ����������, false - ��������� ����������</param>
        /// <param name="controlAccelerometer"></param>
        public static void SetOptions(float soundVolume, float musicVolume, bool soundMuted, bool musicMuted, bool controlJoystick, bool controlAccelerometer, bool graphicsHigh)
        {
            PlayerPrefs.SetFloat("soundVolume", soundVolume);
            PlayerPrefs.SetFloat("musicVolume", musicVolume);
            if (soundMuted)
            {
                PlayerPrefs.SetInt("soundMuted", 1);
            }
            else
            {
                PlayerPrefs.SetInt("soundMuted", 0);
            }
            if (musicMuted)
            {
                PlayerPrefs.SetInt("musicMuted", 1);
            }
            else
            {
                PlayerPrefs.SetInt("musicMuted", 0);
            }
            if (controlJoystick)
            {
                PlayerPrefs.SetInt("controlJoystick", 1);
            }
            else
            {
                PlayerPrefs.SetInt("controlJoystick", 0);
            }
            if (controlAccelerometer)
            {
                PlayerPrefs.SetInt("controlAccelerometer", 1);
            }
            else
            {
                PlayerPrefs.SetInt("controlAccelerometer", 0);
            }
            if (graphicsHigh)
            {
                PlayerPrefs.SetInt("graphicsHigh", 1);
            }
            else
            {
                PlayerPrefs.SetInt("graphicsHigh", 0);
            }
        }
        /// <summary>
        /// �������� ��������
        /// </summary>
        /// <param name="soundVolume"></param>
        /// <param name="musicVolume"></param>
        /// <param name="soundMode"></param>
        /// <param name="musicMode"></param>
        /// <param name="controlJoystick"></param>
        /// <param name="controlAccelerometer"></param>
        public static void GetOptions(out float soundVolume, out float musicVolume, out bool soundMode, out bool musicMode, out bool controlJoystick, out bool controlAccelerometer, out bool graphicsHigh)
        {
            soundVolume = PlayerPrefs.GetFloat("soundVolume");
            musicVolume = PlayerPrefs.GetFloat("musicVolume");

            int checkSoundMuted = PlayerPrefs.GetInt("soundMuted");
            if (checkSoundMuted == 1)
            {
                soundMode = true;
            }
            else
            {
                soundMode = false;
            }
            int checkMusicMuted = PlayerPrefs.GetInt("musicMuted");
            if (checkMusicMuted == 1)
            {
                musicMode = true;
            }
            else
            {
                musicMode = false;
            }
            int checkControlJoystick = PlayerPrefs.GetInt("controlJoystick");
            if (checkControlJoystick == 1)
            {
                controlJoystick = true;
            }
            else
            {
                controlJoystick = false;
            }
            int checkControlAccelerometer = PlayerPrefs.GetInt("controlAccelerometer");
            if (checkControlAccelerometer == 1)
            {
                controlAccelerometer = true;
            }
            else
            {
                controlAccelerometer = false;
            }
            int checkgraphicsHigh = PlayerPrefs.GetInt("graphicsHigh");
            if (checkgraphicsHigh == 1)
            {
                graphicsHigh = true;
            }
            else
            {
                graphicsHigh = false;
            }
        }
        
        public static void SetOptionsDataChecker(bool check)
        {
            if (check)
            {
                PlayerPrefs.SetInt("optionsData", 1);
            }
            else
            {
                PlayerPrefs.SetInt("optionsData", 0);
            }
        }
        public static bool GetOptionsDataChecker()
        {
            int check = PlayerPrefs.GetInt("optionsData");
            bool mode;
            if (check == 1)
            {
                mode = true;
            }
            else
            {
                mode = false;
            }
            return mode;
        }
        
        /// <summary>
        /// �������� �������� ����������
        /// </summary>
        /// <param name="controlJoystick"></param>
        /// <param name="controlAccelerometer"></param>
        public static void GetControlOptions(out bool controlJoystick, out bool controlAccelerometer)
        {
            int checkControlJoystick = PlayerPrefs.GetInt("controlJoystick");
            if (checkControlJoystick == 1)
            {
                controlJoystick = true;
            }
            else
            {
                controlJoystick = false;
            }
            int checkControlAccelerometer = PlayerPrefs.GetInt("controlAccelerometer");
            if (checkControlAccelerometer == 1)
            {
                controlAccelerometer = true;
            }
            else
            {
                controlAccelerometer = false;
            }
        }
        public static void SetTextProgress(int textProgress)
        {
            PlayerPrefs.SetInt("textProgress", textProgress);
        }
        public static int GetTextProgress()
        {
            int textProgress = PlayerPrefs.GetInt("textProgress");
            return textProgress;
        }
        public static void ResetTextProgress()
        {
            PlayerPrefs.SetInt("textProgress", 0);
        }
        /// <summary>
        /// ������ ���� �� ����������� ���� �� �������
        /// </summary>
        /// <param name="check"></param>
        public static void SetContinuousTaken(bool check)
        {
            if (check)
            {
                PlayerPrefs.SetInt("continuousTaken", 1);
            }
            else
            {
                PlayerPrefs.SetInt("continuousTaken", 0);
            }
        }
        /// <summary>
        /// �������� ���� �� ����������� ���� �� �������
        /// </summary>
        /// <returns></returns>
        public static bool GetContinuousTaken()
        {
            int check = PlayerPrefs.GetInt("continuousTaken");
            bool mode;
            if (check == 1)
            {
                mode = true;
            }
            else
            {
                mode = false;
            }
            return mode;
        }



        #endregion

        #region �������� � ���������� �������
        /// <summary>
        /// ���������� ��������� ������, �������� ��� ������ � ����
        /// </summary>
        /// <param name="x">���������� x</param>
        /// <param name="y">���������� y</param>
        /// <param name="z">���������� z</param>
        public static void SaveCoordinatesTemp(float x, float y, float z)
        {
            PlayerPrefs.SetFloat("xPosTemp", x);
            PlayerPrefs.SetFloat("yPosTemp", y);
            PlayerPrefs.SetFloat("zPosTemp", z);
        }
        /// <summary>
        /// �������� ��������� ������, �������� ��� ������ � ����
        /// </summary>
        /// <param name="x">���������� x</param>
        /// <param name="y">���������� y</param>
        /// <param name="z">���������� z</param>
        public static void LoadCoordinatesTemp(out float x, out float y, out float z)
        {
            x = PlayerPrefs.GetFloat("xPosTemp");
            y = PlayerPrefs.GetFloat("yPosTemp");
            z = PlayerPrefs.GetFloat("zPosTemp");
        }
        /// <summary>
        /// ���������, ��������� �� ����� �� ������ ����������� ������, �������� ��� ������ � ����
        /// </summary>
        /// <param name="check"></param>
        public static void SetInProgressTemp(bool check)
        {
            if (check)
            {
                PlayerPrefs.SetInt("inProgressTemp", 1);
            }
            else
            {
                PlayerPrefs.SetInt("inProgressTemp", 0);
            }
        }
        /// <summary>
        /// ���������, ��������� �� ����� �� ������ ����������� ������, �������� ��� ������ � ����
        /// </summary>
        /// <returns></returns>
        public static bool GetInProgressTemp()
        {
            int check = PlayerPrefs.GetInt("inProgressTemp");
            bool mode;
            if (check == 1)
            {
                mode = true;
            }
            else
            {
                mode = false;
            }
            return mode;
        }
        public static void SaveCamAxisTemp(float x, float y)
        {
            PlayerPrefs.SetFloat("xCamAxisTemp", x);
            PlayerPrefs.SetFloat("yCamAxisTemp", y);
        }
        public static void LoadCamAxisTemp(out float x, out float y)
        {
            x = PlayerPrefs.GetFloat("xCamAxisTemp");
            y = PlayerPrefs.GetFloat("yCamAxisTemp");
        }
        public static void DelCamAxisTemp()
        {
            PlayerPrefs.DeleteKey("xCamAxisTemp");
            PlayerPrefs.DeleteKey("yCamAxisTemp");
        }
        /// <summary>
        /// ����� ������� �����
        /// </summary>
        public static void ResetScene()
        {
            PlayerPrefs.SetInt("scene", 0);
        }
        /// <summary>
        /// ������ ������� �����
        /// </summary>
        /// <param name="scene"></param>
        public static void SetScene(int scene)
        {
            PlayerPrefs.SetInt("scene", scene);
        }
        /// <summary>
        /// �������� ������� �����
        /// </summary>
        /// <returns></returns>
        public static int GetScene()
        {
            int scene = PlayerPrefs.GetInt("scene");
            return scene;
        }
        /// <summary>
        /// ���������, ������ �� ��� ������ ������
        /// </summary>
        /// <param name="check"></param>
        public static void SetFirstLevelLaunch(bool check)
        {
            if (check)
            {
                PlayerPrefs.SetInt("firstLevelLaunch", 1);
            }
            else
            {
                PlayerPrefs.SetInt("firstLevelLaunch", 0);
            }
        }
        /// <summary>
        /// ���������, ������ �� ��� ������ ������, ��� ��������� ������ �����
        /// </summary>
        /// <returns></returns>
        public static bool GetFirstLevelLaunch()
        {
            int check = PlayerPrefs.GetInt("firstLevelLaunch");
            bool mode;
            if (check == 1)
            {
                mode = true;
            }
            else
            {
                mode = false;
            }
            return mode;
        }
        public static void SetControlChange(bool check)
        {
            if (check)
            {
                PlayerPrefs.SetInt("controlChange", 1);
            }
            else
            {
                PlayerPrefs.SetInt("controlChange", 0);
            }
        }
        public static bool GetControlChange()
        {
            int check = PlayerPrefs.GetInt("controlChange");
            bool mode;
            if (check == 1)
            {
                mode = true;
            }
            else
            {
                mode = false;
            }
            return mode;
        }
        public static void SetPropertiesFormNum(int formNum)
        {
            switch (formNum)
            {
                case 0:
                    PlayerPrefs.SetInt("propertiesFormNum", 0);
                    break;
                case 1:
                    PlayerPrefs.SetInt("propertiesFormNum", 1);
                    break;
                case 2:
                    PlayerPrefs.SetInt("propertiesFormNum", 2);
                    break;
                case 3:
                    PlayerPrefs.SetInt("propertiesFormNum", 3);
                    break;
                case 4:
                    PlayerPrefs.SetInt("propertiesFormNum", 4);
                    break;
                default:
                    break;
            }
        }
        public static int GetPropertiesFormNum()
        {
            int num = PlayerPrefs.GetInt("propertiesFormNum");
            return num;
        }
        #endregion

        #region �������� �����
        public static void SetMusicTime(float time)
        {
            PlayerPrefs.SetFloat("musicTime", time);
        }
        public static float GetMusicTime()
        {
            float time = PlayerPrefs.GetFloat("musicTime");
            return time;
        }
        #endregion



        #region ���������� ����� (�����)
        /// <summary>
        /// ������ ���������� ����� ������, ������������ ����� �����������
        /// </summary>
        /// <param name="scene">����� ������ ��� �����</param>
        /// <param name="stars">���������� �����</param>
        public static void SetStars(int scene, int stars)
        {
            string level = "levelStars_" + scene;
            PlayerPrefs.SetInt(level, stars);
        }
        /// <summary>
        /// �������� ���������� ����� ������, ������������ ����� �����������
        /// </summary>
        /// <param name="scene">����� ������ ��� �����</param>
        /// <returns>���������� �����</returns>
        public static int GetStars(int scene)
        {
            string level = "levelStars_" + scene;
            int stars = PlayerPrefs.GetInt(level);
            return stars;
        }

        /// <summary>
        /// ������ �������������� �������� ���������� ����� ����������� ������
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="stars"></param>
        public static void SetStarsScore(int scene, int stars)
        {
            string level = "levelStarsScore_" + scene;
            PlayerPrefs.SetInt(level, stars);
        }
        /// <summary>
        /// �������� �������������� �������� ���������� ����� ����������� ������
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public static int GetStarsScore(int scene)
        {
            string level = "levelStarsScore_" + scene;
            int stars = PlayerPrefs.GetInt(level);
            return stars;
        }
        /// <summary>
        /// ����� �������������� �������� ���������� ����� ����������� ������
        /// </summary>
        /// <param name="scene"></param>
        public static void ResetStarsScore(int scene)
        {
            string level = "levelStarsScore_" + scene;
            PlayerPrefs.SetInt(level, 0);
        }


        /// <summary>
        /// ��������� ����� ���������� ����� �� ��� ������
        /// </summary>
        /// <param name="starsTotal"></param>
        public static void SetStarsTotal(int starsTotal)
        {
            PlayerPrefs.SetInt("starsTotal", starsTotal);
        }
        /// <summary>
        /// ��������� ����� ���������� ����� �� ��� ������
        /// </summary>
        /// <returns></returns>
        public static int GetStarsTotal()
        {
            int starsTotal = PlayerPrefs.GetInt("starsTotal");
            return starsTotal;
        }


        
        public static void SetStarsEndlessMode(int starsEndlessModeTotal)
        {
            PlayerPrefs.SetInt("starsEndlessMode", starsEndlessModeTotal);
        }
        public static int GetStarsEndlessMode()
        {
            int starsEndlessModeTotal = PlayerPrefs.GetInt("starsEndlessMode");
            return starsEndlessModeTotal;
        }
        public static void ResetStarsEndlessMode()
        {
            PlayerPrefs.SetInt("starsEndlessMode", 0);
        }

        /// <summary>
        /// �������� ������ ������������ ������
        /// </summary>
        /// <param name="starsEndlessModeTotal"></param>
        public static void SetStarsEndlessModeTotal(int starsEndlessModeTotal)
        {
            PlayerPrefs.SetInt("starsEndlessModeTotal", starsEndlessModeTotal);
        }
        /// <summary>
        /// ���������� ������ ������������ ������
        /// </summary>
        /// <returns></returns>
        public static int GetStarsEndlessModeTotal()
        {
            int starsEndlessModeTotal = PlayerPrefs.GetInt("starsEndlessModeTotal");
            return starsEndlessModeTotal;
        }

        //���������� ���������� ���������� ������, ��� ������ � ����
        public static void SetStarsScoreTemp(int stars)
        {
            PlayerPrefs.SetInt("levelStarsScoreTemp", stars);
        }
        public static int GetStarsScoreTemp()
        {
            int stars = PlayerPrefs.GetInt("levelStarsScoreTemp");
            return stars;
        }
        public static void ResetStarsScoreTemp()
        {
            PlayerPrefs.SetInt("levelStarsScoreTemp", 0);
        }
        public static void SetEndlessScoreTemp(int endlessScore)
        {
            PlayerPrefs.SetInt("endlessScoreTemp", endlessScore);
        }
        public static int GetEndlessScoreTemp()
        {
            int endlessScore = PlayerPrefs.GetInt("endlessScoreTemp");
            return endlessScore;
        }
        public static void ResetEndlessScoreTemp()
        {
            PlayerPrefs.SetInt("endlessScoreTemp", 0);
        }
        #endregion
    }
}