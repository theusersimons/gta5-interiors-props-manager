using GTA;
using GTA.Math;
using GTA.Native;
using System.Windows.Forms;

namespace gta5_interiors_props_manager
{
    public class Main : Script
    {
        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.H)
            {
                Vector3 characterPosition = Game.Player.Character.Position;

                int interiorId = Function.Call<int>(
                    Hash.GET_INTERIOR_AT_COORDS,
                    characterPosition.X,
                    characterPosition.Y,
                    characterPosition.Z
                );

                bool isValidInterior = Function.Call<bool>(Hash.IS_VALID_INTERIOR, interiorId);

                bool isInteriorReady = Function.Call<bool>(Hash.IS_INTERIOR_READY, interiorId);

                // Если интерьер не валидный или не готов к использованию прерываем выполнение.
                if (!isValidInterior || !isInteriorReady) return;

                string inputMethod = Game.GetUserInput();

                // В зависимости от введенного метода выполняем логику.
                switch (inputMethod)
                {
                    case "enable":
                    case "disable":
                    {
                        string inputEntitySetName = Game.GetUserInput();

                        Function.Call(
                            inputMethod == "enable"
                                ? Hash.ACTIVATE_INTERIOR_ENTITY_SET
                                : Hash.DEACTIVATE_INTERIOR_ENTITY_SET,
                            interiorId,
                            inputEntitySetName
                        );

                        break;
                    }

                    case "refresh":
                    {
                        Function.Call(Hash.REFRESH_INTERIOR, interiorId);

                        break;
                    }
                }
            }
        }

        public Main()
        {
            KeyDown += onKeyDown;
        }
    }
}
