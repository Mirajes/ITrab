namespace AtomSingleton
{
    public class Initializer
    {
        private InputSystem_Actions _inputMap;

        public void InitInputs(Player player)
        {
            _inputMap = new();

            _inputMap.Player_Atom.Move.started += player.OnMoveInput;
            _inputMap.Player_Atom.Rotate.started += player.OnRotateInput;
            _inputMap.Player_Atom.Jump.started += player.OnJumpInput;

            _inputMap.Enable();
        }

        public void DeinitInputs(Player player)
        {
            _inputMap.Player_Atom.Move.started -= player.OnMoveInput;
            _inputMap.Player_Atom.Rotate.started -= player.OnRotateInput;
            _inputMap.Player_Atom.Jump.started -= player.OnJumpInput;
        }
    }
}