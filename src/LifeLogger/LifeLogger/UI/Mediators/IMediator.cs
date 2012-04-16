namespace LifeLogger.UI.Mediators
{
    using System.Windows.Forms;

    public interface IMediator
    {
        bool IsMainForm { get; }
        Form MediatingForm { get; }
    }
}
