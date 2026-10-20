namespace AtomSingleton
{
    public class Initializer
    {
        private InputSystem_Actions _inputMap;
        public InputSystem_Actions InputMap => _inputMap;

        public void Init()
        {
            _inputMap = new();
        }

        public void DeInit()
        {
            if (_inputMap == null) return;
            
            _inputMap.Dispose();
            _inputMap = null;
        }

        public void InitInputs(Player player)
        {
            _inputMap.AtomPlayer.Move.started += player.OnMoveInput;
            _inputMap.AtomPlayer.Rotate.started += player.OnRotateInput;
            _inputMap.AtomPlayer.Jump.started += player.OnJumpInput;
        }

        public void DeInitInputs(Player player)
        {
            _inputMap.AtomPlayer.Move.started -= player.OnMoveInput;
            _inputMap.AtomPlayer.Rotate.started -= player.OnRotateInput;
            _inputMap.AtomPlayer.Jump.started -= player.OnJumpInput;
        }

        public void InitInputs(GameManager gameManager)
        {
            _inputMap.AtomGame.Pause.started += gameManager.OnPauseInput;
        }

        public void DeInitInputs(GameManager gameManager)
        {
            _inputMap.AtomGame.Pause.started -= gameManager.OnPauseInput;
        }
    }
}