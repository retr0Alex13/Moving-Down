using UnityEngine;

namespace UI
{
    public class MenuPresenter : MonoBehaviour
    {
        // Visualize restart delay
        [SerializeField] private MenuView menuView;
        [SerializeField] private PlayerLanding player;

        private PlayerModel playerModel;
        private const float initialPositionTolerance = 0.1f;
        private bool shouldCheckMenuAndHudVisibility = true;

        private void Awake()
        {
            playerModel = new PlayerModel();

            player.OnPlayerLanded += HandlePlayerLanded;
            //PlayerHealth.OnPlayerDie += playerModel.SetPlayerDead;
        }

        private void OnDestroy()
        {
            player.OnPlayerLanded -= HandlePlayerLanded;
            //PlayerHealth.OnPlayerDie -= playerModel.SetPlayerDead;
        }

        private void Update()
        {
            //if (playerModel.IsDead)
            //{
            //    menuView.SetContinueButtonVisibility(false);
            //    menuView.SetMenuVisibility(false);
            //    menuView.SetHudVisibility(false);
            //}
            if (shouldCheckMenuAndHudVisibility && playerModel.IsFirstLanded)
                CheckPlayerPosition();
        }

        private void CheckPlayerPosition()
        {
            float positionDifference = playerModel.GetPositionDifference(player.transform.position);
            if (positionDifference > initialPositionTolerance)
            {
                menuView.SetMenuVisibility(true);
                menuView.SetHudVisibility(true);
                shouldCheckMenuAndHudVisibility = false;
            }
        }

        public void ToggleMenuAndHud()
        {
            shouldCheckMenuAndHudVisibility = false;

            bool menuVisibility = !menuView.GetMenuVisibility();
            bool hudVisibility = !menuView.GetHudVisibility();

            menuView.SetMenuVisibility(menuVisibility);
            menuView.SetHudVisibility(hudVisibility);
        }

        private void HandlePlayerLanded()
        {
            playerModel.SetInitialPosition(player.GetInitialPlayerPosition());
            shouldCheckMenuAndHudVisibility = true;
        }
    }
}