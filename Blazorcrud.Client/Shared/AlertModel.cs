namespace Blazorcrud.Client.Shared
{
    public class AlertModel
    {
        public string Id { get; set; }
        public AlertType Type { get; set; }
        public string Message { get; set; }
        public bool AutoClose { get; set; }
        public bool KeepAfterRouteChange { get; set; }
        public bool Fade { get; set; }
    }

    public enum AlertType
    {
        Success,
        Error,
        Info,
        Warning
    }
}