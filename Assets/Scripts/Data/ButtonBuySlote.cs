using System;

namespace Scripts.Data
{
    [Serializable]
    public class ButtonBuySlote
    {
        public int _multiplicationFactor;

        public void SetMultiplicationFactor(int multiplicationFactor)
        {
            _multiplicationFactor = multiplicationFactor;
        }
    }
}