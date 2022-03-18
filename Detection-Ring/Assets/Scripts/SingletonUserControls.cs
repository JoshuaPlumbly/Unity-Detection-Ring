namespace Plumbly
{
    public static class SingletonUserControls
    {
        private static UserInputAction _userInput;

        public static UserInputAction Get()
        {
            if (_userInput != null)
                return _userInput;

            _userInput = new UserInputAction();
            _userInput.PlayerMovement.Enable();
            _userInput.PlayerActions.Enable();
            return _userInput;
        }
    }
}