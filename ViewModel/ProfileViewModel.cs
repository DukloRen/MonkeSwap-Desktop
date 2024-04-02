using System;

namespace MonkeSwap_Desktop.ViewModel
{
    internal class ProfileViewModel : ViewModelBase
    {
        public event EventHandler ChildEventRaised;

        // Method to raise the event
        public void RaiseChildEvent()
        {
            ChildEventRaised?.Invoke(this, EventArgs.Empty);
        }
    }
}
