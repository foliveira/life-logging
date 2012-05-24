namespace LifeLogger.UI.Mediators
{
    using System.Windows.Forms;

    public interface IMediator<out T> 
        where T : Form, new()
    {
        bool IsMainForm { get; }
        T MediatingForm { get; }

        void RegisterEvents();
    }
}
